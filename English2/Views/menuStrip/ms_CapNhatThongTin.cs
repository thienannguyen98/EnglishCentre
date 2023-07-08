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
namespace English2.Views.menuStrip
{
    public partial class ms_CapNhatThongTin : Form
    {
        public ms_CapNhatThongTin()
        {
            InitializeComponent();
            loadData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        void loadData()
        {
            tt_taiKhoan tk = db.tt_taiKhoan.Single(t => t.tkThanhVien.Equals(fMain.username));

            tt_thanhVien tv = db.tt_thanhVien.Single(t => t.maTV.Equals(tk.maTV));

            txtHo.Text = tv.hoTV;
            txtTen.Text = tv.tenTV;
            dateNgaySinh.Text = tv.ngaySinh.ToString();
            txtNoiSinh.Text = tv.noiSinh;

            //Group gender nên chỉ cần check case rB Nữ có checked k -> false => rbNam checked.
            rBNu.Checked = tv.gioiTinh == 0;

            txtQuocTich.Text = tv.quocTich;
            txtSDT.Text = tv.soDT;
            txtMail.Text = tv.eMail;
            txtDiaChiLL.Text = tv.dcLienLac;
            txtDiaChiTT.Text = tv.dcThuongTru;

            rBDocThan.Checked = tv.honNhan == 0;
            rBCoGiaDinh.Checked = tv.honNhan == 1;
            rBDaLyHon.Checked = tv.honNhan == 2;

            txtCMND.Text = tv.soCMND;
            txtNoiCapCMND.Text = tv.noiCapCMND;
            dateNgayCapCMND.Text = tv.ngayCapCMND.ToString();
            if (tv.hinhCN.Length > 0)
                pBHinhNV.Image = new Bitmap(tv.hinhCN);
        }
        void checkToSave()
        {

            if (txtHo.Text.Trim().Length == 0)
                toolTipErr.Show("Bạn chưa nhập họ", txtHo, 30, -25, 3000);
            else if (txtTen.Text.Trim().Length == 0)
                toolTipErr.Show("Bạn chưa nhập tên", txtTen, 30, -25, 3000);
            else if (txtMail.Text.Trim().Length == 0)
                toolTipErr.Show("Bạn chưa nhập email", txtMail, 30, -25, 3000);
            else if (txtSDT.Text.Trim().Length == 0)
                toolTipErr.Show("Bạn chưa nhập số điện thoại", txtSDT, 30, -25, 3000);
            else if (txtCMND.Text.Trim().Length == 0)
                toolTipErr.Show("Bạn chưa nhập số CMND", txtCMND, 30, -25, 3000);
            else
            {
                ValidationRegex.checkPhone(txtSDT, toolTipErr);
                ValidationRegex.checkMail(txtMail, toolTipErr);
                ValidationRegex.checkCMND(txtCMND, toolTipErr);
            }

        }
        void capNhatTT()
        {
            int gender = rBNam.Checked ? 1 : 0;
            int marriage = rBDocThan.Checked ? 0 : (rBCoGiaDinh.Checked ? 1 : 2);
            try
            {
                if(!ValidationRegex.PhoneRegex(txtSDT.Text.Trim()) || !ValidationRegex.MailRegex(txtMail.Text.Trim()) || !ValidationRegex.CMNDRegex(txtCMND.Text.Trim()) || txtHo.Text.Trim().Length == 0 || txtTen.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Cập nhật thất bại!");
                }
                tt_taiKhoan tk = db.tt_taiKhoan.Single(t => t.tkThanhVien.Equals(fMain.username));
                tt_thanhVien tv = db.tt_thanhVien.Single(t => t.maTV.Equals(tk.maTV));
                tv.hoTV = txtHo.Text.Trim();
                tv.tenTV = txtTen.Text.Trim();
                tv.htVietTat = "";//Để lại
                tv.gioiTinh = gender;
                tv.ngaySinh = DateTime.Parse(dateNgaySinh.Text);
                tv.noiSinh = txtNoiSinh.Text.Trim();
                tv.quocTich = txtQuocTich.Text.Trim();
                tv.soDT = txtSDT.Text.Trim();
                tv.eMail = txtMail.Text.Trim();
                tv.honNhan = marriage;
                tv.dcLienLac = txtDiaChiLL.Text.Trim();
                tv.dcThuongTru = txtDiaChiTT.Text.Trim();
                tv.soCMND = txtCMND.Text.Trim();
                tv.noiCapCMND = txtNoiCapCMND.Text.Trim();
                tv.ngayCapCMND = DateTime.Parse(dateNgayCapCMND.Text);
                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công!");
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật thông tin thất bại!");
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            capNhatTT();
        }
        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "All Images Type |*.png;*.jpg;*.bmp";
            of.InitialDirectory = "D:\\ImageEmployee";
            string location = "";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pBHinhNV.Image = Image.FromFile(of.FileName);
                location = of.FileName;
                pBHinhNV.ImageLocation = location;

                tt_taiKhoan tk = db.tt_taiKhoan.Single(t => t.tkThanhVien.Equals(fMain.username));
                tt_thanhVien tv = db.tt_thanhVien.Single(t => t.maTV.Equals(tk.maTV));
                tv.hinhCN = pBHinhNV.ImageLocation.ToString();
            }

        }
    }
}
