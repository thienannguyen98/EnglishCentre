using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English2.Models;
using English2.Helpers;
using English2.Views.thanhVien;

namespace English2.Views.taiKhoan
{
    public partial class tk_CapNhatTaiKhoan : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public tk_CapNhatTaiKhoan()
        {
            InitializeComponent();
        }
        #region methods
        public string tempUsername;
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
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        /// <summary>
        /// Phương thức dùng để cập nhật thông tin TÀI KHOẢN
        /// </summary>
        void capNhatTT()
        {

            try
            {
                tt_taiKhoan tk = db.tt_taiKhoan.Single(t => t.tkThanhVien.Equals(txtUsername.Text));
                string tb;
                ValidationRegex.checkPass(txtMKMoi, toolTipErr);
                if (ValidationRegex.PasswordRegex(txtMKMoi.Text, out tb))
                {
                    tk.matKhau = ComputeHash(txtMKMoi.Text, new SHA256CryptoServiceProvider());
                    tk.capDo = Int32.Parse(txtCapDo.Text);
                    tk.duocSD = checkBDuocSD.Checked;
                    tk.ghiChu = txtGhiChu.Text;
                    
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                else if(txtMKMoi.Text.Trim().Length==0)
                {
                    tk.capDo = Int32.Parse(txtCapDo.Text);
                    tk.duocSD = checkBDuocSD.Checked;
                    tk.ghiChu = txtGhiChu.Text;
                    
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                string permission = "";
                permission += checkTaiKhoan_1.Checked ? "1#" : "";
                permission += checkThanhVien_2.Checked ? "2#" : "";
                permission += checkPhong_3.Checked ? "3#" : "";
                permission += checkCaHoc_4.Checked ? "4#" : "";
                permission += checkKhoa_5.Checked ? "5#" : "";
                permission += checkCTHoc_6.Checked ? "6#" : "";
                permission += checkMon_7.Checked ? "7#" : "";
                permission += checkLop_8.Checked ? "8#" : "";
                permission += checkGV_9.Checked ? "9#" : "";
                permission += checkHS_10.Checked ? "10#" : "";
                permission += checkThu_11.Checked ? "11" : "";

                tk.quyenHan = permission;

                db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật thông tin thất bại!");
            }
        }
        #endregion

        #region event
        private void btnSave_Click(object sender, EventArgs e)
        {
            capNhatTT();
        }

        private void picBack_Click(object sender, EventArgs e)
        {
            tk_DanhSachTaiKhoan f = new tk_DanhSachTaiKhoan();
            Addform(f);
        }

        private void txtMKMoi_Leave(object sender, EventArgs e)
        {
            ValidationRegex.checkPass(txtMKMoi, toolTipErr);
        }
        #endregion


    }
}
