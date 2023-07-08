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
using English2.Views.thanhVien;

namespace English2.Views.khoaHoc
{
    public partial class kh_DanhSachKhoaHoc : Form
    {
        public kh_DanhSachKhoaHoc()
        {
            InitializeComponent();
            loadData();
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDanhSachKH.Columns.Add(nutXoa);
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        int maKH;
        #region methods
        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            this.dgDanhSachKH.DataSource = db.tt_khoaHoc.Select(c => new
            {
                maKhoa = c.maKH,
                tenkh =c.tenHK,
                batDau = c.batDau,
                ketThuc = c.ketThuc,
            }).OrderBy(x => x.tenkh).ToList();
        }
        void styleData()
        {
            this.dgDanhSachKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachKH.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDanhSachKH.MultiSelect = false;
            this.dgDanhSachKH.RowTemplate.Height = 70;
            this.dgDanhSachKH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachKH.Columns[0].HeaderText = "Mã khóa học";
            this.dgDanhSachKH.Columns[0].Width = 135;
            this.dgDanhSachKH.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachKH.Columns[0].Visible = false;
            this.dgDanhSachKH.Columns[1].HeaderText = "Tên khóa học";
            this.dgDanhSachKH.Columns[1].Width = 235;

            this.dgDanhSachKH.Columns[2].HeaderText = "Ngày bắt đầu";
            this.dgDanhSachKH.Columns[2].Width = 135;
            this.dgDanhSachKH.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgDanhSachKH.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachKH.Columns[3].HeaderText = "Ngày kết thúc";
            this.dgDanhSachKH.Columns[3].Width = 135;
            this.dgDanhSachKH.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgDanhSachKH.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachKH.Columns[4].HeaderText = "Xóa";
            this.dgDanhSachKH.Columns[4].Width = 80;
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
        void xoaKhoaHoc()
        {
            string maKhoa = dgDanhSachKH.SelectedCells[0].OwningRow.Cells["maKhoa"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    tt_khoaHoc del = db.tt_khoaHoc.Where(p => p.maKH.ToString().Equals(maKhoa)).SingleOrDefault();
                    db.tt_khoaHoc.Remove(del);
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
            this.dgDanhSachKH.DataSource = db.tt_khoaHoc.Where(c => c.tenHK.Contains(txtTimKiem.Text) || c.maKH.ToString().Contains(txtTimKiem.Text) || c.batDau.ToString().Contains(txtTimKiem.Text)).Select(c => new
            {
                maKhoa = c.maKH,
                tenkh = c.tenHK,
                batDau = c.batDau,
                ketThuc = c.ketThuc,
            }).ToList();
        }

        #endregion
        public void taoKhoaHoc()
        {
            try
            {
                if (txtTenKhoa.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Thêm thất bại!");
                }
                tt_khoaHoc kh = new tt_khoaHoc()
                {
                    tenHK = txtTenKhoa.Text,
                    batDau = DateTime.Parse(dateBD.Text),
                    ketThuc = DateTime.Parse(dateKT.Text),
                    ghiChu = txtGhiChu.Text
                };
                //Chi nhanh

                db.tt_khoaHoc.Add(kh);
                db.SaveChanges();

                MessageBox.Show("Thêm thành công");
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại!");
            }

        }
        void capNhatTT()
        {
            try
            {
                if (txtTenKhoa.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Cập nhật thất bại!");
                }
                tt_khoaHoc kh = db.tt_khoaHoc.Single(t => t.maKH.Equals(maKH));
                kh.tenHK = txtTenKhoa.Text;
                kh.batDau = DateTime.Parse(dateBD.Text);
                kh.ketThuc = DateTime.Parse(dateKT.Text);
                kh.ghiChu = txtGhiChu.Text;
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
            taoKhoaHoc();
        }

        private void cbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgDanhSachKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maKH = Int32.Parse(this.dgDanhSachKH.CurrentRow.Cells[1].Value.ToString());
            tt_khoaHoc kh = db.tt_khoaHoc.Single(t => t.maKH.Equals(maKH));
            txtTenKhoa.Text = kh.tenHK;
            dateBD.Text = kh.batDau.ToString();
            dateKT.Text = kh.ketThuc.ToString();
            txtGhiChu.Text = kh.ghiChu;
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
                xoaKhoaHoc();
            }
        }
    }
}
