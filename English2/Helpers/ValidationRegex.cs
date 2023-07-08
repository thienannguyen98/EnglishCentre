using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace English2.Helpers
{
    class ValidationRegex
    {
        #region Regex
        /// <summary>
        /// Method to Regular Expression
        /// </summary>
        /// <param name="str"></param>
        /// <param name="reg"></param>
        /// <returns></returns>
        public static bool checkRegex(string str, string reg)
        {
            Regex rg = new Regex(reg);  
            return rg.IsMatch(str);
        }
        /// <summary>
        /// Check Passwork
        /// Yêu cầu ít nhất 1 chữ in hoa, 1 chữ thường, 1 số, 1 kí tự đặc biệt (length >8)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool PasswordRegex(string password, out string notify)
        {
            notify = string.Empty;
            var hasNumber = new Regex(@"[0-9]+"); //Có ít nhất 1 số
            var hasLowerChar = new Regex(@"[a-z]+");//Có ít nhất 1 chữ thường
            var hasUpperChar = new Regex(@"[A-Z]+");//Có ít nhất 1 chữ hoa
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"); //Có kí tự đặc biệt
            var hasUnicode = new Regex(@"[^\u0000-\u007F]+"); //Unicode
            bool flag = false;
            //Kiểm tra độ dài
            if (password.Length < 8 || password.Length > 16)
            {
                notify = "Mật khẩu phải có độ dài từ 8 đến 16";
                flag = false;
            }
            else
            {
                //Kiểm tra có UNICODE không
                if (hasUnicode.IsMatch(password))
                {
                    notify = "Không cho phép chữ có dấu";
                    flag = false;
                }
                //Bậc 3: Very good (Có chữ hoa, chữ thường, số và kí tự)
                else if (hasUpperChar.IsMatch(password) && hasNumber.IsMatch(password) && hasLowerChar.IsMatch(password) && hasSymbols.IsMatch(password))
                {
                    notify = "Rất tốt";
                    flag = true;
                }
                //Bậc 2: Good (Có chữ hoa, chữ thường và số)
                else if (hasUpperChar.IsMatch(password) && hasNumber.IsMatch(password) && hasLowerChar.IsMatch(password))
                {
                    notify = "Tốt";
                    flag = true;
                }
                //Bậc 1: Weak (Có chữ thường và số)
                else if (hasNumber.IsMatch(password) && hasLowerChar.IsMatch(password))
                {
                    notify = "Yếu";
                    flag = true;
                }
                else
                {
                    notify = "Mật khẩu không hợp lệ!";
                    flag = false;
                }
            }
            return flag;
        }
        /// <summary>
        /// Check Username
        /// Yêu cầu có ít nhất 1 chữ, 1 số (8 < length < 20)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool UsernameRegex(string str)
        {
            return checkRegex(str, @"^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$");
        }
        /// <summary>
        /// Check Phone Number
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool PhoneRegex(String str)
        {
            return checkRegex(str, @"^((090)|(091)|(070)|(079)|(077)|(076)|(078)|(089)|(090)|(093)|(083)|(084)|(085)|(081)|(082)|(088)|(094)|(032)|(033)|(034)|(035)|(036)|(037)|(038)|(039)|(086)|(096)|(097)|(098)|(056)|(058)|(092)|(059)|(099))"
                                  + @"([0-9]{7})$");

        }
        /// <summary>
        /// Check Mail Address
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool MailRegex(string str)
        {
            return checkRegex(str, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                + "@"
                                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        }
        /// <summary>
        /// Check số CMND
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CMNDRegex(string str)
        {
            return checkRegex(str, @"^(([0-9]{9})|([0-9]{12}))$");
        }
        public static void checkPass(TextBox txtPass, ToolTip err)
        {
            string tam;
            if (txtPass.Text.Trim().Length > 0)
            {

                PasswordRegex(txtPass.Text, out tam);
                err.Show(tam, txtPass, 30, -25, 3000);
            }
        }
        public static void checkMail(TextBox txtMail, ToolTip err)
        {
            if (txtMail.Text.Trim().Length > 0 && !MailRegex(txtMail.Text))
            {
                err.Show("Email không hợp lệ", txtMail, 30, -25, 3000);
            }
        }
        public static void checkPhone(TextBox txtPhone, ToolTip err)
        {
            if (txtPhone.Text.Trim().Length > 0 && !PhoneRegex(txtPhone.Text))
            {
                err.Show("Số điện thoại không đúng", txtPhone, 30, -25, 3000);
            }
        }
        public static void checkCMND(TextBox txtCMND, ToolTip err)
        {
            if (txtCMND.Text.Trim().Length > 0 && !CMNDRegex(txtCMND.Text))
            {
                err.Show("Số chứng minh nhân dân không đúng", txtCMND, 30, -25, 3000);
            }
        }
        public static void checkUsername(TextBox txtUsername, ToolTip err)
        {
            if (txtUsername.Text.Trim().Length > 0 && !UsernameRegex(txtUsername.Text.Trim()))
            {
                err.Show("Độ dài phải từ 8 đến 20, phải có ít nhất 1 số và 1 chữ", txtUsername, 30, -25, 3000);
            }
        }
        
        #endregion
    }
}
