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
using English2.Views.hocSinh;
using English2.Views.lopHoc;
namespace English2.Views.ghiDanh
{
    public partial class gd_ThuHP : Form
    {
        public gd_ThuHP()
        {
            InitializeComponent();
            
            dgKhoaHoc.ClearSelection();
            dgKhoaHoc.MultiSelect = false;

        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public string maHSThuHP = "";
        void startThuTien()
        {
            txtTienCanDong.Text = "";
            txtTienDaDong.Text = "";
            txtHocPhi.Text = "";
            txtMienGiam.Text = "";
            txtTien.Text = "";
            txtNguoiDong.Text = "";
            txtDChi.Text = "";
            txtGhiChu.Text = "";
        }
        void setDataThuHP()
        {
            txtHocPhi.Text = this.dgKhoaHoc.CurrentRow.Cells[5].Value.ToString();
            txtMienGiam.Text = this.dgKhoaHoc.CurrentRow.Cells[6].Value.ToString();
            int maLopp = Int32.Parse(this.dgKhoaHoc.CurrentRow.Cells[1].Value.ToString());
            //Tồn tại thì mới sum đc
            var tienDaThu = "";
            try
            {
                tienDaThu = db.tt_dongHP.Where(c => c.maHS == maHSThuHP && c.maLop == maLopp).Sum(c => c.soTien).ToString();
            }
            catch (Exception)
            {
                tienDaThu = "0";
            }
            int tienCanDong = Int32.Parse(txtHocPhi.Text) - Int32.Parse(tienDaThu.ToString()) - Int32.Parse(txtMienGiam.Text);
            txtTienDaDong.Text = tienDaThu.ToString();
            txtTienCanDong.Text = tienCanDong.ToString();
            tt_hocSinh hs = db.tt_hocSinh.Single(c => c.maHS == maHSThuHP);
            txtNguoiDong.Text = "Phụ huynh của " + hs.hoHS + " " + hs.tenHS;
            if (tienCanDong == 0)
            {
                tt_dsLop hsl = db.tt_dsLop.Single(c => c.maHS.Equals(maHSThuHP) && c.maLop == maLopp);
                hsl.trangThai = true;
                db.SaveChanges();

                grThu.Visible = false;
                grGhiChu.Visible = false;
                btnThu.Visible = false;
                PictureBox pic = new PictureBox();
                pic.Size = new Size(64, 64);
                pic.Location = new Point(20, 20);
                pic.ImageLocation = "D:\\WinformC#\\English2\\English2\\Resources\\check.png";
                pic.Image = Properties.Resources.check;
                Label tb = new Label();
                tb.Size = new Size(350, 64);
                tb.Font = new System.Drawing.Font("Times New Roman", 20, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tb.ForeColor = Color.Red;
                tb.Text = "Đã thu đầy đủ học phí của khóa học này!";



                tb.Location = new Point(100, 20);
                this.pnThuTien.Controls.Add(tb);
                this.pnThuTien.Controls.Add(pic);
            }
            else
            {
                grThu.Visible = true;
                grGhiChu.Visible = true;
                btnThu.Visible = true;
            }
        }
        //Khóa học
        void loadDataKH()
        {
            this.dgKhoaHoc.DataSource = db.tt_dsLop.Where(c => c.maHS == maHSThuHP).Select(c => new
            {
                maHS = c.maHS,
                maLop = c.maLop,
                tenLop = c.tt_lopHoc.tenLop,
                tenKH = c.tt_lopHoc.tt_khoaHoc.tenHK,
                tenCT = c.tt_lopHoc.tt_chuongTrinh.tenCT,
                hocPhi = c.tt_lopHoc.hocPhi,
                mienGiam = c.mienGiam,
            }).ToList();
        }
        void styleDataKH()
        {
            this.dgKhoaHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgKhoaHoc.MultiSelect = false;
            this.dgKhoaHoc.Columns[0].Visible = false;
            this.dgKhoaHoc.Columns[1].Visible = false;

            this.dgKhoaHoc.Columns[2].HeaderText = "Tên lớp";
            this.dgKhoaHoc.Columns[2].Width = 120;

            this.dgKhoaHoc.Columns[3].HeaderText = "Tên khóa học";
            this.dgKhoaHoc.Columns[3].Width = 140;

            this.dgKhoaHoc.Columns[4].HeaderText = "Tên chương trình";
            this.dgKhoaHoc.Columns[4].Width = 150;


            this.dgKhoaHoc.Columns[5].HeaderText = "Học phí";
            this.dgKhoaHoc.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgKhoaHoc.Columns[5].Width = 90;

            this.dgKhoaHoc.Columns[6].HeaderText = "Miễn giảm";
            this.dgKhoaHoc.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgKhoaHoc.Columns[6].Width = 90;
        }
        private void dgKhoaHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            setDataThuHP();
        }


        private void btnThu_Click(object sender, EventArgs e)
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                //tt_DongHP
                int maxLanThu = 1;
                int maLopThu = Int32.Parse(this.dgKhoaHoc.CurrentRow.Cells[1].Value.ToString());
                var checkExist = dbb.tt_dongHP.Where(c => c.maHS.Equals(maHSThuHP) && c.maLop == maLopThu);
                var count = checkExist.Count();
                if (count > 0)
                    maxLanThu = db.tt_dongHP.ToList().Where(p => p.maHS.Equals(maHSThuHP) && p.maLop.Equals(maLopThu)).Max(p => Convert.ToInt32(p.lanThu)) + 1;
                try
                {
                    int maxBienLai = dbb.tt_dongHP.ToList().Max(p => Convert.ToInt32(p.soBienLai)) + 1;
                    tt_dongHP dongHP = new tt_dongHP()
                    {
                        maLop = Int32.Parse(this.dgKhoaHoc.CurrentRow.Cells[1].Value.ToString()),
                        maHS = maHSThuHP,
                        soBienLai = maxBienLai.ToString(),
                        soTien = long.Parse(txtTien.Text),
                        lanThu = maxLanThu,
                        mienGiam = long.Parse(txtMienGiam.Text),
                    };
                    dbb.tt_dongHP.Add(dongHP);
                    string userName = fMain.username;
                    tt_taiKhoan tv = db.tt_taiKhoan.Single(c => c.tkThanhVien.Equals(userName));
                    int maTvien = tv.maTV;
                    int maCNhanh = Int32.Parse(tv.tt_thanhVien.maCN.ToString());
                    tt_thuTien thuT = new tt_thuTien()
                    {
                        maCN = maCNhanh,
                        soBienLai = maxBienLai.ToString(),
                        ngayThu = DateTime.Now,
                        chietKhau = 0,
                        soTien = long.Parse(txtTien.Text),
                        nguoiNop = txtNguoiDong.Text,
                        dcNguoiNop = txtDChi.Text,
                        maTV = maTvien,
                    };
                    dbb.tt_thuTien.Add(thuT);
                    dbb.SaveChanges();
                    MessageBox.Show("Thu tiền thành công");
                    setDataThuHP();
                    startThuTien();
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Thu tiền thất bại!");
                }
            }
        }

        private void gd_ThuHP_Load(object sender, EventArgs e)
        {
            dgKhoaHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgKhoaHoc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgKhoaHoc.RowTemplate.Height = 70;
            loadDataKH();
            styleDataKH();
        }

        private void gd_ThuHP_Leave(object sender, EventArgs e)
        {
            startThuTien();
            grThu.Visible = true;
            grGhiChu.Visible = true;
            btnThu.Visible = true;
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

        private void picBack_Click(object sender, EventArgs e)
        {
            hs_DanhSachHocSinh f = new hs_DanhSachHocSinh();
            Addform(f);
        }
        
        
    }
}
