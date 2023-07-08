using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using English2.Models;
using English2.Views.hocSinh;
namespace English2.Views.lopHoc
{
    
    public partial class lh_CTLopHoc : MaterialSkin.Controls.MaterialForm
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public string tenLop;
        public int maLop;
        public int maCN;
        public lh_CTLopHoc()
        {
            InitializeComponent();
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

        }

        private void lh_CTLopHoc_Load(object sender, EventArgs e)
        {
            lh_DSHocSinhLop f = new lh_DSHocSinhLop();
            f.maLopDSl = this.maLop;
            changeFormHS(f);

            lh_GiaoVien fGV = new lh_GiaoVien();
            fGV.maLopGV = this.maLop;

            lh_LichHoc fLich = new lh_LichHoc();
            fLich.maLopLich = this.maLop;
            fLich.tenLopLich = tenLop;

            lbLop.Text = tenLop;
        }

        #region Methods
        /// <summary>
        /// Method dùng để thêm form mới vào panelMain của formMainAdmin
        /// </summary>
        /// <param name="f"></param>
        public void Addform(Form f)
        {
            fMain.pnMain.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            fMain.pnMain.Controls.Add(f);
            f.Show();
        }
        private void changeFormLich(Form f)
        {
           this.pnMainLich.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            pnMainLich.Controls.Add(f);
            f.Show();
        }
        private void changeFormGV(Form f)
        {
            this.pnGV.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            pnGV.Controls.Add(f);
            f.Show();
        }
        private void changeFormHS(Form f)
        {
            pnMainHS.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            pnMainHS.Controls.Add(f);
            f.Show();
        }
        #endregion

        #region Event

        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //hs_DanhSachHocSinh f = new hs_DanhSachHocSinh();
            //changeForm(f);
            
        }
        
        private void tabLichHoc_Enter(object sender, EventArgs e)
        {
            lh_LichHoc f = new lh_LichHoc();
            f.maLopLich = this.maLop;
            f.tenLopLich = this.tenLop;
            changeFormLich(f);
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            //lh_DSHocSinhLop f = new lh_DSHocSinhLop();
            //f.maLopDSl = this.maLop;
            //changeFormHS(f);
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            //pnMainHS.Controls.Clear();
        }

        private void tabLichHoc_Leave(object sender, EventArgs e)
        {
            this.pnMainLich.Controls.Clear();
        }


        private void tabGV_Enter(object sender, EventArgs e)
        {
            lh_GiaoVien f = new lh_GiaoVien();
            f.maLopGV = this.maLop;
            changeFormGV(f);
        }

        private void tabGV_Leave(object sender, EventArgs e)
        {
            this.pnGV.Controls.Clear();
        }

        private void picBack_Click(object sender, EventArgs e)
        {
            lh_DanhSachLopHoc f = new lh_DanhSachLopHoc();
            Addform(f);
        }
    }
}
