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
using System.Security.Cryptography;
using MaterialSkin;
namespace English2.Views
{
    public partial class DangNhap : MaterialSkin.Controls.MaterialForm
    {
        public DangNhap()
        {
            InitializeComponent();
            lbName.Parent = panel3;
            lbName.Location = new Point(115, 19);
            lbName.BackColor = Color.Transparent;
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            
            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Orange700, Primary.Orange700,
                Primary.Orange700, Accent.Orange700,
                TextShade.BLACK
            );

        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public bool isValid(string username, string password)
        {
           
                bool flag = false;
                
                try
                {
                    flag = (db.tt_taiKhoan.Single(t => t.tkThanhVien.Equals(username) && t.matKhau.Equals(password) ) != null);
                }
                catch (Exception)
                {

                }
                return flag;
            
        }
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tk = this.account.Text.Trim();
            string mk = this.password.Text.Trim();
            mk = ComputeHash(mk, new SHA256CryptoServiceProvider());
            if (isValid(tk, mk))
            {
                fMain.username = tk;
                this.Dispose();
            }
            else
            {
                this.toolError.Show("Tài khoản hoặc mật khẩu không chính xác!", this.account, 0, -65, 5000);
                this.account.Clear();
                this.password.Clear();
                this.account.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
