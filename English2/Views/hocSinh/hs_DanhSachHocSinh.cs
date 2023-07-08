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
using English2.Views.ghiDanh;
namespace English2.Views.hocSinh
{
    public partial class hs_DanhSachHocSinh : Form
    {
        ttAnhNguEntities db = new ttAnhNguEntities();
        public hs_DanhSachHocSinh()
        {
            InitializeComponent();
            loadData();
            DataGridViewButtonColumn nutThu = new DataGridViewButtonColumn();
            this.dgDSHocSinh.Columns.Add(nutThu);

            DataGridViewButtonColumn nutSua = new DataGridViewButtonColumn();
            this.dgDSHocSinh.Columns.Add(nutSua);

            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgDSHocSinh.Columns.Add(nutXoa);
            styleData();
        }
        #region Methods
        /// <summary>
        /// Method dùng để thêm form mới vào panelMain của formMainAdmin
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
        /// Method dùng để upload Data từ DB vào Datagridview
        /// </summary>
        void loadData()
        {
            this.dgDSHocSinh.DataSource = db.tt_hocSinh.Select(c => new
            {
                maHS = c.maHS,
                Ho = c.hoHS,
                Ten = c.tenHS,
                gender = c.gioiTinh == 1 ? "Nam" : "Nữ",
                NgaySinh = c.ngaySinh,
                noiSinh = c.noiSinh,
                SDT = c.soDT,
                Mail = c.eMail,
            }).OrderBy(x=>x.Ten).ToList();
        }
        void styleData()
        {
            this.dgDSHocSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgDSHocSinh.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgDSHocSinh.MultiSelect = false;
            this.dgDSHocSinh.RowTemplate.Height = 70;
            this.dgDSHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSHocSinh.Columns[0].HeaderText = "Mã";
            this.dgDSHocSinh.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSHocSinh.Columns[0].Width = 70;
            this.dgDSHocSinh.Columns[0].Visible = false;
            this.dgDSHocSinh.Columns[1].HeaderText = "Họ";
            this.dgDSHocSinh.Columns[1].Width = 150;
            this.dgDSHocSinh.Columns[2].HeaderText = "Tên";
            this.dgDSHocSinh.Columns[2].Width = 90;
            this.dgDSHocSinh.Columns[3].HeaderText = "Giới tính";
            this.dgDSHocSinh.Columns[3].Width = 80;
            this.dgDSHocSinh.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSHocSinh.Columns[4].HeaderText = "Ngày sinh";
            this.dgDSHocSinh.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgDSHocSinh.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgDSHocSinh.Columns[4].Width = 100;
            //noi sinh
            this.dgDSHocSinh.Columns[5].HeaderText = "Nơi sinh";
            
            this.dgDSHocSinh.Columns[5].Width = 90;
            //sdt
            this.dgDSHocSinh.Columns[6].HeaderText = "Số điện thoại";
            this.dgDSHocSinh.Columns[6].Width = 110;
            //email
            this.dgDSHocSinh.Columns[7].HeaderText = "Email";
            this.dgDSHocSinh.Columns[7].Width = 150;

            this.dgDSHocSinh.Columns[8].HeaderText = "Thu tiền";
            this.dgDSHocSinh.Columns[8].Width = 80;

            this.dgDSHocSinh.Columns[9].HeaderText = "Chỉnh sửa";
            this.dgDSHocSinh.Columns[9].Width = 80;
            this.dgDSHocSinh.Columns[10].Width = 80;
            this.dgDSHocSinh.Columns[10].HeaderText = "Xóa";

        }

