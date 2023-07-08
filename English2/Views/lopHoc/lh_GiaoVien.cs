using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English2.Models;
namespace English2.Views.lopHoc
{
    public partial class lh_GiaoVien : Form
    {
        public lh_GiaoVien()
        {
            InitializeComponent();
            
        }
        public int maLopGV;
        ttAnhNguEntities db = new ttAnhNguEntities();
        void loadDataGV()
        {
            this.dgGV.DataSource = db.tt_dsGiaoVienLop.Where(c => c.maLop.Equals(maLopGV)).Select(c => new
            {
                hoTen = c.tt_thanhVien.hoTV + " " + c.tt_thanhVien.tenTV,
                SDT = c.tt_thanhVien.soDT,
                Mail = c.tt_thanhVien.eMail,
                bd = c.ngayNhanLop,
                kt = c.ngayThoiDay,
                trangThai = c.trangThai == false ? "Đang lên kế hoạch" : "Đang dạy",
                maGV = c.maTV,
            }).OrderBy(x=> x.hoTen).ToList();
        }


        void dataStyleGV()
        {
            this.dgGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgGV.MultiSelect = false;
            this.dgGV.RowTemplate.Height = 70;

            this.dgGV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgGV.Columns[0].HeaderText = "Họ tên";
            this.dgGV.Columns[0].Width = 220;
            //sdt
            this.dgGV.Columns[1].HeaderText = "Số điện thoại";
            this.dgGV.Columns[1].Width = 120;
            //email
            this.dgGV.Columns[2].HeaderText = "Email";
            this.dgGV.Columns[2].Width = 220;
            //ho
            this.dgGV.Columns[3].HeaderText = "Ngày nhận lớp";
            this.dgGV.Columns[3].Width = 120;
            this.dgGV.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgGV.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ten
            this.dgGV.Columns[4].HeaderText = "Ngày kết thúc";
            this.dgGV.Columns[4].Width = 120;
            this.dgGV.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgGV.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
         
            //ngaysinh
            this.dgGV.Columns[5].HeaderText = "Trạng thái";
            this.dgGV.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgGV.Columns[5].Width = 150;
            this.dgGV.Columns[6].Visible = false;
      
        }
        void xoaGV()
        {
            using (ttAnhNguEntities dbbb = new ttAnhNguEntities())
            {
                string id = dgGV.SelectedCells[7].OwningRow.Cells["maGV"].Value.ToString();
                DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        tt_dsGiaoVienLop del = dbbb.tt_dsGiaoVienLop.FirstOrDefault(p => p.maTV.ToString() == id);
                        dbbb.tt_dsGiaoVienLop.Remove(del);
                        dbbb.SaveChanges();
                        MessageBox.Show("Xóa thành công");
                        loadDataGV();
                        dataStyleGV();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không xóa được: " + ex.Message);

                    }
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            this.dgGV.DataSource = db.tt_dsGiaoVienLop.Where(c => c.maLop.Equals(maLopGV) && (c.tt_thanhVien.hoTV.Contains(txtTimKiem.Text) || c.tt_thanhVien.soDT.Contains(txtTimKiem.Text))).Select(c => new
            {
                hoTen = c.tt_thanhVien.hoTV + " " + c.tt_thanhVien.tenTV,
                SDT = c.tt_thanhVien.soDT,
                Mail = c.tt_thanhVien.eMail,
                bd = c.ngayNhanLop,
                kt = c.ngayThoiDay,
                trangThai = c.trangThai == false ? "Đang lên kế hoạch" : "Đang dạy",
                maGV = c.maTV,
            }).ToList();
            dataStyleGV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            xoaGV();

        }

        private void lh_DSGV_Load(object sender, EventArgs e)
        {
            loadDataGV();
            dataStyleGV();
            tt_lopHoc lh = db.tt_lopHoc.Single(t => t.maLop == (maLopGV));
            
        }
    }
}
