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
namespace English2.Views.chuongTrinhHoc
{
    public partial class cth_DanhSachChuongTrinhHoc : Form
    {
        public cth_DanhSachChuongTrinhHoc()
        {
            InitializeComponent();
            loadData();
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDanhSachChTrinh.Columns.Add(nutXoa);
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        int maCT;
        #region methods
        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            this.dgDanhSachChTrinh.DataSource = db.tt_chuongTrinh.Select(c => new
            {
                maCT = c.maCT,
                tenCT = c.tenCT,
                loaiCT = c.loaiCT,
                thoiLuong = c.thoiLuong,
                hocPhi = c.hocPhi,
            }).OrderBy(x=>x.tenCT).ToList();
        }
        void styleData()
        {
            this.dgDanhSachChTrinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachChTrinh.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDanhSachChTrinh.MultiSelect = false;
            this.dgDanhSachChTrinh.RowTemplate.Height = 70;
            this.dgDanhSachChTrinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachChTrinh.Columns[0].HeaderText = "Mã chương trình";
            this.dgDanhSachChTrinh.Columns[0].Width = 80;
            this.dgDanhSachChTrinh.Columns[0].Visible = false;
            this.dgDanhSachChTrinh.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachChTrinh.Columns[1].HeaderText = "Tên chương trình";
            this.dgDanhSachChTrinh.Columns[1].Width = 225;
            this.dgDanhSachChTrinh.Columns[2].HeaderText = "Loại chương trình";
            this.dgDanhSachChTrinh.Columns[2].Width = 140;
            this.dgDanhSachChTrinh.Columns[3].HeaderText = "Số tiết";
            this.dgDanhSachChTrinh.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachChTrinh.Columns[3].Width = 90;
            this.dgDanhSachChTrinh.Columns[4].HeaderText = "Học phí";
            this.dgDanhSachChTrinh.Columns[4].Width = 100;
            this.dgDanhSachChTrinh.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgDanhSachChTrinh.Columns[5].HeaderText = "Xóa";
            this.dgDanhSachChTrinh.Columns[5].Width = 80;
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
        void xoaCTHoc()
        {
            string maCT = dgDanhSachChTrinh.SelectedCells[1].OwningRow.Cells["maCT"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    tt_chuongTrinh del = db.tt_chuongTrinh.Where(p => p.maCT.ToString().Equals(maCT)).SingleOrDefault();
                    db.tt_chuongTrinh.Remove(del);
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
            this.dgDanhSachChTrinh.DataSource = db.tt_chuongTrinh.Where(c => c.tenCT.Contains(txtTimKiem.Text)).Select(c => new
            {
                maCT = c.maCT,
                tenCT = c.tenCT,
                loaiCT = c.loaiCT,
                thoiLuong = c.thoiLuong,
                hocPhi = c.hocPhi,
                ghiChu = c.ghiChu
            }).ToList();
        }

        #endregion
        public void taoChuongTrinh()
        {
            try
            {
                if (txtTenCT.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Thêm thất bại!");
                }
                tt_chuongTrinh ct = new tt_chuongTrinh()
                {
                    tenCT = txtTenCT.Text,
                    loaiCT = txtLoaiCT.Text,
                    thoiLuong = Int32.Parse(txtThoiLuongCT.Text),
                    dvt = "Tiết",
                    hocPhi = Int32.Parse(txtHocPhiCT.Text),
                    ghiChu = txtGhiChu.Text,
                };
                //Chi nhanh

                db.tt_chuongTrinh.Add(ct);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công");
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại!");
            }

        }
        /// <summary>
        /// Cập nhật
        /// </summary>
        void capNhatTT()
        {
            try
            {
                if (txtTenCT.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Cập nhật thất bại!");
                }
                tt_chuongTrinh ct = db.tt_chuongTrinh.Single(t => t.maCT.Equals(maCT));
                ct.tenCT = txtTenCT.Text;
                ct.loaiCT = txtLoaiCT.Text;
                ct.thoiLuong = Int32.Parse(txtThoiLuongCT.Text);
                ct.dvt = "Tiết";
                ct.hocPhi = Int32.Parse(txtHocPhiCT.Text);
                ct.ghiChu = txtGhiChu.Text;
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
            taoChuongTrinh();
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            cth_DSChiTietCT f = new cth_DSChiTietCT();
            f.maCtrinh = Int32.Parse(this.dgDanhSachChTrinh.CurrentRow.Cells[1].Value.ToString());
            f.tenCtrinh = this.dgDanhSachChTrinh.CurrentRow.Cells[2].Value.ToString();
            Addform(f);
        }

        private void dgDanhSachChTrinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maCT = Int32.Parse(this.dgDanhSachChTrinh.CurrentRow.Cells[1].Value.ToString());
            tt_chuongTrinh ct = db.tt_chuongTrinh.Single(t => t.maCT.Equals(maCT));
            txtTenCT.Text = ct.tenCT;
            txtLoaiCT.Text = ct.loaiCT;
            txtThoiLuongCT.Text = ct.thoiLuong.ToString();
            txtHocPhiCT.Text = ct.hocPhi.ToString();
            txtGhiChu.Text = ct.ghiChu;
        }
        private void dgDSMon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 0)
            {
                xoaCTHoc();
            }
        }

        private void dgDSMon_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
    }
}
