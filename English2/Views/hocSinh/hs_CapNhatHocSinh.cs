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

namespace English2.Views.hocSinh
{
    public partial class hs_CapNhatHocSinh : Form
    {
        public hs_CapNhatHocSinh()
        {
            InitializeComponent();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
       
        #region Methods
        public string maHSinh;
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
            else
            {
                ValidationRegex.checkPhone(txtSDT, toolTipErr);
                ValidationRegex.checkMail(txtMail, toolTipErr);
            }

        }
        /// <summary>
        /// Method dùng để cập nhật TT Thành viên
        /// Lấy dữ liệu từ các textbox
        /// </summary>
        void capNhatTT()
        {
            int gender = rBNam.Checked ? 1 : 0;
            checkToSave();
            try
            {
                if (!ValidationRegex.PhoneRegex(txtSDT.Text.Trim()) || !ValidationRegex.MailRegex(txtMail.Text.Trim()) || txtHo.Text.Trim().Length == 0 || txtTen.Text.Trim().Length == 0 || txtSDT.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Cập nhật thất bại!");
                }
                tt_hocSinh hs = db.tt_hocSinh.Single(t => t.maHS.Equals(maHSinh));
                hs.maHS = maHSinh;
                hs.hoHS = txtHo.Text.Trim();
                hs.tenHS = txtTen.Text.Trim();
                hs.gioiTinh = gender;
                hs.ngaySinh = DateTime.Parse(dateNgaySinh.Text);
                hs.noiSinh = txtNoiSinh.Text.Trim();
                hs.soDT = txtSDT.Text.Trim();
                hs.eMail = txtMail.Text.Trim();
                //hs.hinhHS = (pBHinhNV.Image != null) ? (pBHinhNV.ImageLocation.ToString()) : "";
                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công!");
            }
            catch (Exception)
            {
            }
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
                tt_hocSinh tk = db.tt_hocSinh.Single(t => t.maHS.Equals(maHSinh));
                tk.hinhHS = pBHinhNV.ImageLocation.ToString();
            }

        }

        #endregion
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
            hs_DanhSachHocSinh f = new hs_DanhSachHocSinh();
            Addform(f);
        }
    }
}
