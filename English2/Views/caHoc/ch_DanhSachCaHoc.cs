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

namespace English2.Views.caHoc
{
    public partial class ch_DanhSachCaHoc : Form
    {
        public ch_DanhSachCaHoc()
        {
            InitializeComponent();
            loadData();
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDanhSachCa.Columns.Add(nutXoa);
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        int maCa;
        #region methods
        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            this.dgDanhSachCa.DataSource = db.tt_caHoc.Select(c => new
            {
                maCa = c.maCa,
                buoiHoc = c.buoiHoc == 1 ? "Sáng" : (c.buoiHoc == 2 ? "Trưa" : "Tối"),
                tenCa = c.tenCa,
                kyHieu = c.kyHieu,
                batDau = c.batDau,
                ketThuc = ((c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) < 10) && ((c.thoiLuong + c.batDau.Minute) % 60 < 10) ? 
                ("0" + (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) + ":0" + (c.thoiLuong + c.batDau.Minute) % 60) : ((
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) < 10) ? 
                ("0" + (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) + ":" + (c.thoiLuong + c.batDau.Minute) % 60) : 
                (((c.thoiLuong + c.batDau.Minute) % 60 < 10) ? 
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60 + ":0" + (c.thoiLuong + c.batDau.Minute) % 60) : 
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60 + ":" + (c.thoiLuong + c.batDau.Minute) % 60)) )
                ,
            }).OrderBy(x=> x.buoiHoc).ToList();
            

        }
        void styleData()
        {
            this.dgDanhSachCa.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDanhSachCa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachCa.MultiSelect = false;
            this.dgDanhSachCa.RowTemplate.Height = 70;
            this.dgDanhSachCa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachCa.Columns[0].HeaderText = "Mã ca";
            this.dgDanhSachCa.Columns[0].Width = 80;
            this.dgDanhSachCa.Columns[0].Visible = false;
            this.dgDanhSachCa.Columns[1].HeaderText = "Buổi học";
            this.dgDanhSachCa.Columns[1].Width = 90;
            this.dgDanhSachCa.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgDanhSachCa.Columns[2].HeaderText = "Tên ca";
            this.dgDanhSachCa.Columns[2].Width = 140;
            this.dgDanhSachCa.Columns[3].HeaderText = "Ký hiệu";
            this.dgDanhSachCa.Columns[3].Width = 90;
            this.dgDanhSachCa.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachCa.Columns[4].HeaderText = "Bắt đầu";
            this.dgDanhSachCa.Columns[4].DefaultCellStyle.Format = "HH:mm";
            this.dgDanhSachCa.Columns[4].Width = 110;
            this.dgDanhSachCa.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Chỉnh lại là thời gian kết thúc
            this.dgDanhSachCa.Columns[5].HeaderText = "Kết thúc";
            this.dgDanhSachCa.Columns[5].Width = 110;
            this.dgDanhSachCa.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachCa.Columns[5].DefaultCellStyle.Format = "HH:mm";
            this.dgDanhSachCa.Columns[6].HeaderText = "Xóa";
            this.dgDanhSachCa.Columns[6].Width = 80;
        }
        /// <summary>
        /// Method dùng để add form vào mainpanel của form MainAdmin
        /// </summary>
        /// <param name="f"></param>
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
        /// <summary>
        /// Method dùng để xóa đôi tượng đang select bằng cách lấy maP 
        /// -> Tìm 
        /// và rồi Remove
        /// 
        /// </summary>
        void xoaCaHoc()
        {
            string maCa = dgDanhSachCa.SelectedCells[0].OwningRow.Cells["maCa"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    tt_caHoc del = db.tt_caHoc.Where(p => p.maCa.ToString().Equals(maCa)).SingleOrDefault();
                    db.tt_caHoc.Remove(del);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    loadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Không xóa được");
                    loadData();
                }
            }
            loadData();
        }

        #endregion

        #region event

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            capNhatTT();
        }
        /// <summary>
        /// Tìm kiếm tài khoản theo Username hoặc mã TV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.dgDanhSachCa.DataSource = db.tt_caHoc.Where(c => c.tenCa.Contains(txtTimKiem.Text) || c.buoiHoc.ToString().Contains(txtTimKiem.Text) || c.thoiLuong.ToString().Contains(txtTimKiem.Text)).Select(c => new
            {
                maCa = c.maCa,
                buoiHoc = c.buoiHoc == 1 ? "Sáng" : (c.buoiHoc == 2 ? "Trưa" : "Tối"),
                tenCa = c.tenCa,
                kyHieu = c.kyHieu,
                batDau = c.batDau,
                ketThuc = ((c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) < 10) && ((c.thoiLuong + c.batDau.Minute) % 60 < 10) ?
                ("0" + (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) + ":0" + (c.thoiLuong + c.batDau.Minute) % 60) : ((
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) < 10) ?
                ("0" + (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60) + ":" + (c.thoiLuong + c.batDau.Minute) % 60) :
                (((c.thoiLuong + c.batDau.Minute) % 60 < 10) ?
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60 + ":0" + (c.thoiLuong + c.batDau.Minute) % 60) :
                (c.batDau.Hour + (c.thoiLuong + c.batDau.Minute) / 60 + ":" + (c.thoiLuong + c.batDau.Minute) % 60))),
            }).ToList();
        }

        #endregion
        public void taoCaHoc()
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    if (txtTenCa.Text.Trim().Length == 0)
                    {
                        throw new ArithmeticException("Thêm thất bại!");
                    }
                    tt_caHoc ch = new tt_caHoc()
                    {
                        buoiHoc = cbBuoi.SelectedIndex + 1,
                        tenCa = txtTenCa.Text,
                        kyHieu = txtKyHieu.Text,
                        batDau = DateTime.Parse(mtTGBD.Text),
                        thoiLuong = Int32.Parse(txtThoiLuong.Text),
                        ghiChu = txtGhiChu.Text
                    };
                    dbb.tt_caHoc.Add(ch);
                    dbb.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                    loadData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
                
        }
        /// <summary>
        /// Chỉnh sửa ca học
        /// </summary>
        void capNhatTT()
        {

            try
            {
                if (txtTenCa.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Cập nhật thất bại!");
                }
                tt_caHoc ch = db.tt_caHoc.Single(t => t.maCa.Equals(maCa));
                ch.buoiHoc = cbBuoi.SelectedIndex + 1;
                ch.tenCa = txtTenCa.Text;
                ch.kyHieu = txtKyHieu.Text;
                ch.batDau = DateTime.Parse(mtTGBD.Text);
                ch.thoiLuong = Int32.Parse(txtThoiLuong.Text);
                ch.ghiChu = txtGhiChu.Text;
                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công!");
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật thông tin thất bại!");
            }
        }
        private void btnTao_Click(object sender, EventArgs e)
        {
            taoCaHoc();
        }

        private void cbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgDanhSachCa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maCa = Int32.Parse(this.dgDanhSachCa.CurrentRow.Cells[1].Value.ToString());
            tt_caHoc ph = db.tt_caHoc.Single(t => t.maCa.Equals(maCa));
            cbBuoi.SelectedIndex = ph.buoiHoc - 1;
            txtTenCa.Text = ph.tenCa;
            txtKyHieu.Text = ph.kyHieu;
            txtThoiLuong.Text = ph.thoiLuong.ToString();
            txtGhiChu.Text = ph.ghiChu;
            mtTGBD.Text = ph.batDau.ToString("HH-mm");
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
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 0)
            {
                xoaCaHoc();
            }
        }
    }
}
