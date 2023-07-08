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
namespace English2.Views.giaoVien
{
    public partial class gv_DSGiaoVien : Form
    {
        public gv_DSGiaoVien()
        {
            InitializeComponent();
            this.cbCN.DataSource = db.tt_chiNhanh.Select(c => c.tenCN).ToList();
            loadData();
            DataGridViewButtonColumn nutSua = new DataGridViewButtonColumn();
            this.dgGiaoVien.Columns.Add(nutSua);
            DataGridViewButtonColumn nutXoa = new DataGridViewButtonColumn();
            this.dgGiaoVien.Columns.Add(nutXoa);
            styleData();
        }
        ttAnhNguEntities db = new ttAnhNguEntities();

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
            
            this.dgGiaoVien.DataSource = db.tt_thanhVien.Where(c => c.tt_chiNhanh.tenCN.Equals(cbCN.Text) && c.chucDanh.Equals("Giáo Viên")).Select(c => new
            {
                MaTV = c.maTV,
                Ho = c.hoTV,
                Ten = c.tenTV,
                gender = c.gioiTinh == 1 ? "Nam" : "Nữ",
                NgaySinh = c.ngaySinh,
                SDT = c.soDT,
                Mail = c.eMail,
                chiNhanh = c.tt_chiNhanh.tenCN,
            }).OrderBy(x => x.Ten).ToList();

            
        }

