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
using English2.Helpers;
using English2.Views.thanhVien;

namespace English2.Views.lopHoc
{
    public partial class lh_CapNhatLop : Form
    {
        public lh_CapNhatLop()
        {
            InitializeComponent();
            cbChiNhanh.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
            cbKhoaHoc.DataSource = db.tt_khoaHoc.Select(c => c.tenHK).ToList();
            cbChuongTrinh.DataSource = db.tt_chuongTrinh.Select(c => c.tenCT).ToList();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public int maLop;
        void capNhatTT()
        {
            if (txtTenLop.Text.Trim().Length == 0)
            {
                throw new ArithmeticException("Cập nhật thất bại!");
            }
            try
            {
                tt_lopHoc lh = db.tt_lopHoc.Single(t => t.maLop.Equals(maLop));
                lh.tenLop = txtTenLop.Text;
                lh.hocPhi = Int32.Parse(txtHocPhi.Text);
                lh.ngayKG = DateTime.Parse(dateKG.Text);
                lh.ngayKT = DateTime.Parse(dateKT.Text);
                lh.thoiLuong = Int32.Parse(txtThoiLuong.Text);
                lh.ghiChu = txtGhiChu.Text;
                //Chương trình
                tt_chuongTrinh ct = db.tt_chuongTrinh.Single(t => t.tenCT == (cbChuongTrinh.SelectedValue.ToString()));
                lh.maCT = ct.maCT;
                //Khóa học
                tt_khoaHoc kh = db.tt_khoaHoc.Single(t => t.tenHK == (cbKhoaHoc.SelectedValue.ToString()));
                lh.maHK = kh.maKH;
                //Chi nhánh
                tt_chiNhanh cn = db.tt_chiNhanh.Single(t => t.tenCN == (cbChiNhanh.SelectedValue.ToString()));
                lh.maCN = cn.maCN;
                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công!");
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật thông tin thất bại!");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            capNhatTT();
        }
        private void Addform(Form f)
        {
            fMain.pnMain.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            fMain.pnMain.Controls.Add(f);
            f.Show();
        }
        private void picBack_Click(object sender, EventArgs e)
        {
            lh_DanhSachLopHoc f = new lh_DanhSachLopHoc();
            Addform(f);
        }
    }
}