        /// <summary>
        /// Method dùng để xóa thành viên theo đối tượng đã select
        /// Lấy Mã TV -> Tìm
        /// Xóa
        /// </summary>
        void xoaHocSinh()
        {
            using (ttAnhNguEntities dbbb = new ttAnhNguEntities())
            {
                string id = dgDSHocSinh.SelectedCells[0].OwningRow.Cells["maHS"].Value.ToString();
                DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        tt_hocSinh del = dbbb.tt_hocSinh.FirstOrDefault(p => p.maHS == id);
                        dbbb.tt_hocSinh.Remove(del);
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
        #endregion

        #region Event

        /// <summary>
        /// Tìm thành viên theo Tên TV hoặc số đt của TV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTim_Click(object sender, EventArgs e)
        {
            int i = cbTranThaiHP.SelectedIndex;
            switch (i)
            {
                case 0:
                    this.dgDSHocSinh.DataSource = db.tt_hocSinh.Where(c => c.tenHS.Contains(txtTimTV.Text) || c.soDT.Contains(txtTimTV.Text)).Select(c => new
                    {
                        maHS = c.maHS,
                        Ho = c.hoHS,
                        Ten = c.tenHS,
                        gender = c.gioiTinh == 1 ? "Nam" : "Nữ",
                        NgaySinh = c.ngaySinh,
                        noiSinh = c.noiSinh,
                        SDT = c.soDT,
                        Mail = c.eMail,
                    }).ToList();
                    break;
                case 1:
                    this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => c.trangThai == true && (c.tt_hocSinh.tenHS.Contains(txtTimTV.Text) || c.tt_hocSinh.soDT.Contains(txtTimTV.Text))).Select(c => new
                    {
                        maHS = c.maHS,
                        Ho = c.tt_hocSinh.hoHS,
                        Ten = c.tt_hocSinh.tenHS,
                        gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                        NgaySinh = c.tt_hocSinh.ngaySinh,
                        noiSinh = c.tt_hocSinh.noiSinh,
                        SDT = c.tt_hocSinh.soDT,
                        Mail = c.tt_hocSinh.eMail,
                    }).Distinct().ToList();
                    break;
                case 2:
                    this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => c.trangThai == false && (c.tt_hocSinh.tenHS.Contains(txtTimTV.Text) || c.tt_hocSinh.soDT.Contains(txtTimTV.Text))).Select(c => new
                    {
                        maHS = c.maHS,
                        Ho = c.tt_hocSinh.hoHS,
                        Ten = c.tt_hocSinh.tenHS,
                        gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                        NgaySinh = c.tt_hocSinh.ngaySinh,
                        noiSinh = c.tt_hocSinh.noiSinh,
                        SDT = c.tt_hocSinh.soDT,
                        Mail = c.tt_hocSinh.eMail,
                    }).Distinct().ToList();
                    break;
                default:
                    break;
            }
        }
        void capNhatTT()
        {
            hs_CapNhatHocSinh f = new hs_CapNhatHocSinh();
            Addform(f);
            f.maHSinh = this.dgDSHocSinh.CurrentRow.Cells[3].Value.ToString();
            tt_hocSinh hs = db.tt_hocSinh.Single(t => t.maHS.Equals(f.maHSinh));
            f.txtHo.Text = hs.hoHS;
            f.txtTen.Text = hs.tenHS;
            f.rBNam.Checked = hs.gioiTinh == 1;
            f.rBNu.Checked = hs.gioiTinh == 0;
            f.dateNgaySinh.Text = hs.ngaySinh.ToString();
            f.txtNoiSinh.Text = hs.noiSinh;
            f.txtSDT.Text = hs.soDT;
            f.txtMail.Text = hs.eMail;
            f.txtDiaChiLL.Text = hs.dcLienLac;
            f.txtDiaChiTT.Text = hs.dcThuongTru;
            if (hs.hinhHS.Length > 0)
                f.pBHinhNV.Image = new Bitmap(hs.hinhHS);
        }
        #endregion

        private void dgDanhSachTK_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Thu
            Image thu = Properties.Resources.money;
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.money.Width;
                var h = Properties.Resources.money.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(thu, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
            //Sửa
            Image refresh2 = Properties.Resources.refresh2;
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 1)
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
            if (e.ColumnIndex == 2)
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
                gd_ThuHP f = new gd_ThuHP();
                f.maHSThuHP = this.dgDSHocSinh.CurrentRow.Cells[3].Value.ToString();
                Addform(f);
                
            }
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 1)
            {
                capNhatTT();
            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 2)
            {
                xoaHocSinh();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            hs_ThemHocSinh f = new hs_ThemHocSinh();
            Addform(f);
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string maHSDS = this.dgDSHocSinh.CurrentRow.Cells[3].Value.ToString();
            var checkExist = db.tt_dsLop.Where(c => c.maHS.Equals(maHSDS) && c.trangThai == false);
            var count = checkExist.Count();
            if (count > 0)
            {
                gd_ThuHP f = new gd_ThuHP();
                
                f.maHSThuHP = this.dgDSHocSinh.CurrentRow.Cells[3].Value.ToString();
                Addform(f);
            }
            else
            {
                gd_DangKy f = new gd_DangKy();
                f.maHSDangKy = this.dgDSHocSinh.CurrentRow.Cells[3].Value.ToString();
                Addform(f);
            }
        }

        private void cbTranThaiHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbTranThaiHP.SelectedIndex;
            switch (i)
            {
                case 0:
                    loadData();
                    break;
                case 1:
                    this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => c.trangThai == true).Select(c => new
                    {
                        maHS = c.maHS,
                        Ho = c.tt_hocSinh.hoHS,
                        Ten = c.tt_hocSinh.tenHS,
                        gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                        NgaySinh = c.tt_hocSinh.ngaySinh,
                        noiSinh = c.tt_hocSinh.noiSinh,
                        SDT = c.tt_hocSinh.soDT,
                        Mail = c.tt_hocSinh.eMail,
                    }).Distinct().ToList();
                    break;
                case 2:
                    this.dgDSHocSinh.DataSource = db.tt_dsLop.Where(c => c.trangThai == false).Select(c =>  new
                    {
                        maHS = c.maHS,
                        Ho = c.tt_hocSinh.hoHS,
                        Ten = c.tt_hocSinh.tenHS,
                        gender = c.tt_hocSinh.gioiTinh == 1 ? "Nam" : "Nữ",
                        NgaySinh = c.tt_hocSinh.ngaySinh,
                        noiSinh = c.tt_hocSinh.noiSinh,
                        SDT = c.tt_hocSinh.soDT,
                        Mail = c.tt_hocSinh.eMail,
                    }).Distinct().ToList();
                    break;
                default:
                    break;
            }
        }
    }
}
