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
namespace English2.Views.ghiDanh
{
    public partial class gd_DangKy : Form
    {
        public gd_DangKy()
        {
            InitializeComponent();
            this.cbKhoaHoc.DataSource = db.tt_khoaHoc.Select(c => c.tenHK).ToList();

            this.cbChuongTrinh.DataSource = db.tt_chuongTrinh.Select(c => c.tenCT).ToList();

        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public string maHSDangKy = "";
        //Lớp
        void loadDataLop()
        {
            tt_khoaHoc kh = db.tt_khoaHoc.Single(c => c.tenHK.Equals(cbKhoaHoc.Text));
            tt_chuongTrinh ct = db.tt_chuongTrinh.Single(c => c.tenCT.Equals(cbChuongTrinh.Text));
            this.dgChonLop.DataSource = db.tt_lopHoc.Where(c => c.maHK == kh.maKH && c.maCT == ct.maCT).Select(c => new
            {
                maLop = c.maLop,
                chiNhanh = c.tt_chiNhanh.tenCN,
                tenLop = c.tenLop,
                ngayKG = c.ngayKG,
                thoiLuong = c.thoiLuong,
                siSo = c.siSoHKT,
                hocPhi = c.hocPhi,
            }).OrderBy(x => x.tenLop).ToList();
        }
        void styleDataLop()
        {
            dgChonLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgChonLop.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgChonLop.MultiSelect = false;
            dgChonLop.RowTemplate.Height = 50;

            this.dgChonLop.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgChonLop.Columns[0].Visible = false;

            this.dgChonLop.Columns[1].HeaderText = "Chi nhánh";
            this.dgChonLop.Columns[1].Width = 140;

            this.dgChonLop.Columns[2].HeaderText = "Tên lớp";
            this.dgChonLop.Columns[2].Width = 140;

            this.dgChonLop.Columns[3].HeaderText = "Khai giảng";
            this.dgChonLop.Columns[3].Width = 130;
            this.dgChonLop.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgChonLop.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgChonLop.Columns[4].HeaderText = "Thời lượng";
            this.dgChonLop.Columns[4].Width = 110;
            this.dgChonLop.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgChonLop.Columns[5].HeaderText = "Sỉ số";
            this.dgChonLop.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgChonLop.Columns[5].Width = 90;

            this.dgChonLop.Columns[6].HeaderText = "Học phí";
            this.dgChonLop.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgChonLop.Columns[6].Width = 140;
        }
        //Lịch học
        void loadDataLich()
        {
            int maLop = Int32.Parse(this.dgChonLop.CurrentRow.Cells[0].Value.ToString());
            this.dgLichHoc.DataSource = db.tt_lichHoc.Where(c => c.maLop == maLop).Select(c => new
            {
                maLop = c.maLop,
                thuHoc = c.thu,
                buoiHoc = c.tt_caHoc.buoiHoc == 1 ? "Sáng" : (c.tt_caHoc.buoiHoc == 2 ? "Trưa" : "Tối"),
                tenCa = c.tt_caHoc.tenCa,
                batDau = c.tt_caHoc.batDau,
                ketThuc = ((c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) && ((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) : ((
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60))),
            }).OrderBy(x=> x.buoiHoc).ToList();
        }
        void styleDataLich()
        {

            //
            this.dgLichHoc.Columns[0].Visible = false;
            //ho
            this.dgLichHoc.Columns[1].HeaderText = "Thứ";
            this.dgLichHoc.Columns[1].Width = 130;
            this.dgLichHoc.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ten
            this.dgLichHoc.Columns[2].HeaderText = "Buổi học";
            this.dgLichHoc.Columns[2].Width = 140;
            this.dgLichHoc.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Giới tính
            this.dgLichHoc.Columns[3].HeaderText = "Tên ca";
            this.dgLichHoc.Columns[3].Width = 190;

            //bd
            this.dgLichHoc.Columns[4].HeaderText = "Bắt đầu";
            this.dgLichHoc.Columns[4].DefaultCellStyle.Format = "HH:mm";
            this.dgLichHoc.Columns[4].Width = 140;
            this.dgLichHoc.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Chỉnh lại là thời gian kết thúc
            this.dgLichHoc.Columns[5].HeaderText = "Kết thúc";
            this.dgLichHoc.Columns[5].Width = 140;
            this.dgLichHoc.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgLichHoc.Columns[5].DefaultCellStyle.Format = "HH:mm";
        }
        void dangKyKH()
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                //--Lưu dữ liệu
                try
                {
                    int maLop = Int32.Parse(this.dgChonLop.CurrentRow.Cells[0].Value.ToString());

                tt_dsLop hs = new tt_dsLop()
                {
                    maLop = maLop,
                    maHS = maHSDangKy,
                    trangThai = false,
                    mienGiam = Int64.Parse(txtMG.Text),
                };
                dbb.tt_dsLop.Add(hs);
                dbb.SaveChanges();
                MessageBox.Show("Đăng ký thành công");
                    gd_ThuHP f = new gd_ThuHP();
                    f.maHSThuHP = maHSDangKy;
                    Addform(f);
                }
                catch (Exception)
                {
                    MessageBox.Show("Đăng ký thất bại!");
                }
            }
        }

        //
        private void cbChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataLop();
            styleDataLop();
        }

        private void cbKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataLop();
            styleDataLop();
        }

        private void dgChonLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            loadDataLich();
            styleDataLich();
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            dangKyKH();

            
        }

        private void gd_DangKy_Load(object sender, EventArgs e)
        {
            dgLichHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgLichHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLichHoc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgLichHoc.MultiSelect = false;
            dgLichHoc.RowTemplate.Height = 50;
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
