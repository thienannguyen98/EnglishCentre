using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using English2.Views;
using English2.Models;
using English2.Views.thanhVien;

namespace English2.Views.Phong
{
    public partial class p_DanhSachPhong : Form
    {
        public p_DanhSachPhong()
        {
            InitializeComponent();
            loadData();
            this.cbChiNhanh.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
            this.cbCN.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDanhSachPhong.Columns.Add(nutXoa);
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        public int maP;
        #region methods
        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            this.dgDanhSachPhong.DataSource = db.tt_phongHoc.Where(c => c.tt_chiNhanh.tenCN.ToString().Equals(cbChiNhanh.Text)).Select(c => new
            {
                maPhong = c.maPhong,
                tenPhong = c.tenPhong,
                hoatDong = c.hoatDong == true ? "Hoạt động" : "Tạm ngưng",
                sucChua = c.sucChua,
                coProjector = c.coProjector == true ? "Có" : "Không có",
                //coProjector = t.TrueValue.ToString(),
                coTV = c.coTiVi == true ? "Có" : "Không có"
            }).OrderBy(x => x.tenPhong).ToList();

        }
        void styleData()
        {
            this.dgDanhSachPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachPhong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDanhSachPhong.MultiSelect = false;
            this.dgDanhSachPhong.RowTemplate.Height = 70;
            this.dgDanhSachPhong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[0].HeaderText = "Mã phòng";
            this.dgDanhSachPhong.Columns[0].Width = 110;
            this.dgDanhSachPhong.Columns[0].Visible = false;
            this.dgDanhSachPhong.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[1].HeaderText = "Tên phòng";
            this.dgDanhSachPhong.Columns[1].Width = 110;

            this.dgDanhSachPhong.Columns[2].HeaderText = "Trạng thái";
            this.dgDanhSachPhong.Columns[2].Width = 100;
            this.dgDanhSachPhong.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[3].HeaderText = "Sức chứa";
            this.dgDanhSachPhong.Columns[3].Width = 100;
            this.dgDanhSachPhong.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[4].HeaderText = "Projector";
            //this.dgDanhSachPhong.
            this.dgDanhSachPhong.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[4].Width = 100;
            this.dgDanhSachPhong.Columns[5].HeaderText = "Tivi";
            this.dgDanhSachPhong.Columns[5].Width = 100;
            this.dgDanhSachPhong.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachPhong.Columns[6].HeaderText = "Xóa";
            this.dgDanhSachPhong.Columns[6].Width = 80;
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
        void xoaPhong()
        {
            string maP = dgDanhSachPhong.SelectedCells[1].OwningRow.Cells["maPhong"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    tt_phongHoc del = db.tt_phongHoc.Where(p => p.maPhong.ToString().Equals(maP)).SingleOrDefault();
                    db.tt_phongHoc.Remove(del);
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
        /// <summary>
        /// Them Phòng
        /// </summary>
        public void taoPhong()
        {
            try
            {
                if (txtTenPhong.Text.Trim().Length == 0)
                {
                    throw new ArithmeticException("Thêm thất bại!");
                }
                tt_phongHoc ph = new tt_phongHoc()
                {
                    tenPhong = txtTenPhong.Text,
                    hoatDong = checkBTrangThai.Checked,
                    sucChua = Int32.Parse(txtSucChua.Text),
                    coProjector = checkBProjector.Checked,
                    coTiVi = checkBTivi.Checked,
                    ghiChu = txtGhiChu.Text
                };
                //Chi nhanh
                tt_chiNhanh tn = db.tt_chiNhanh.Single(t => t.tenCN == (cbCN.SelectedValue.ToString()));
                ph.maCN = tn.maCN;
                db.tt_phongHoc.Add(ph);
                db.SaveChanges();
                MessageBox.Show("Thêm phòng thành công");
                loadData();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }
        /// <summary>
        /// Chỉnh sửa Phòng
        /// </summary>
        void capNhatTT()
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    if (txtTenPhong.Text.Trim().Length == 0)
                    {
                        throw new ArithmeticException("Cập nhật thất bại!");
                    }
                    tt_phongHoc ph = dbb.tt_phongHoc.Single(t => t.maPhong.Equals(maP));
                    ph.tenPhong = txtTenPhong.Text.Trim();
                    ph.sucChua = Int32.Parse(txtSucChua.Text.Trim());
                    ph.hoatDong = checkBTrangThai.Checked;
                    ph.coProjector = checkBProjector.Checked;
                    ph.coTiVi = checkBTivi.Checked;
                    ph.ghiChu = txtGhiChu.Text.Trim();
                    dbb.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!");
                }
            }
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
            this.dgDanhSachPhong.DataSource = db.tt_phongHoc.Where(c => c.tenPhong.Contains(txtTimKiem.Text) && c.tt_chiNhanh.tenCN.ToString().Equals(cbChiNhanh.Text)).Select(c => new
            {
                maPhong = c.maPhong,
                tenPhong = c.tenPhong,
                hoatDong = c.hoatDong == true ? "Hoạt động" : "Tạm ngưng",
                sucChua = c.sucChua,
                coProjector = c.coProjector == true ? "Có" : "Không có",
                //coProjector = t.TrueValue.ToString(),
                coTV = c.coTiVi == true ? "Có" : "Không có"
            }).ToList();
        }
        
        private void btnTao_Click(object sender, EventArgs e)
        {
            taoPhong();
        }

        private void cbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgDanhSachPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maP = Int32.Parse(this.dgDanhSachPhong.CurrentRow.Cells[1].Value.ToString());
            tt_phongHoc ph = db.tt_phongHoc.Single(t => t.maPhong.Equals(maP));
            //Gán mã TV được selected vào biến tam để form CapNhatTV sử dụng
            cbCN.Text = ph.tt_chiNhanh.tenCN;
            txtSucChua.Text = ph.sucChua.ToString();
            txtTenPhong.Text = ph.tenPhong;
            txtGhiChu.Text = ph.ghiChu;
            checkBTrangThai.Checked = (bool)ph.hoatDong;
            checkBProjector.Checked = (bool)ph.coProjector;
            checkBTivi.Checked = (bool)ph.coTiVi;
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
                xoaPhong();
        }
        #endregion


    }
}
