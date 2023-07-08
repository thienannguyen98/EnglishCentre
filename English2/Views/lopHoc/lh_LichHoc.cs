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
using English2.Views.thanhVien;

namespace English2.Views.lopHoc
{
    public partial class lh_LichHoc : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        lh_CTLopHoc f = new lh_CTLopHoc();
        public int maLopLich;
        public string tenLopLich;
        public lh_LichHoc()
        {
            InitializeComponent();
            
        }
        void loadDataLichHoc()
        {
            //List<tt_lichHoc> ch = db.tt_lichHoc.Where(c => c.maLop.Equals(maLopLich)).ToList();
            //int ma = ch[1].maCa;
            //tt_dangKyGD dk = db.tt_dangKyGD.Single(c => c.maCa.Equals(ma));
            //tt_thanhVien tv = db.tt_thanhVien.Single(c => c.maTV.Equals(dk.maTV));

            this.dgLichHoc.DataSource = db.tt_lichHoc.Where(c => c.maLop.Equals(maLopLich)).Select(c => new
            {
                maLop = c.maLop,
                maPhong = c.maPhong,
                maCa = c.maCa,
                thuHoc = c.thu == 1 ?"Chủ nhật" :"Thứ " + c.thu,
                buoiHoc = c.tt_caHoc.buoiHoc == 1 ? "Sáng" : (c.tt_caHoc.buoiHoc == 2 ? "Trưa" : "Tối"),
                tenCa = c.tt_caHoc.tenCa,
                batDau = c.tt_caHoc.batDau,
                ketThuc = ((c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) && ((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) : ((
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60)))
                ,
                tenPhong = c.tt_phongHoc.tenPhong,

                //hoTenGV = tv.hoTV + " " + tv.tenTV,
            }).OrderBy(x=> x.buoiHoc).ToList();
        }
        //Customize LichHoc
        void dataStyleLichHoc()
        {
            this.dgLichHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgLichHoc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgLichHoc.MultiSelect = false;
            this.dgLichHoc.RowTemplate.Height = 70;

            this.dgLichHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgLichHoc.Columns[0].Visible = false;
            this.dgLichHoc.Columns[1].Visible = false;
            this.dgLichHoc.Columns[2].Visible = false;
            //ho
            this.dgLichHoc.Columns[3].HeaderText = "Thứ";
            this.dgLichHoc.Columns[3].Width = 110;
            //ten
            this.dgLichHoc.Columns[4].HeaderText = "Buổi học";
            this.dgLichHoc.Columns[4].Width = 150;
            this.dgLichHoc.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Giới tính
            this.dgLichHoc.Columns[5].HeaderText = "Tên ca";
            this.dgLichHoc.Columns[5].Width = 200;
            
            //bd
            this.dgLichHoc.Columns[6].HeaderText = "Bắt đầu";
            this.dgLichHoc.Columns[6].DefaultCellStyle.Format = "HH:mm";
            this.dgLichHoc.Columns[6].Width = 160;
            this.dgLichHoc.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Chỉnh lại là thời gian kết thúc
            this.dgLichHoc.Columns[7].HeaderText = "Kết thúc";
            this.dgLichHoc.Columns[7].Width = 160;
            this.dgLichHoc.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgLichHoc.Columns[7].DefaultCellStyle.Format = "HH:mm";
            this.dgLichHoc.Columns[8].HeaderText = "Phòng";
            this.dgLichHoc.Columns[8].Width = 160;
            this.dgLichHoc.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //HoTen GV
            //this.dgLichHoc.Columns[9].HeaderText = "Giáo viên";
            //this.dgLichHoc.Columns[9].Width = 160;
            ////Tên môn
            //this.dgLichHoc.Columns[10].HeaderText = "Môn học";
            //this.dgLichHoc.Columns[10].Width = 160;
        }

        private void lh_LichHoc_Load(object sender, EventArgs e)
        {

            loadDataLichHoc();
            dataStyleLichHoc();
        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            lh_ThemCTLop f = new lh_ThemCTLop();
            f.maLopThemLich = this.maLopLich;
            f.tenLopThemLich = this.tenLopLich;
            Addform(f);
            
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
        private void btnXem_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
