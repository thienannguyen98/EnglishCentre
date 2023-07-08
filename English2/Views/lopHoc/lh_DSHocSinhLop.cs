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
    public partial class lh_DSHocSinhLop : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        lh_CTLopHoc f = new lh_CTLopHoc();
        public int maLopDSl;
        public lh_DSHocSinhLop()
        {
            InitializeComponent();
            
        }
        void loadDataHS()
        {
            if (cbTrangThai.SelectedIndex == 0)
            {
                this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => c.maLop.Equals(maLopDSl)).Select(c => new
                {
                    maLop = c.maLop,
                    maHS = c.tt_hocSinh.maHS,
                    tenLop = c.tt_lopHoc.tenLop,
                    Ho = c.tt_hocSinh.hoHS,
                    Ten = c.tt_hocSinh.tenHS,
                    gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                    NgaySinh = c.tt_hocSinh.ngaySinh,
                    SDT = c.tt_hocSinh.soDT,
                    Mail = c.tt_hocSinh.eMail,
                    trangThai = c.trangThai == true ? "Đã đóng tiền" : "Chưa đóng tiền"
                }).ToList();
            }
            else
            {
                bool state = cbTrangThai.SelectedIndex == 2 ? false : true;
                this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => c.maLop.Equals(maLopDSl) && c.trangThai ==state ).Select(c => new
                {
                    maLop = c.maLop,
                    maHS = c.tt_hocSinh.maHS,
                    tenLop = c.tt_lopHoc.tenLop,
                    Ho = c.tt_hocSinh.hoHS,
                    Ten = c.tt_hocSinh.tenHS,
                    gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                    NgaySinh = c.tt_hocSinh.ngaySinh,
                    SDT = c.tt_hocSinh.soDT,
                    Mail = c.tt_hocSinh.eMail,
                    trangThai = c.trangThai == true ? "Đã đóng tiền" : "Chưa đóng tiền"
                }).OrderBy(x => x.Ten).ToList();
            }
        }


        void dataStyleHS()
        {
            this.dgDSHocSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDSHocSinh.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDSHocSinh.MultiSelect = false;
            this.dgDSHocSinh.RowTemplate.Height = 70;

            this.dgDSHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          
            this.dgDSHocSinh.Columns[0].Visible = false;
            this.dgDSHocSinh.Columns[1].Visible = false;
            //tenLop
            this.dgDSHocSinh.Columns[2].Visible = false;


            //ho
            this.dgDSHocSinh.Columns[3].HeaderText = "Họ";
            this.dgDSHocSinh.Columns[3].Width = 190;
            //ten
            this.dgDSHocSinh.Columns[4].HeaderText = "Tên";
            this.dgDSHocSinh.Columns[4].Width = 100;
            
            //Giới tính
            this.dgDSHocSinh.Columns[5].HeaderText = "Giới tính";
            this.dgDSHocSinh.Columns[5].Width = 100;
            this.dgDSHocSinh.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ngaysinh
            this.dgDSHocSinh.Columns[6].HeaderText = "Ngày sinh";
            this.dgDSHocSinh.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgDSHocSinh.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSHocSinh.Columns[6].Width = 125;
            //sdt
            this.dgDSHocSinh.Columns[7].HeaderText = "Số điện thoại";
            this.dgDSHocSinh.Columns[7].Width = 125;
            //email
            this.dgDSHocSinh.Columns[8].HeaderText = "Email";
            this.dgDSHocSinh.Columns[8].Width = 210;
            //Trangthai
            this.dgDSHocSinh.Columns[9].HeaderText = "Trạng thái";
            this.dgDSHocSinh.Columns[9].Width = 135;
            this.dgDSHocSinh.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          
        }
        void xoaHocSinh()
        {
            using (ttAnhNguEntities dbbb = new ttAnhNguEntities())
            {
                string id = dgDSHocSinh.SelectedCells[0].OwningRow.Cells["maHS"].Value.ToString();
                DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        tt_hocSinh del = dbbb.tt_hocSinh.FirstOrDefault(p => p.maHS == id);
                        dbbb.tt_hocSinh.Remove(del);
                        dbbb.SaveChanges();
                        MessageBox.Show("Xóa thành công");
                        loadDataHS();
                        dataStyleHS();
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
            if (cbTrangThai.SelectedIndex == 0)
            {
                this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => c.maLop.Equals(maLopDSl) && ((c.tt_hocSinh.hoHS + " " + c.tt_hocSinh.tenHS).Contains(txtTimHS.Text.Trim()) || c.tt_hocSinh.soDT.Contains(txtTimHS.Text.Trim()))).Select(c => new
                {
                    maLop = c.maLop,
                    maHS = c.tt_hocSinh.maHS,
                    tenLop = c.tt_lopHoc.tenLop,
                    Ho = c.tt_hocSinh.hoHS,
                    Ten = c.tt_hocSinh.tenHS,
                    gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                    NgaySinh = c.tt_hocSinh.ngaySinh,
                    SDT = c.tt_hocSinh.soDT,
                    Mail = c.tt_hocSinh.eMail,
                    trangThai = c.trangThai == true ? "Đã đóng tiền" : "Chưa đóng tiền"
                }).ToList();
            }
            else
            {
                bool state = cbTrangThai.SelectedIndex == 2 ? false : true;
                this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => (c.maLop.Equals(maLopDSl) && ((c.tt_hocSinh.hoHS + " " + c.tt_hocSinh.tenHS).Contains(txtTimHS.Text.Trim()) || c.tt_hocSinh.soDT.Contains(txtTimHS.Text.Trim()))) && c.trangThai == state).Select(c => new
                {
                    maLop = c.maLop,
                    maHS = c.tt_hocSinh.maHS,
                    tenLop = c.tt_lopHoc.tenLop,
                    Ho = c.tt_hocSinh.hoHS,
                    Ten = c.tt_hocSinh.tenHS,
                    gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                    NgaySinh = c.tt_hocSinh.ngaySinh,
                    SDT = c.tt_hocSinh.soDT,
                    Mail = c.tt_hocSinh.eMail,
                    trangThai = c.trangThai == true ? "Đã đóng tiền" : "Chưa đóng tiền"
                }).ToList();
            }
            dataStyleHS();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            xoaHocSinh();

        }

        private void lh_DSHocSinhLop_Load(object sender, EventArgs e)
        {
            cbTrangThai.SelectedIndex = 0;
            loadDataHS();
            dataStyleHS();
        }

        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataHS();
        }

        private void dgDanhSachTK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
