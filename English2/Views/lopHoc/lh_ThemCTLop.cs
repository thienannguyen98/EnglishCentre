using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English2.Models;
using English2.Views.thanhVien;
using MaterialSkin;

namespace English2.Views.lopHoc
{
    public partial class lh_ThemCTLop : MaterialSkin.Controls.MaterialForm
    {
        public lh_ThemCTLop()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
            start();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public int maLopThemLich;
        public int maCN;
        public int maCaGV;
        public string tenLopThemLich;
        #region method
        void enableChange()
        {
            ((Control)this.chonLich).Enabled = false;
        }
        void start()
        {
            this.btnNextToGV.Enabled = false;
            this.btnNextToPhong.Enabled = false;
            this.btnNexToXN.Enabled = false;

        }

        void loadDataCaHoc()
        {
            this.dgDanhSachCa.DataSource = db.tt_caHoc.Select(c => new
            {
                maCa = c.maCa,
                buoiHoc = c.buoiHoc == 1 ? "Sáng" : (c.buoiHoc == 2 ? "Trưa" : "Tối"),
                tenCa = c.tenCa,
                batDau = c.batDau,
                ketThuc = ((c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) < 10) && ((c.thoiLuong + c.batDau.Minute) % 60 < 10) ?
                ("0" + (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) + ":0" + (c.thoiLuong + c.batDau.Minute) % 60) : ((
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) < 10) ?
                ("0" + (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) + ":" + (c.thoiLuong + c.batDau.Minute) % 60) :
                (((c.thoiLuong + c.batDau.Minute) % 60 < 10) ?
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60 + ":0" + (c.thoiLuong + c.batDau.Minute) % 60) :
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60 + ":" + (c.thoiLuong + c.batDau.Minute) % 60))),
            }).ToList();
            this.dgDanhSachCa.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDanhSachCa.MultiSelect = false;
            this.dgDanhSachCa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachCa.RowTemplate.Height = 100;
            this.dgDanhSachCa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachCa.Columns[0].HeaderText = "Mã ca";
            this.dgDanhSachCa.Columns[0].Width = 80;
            this.dgDanhSachCa.Columns[0].Visible = false;
            this.dgDanhSachCa.Columns[1].HeaderText = "Buổi học";
            this.dgDanhSachCa.Columns[1].Width = 190;
            this.dgDanhSachCa.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgDanhSachCa.Columns[2].HeaderText = "Tên ca";
            this.dgDanhSachCa.Columns[2].Width = 240;


            this.dgDanhSachCa.Columns[3].HeaderText = "Thời gian bắt đầu";
            this.dgDanhSachCa.Columns[3].DefaultCellStyle.Format = "HH:mm";
            this.dgDanhSachCa.Columns[3].Width = 190;
            this.dgDanhSachCa.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Chỉnh lại là thời gian kết thúc
            this.dgDanhSachCa.Columns[4].HeaderText = "Thời gian kết thúc";
            this.dgDanhSachCa.Columns[4].Width = 190;
            this.dgDanhSachCa.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachCa.Columns[4].DefaultCellStyle.Format = "HH:mm";
        }
        //giaovien
        void loadDataGiaoVien()
        {

            tt_monHoc mh = db.tt_monHoc.Single(c => c.tenMon.Equals(cbTenMon.Text));
            int maMon = mh.maMon;
            this.dgGiaoVien.DataSource = db.tt_dangKyGD.Where(c => c.maCa == maCaGV && c.maMon == maMon).Select(c => new
            {
                maTV = c.maTV,
                hoTen = c.tt_thanhVien.hoTV + " " + c.tt_thanhVien.tenTV,
                sdt = c.tt_thanhVien.soDT,
                email = c.tt_thanhVien.eMail,
                diaChi = c.tt_thanhVien.dcLienLac,

            }
            ).ToList();
            this.dgGiaoVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgGiaoVien.MultiSelect = false;
            this.dgGiaoVien.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgGiaoVien.RowTemplate.Height = 70;
            this.dgGiaoVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgGiaoVien.Columns[0].Visible = false;


            this.dgGiaoVien.Columns[1].HeaderText = "Họ tên";
            this.dgGiaoVien.Columns[1].Width = 220;

            this.dgGiaoVien.Columns[2].HeaderText = "Số điện thoại";
            this.dgGiaoVien.Columns[2].Width = 160;

            this.dgGiaoVien.Columns[3].HeaderText = "Email";
            this.dgGiaoVien.Columns[3].Width = 230;

            this.dgGiaoVien.Columns[4].HeaderText = "Địa chỉ";
            this.dgGiaoVien.Columns[4].Width = 200;
        }
        //Phonghoc
        void loadDataPhongHoc()
        {
            tt_lopHoc lh = db.tt_lopHoc.Single(t => t.maLop == maLopThemLich);
            this.dgDanhSachPhong.DataSource = db.tt_phongHoc.Where(c => c.tt_chiNhanh.maCN == lh.maCN).Select(c => new
            {
                maPhong = c.maPhong,
                tenPhong = c.tenPhong,
                hoatDong = c.hoatDong == true ? "Hoạt động" : "Trống",
                sucChua = c.sucChua,
                coProjector = c.coProjector == true ? "Có" : "Không có",
                coTV = c.coTiVi == true ? "Có" : "Không có"
            }).ToList();
            this.dgDanhSachPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachPhong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDanhSachPhong.MultiSelect = false;
            this.dgDanhSachPhong.RowTemplate.Height = 100;
            this.dgDanhSachPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[0].HeaderText = "Mã phòng";
            this.dgDanhSachPhong.Columns[0].Width = 110;
            this.dgDanhSachPhong.Columns[0].Visible = false;
            this.dgDanhSachPhong.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[1].HeaderText = "Tên phòng";
            this.dgDanhSachPhong.Columns[1].Width = 180;

            this.dgDanhSachPhong.Columns[2].HeaderText = "Trạng thái";
            this.dgDanhSachPhong.Columns[2].Width = 150;
            this.dgDanhSachPhong.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[3].HeaderText = "Sức chứa";
            this.dgDanhSachPhong.Columns[3].Width = 160;
            this.dgDanhSachPhong.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[4].HeaderText = "Projector";
            //this.dgDanhSachPhong.
            this.dgDanhSachPhong.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[4].Width = 160;
            this.dgDanhSachPhong.Columns[5].HeaderText = "Tivi";
            this.dgDanhSachPhong.Columns[5].Width = 160;
            this.dgDanhSachPhong.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        //Them
        public void taoLich()
        {
            try
            {
                tt_lichHoc lh = new tt_lichHoc();
                lh.maLop = maLopThemLich;
                lh.maCa = Int32.Parse(this.dgDanhSachCa.CurrentRow.Cells[0].Value.ToString());
                lh.maPhong = Int32.Parse(this.dgDanhSachPhong.CurrentRow.Cells[0].Value.ToString());
                lh.thu = cbThuHoc.SelectedIndex + 1;
                db.tt_lichHoc.Add(lh);
                //
                tt_dsGiaoVienLop gv = new tt_dsGiaoVienLop();
                gv.maTV = Int32.Parse(this.dgGiaoVien.CurrentRow.Cells[0].Value.ToString());
                gv.maLop = this.maLopThemLich;
                gv.dayThu = (cbThuHoc.SelectedIndex + 1).ToString();
                gv.ngayNhanLop = DateTime.Parse(dateNhanLop.Text);
                gv.ngayThoiDay = DateTime.Parse(dateKT.Text);
                db.tt_dsGiaoVienLop.Add(gv);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công");
                lbXacNhan.ForeColor = Color.DeepSkyBlue;
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại!");
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

        #region event
        private void dgDanhSachCa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cbThuHoc.Text.Length > 0)
            {
                this.btnNextToGV.Enabled = true;
            }
            maCaGV = Int32.Parse(this.dgDanhSachCa.CurrentRow.Cells[0].Value.ToString());
            DateTime date = DateTime.Parse(this.dgDanhSachCa.CurrentRow.Cells[3].Value.ToString());
            txtCaHoc.Text = (this.dgDanhSachCa.CurrentRow.Cells[2].Value.ToString()) + " - Từ " + (date.ToString("hh:mm ", CultureInfo.InvariantCulture) + " đến " + (this.dgDanhSachCa.CurrentRow.Cells[4].Value.ToString()));
        }
        private void lh_ThemCTLop_Load(object sender, EventArgs e)
        {
            loadDataCaHoc();
            loadDataPhongHoc();
            tt_lopHoc lh = db.tt_lopHoc.Single(t => t.maLop == (maLopThemLich));
            this.cbTenMon.DataSource = db.tt_ctChuongTrinh.Where(c => c.maCT == lh.maCT).Select(c => c.tt_monHoc.tenMon).ToList();
            loadDataGiaoVien();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            taoLich();
            lh_CTLopHoc f = new lh_CTLopHoc();
            f.maLop = maLopThemLich;
            Addform(f);
            f.materialTabControl1.SelectedIndex = 2;
        }


        private void giaoVien_Enter(object sender, EventArgs e)
        {
            loadDataGiaoVien();
        }

        private void cbTenMon_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            loadDataGiaoVien();
        }
        

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (materialTabControl1.SelectedIndex)
            {
                case 1:
                    cbChonLich.Checked = cbChonLich.Enabled = true;
                    lbLich.ForeColor = Color.DeepSkyBlue;
                    lbGiaoVien.ForeColor = Color.Black;
                    sep1.LineColor = Color.DeepSkyBlue;
                    break;
                case 2:
                    cbChonGV.Checked = cbChonGV.Enabled = true;
                    lbGiaoVien.ForeColor = Color.DeepSkyBlue;
                    lbPhong.ForeColor = Color.Black;
                    sep2.LineColor = Color.DeepSkyBlue;
                    break;
                case 3:
                    cbPhong.Checked = cbPhong.Enabled = true;
                    lbPhong.ForeColor = Color.DeepSkyBlue;
                    lbXacNhan.ForeColor = Color.Black;
                    cbXacNhan.Checked = cbXacNhan.Enabled = true;
                    sep3.LineColor = Color.DeepSkyBlue;
                    break;
            }
        }

        private void btnNextToGV_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 1;
        }

        private void btnBackLich_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 0;
        }

        private void btnNextToPhong_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 2;
        }

        private void btnBackGV_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 1;
        }

        private void btnNexToXN_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 3;
        }

        private void btnBackToPhong_Click(object sender, EventArgs e)
        {
            materialTabControl1.SelectedIndex = 2;
        }

        private void cbThuHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtThuHoc.Text = cbThuHoc.Text;
        }

        private void dgGiaoVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnNextToPhong.Enabled = true;
            this.txtGiaoVien.Text = this.dgGiaoVien.CurrentRow.Cells[1].Value.ToString();
            this.txtBD.Text = this.dateNhanLop.Text;
            this.txtKT.Text = this.dateKT.Text;
            this.txtMonHoc.Text = this.cbTenMon.Text;
        }

        private void dgDanhSachPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnNexToXN.Enabled = true;
            this.txtPhong.Text = this.dgDanhSachPhong.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picBack_Click(object sender, EventArgs e)
        {

            lh_CTLopHoc f = new lh_CTLopHoc();
            f.maLop = this.maLopThemLich;

            lh_DSHocSinhLop fHS = new lh_DSHocSinhLop();
            fHS.maLopDSl = this.maLopThemLich;

            lh_LichHoc fLich = new lh_LichHoc();
            //KHÔNG LẤY ĐƯỢC TÊN LÀ VÌ KHI LOAD LẠI BÊN KIA NÓ LOAD LẠI CÁI LB
            f.lbLop.Text = this.tenLopThemLich;

            Addform(f);
            f.materialTabControl1.SelectedIndex = 2;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {


            lh_CTLopHoc f = new lh_CTLopHoc();
            f.maLop = this.maLopThemLich;

            lh_DSHocSinhLop fHS = new lh_DSHocSinhLop();
            fHS.maLopDSl = this.maLopThemLich;

            lh_LichHoc fLich = new lh_LichHoc();
            fLich.maLopLich = this.maLopThemLich;
            f.lbLop.Text = this.tenLopThemLich;

            Addform(f);
            f.materialTabControl1.SelectedIndex = 2;
        }
        #endregion
    }
}
