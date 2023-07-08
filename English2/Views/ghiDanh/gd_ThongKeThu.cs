using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English2.Models;
namespace English2.Views.ghiDanh
{
    public partial class gd_ThongKeThu : Form
    {
        public gd_ThongKeThu()
        {
            InitializeComponent();
            start();
            loadData();
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();

        void start()
        {
            this.cbChiNhanh.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
            this.dgDSBienLai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDSBienLai.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDSBienLai.MultiSelect = false;
            this.dgDSBienLai.RowTemplate.Height = 70;
            this.dgDSBienLai.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        void loadData()
        {
            this.dgDSBienLai.DataSource = db.tt_thuTien.Where(c => c.tt_chiNhanh.tenCN.Equals(cbChiNhanh.Text) && DbFunctions.TruncateTime(c.ngayThu) >= dateTu.Value.Date && DbFunctions.TruncateTime(c.ngayThu) <= dateDen.Value.Date).Select(c => new
            {
                id = c.idThuTien,
                soBLai = c.soBienLai,
                soTien = c.soTien,
                nguoiNop = c.nguoiNop,
                dChiNgNop = c.dcNguoiNop,
                dateThu = c.ngayThu,
                tenTV = c.tt_thanhVien.hoTV + " " + c.tt_thanhVien.tenTV,

            }).OrderByDescending(x => DbFunctions.TruncateTime(x.dateThu)).ToList();
            var tongThu = "";
            try
            {
                tongThu = db.tt_thuTien.Where(c => c.tt_chiNhanh.tenCN.Equals(cbChiNhanh.Text) && DbFunctions.TruncateTime(c.ngayThu) >= dateTu.Value.Date && DbFunctions.TruncateTime(c.ngayThu) <= dateDen.Value.Date).Sum(c => c.soTien).ToString();
            }
            catch (Exception)
            {
                tongThu = "0";
            }
            lblTongThu.Text = tongThu;

        }
        void styleData()
        {
            
            this.dgDSBienLai.Columns[0].Visible = false;
            this.dgDSBienLai.Columns[1].HeaderText = "Số biên lai";
            this.dgDSBienLai.Columns[1].Width = 95;
            this.dgDSBienLai.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSBienLai.Columns[2].HeaderText = "Số tiền";
            this.dgDSBienLai.Columns[2].Width = 110;
            this.dgDSBienLai.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgDSBienLai.Columns[3].HeaderText = "Người nộp tiền";
            this.dgDSBienLai.Columns[3].Width = 220;
           
            this.dgDSBienLai.Columns[4].HeaderText = "Địa chỉ người nộp";
            this.dgDSBienLai.Columns[4].Width = 190;
            this.dgDSBienLai.Columns[5].HeaderText = "Thời gian";
            this.dgDSBienLai.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss";
            this.dgDSBienLai.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSBienLai.Columns[5].Width = 150;

            this.dgDSBienLai.Columns[6].HeaderText = "Người thu";
            this.dgDSBienLai.Columns[6].Width = 170;
        }

        private void gd_ThongKeThu_Load(object sender, EventArgs e)
        {
            
        }

        private void dateDen_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dateTu_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbChiNhanh_SelectedValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // add excel thành 1 văn bản
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "CustomerDetail";
            for (int i = 1; i < dgDSBienLai.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgDSBienLai.Columns[i - 1].HeaderText;
            }// số hàng
            for (int i = 0; i < dgDSBienLai.Rows.Count; i++)// count thêm vào chỉ 1
            {// số cột
                for (int j = 0; j < dgDSBienLai.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgDSBienLai.Rows[i].Cells[j].Value.ToString();//
                    //app.Cells[i + 2, j + 1] = dgvthongke.Rows[i].Cells[j].Value.ToString();
                }
            }
            var saveFileDialoge = new SaveFileDialog();
            saveFileDialoge.FileName = "Thống_kê";// lưu với tệ file name
            saveFileDialoge.DefaultExt = ".xlsx";// lưu với đuôi xlsx
            if (saveFileDialoge.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            app.Quit();
        }
    }
}
