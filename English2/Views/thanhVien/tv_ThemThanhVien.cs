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
    public partial class tv_ThemThanhVien : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public tv_ThemThanhVien()
        {
            InitializeComponent();
            this.cBPhongBan.DataSource = db.tt_phongBan.Select(c => c.tenPB).ToList();
            this.cbCN.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
        }
        
        #region Methods
        public int tempCN;
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
        /// Method dùng để thêm thành viên lấy dữ liệu từ các Textbox
        /// Dùng trim() để bỏ đi các 'space' thừa
        /// </summary>
        void addThanhVien()
        {
            int gender = rBNam.Checked ? 1 : 0;
            int marriage = rBDocThan.Checked ? 0 : (rBCoGiaDinh.Checked ? 1 : 2);
            checkToSave();
            int maTam = -1;

            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    if (!ValidationRegex.PhoneRegex(txtSDT.Text.Trim()) || !ValidationRegex.MailRegex(txtMail.Text.Trim()) || !ValidationRegex.CMNDRegex(txtCMND.Text.Trim()) || txtHo.Text.Trim().Length == 0 || txtTen.Text.Trim().Length == 0 || cbChucDanh.Text.Trim().Length == 0)
                    {
                        throw new ArithmeticException("Thêm thất bại!");
                    }

                    tt_thanhVien tv = new tt_thanhVien()
                    {
                        maTV = maTam,
                        hoTV = txtHo.Text.Trim(),
                        tenTV = txtTen.Text.Trim(),
                        htVietTat = "",
                        gioiTinh = gender,
                        ngaySinh = DateTime.Parse(dateNgaySinh.Text),
                        noiSinh = txtNoiSinh.Text.Trim(),
                        quocTich = txtQuocTich.Text.Trim(),
                        soDT = txtSDT.Text.Trim(),
                        eMail = txtMail.Text.Trim(),
                        honNhan = marriage,
                        dcLienLac = txtDiaChiLL.Text.Trim(),
                        dcThuongTru = txtDiaChiTT.Text.Trim(),
                        soCMND = txtCMND.Text.Trim(),
                        noiCapCMND = txtNoiCapCMND.Text.Trim(),
                        ngayCapCMND = DateTime.Parse(dateNgayCapCMND.Text),
                        chucDanh = cbChucDanh.Text.Trim(),
                        ngayNhanViec = DateTime.Now,
                        hocVi = txtHocVi.Text.Trim(),
                        chuyenMon = txtChuyenMon.Text.Trim(),
                        ngoaiNgu = txtNgoaiNgu.Text.Trim(),
                        tinHoc = txtTinHoc.Text.Trim(),
                        kyNangKhac = txtKiNangKhac.Text.Trim(),

                        hinhCN = (pBHinhNV.Image != null) ? (pBHinhNV.ImageLocation.ToString()) : "",

                        qhGiaDinh = txtQuanHeGD.Text,
                    };
                    //Chi nhanh 
                    tt_chiNhanh tn = dbb.tt_chiNhanh.Single(t => t.tenCN == (cbCN.SelectedValue.ToString()));
                    tv.maCN = tn.maCN;
                    //Phong ban
                    tt_phongBan pb = dbb.tt_phongBan.Single(t => t.tenPB == (cBPhongBan.SelectedValue.ToString()));
                    tv.maPB = pb.maPB;
                    dbb.tt_thanhVien.Add(tv);
                    dbb.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                }
                catch (Exception)
                {
                }
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
        public int flag = 1;
        #endregion

        #region Event
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
            }

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            addThanhVien();
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
        #endregion
    }
}
