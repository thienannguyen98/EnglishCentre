//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace English2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tt_hocSinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tt_hocSinh()
        {
            this.tt_ctBangDiem = new HashSet<tt_ctBangDiem>();
            this.tt_danhGiaHS = new HashSet<tt_danhGiaHS>();
            this.tt_diemDanh = new HashSet<tt_diemDanh>();
            this.tt_dongHP = new HashSet<tt_dongHP>();
            this.tt_donTu = new HashSet<tt_donTu>();
            this.tt_dsLop = new HashSet<tt_dsLop>();
            this.tt_hocSinhTuVan = new HashSet<tt_hocSinhTuVan>();
            this.tt_qhHocSinh = new HashSet<tt_qhHocSinh>();
            this.tt_qhHocSinh1 = new HashSet<tt_qhHocSinh>();
        }
    
        public string maHS { get; set; }
        public string hoHS { get; set; }
        public string tenHS { get; set; }
        public int gioiTinh { get; set; }
        public System.DateTime ngaySinh { get; set; }
        public string noiSinh { get; set; }
        public string soDT { get; set; }
        public string eMail { get; set; }
        public string facebook { get; set; }
        public string dcLienLac { get; set; }
        public string dcThuongTru { get; set; }
        public string hinhHS { get; set; }
        public string hoTenC { get; set; }
        public string soDTC { get; set; }
        public string emailC { get; set; }
        public string quanHeHS { get; set; }
        public string kenhQC { get; set; }
        public string ghiChu { get; set; }
        public Nullable<bool> hocThu { get; set; }
        public Nullable<System.DateTime> ngayDK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_ctBangDiem> tt_ctBangDiem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_danhGiaHS> tt_danhGiaHS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_diemDanh> tt_diemDanh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_dongHP> tt_dongHP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_donTu> tt_donTu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_dsLop> tt_dsLop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_hocSinhTuVan> tt_hocSinhTuVan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_qhHocSinh> tt_qhHocSinh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_qhHocSinh> tt_qhHocSinh1 { get; set; }
    }
}