        void styleData()
        {
            this.dgGiaoVien.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dgGiaoVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgGiaoVien.MultiSelect = false;
            this.dgGiaoVien.RowTemplate.Height = 70;
            this.dgGiaoVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgGiaoVien.Columns[0].HeaderText = "Mã";
            this.dgGiaoVien.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgGiaoVien.Columns[0].Width = 70;
            this.dgGiaoVien.Columns[0].Visible = false;
            this.dgGiaoVien.Columns[1].HeaderText = "Họ";
            this.dgGiaoVien.Columns[1].Width = 170;
            this.dgGiaoVien.Columns[2].HeaderText = "Tên";
            this.dgGiaoVien.Columns[2].Width = 90;
            this.dgGiaoVien.Columns[3].HeaderText = "Giới tính";
            this.dgGiaoVien.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgGiaoVien.Columns[3].ReadOnly = true;
            this.dgGiaoVien.Columns[3].Width = 90;
            this.dgGiaoVien.Columns[4].HeaderText = "Ngày sinh";
            this.dgGiaoVien.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            this.dgGiaoVien.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgGiaoVien.Columns[4].Width = 100;
            this.dgGiaoVien.Columns[5].HeaderText = "Số điện thoại";
            this.dgGiaoVien.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgGiaoVien.Columns[5].Width = 110;
            this.dgGiaoVien.Columns[6].HeaderText = "Email";
            this.dgGiaoVien.Columns[6].Width = 160;

            this.dgGiaoVien.Columns[7].HeaderText = "Chi nhánh";
            this.dgGiaoVien.Columns[7].Width = 140;
            this.dgGiaoVien.Columns[8].HeaderText = "Chỉnh sửa";
            this.dgGiaoVien.Columns[8].Width = 80;
            this.dgGiaoVien.Columns[9].HeaderText = "Xóa";
            this.dgGiaoVien.Columns[9].Width = 80;
        }
        /// <summary>
        /// Method dùng để xóa thành viên theo đối tượng đã select
        /// Lấy Mã TV -> Tìm
        /// Xóa
        /// </summary>
        void xoaThanhVien()
        {
            using (ttAnhNguEntities dbbb = new ttAnhNguEntities())
            {
                string id = dgGiaoVien.SelectedCells[0].OwningRow.Cells["MaTV"].Value.ToString();
                DialogResult dr = MessageBox.Show("Bạn có thật sự muốn xóa?", "Đồng ý", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        int maTV = Int32.Parse(id);
                        tt_thanhVien del = dbbb.tt_thanhVien.FirstOrDefault(p => p.maTV == maTV);
                        dbbb.tt_thanhVien.Remove(del);
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
            this.dgGiaoVien.DataSource = db.tt_thanhVien.Where(c => c.tt_chiNhanh.tenCN.Equals(cbCN.Text) && c.chucDanh.Equals("Giáo viên") && ((c.hoTV + " " + c.tenTV).Contains(txtTimTV.Text.Trim()) || c.soDT.Contains(txtTimTV.Text.Trim()))).Select(c => new
            {
                MaTV = c.maTV,
                Ho = c.hoTV,
                Ten = c.tenTV,
                gender = c.gioiTinh == 1 ? "Nam" : "Nữ",
                NgaySinh = c.ngaySinh,
                SDT = c.soDT,
                Mail = c.eMail,
                chiNhanh = c.tt_chiNhanh.tenCN,
            }).ToList();
        }
        void capNhatTT()
        {
            tv_CapNhatThanhVien f = new tv_CapNhatThanhVien();
            Addform(f);
            f.flag = 2;
            int maTV = Int32.Parse(this.dgGiaoVien.CurrentRow.Cells[2].Value.ToString());
            tt_thanhVien tk = db.tt_thanhVien.Single(t => t.maTV.Equals(maTV));
            //Gán mã TV được selected vào biến tam để form CapNhatTV sử dụng
            f.tam = maTV;
            f.txtHo.Text = tk.hoTV;
            f.txtTen.Text = tk.tenTV;
            f.dateNgaySinh.Text = tk.ngaySinh.ToString();
            f.txtNoiSinh.Text = tk.noiSinh;

            //Group gender nên chỉ cần check case rB Nữ có checked k -> false => rbNam checked.
            f.rBNu.Checked = tk.gioiTinh == 0;

            f.txtQuocTich.Text = tk.quocTich;
            f.txtSDT.Text = tk.soDT;
            f.txtMail.Text = tk.eMail;
            f.txtDiaChiLL.Text = tk.dcLienLac;
            f.txtDiaChiTT.Text = tk.dcThuongTru;

            f.rBDocThan.Checked = tk.honNhan == 0;
            f.rBCoGiaDinh.Checked = tk.honNhan == 1;
            f.rBDaLyHon.Checked = tk.honNhan == 2;

            f.txtCMND.Text = tk.soCMND;
            f.txtNoiCapCMND.Text = tk.noiCapCMND;
            f.dateNgayCapCMND.Text = tk.ngayCapCMND.ToString();
            f.cbCN.Text = tk.chucDanh;
            //Phong ban
            f.cBPhongBan.Text = tk.tt_phongBan.tenPB;
            f.cbChucDanh.Text = tk.chucDanh;
            //CN hiện tại chỉ có 1
            f.cbCN.Text = tk.tt_chiNhanh.tenCN;
            //
            f.txtHocVi.Text = tk.hocVi;
            f.txtChuyenMon.Text = tk.chuyenMon;
            f.txtNgoaiNgu.Text = tk.ngoaiNgu;
            f.txtTinHoc.Text = tk.tinHoc;
            f.txtKiNangKhac.Text = tk.kyNangKhac;
            f.txtQuanHeGD.Text = tk.qhGiaDinh;
            if (tk.hinhCN.Length > 0)
                f.pBHinhNV.Image = new Bitmap(tk.hinhCN);
        }
        #endregion

        private void btnDK_Click(object sender, EventArgs e)
        {
            gv_DangKyGD f = new gv_DangKyGD();
            f.maTV = Int32.Parse(this.dgGiaoVien.CurrentRow.Cells[2].Value.ToString());
            Addform(f);
        }

        private void btnThem(object sender, EventArgs e)
        {
            tv_ThemThanhVien f = new tv_ThemThanhVien();
            Addform(f);
            f.flag = 2;
            f.cbChucDanh.Text = "Giáo Viên";
        }

        private void cbCN_SelectedIndexChanged(object sender, EventArgs e)
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
                xoaThanhVien();
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            gv_Chat f = new gv_Chat();
            f.Show();
        }
    }
}
