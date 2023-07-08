using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English2.Views.thanhVien;
using English2.Views.taiKhoan;
using English2.Views.Phong;
using English2.Views.caHoc;
using English2.Views.monHoc;
using English2.Views.chuongTrinhHoc;
using English2.Views.khoaHoc;
using English2.Views.menuStrip;
using English2.Views.hocSinh;
using English2.Views.lopHoc;
using English2.Views.ghiDanh;
using English2.Views.giaoVien;
using English2.Models;
namespace English2.Views
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            new DangNhap().ShowDialog();
            this.btnUser.DropDownControl = popupMenu1;
            btnUser.Text = username;


        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public static string username;
        private void Addform(Form f)
        {
            try
            {
                fMain.pnMain.Controls.Clear();
                f.TopLevel = false;
                f.AutoScroll = true;
                f.Dock = DockStyle.Fill;
                f.FormBorderStyle = FormBorderStyle.None;
                fMain.pnMain.Controls.Add(f);
                f.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        private void dsTaiKhoan_Click(object sender, EventArgs e)
        {
            tk_DanhSachTaiKhoan f = new tk_DanhSachTaiKhoan();
            Addform(f);
        }

        private void dsThanhVien_Click(object sender, EventArgs e)
        {
            tv_DanhSachThanhVien f = new tv_DanhSachThanhVien();
            Addform(f);
        }
        
        private void qlChiNhanh_Click(object sender, EventArgs e)
        {

        }

        private void qlPhongHoc_Click(object sender, EventArgs e)
        {
            p_DanhSachPhong f = new p_DanhSachPhong();
            Addform(f);
        }

        private void qlCaHoc_Click(object sender, EventArgs e)
        {
            ch_DanhSachCaHoc f = new ch_DanhSachCaHoc();
            Addform(f);
        }
        
        private void qlKhoaHoc_Click(object sender, EventArgs e)
        {
            kh_DanhSachKhoaHoc f = new kh_DanhSachKhoaHoc();
            Addform(f);
        }

        private void qlCTHoc_Click(object sender, EventArgs e)
        {
            cth_DanhSachChuongTrinhHoc f = new cth_DanhSachChuongTrinhHoc();
            Addform(f);
        }
        
        private void qlMonHoc_Click(object sender, EventArgs e)
        {
            mh_DanhSachMonHoc f = new mh_DanhSachMonHoc();
            Addform(f);
        }

        private void dsLop_Click(object sender, EventArgs e)
        {
            lh_DanhSachLopHoc f = new lh_DanhSachLopHoc();
            Addform(f);
        }

        private void qlGiaoVien_Click(object sender, EventArgs e)
        {
            gv_DSGiaoVien f = new gv_DSGiaoVien();
            Addform(f);

        }
        
        private void dsHS_Click(object sender, EventArgs e)
        {
            hs_DanhSachHocSinh f = new hs_DanhSachHocSinh();
            Addform(f);
        }

        private void dkHocThuHP_Click(object sender, EventArgs e)
        {
     
        }

        private void thongTinCaNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ms_CapNhatThongTin f = new ms_CapNhatThongTin();
            Addform(f);
        }

        private void doiMKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ms_DoiMatKhau f = new ms_DoiMatKhau();
            Addform(f);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThongKeThu_11_Click(object sender, EventArgs e)
        {
            gd_ThongKeThu f = new gd_ThongKeThu();
            Addform(f);
        }



        void restart()
        {
            this.TaiKhoan_1.Visible = false;
            this.gach_1.Visible = false;
            //
            this.ThanhVien_2.Visible = false;
            this.gach_2.Visible = false;
            //
            this.Phong_3.Visible = false;
            this.gach_3.Visible = false;
            //
            this.CaHoc_4.Visible = false;
            this.gach_4.Visible = false;
            //
            this.KhoaHoc_5.Visible = false;
            this.gach_5.Visible = false;
            //
            this.CTHoc_6.Visible = false;
            this.gach_6.Visible = false;
            //
            this.MonHoc_7.Visible = false;
            this.gach_7.Visible = false;
            //
            this.LopHoc_8.Visible = false;
            this.gach_8.Visible = false;
            //
            this.GiaoVien_9.Visible = false;
            this.gach_9.Visible = false;
            //
            this.HocSinh_10.Visible = false;
            this.gach_10.Visible = false;
            //
            this.ThongKeThu_11.Visible = false;
        }
        private void fMain_Load(object sender, EventArgs e)
        {

            restart();

            tt_taiKhoan tv = db.tt_taiKhoan.Single(c => c.tkThanhVien.Equals(username));
            String[] arrPermission = tv.quyenHan.Split('#');//VD: 1#2#8#10#
            for (int i = 0; i < arrPermission.Length; i++)
            {
                switch (arrPermission[i])
                {
                    case "1":
                        this.TaiKhoan_1.Visible = true;
                        this.gach_1.Visible = true;
                        continue;
                    case "2":
                        this.ThanhVien_2.Visible = true;
                        this.gach_2.Visible = true;
                        continue;
                    case "3":
                        this.Phong_3.Visible = true;
                        this.gach_3.Visible = true;
                        continue;
                    case "4":
                        this.CaHoc_4.Visible = true;
                        this.gach_4.Visible = true;
                        continue;
                    case "5":
                        this.KhoaHoc_5.Visible = true;
                        this.gach_5.Visible = true;
                        continue;
                    case "6":
                        this.CTHoc_6.Visible = true;
                        this.gach_6.Visible = true;
                        continue;
                    case "7":
                        this.MonHoc_7.Visible = true;
                        this.gach_7.Visible = true;
                        continue;
                    case "8":
                        this.LopHoc_8.Visible = true;
                        this.gach_8.Visible = true;
                        continue;
                    case "9":
                        this.GiaoVien_9.Visible = true;
                        this.gach_9.Visible = true;
                        continue;
                    case "10":
                        this.HocSinh_10.Visible = true;
                        this.gach_10.Visible = true;
                        continue;
                    case "11":
                        this.ThongKeThu_11.Visible = true;
                        continue;

                    default:
                        break;
                }
            }

        }
    }
}
