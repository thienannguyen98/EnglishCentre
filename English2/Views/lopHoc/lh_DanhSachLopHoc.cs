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
    public partial class lh_DanhSachLopHoc : Form
    {
        public lh_DanhSachLopHoc()
        {
            InitializeComponent();
            cbChiNhanh.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
            cbKhoaHoc.DataSource = db.tt_khoaHoc.Select(c => c.tenHK).ToList();
            loadData();
            DataGridViewButtonColumn nutSua = new DataGridViewButtonColumn();
            this.dgDSLopHoc.Columns.Add(nutSua);

            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDSLopHoc.Columns.Add(nutXoa);
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        #region methods
        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            this.dgDSLopHoc.DataSource = db.tt_lopHoc.Where(c => c.tt_chiNhanh.tenCN.Equals(cbChiNhanh.Text) && c.tt_khoaHoc.tenHK.Equals(cbKhoaHoc.Text)).Select(c => new
            {
                maLop = c.maLop,
                tenLop = c.tenLop,
                chuongTrinh = c.tt_chuongTrinh.tenCT,
                batDau = c.ngayKG,
                ketThuc = c.ngayKT,
                thoiLuong = c.tt_chuongTrinh.thoiLuong,
                hocPhi = c.hocPhi

            }).OrderBy(x=> x.tenLop).ToList();
            
        }
        void styleData()
        {
            this.dgDSLopHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDSLopHoc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDSLopHoc.MultiSelect = false;
            this.dgDSLopHoc.RowTemplate.Height = 70;
            this.dgDSLopHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSLopHoc.Columns[0].Visible = false;
            this.dgDSLopHoc.Columns[1].HeaderText = "Tên lớp";
            this.dgDSLopHoc.Columns[1].Width = 140;

            this.dgDSLopHoc.Columns[2].HeaderText = "Chương trình";
            this.dgDSLopHoc.Columns[2].Width = 237;

            this.dgDSLopHoc.Columns[3].HeaderText = "Bắt đầu";
            this.dgDSLopHoc.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSLopHoc.Columns[3].Width = 110;
            this.dgDSLopHoc.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";

            this.dgDSLopHoc.Columns[4].HeaderText = "Kết thúc";
            this.dgDSLopHoc.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSLopHoc.Columns[4].Width = 110;
            this.dgDSLopHoc.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";

            this.dgDSLopHoc.Columns[5].HeaderText = "Thời lượng";
            this.dgDSLopHoc.Columns[5].Width = 100;
            this.dgDSLopHoc.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgDSLopHoc.Columns[6].HeaderText = "Học phí";
            this.dgDSLopHoc.Columns[6].Width = 120;
            this.dgDSLopHoc.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dgDSLopHoc.Columns[7].HeaderText = "Chỉnh sửa";
            this.dgDSLopHoc.Columns[8].HeaderText = "Xóa";
            this.dgDSLopHoc.Columns[7].Width = 80;
            this.dgDSLopHoc.Columns[8].Width = 80;
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
            string maLop = dgDSLopHoc.SelectedCells[0].OwningRow.Cells["maLop"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    tt_lopHoc del = db.tt_lopHoc.Where(p => p.maLop.ToString().Equals(maLop)).SingleOrDefault();
                    db.tt_lopHoc.Remove(del);
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
        private void btnXoa_Click(object sender, EventArgs e)
        {
            xoaCTHoc();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            
        }
        void capNhatTT()
        {
            lh_CapNhatLop f = new lh_CapNhatLop();
            f.maLop = Int32.Parse(this.dgDSLopHoc.CurrentRow.Cells[2].Value.ToString());
            tt_lopHoc lt = db.tt_lopHoc.Single(t => t.maLop.Equals(f.maLop));

            f.txtTenLop.Text = lt.tenLop;
            f.txtHocPhi.Text = lt.hocPhi.ToString();
            f.txtThoiLuong.Text = lt.thoiLuong.ToString();
            f.txtGhiChu.Text = lt.ghiChu;
            f.dateKG.Text = lt.ngayKG.ToString();
            f.dateKT.Text = lt.ngayKT.ToString();

            f.cbChiNhanh.Text = lt.tt_chiNhanh.tenCN.ToString();
            f.cbChuongTrinh.Text = lt.tt_chuongTrinh.tenCT.ToString();
            f.cbKhoaHoc.Text = lt.tt_khoaHoc.tenHK.ToString();

            Addform(f);
        }
        /// <summary>
        /// Tìm kiếm tài khoản theo Username hoặc mã TV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.dgDSLopHoc.DataSource = db.tt_lopHoc.Where(c => c.tt_chiNhanh.tenCN.Equals(cbChiNhanh.Text) && c.tt_khoaHoc.tenHK.Equals(cbKhoaHoc.Text) && (c.tenLop.Contains(txtTimKiem.Text) || c.tt_chuongTrinh.tenCT.Contains(txtTimKiem.Text))).Select(c => new
            {
                maLop = c.maLop,
                tenLop = c.tenLop,
                chuongTrinh = c.tt_chuongTrinh.tenCT,
                batDau = c.ngayKG,
                ketThuc = c.ngayKT,
                thoiLuong = c.thoiLuong,
                hocPhi = c.hocPhi
            }).ToList();
        }

        #endregion

        private void btnTao_Click(object sender, EventArgs e)
        {
            //cth_ThemChuongTrinhHoc f = new cth_ThemChuongTrinhHoc();
            //Addform(f);
        }
        private void btnXem_Click_1(object sender, EventArgs e)
        {
            lh_CTLopHoc f = new lh_CTLopHoc();
            f.maLop = Int32.Parse(this.dgDSLopHoc.CurrentRow.Cells[2].Value.ToString());
            f.tenLop = (this.dgDSLopHoc.CurrentRow.Cells[3].Value.ToString());
            Addform(f);
        }
        private void dgDSLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgDSLopHoc_DoubleClick(object sender, EventArgs e)
        {
            lh_CTLopHoc f = new lh_CTLopHoc();
            f.maLop = Int32.Parse(this.dgDSLopHoc.CurrentRow.Cells[0].Value.ToString());
            f.tenLop = (this.dgDSLopHoc.CurrentRow.Cells[1].Value.ToString());
            Addform(f);
        }

        private void cbKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }
        private void dgDanhSachTK_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Sửa
            Image refresh2 = Properties.Resources.refresh2;
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.refresh2.Width;
                var h = Properties.Resources.refresh2.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(refresh2, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
            //Xóa
            Image xoa = Properties.Resources.trash;
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 1)
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
                capNhatTT();
            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 1)
            {
                xoaCTHoc();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lh_ThemLop f = new lh_ThemLop();
            Addform(f);
        }
    }
}
