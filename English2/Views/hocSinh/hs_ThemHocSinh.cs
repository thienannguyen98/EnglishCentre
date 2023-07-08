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
using English2.Views.ghiDanh;
using English2.Views.thanhVien;

namespace English2.Views.hocSinh
{
    public partial class hs_ThemHocSinh : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public hs_ThemHocSinh()
        {
            InitializeComponent();
        }

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
            else
            {
                ValidationRegex.checkPhone(txtSDT, toolTipErr);
                ValidationRegex.checkMail(txtMail, toolTipErr);
            }

        }
        public int tempCN;
        /// <summary>
        /// Method dùng để thêm thành viên lấy dữ liệu từ các Textbox
        /// Dùng trim() để bỏ đi các 'space' thừa
        /// </summary>
        void addHS()
        {
            checkToSave();
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                //--Lưu dữ liệu
                int gender = rBNam.Checked ? 1 : 0;
                try
                {
                    if (!ValidationRegex.PhoneRegex(txtSDT.Text.Trim()) || !ValidationRegex.MailRegex(txtMail.Text.Trim()) || txtHo.Text.Trim().Length == 0 || txtTen.Text.Trim().Length == 0 || txtSDT.Text.Trim().Length == 0)
                    {
                        throw new ArithmeticException("Thêm thất bại!");
                    }
                    int max = dbb.tt_hocSinh.ToList().Max(p => Convert.ToInt32(p.maHS)) + 1;
                    tt_hocSinh hs = new tt_hocSinh()
                    {
                        maHS = max.ToString(),
                        hoHS = txtHo.Text.Trim(),
                        tenHS = txtTen.Text.Trim(),
                        gioiTinh = gender,
                        ngaySinh = DateTime.Parse(dateNgaySinh.Text),
                        noiSinh = txtNoiSinh.Text.Trim(),
                        soDT = txtSDT.Text.Trim(),
                        eMail = txtMail.Text.Trim(),
                        hinhHS = (pBHinhNV.Image != null) ? (pBHinhNV.ImageLocation.ToString()) : "",
                    };
                    dbb.tt_hocSinh.Add(hs);
                    dbb.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                }
                catch (Exception)
                {
                }
            }
        }
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            addHS();
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
        public int flagHS = 1;
        private void picBack_Click(object sender, EventArgs e)
        {
                hs_DanhSachHocSinh f = new hs_DanhSachHocSinh();
                Addform(f);
        }
    }
}
