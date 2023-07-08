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

namespace English2.Views.taiKhoan
{
    public partial class tk_TaoTaiKhoan : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public tk_TaoTaiKhoan()
        {
            InitializeComponent();
            this.cbMaTV.DataSource = db.tt_thanhVien.Select(c => c.hoTV + " " + c.tenTV + " - " + c.soDT + " - " + c.maTV).ToList();
            this.cbMaTV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteT();
        }
        #region Methods
        /// <summary>
        /// Hashing SHA256
        /// </summary>
        /// <param name="input"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        /// <summary>
        /// AutoComplete txtHoTen
        /// </summary>
        void AutoCompleteT()
        {
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            string[] tks = db.tt_thanhVien.Select(c => c.hoTV + " " + c.tenTV + " - " + c.maTV + " - " + c.soDT).ToArray();

            cbMaTV.AutoCompleteCustomSource.AddRange(tks);
            //OK
        }

        void checkRePassword()
        {
            if (txtRepassword.Text.Trim().Length > 0 && !txtRepassword.Text.Trim().Equals(txtPassword.Text.Trim()))
            {
                this.toolTipErr.Show("Nhập lại mật khẩu không đúng!", txtRepassword, 30, -25, 3000);
            }
        }
        void checkToAdd()
        {
            if(txtUsername.Text.Trim().Length==0)
                this.toolTipErr.Show("Bạn chưa nhập tài khoản!", txtUsername, 30, -25, 3000);
            else if (txtPassword.Text.Trim().Length == 0)
                this.toolTipErr.Show("Bạn chưa nhập mật khẩu!", txtPassword, 30, -25, 3000);
            else if (txtRepassword.Text.Trim().Length == 0)
                this.toolTipErr.Show("Bạn chưa nhập lại mật khẩu!", txtRepassword, 30, -25, 3000);
            else
            {
                ValidationRegex.checkUsername(txtUsername, toolTipErr);
                ValidationRegex.checkPass(txtPassword, toolTipErr);
                checkRePassword();
            }
            
        }
        /// <summary>
        /// Method dùng để tạo Tài khoản = cách lấy dữ liệu từ các textbox
        /// </summary>
        void addTaiKhoan()
        {
            string temp = cbMaTV.SelectedItem.ToString();
            string[] splitMa = temp.Split('-');
            string tam;
            checkToAdd();
            try
            {
                if (!ValidationRegex.UsernameRegex(txtUsername.Text.Trim()) || !ValidationRegex.PasswordRegex(txtPassword.Text.Trim(), out tam) || !txtRepassword.Text.Trim().Equals(txtPassword.Text.Trim()) || txtUsername.Text.Trim().Length == 0 || txtPassword.Text.Trim().Length == 0 || txtRepassword.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Tạo tài khoản thất bại!");
                }
                tt_taiKhoan tk = new tt_taiKhoan()
                {
                    maTV = Int32.Parse(splitMa[2].Trim()),
                    //maTV = 3,
                    tkThanhVien = txtUsername.Text,
                    matKhau = ComputeHash(txtPassword.Text, new SHA256CryptoServiceProvider()),
                    ngayCap = DateTime.Now.Date,
                    duocSD = rbDuocSD.Checked,
                    capDo = Int32.Parse(txtCapDo.Text),
                    quyenHan = "",
                    ipTruyCap = "18000",
                    tcGanNhat = DateTime.Now.Date,
                    mac = "",
                    trangMacDinh = "",
                    ghiChu = ""
                };

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


                db.tt_taiKhoan.Add(tk);
                db.SaveChanges();
                MessageBox.Show("Tạo tài khoản thành công");
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            addTaiKhoan();
        }
        private void picBack_Click(object sender, EventArgs e)
        {
            tk_DanhSachTaiKhoan f = new tk_DanhSachTaiKhoan();
            Addform(f);
        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsername_Leave_1(object sender, EventArgs e)
        {
            ValidationRegex.checkUsername(txtUsername, toolTipErr);

        }

        private void txtPassword_Leave_1(object sender, EventArgs e)
        {
            ValidationRegex.checkPass(txtPassword, toolTipErr);
        }

        private void txtRepassword_Leave_1(object sender, EventArgs e)
        {
            checkRePassword();
        }
        #endregion
    }
}
