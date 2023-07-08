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
using English2.Views.giaoVien;
namespace English2.Views.thanhVien
{
    public partial class tv_CapNhatThanhVien : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public tv_CapNhatThanhVien()
        {
            InitializeComponent();
            this.cBPhongBan.DataSource = db.tt_phongBan.Select(c => c.tenPB).ToList();
            this.cbCN.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
        }
        public int tam;
        public int tempCN;
        public int flag = 1;
        #region Methods
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
        /// <summary>
        /// Method dùng để cập nhật TT Thành viên
        /// Lấy dữ liệu từ các textbox
        /// </summary>
        void capNhatTT()
        {
            int gender = rBNam.Checked ? 1 : 0;
            int marriage = rBDocThan.Checked ? 0 : (rBCoGiaDinh.Checked ? 1 : 2);
            checkToSave();
           
                try
                {
                if (!ValidationRegex.PhoneRegex(txtSDT.Text.Trim()) || !ValidationRegex.MailRegex(txtMail.Text.Trim()) || !ValidationRegex.CMNDRegex(txtCMND.Text.Trim()) || txtHo.Text.Trim().Length == 0 || txtTen.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Cập nhật thất bại!");
                }
                tt_thanhVien tk = db.tt_thanhVien.Single(t => t.maTV.Equals(tam));
                    tk.hoTV = txtHo.Text.Trim();
                    tk.tenTV = txtTen.Text.Trim();
                    tk.htVietTat = "";//Để lại
                    tk.gioiTinh = gender;
                    tk.ngaySinh = DateTime.Parse(dateNgaySinh.Text);
                    tk.noiSinh = txtNoiSinh.Text.Trim();
                    tk.quocTich = txtQuocTich.Text.Trim();
                    tk.soDT = txtSDT.Text.Trim();
                    tk.eMail = txtMail.Text.Trim();
                    tk.honNhan = marriage;
                    tk.dcLienLac = txtDiaChiLL.Text.Trim();
                    tk.dcThuongTru = txtDiaChiTT.Text.Trim();
                    tk.soCMND = txtCMND.Text.Trim();
                    tk.noiCapCMND = txtNoiCapCMND.Text.Trim();
                    tk.ngayCapCMND = DateTime.Parse(dateNgayCapCMND.Text);
                    tk.chucDanh = cbChucDanh.Text.Trim();
                    tk.ngayNhanViec = DateTime.Now;
                    tk.hocVi = txtHocVi.Text.Trim();
                    tk.chuyenMon = txtChuyenMon.Text.Trim();
                    tk.ngoaiNgu = txtNgoaiNgu.Text.Trim();
                    tk.tinHoc = txtTinHoc.Text.Trim();
                    tk.kyNangKhac = txtKiNangKhac.Text.Trim();
                    //Chi nhanh
                    tt_chiNhanh tn = db.tt_chiNhanh.Single(t => t.tenCN == (cbCN.SelectedValue.ToString()));
                    tk.maCN = tn.maCN;
                    //Phong ban
                    tt_phongBan pb = db.tt_phongBan.Single(t => t.tenPB == (cBPhongBan.SelectedValue.ToString()));
                    tk.maPB = pb.maPB;
                    tk.qhGiaDinh = txtQuanHeGD.Text.Trim();
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                
                }
                catch (Exception)
                {
                   
                }
            

            
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
        #endregion

        #region Event

        private void btnLuu_Click_1(object sender, EventArgs e)
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
                tt_thanhVien tk = db.tt_thanhVien.Single(t => t.maTV.Equals(tam));
                tk.hinhCN = pBHinhNV.ImageLocation.ToString();
            }

        }
        private void picBack_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                tv_DanhSachThanhVien f = new tv_DanhSachThanhVien();
                Addform(f);
            }
            else
            {
                gv_DSGiaoVien f = new gv_DSGiaoVien();
                Addform(f);
            }
        }
        private void txtSDT_Leave(object sender, EventArgs e)
        {
            ValidationRegex.checkPhone(txtSDT, toolTipErr);
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {
            ValidationRegex.checkMail(txtMail, toolTipErr);
        }

        private void txtCMND_Leave(object sender, EventArgs e)
        {
            ValidationRegex.checkCMND(txtCMND, toolTipErr);
        }

        private void txtHo_Leave(object sender, EventArgs e)
        {
            if (txtHo.Text.Trim().Length == 0)
            {
                toolTipErr.Show("Bạn chưa nhập họ", txtHo, 30, -25, 3000);
            }
        }

        private void txtTen_Leave(object sender, EventArgs e)
        {
            if (txtTen.Text.Trim().Length == 0)
            {
                toolTipErr.Show("Bạn chưa nhập tên", txtTen, 30, -25, 3000);
            }
        }

        #endregion


    }
}
