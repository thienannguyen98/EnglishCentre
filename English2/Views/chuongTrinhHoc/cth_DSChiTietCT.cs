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
    public partial class cth_DSChiTietCT : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public int maCtrinh;
        public string tenCtrinh;
        #region methods
        public cth_DSChiTietCT()
        {
            InitializeComponent();
            //loadData();
            this.cbMonHoc.DataSource = db.tt_monHoc.Select(c => c.tenMon).ToList();
            this.dgDSMon.RowTemplate.Height = 70;
            loadData();
            //lbTitle.Text = tenCtrinh;
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDSMon.Columns.Add(nutXoa);
            styleData();
        }

        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            this.dgDSMon.DataSource = db.tt_ctChuongTrinh.Where(c => c.maCT.Equals(maCtrinh)).Select(c => new
            {
                maMon = c.maMon,
                tenMon = c.tt_monHoc.tenMon,
                thoiLuong = c.thoiLuong,
                dvt = c.dvt,
            }).OrderBy(x=> x.tenMon).ToList(); 
        }
        void styleData()
        {
            this.dgDSMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDSMon.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDSMon.MultiSelect = false;
            this.dgDSMon.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSMon.Columns[0].HeaderText = "Mã môn";
            this.dgDSMon.Columns[0].Visible = false;
            this.dgDSMon.Columns[1].HeaderText = "Tên môn học";
            this.dgDSMon.Columns[1].Width = 190;

            this.dgDSMon.Columns[2].HeaderText = "Thời lượng";
            this.dgDSMon.Columns[2].Width = 130;
            this.dgDSMon.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dgDSMon.Columns[3].HeaderText = "Đơn vị";
            this.dgDSMon.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSMon.Columns[3].Width = 130;
            this.dgDSMon.Columns[4].HeaderText = "Xóa";
            this.dgDSMon.Columns[4].Width = 80;
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
        //Binding

        /// <summary>
        /// Method dùng để xóa đôi tượng đang select bằng cách lấy maP 
        /// -> Tìm 
        /// và rồi Remove
        /// 
        /// </summary>
        void xoaMon()
        {
            string maMon = dgDSMon.SelectedCells[1].OwningRow.Cells["maMon"].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    tt_ctChuongTrinh del = db.tt_ctChuongTrinh.Where(p => p.maMon.ToString().Equals(maMon) && p.maCT == maCtrinh).SingleOrDefault();
                    db.tt_ctChuongTrinh.Remove(del);
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
            xoaMon();
        }


        /// <summary>
        /// Tìm kiếm tài khoản theo Username hoặc mã TV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.dgDSMon.DataSource = db.tt_ctChuongTrinh.Where(c => c.tt_monHoc.tenMon.Contains(txtTimKiem.Text) && c.maCT == maCtrinh).Select(c => new
            {
                maMon = c.maMon,
                tenMon = c.tt_monHoc.tenMon,
                thoiLuong = c.thoiLuong,
                dvt = c.dvt,
            }).ToList();
        }

        #endregion
        public void addMonHoc()
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    tt_monHoc tn = dbb.tt_monHoc.Single(t => t.tenMon == (cbMonHoc.SelectedValue.ToString()));
                    // có nhìu môn giống nhau vì Mã CT
                    tt_ctChuongTrinh mh = new tt_ctChuongTrinh()
                    {
                        maCT = maCtrinh,
                        maMon = tn.maMon,
                        thoiLuong = Int32.Parse(txtThoiLuongMon.Text),
                        dvt = "Tiết",
                        ghiChu = txtGhiChu.Text
                    };
                    mh.maCT = maCtrinh;
                    dbb.tt_ctChuongTrinh.Add(mh);
                    dbb.SaveChanges();
                    MessageBox.Show("Thêm môn học thành công");

                }
                catch (Exception)
                {
                    MessageBox.Show("Môn học đã có trong chương trình này!");
                }
            }
               
        }
        private void btnTao_Click(object sender, EventArgs e)
        {
            addMonHoc();
            loadData();
        }

        private void cth_DSChiTietCT_Load(object sender, EventArgs e)
        {
            loadData();
            lbTitle.Text = tenCtrinh;
            
        }
        void capNhatTT()
        {
            using (ttAnhNguEntities dbb = new ttAnhNguEntities())
            {
                try
                {
                    tt_monHoc tn = dbb.tt_monHoc.Single(t => t.tenMon == (cbMonHoc.SelectedValue.ToString()));
                    tt_ctChuongTrinh ct = dbb.tt_ctChuongTrinh.Single(t => t.maCT.Equals(maCtrinh) && t.maMon == tn.maMon);
                    ct.maMon = tn.maMon;
                    ct.thoiLuong = Int32.Parse(txtThoiLuongMon.Text);
                    ct.dvt = "Tiết";
                    ct.ghiChu = txtGhiChu.Text;
                    dbb.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                catch (Exception)
                {
                    //MessageBox.Show("Cập nhật thông tin thất bại!");
                    MessageBox.Show("Môn học không có trong chương trình!");
                }
            }

                
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            capNhatTT();
            loadData();
        }

        private void dgDSMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int maMH = Int32.Parse(this.dgDSMon.CurrentRow.Cells[1].Value.ToString());
            tt_ctChuongTrinh mh = db.tt_ctChuongTrinh.Single(t => t.maCT.Equals(maCtrinh) && t.maMon == maMH);
            //Gán mã TV được selected vào biến tam để form CapNhatTV sử dụng
            cbMonHoc.Text = mh.tt_monHoc.tenMon;
            txtThoiLuongMon.Text = mh.thoiLuong.ToString();
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
        private void picBack_Click(object sender, EventArgs e)
        {
            cth_DanhSachChuongTrinhHoc f = new cth_DanhSachChuongTrinhHoc();
            Addform(f);
        }
    }
}
