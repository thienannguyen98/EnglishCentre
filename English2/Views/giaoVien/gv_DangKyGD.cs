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
using English2.Views;
namespace English2.Views.giaoVien
{
    public partial class gv_DangKyGD : Form
    {
        public gv_DangKyGD()
        {
            InitializeComponent();
            

            this.dgLichDKDay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgLichDKDay.RowTemplate.Height = 70;
            this.dgLichDKDay.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgLichDKDay.MultiSelect = false;
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public int maTV;
        void loadData()
        {
            this.dgLichDKDay.DataSource = db.tt_dangKyGD.Where(c => c.maTV == maTV).Select(c => new
            {
                maMon = c.maMon,
                maCa = c.maCa,
                tenMon = c.tt_monHoc.tenMon,
                buoiHoc = c.tt_caHoc.buoiHoc == 1 ? "Sáng" : (c.tt_caHoc.buoiHoc == 2 ? "Trưa" : "Tối"),
                thuTT = c.thuTT,
                batDau = c.tt_caHoc.batDau,
                ketThuc = ((c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) && ((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) : ((
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60)))
                ,

            }).OrderBy(x=> x.buoiHoc).ToList();
        }
        void styleData()
        {
            this.dgLichDKDay.Columns[0].Visible = false;
            this.dgLichDKDay.Columns[1].Visible = false;

            this.dgLichDKDay.Columns[2].HeaderText = "Tên môn";
            this.dgLichDKDay.Columns[2].Width = 120;
            this.dgLichDKDay.Columns[3].HeaderText = "Buổi học";
            this.dgLichDKDay.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgLichDKDay.Columns[3].Width = 85;
            this.dgLichDKDay.Columns[4].HeaderText = "Thứ học";
            this.dgLichDKDay.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgLichDKDay.Columns[4].Width = 85;

            //bd
            this.dgLichDKDay.Columns[5].HeaderText = "Bắt đầu";
            this.dgLichDKDay.Columns[5].DefaultCellStyle.Format = "HH:mm";
            this.dgLichDKDay.Columns[5].Width = 85;
            this.dgLichDKDay.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Chỉnh lại là thời gian kết thúc
            this.dgLichDKDay.Columns[6].HeaderText = "Kết thúc";
            this.dgLichDKDay.Columns[6].Width = 85;
            this.dgLichDKDay.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgLichDKDay.Columns[6].DefaultCellStyle.Format = "HH:mm";
            this.dgLichDKDay.Columns[7].HeaderText = "Xóa";
            this.dgLichDKDay.Columns[7].Width = 60;
        }
        void loadDataLich()
        {
            this.dgCaHoc.DataSource = db.tt_caHoc.Select(c => new
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
            }).OrderBy(x=> x.buoiHoc).ToList();
        }
        void styleDataLich()
        {
            dgCaHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCaHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgCaHoc.MultiSelect = false;
            dgCaHoc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgCaHoc.RowTemplate.Height = 50;
            //
            this.dgCaHoc.Columns[0].Visible = false;

            this.dgCaHoc.Columns[1].HeaderText = "Buổi";
            this.dgCaHoc.Columns[1].Width = 90;
            this.dgCaHoc.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgCaHoc.Columns[2].HeaderText = "Tên ca";
            this.dgCaHoc.Columns[2].Width = 130;
            
            //bd
            this.dgCaHoc.Columns[3].HeaderText = "Bắt đầu";
            this.dgCaHoc.Columns[3].DefaultCellStyle.Format = "HH:mm";
            this.dgCaHoc.Columns[3].Width = 90;
            this.dgCaHoc.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Chỉnh lại là thời gian kết thúc
            this.dgCaHoc.Columns[4].HeaderText = "Kết thúc";
            this.dgCaHoc.Columns[4].Width = 90;
            this.dgCaHoc.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgCaHoc.Columns[4].DefaultCellStyle.Format = "HH:mm";
        }
        private void gv_DangKyGD_Load(object sender, EventArgs e)
        {
            loadData();
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgLichDKDay.Columns.Add(nutXoa);
            styleData();
            loadDataLich();
            styleDataLich();
            cbMonHoc.DataSource = db.tt_monHoc.Select(c => c.tenMon).ToList();
        }
        public void dangKy()
        {
            int maCa = Int32.Parse(this.dgCaHoc.CurrentRow.Cells[0].Value.ToString());
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    tt_dangKyGD dk = new tt_dangKyGD()
                    {
                        maTV = maTV,
                        maCa = maCa,
                        thuTT = cbBuoiHoc.Text,

                    };
                    tt_monHoc mh = dbb.tt_monHoc.Single(t => t.tenMon == (cbMonHoc.SelectedValue.ToString()));
                    dk.maMon = mh.maMon;
                    dbb.tt_dangKyGD.Add(dk);
                    dbb.SaveChanges();
                    MessageBox.Show("Đăng ký thành công");
                    loadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Đăng ký thất bại!");
                }
            }


        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            dangKy();
        }
        void capNhatDK()
        {
            try
            {
                int maCa = Int32.Parse(this.dgCaHoc.CurrentRow.Cells[0].Value.ToString());
                tt_dangKyGD dk = db.tt_dangKyGD.Single(t => t.maTV==maTV);
                //dk.maCa = maCa;
                dk.thuTT = cbBuoiHoc.Text;
                tt_monHoc mh = db.tt_monHoc.Single(t => t.tenMon == (cbMonHoc.SelectedValue.ToString()));
                dk.maMon = mh.maMon;
                MessageBox.Show("Cập nhật thông tin thành công nhưng không thể cập nhật ca học mới!");
                db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật thông tin thất bại!");
            }
}
        private void btnSua_Click(object sender, EventArgs e)
        {
            capNhatDK();
        }

