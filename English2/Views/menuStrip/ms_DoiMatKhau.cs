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
namespace English2.Views.menuStrip
{
    public partial class ms_DoiMatKhau : Form
    {
        public ms_DoiMatKhau()
        {
            InitializeComponent();
            txtUsername.Text = fMain.username;
            txtUsername.ReadOnly = true;
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        
        public void doiMK()
        {
            try
            {
                string matKhau = ComputeHash(txtPassword.Text, new SHA256CryptoServiceProvider());
                tt_taiKhoan tk = db.tt_taiKhoan.Single(t => t.tkThanhVien.Equals(txtUsername.Text));
                if (tk.matKhau.Equals(matKhau) && txtReMKMoi.Equals(matKhau))
                {
                    tk.matKhau = ComputeHash(txtMatKhauMoi.Text, new SHA256CryptoServiceProvider());
                    db.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công!");
            }
                else
                    MessageBox.Show("Mật khẩu hiện tại không đúng!");
        }
            catch (Exception)
            {
                MessageBox.Show("Đổi mật khẩu thất bại!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                doiMK();
        }
    }
}
