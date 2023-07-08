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
using MaterialSkin;

namespace English2.Views.taiKhoan
{
    public partial class tk_DanhSachTaiKhoan : MaterialSkin.Controls.MaterialForm
    {

        ttAnhNguEntities db = new ttAnhNguEntities();
        public tk_DanhSachTaiKhoan()
        {
            InitializeComponent();
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
            loadData();
            DataGridViewButtonColumn nutSua = new DataGridViewButtonColumn();
            this.dgDanhSachTK.Columns.Add(nutSua);

            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDanhSachTK.Columns.Add(nutXoa);
            styleData();
        }
        #region methods
        /// <summary>
        /// Upload data from DB
        /// </summary>
        void loadData()
        {
            
            this.dgDanhSachTK.DataSource = db.tt_taiKhoan.Select(c => new
            {
                Username = c.tkThanhVien,
                Ho = c.tt_thanhVien.hoTV,
                Ten = c.tt_thanhVien.tenTV,
                DuocSuDung = c.duocSD,
                CapDo = c.capDo,
                QuyenHan = c.quyenHan
            }).OrderBy(x => x.Username).ToList();
        }
        void styleData()
        {
            this.dgDanhSachTK.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDanhSachTK.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDanhSachTK.MultiSelect = false;
            this.dgDanhSachTK.RowTemplate.Height = 70;
            this.dgDanhSachTK.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachTK.Columns[0].HeaderText = "Username";
            this.dgDanhSachTK.Columns[0].Width = 170;
            this.dgDanhSachTK.Columns[1].HeaderText = "Họ";
            this.dgDanhSachTK.Columns[1].Width = 190;

            this.dgDanhSachTK.Columns[2].HeaderText = "Tên";
            this.dgDanhSachTK.Columns[2].Width = 100;
            this.dgDanhSachTK.Columns[3].HeaderText = "Được sử dụng";
            this.dgDanhSachTK.Columns[3].Width = 100;
            this.dgDanhSachTK.Columns[4].HeaderText = "Cấp độ";
            this.dgDanhSachTK.Columns[4].Width = 100;
            this.dgDanhSachTK.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDanhSachTK.Columns[5].HeaderText = "Quyền hạn";
            this.dgDanhSachTK.Columns[5].Width = 160;

            this.dgDanhSachTK.Columns[6].HeaderText = "Chỉnh sửa";
            this.dgDanhSachTK.Columns[7].HeaderText = "Xóa";
            this.dgDanhSachTK.Columns[7].Width = 80;
            this.dgDanhSachTK.Columns[6].Width = 80;
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
        void xoaTaiKhoan()
        {
            string id = dgDanhSachTK.CurrentRow.Cells[2].Value.ToString();

            DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    tt_taiKhoan del = db.tt_taiKhoan.Where(p => p.tkThanhVien.ToString().Equals(id)).SingleOrDefault();
                    db.tt_taiKhoan.Remove(del);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Không xóa được");
                    
                }
            }
            loadData();
        }
        void capNhatTT()
        {
            tk_CapNhatTaiKhoan f = new tk_CapNhatTaiKhoan();

            string userName = this.dgDanhSachTK.CurrentRow.Cells[2].Value.ToString();
            tt_taiKhoan tk = db.tt_taiKhoan.Single(t => t.tkThanhVien.Equals(userName));
            //Gán mã TV được selected vào biến tam để form CapNhatTV sử dụng
            f.txtMaTV.Text = tk.tt_thanhVien.hoTV + " " + tk.tt_thanhVien.tenTV;
            f.txtUsername.Text = userName;
            f.txtCapDo.Text = tk.capDo.ToString();
            f.checkBDuocSD.Checked = tk.duocSD;
            f.txtGhiChu.Text = tk.ghiChu;

            
            //string permission = tv.quyenHan;
            String[] arrPermission = tk.quyenHan.Split('#');
            for (int i = 0; i < arrPermission.Length; i++)
            {
                switch (arrPermission[i])
                {
                    case "1":
                        f.checkTaiKhoan_1.Checked = true;
                        continue;
                    case "2":
                        f.checkThanhVien_2.Checked = true;
                        continue;
                    case "3":
                        f.checkPhong_3.Checked = true;
                        continue;
                    case "4":
                        f.checkCaHoc_4.Checked = true;
                        continue;
                    case "5":
                        f.checkKhoa_5.Checked = true;
                        continue;
                    case "6":
                        f.checkCTHoc_6.Checked = true;
                        continue;
                    case "7":
                        f.checkMon_7.Checked = true;
                        continue;
                    case "8":
                        f.checkLop_8.Checked = true;
                        continue;
                    case "9":
                        f.checkGV_9.Checked = true;
                        continue;
                    case "10":
                        f.checkHS_10.Checked = true;
                        continue;
                    case "11":
                        f.checkThu_11.Checked = true;
                        continue;

                    default:
                        break;
                }
            }







            Addform(f);
        }
        #endregion

        #region event

        /// <summary>
        /// Tìm kiếm tài khoản theo Username hoặc mã TV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.dgDanhSachTK.DataSource = db.tt_taiKhoan.Where(c => c.tkThanhVien.Contains(txtTimKiem.Text) || c.maTV.ToString().Contains(txtTimKiem.Text) || c.tt_thanhVien.tenTV.ToString().Contains(txtTimKiem.Text)).Select(c => new
            {
                Username = c.tkThanhVien,
                Ho = c.tt_thanhVien.hoTV,
                Ten = c.tt_thanhVien.tenTV,
                DuocSuDung = c.duocSD,
                CapDo = c.capDo,
                QuyenHan = c.quyenHan
            }).ToList();
        }
        private void btnTaoTK_Click(object sender, EventArgs e)
        {
            tk_TaoTaiKhoan f = new tk_TaoTaiKhoan();
            Addform(f);
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
                xoaTaiKhoan();
            }
        }
        #endregion
    }
}
