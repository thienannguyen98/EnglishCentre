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

namespace English2.Views.monHoc
{
    public partial class mh_DanhSachMonHoc : Form
    {
        public mh_DanhSachMonHoc()
        {
            InitializeComponent();
            loadData();
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDanhSachMonHoc.Columns.Add(nutXoa);
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();
        int maMH;
        #region methods
        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            this.dgDanhSachMonHoc.DataSource = db.tt_monHoc.Select(c => new
            {
                maMon = c.maMon,
                tenMon = c.tenMon,
                ghiChu = c.ghiChu
            }).OrderBy(x => x.tenMon).ToList();
        }
        void styleData()
        {
            this.dgDanhSachMonHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachMonHoc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            this.dgDanhSachMonHoc.MultiSelect = false;
            this.dgDanhSachMonHoc.RowTemplate.Height = 70;
            this.dgDanhSachMonHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachMonHoc.Columns[0].HeaderText = "Mã môn học";
            this.dgDanhSachMonHoc.Columns[0].Width = 125;
            this.dgDanhSachMonHoc.Columns[0].Visible = false;
            this.dgDanhSachMonHoc.Columns[1].HeaderText = "Tên môn học";
            this.dgDanhSachMonHoc.Columns[1].Width = 190;
            this.dgDanhSachMonHoc.Columns[2].HeaderText = "Ghi chú";
            this.dgDanhSachMonHoc.Columns[2].Width = 305;
            this.dgDanhSachMonHoc.Columns[3].HeaderText = "Xóa";
            this.dgDanhSachMonHoc.Columns[3].Width = 80;
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
        /// Method dùng để xóa đôi tượng đang select bằng cách lấy Username 
        /// -> Tìm 
        /// và rồi Remove
        /// 
        /// </summary>
        void xoaMon()
        {
            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                using (ttAnhNguEntities dbb = new ttAnhNguEntities())
                {
                    try
                    {
                        tt_monHoc del = dbb.tt_monHoc.Where(p => p.maMon.Equals(maMH)).SingleOrDefault();
                        dbb.tt_monHoc.Remove(del);
                        dbb.SaveChanges();
                        MessageBox.Show("Xóa thành công");
                        loadData();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không xóa được");
                    }
                }
            }
        }

        #endregion

        #region event
        void capNhatMH()
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    if (txtTenMH.Text.Trim().Length == 0)
                    {
                        throw new ArithmeticException("Cập nhật thất bại!");
                    }
                    tt_monHoc mh = dbb.tt_monHoc.Single(t => t.maMon.Equals(maMH));
                    mh.tenMon = txtTenMH.Text;
                    mh.ghiChu = txtGhiChu.Text;
                    dbb.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                    clear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!");
                }
            }


        }

        /// <summary>
        /// Tìm kiếm tài khoản theo Username hoặc mã TV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.dgDanhSachMonHoc.DataSource = db.tt_monHoc.Where(c => c.maMon.ToString().Contains(txtTimKiem.Text) || c.tenMon.Contains(txtTimKiem.Text)).Select(c => new
            {
                maMon = c.maMon,
                tenMon = c.tenMon,
                ghiChu = c.ghiChu
            }).ToList();
        }
        public void clear()
        {
            txtTenMH.Clear();
            txtGhiChu.Clear();
        }
        public void addMonHoc()
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    if (txtTenMH.Text.Trim().Length == 0)
                    {
                        throw new ArithmeticException("Thêm thất bại!");
                    }
                    tt_monHoc mh = new tt_monHoc()
                    {
                        tenMon = txtTenMH.Text,
                        ghiChu = txtGhiChu.Text
                    };
                    dbb.tt_monHoc.Add(mh);
                    dbb.SaveChanges();
                    MessageBox.Show("Thêm môn học thành công");
                    clear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Không thành công");
                }
            }

        }


        #endregion

        private void btnThem_Click(object sender, EventArgs e)
        {
            addMonHoc();
            loadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            capNhatMH();
            loadData();
        }

        private void dgDanhSachMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maMH = Int32.Parse(this.dgDanhSachMonHoc.CurrentRow.Cells[1].Value.ToString());
            tt_monHoc mh = db.tt_monHoc.Single(t => t.maMon.Equals(maMH));
            txtTenMH.Text = mh.tenMon;
            txtGhiChu.Text = mh.ghiChu;
        }
        private void dgDSMon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 0)
            {
                xoaMon();
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