        void xoaThanhVien()
        {
            using (ttAnhNguEntities dbbb = new ttAnhNguEntities())
            {
                
                DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        tt_dangKyGD del = dbbb.tt_dangKyGD.FirstOrDefault(p => p.maTV == maTV);
                        dbbb.tt_dangKyGD.Remove(del);
                        dbbb.SaveChanges();
                        MessageBox.Show("Xóa thành công");
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không xóa được: " + ex.Message);

                    }
                }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void dgLichDKDay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int maMon = Int32.Parse(this.dgLichDKDay.CurrentRow.Cells[1].Value.ToString());
            tt_monHoc mh = db.tt_monHoc.Single(t => t.maMon.Equals(maMon));
            cbMonHoc.Text = mh.tenMon;

            //tt_dangKyGD dk = db.tt_dangKyGD.Single(c => c.maTV == maTV);
            //cbBuoiHoc.Text = dk.thuTT;

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
            gv_DSGiaoVien f = new gv_DSGiaoVien();
            Addform(f);
        }
        private void dgDanhSachTK_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Xóa
            Image xoa = Properties.Resources.trash;
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.trash.Width;
                var h = Properties.Resources.trash.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(xoa, new Rectangle(x, y, w, h));
                e.Handled = true;
            }

        }

        private void dgDanhSachTK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex ==0)
            {
                xoaThanhVien();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            this.dgLichDKDay.DataSource = db.tt_dangKyGD.Where(c => c.maTV == maTV && c.tt_monHoc.tenMon.Contains(txtTimKiem.Text.Trim())).Select(c => new
            {
                maMon = c.maMon,
                maCa = c.maCa,
                tenMon = c.tt_monHoc.tenMon,
                buoiHoc = c.tt_caHoc.buoiHoc == 1 ? "Sáng" : (c.tt_caHoc.buoiHoc == 2 ? "Trưa" : "Tối"),
                thuTT = c.thuTT,
                batDau = c.tt_caHoc.batDau,
                ketThuc = ((c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) && ((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) : ((
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) < 10) ?
                ("0" + (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60) + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (((c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60 < 10) ?
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":0" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60) :
                (c.tt_caHoc.batDau.Hour + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) / 60 + ":" + (c.tt_caHoc.thoiLuong + c.tt_caHoc.batDau.Minute) % 60)))
                ,

            }).OrderBy(x => x.buoiHoc).ToList();
        }
    }
}
