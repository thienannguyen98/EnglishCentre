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
    
    public partial class tt_lopHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tt_lopHoc()
        {
            this.tt_bangDiem = new HashSet<tt_bangDiem>();
            this.tt_danhGiaHS = new HashSet<tt_danhGiaHS>();
            this.tt_dongHP = new HashSet<tt_dongHP>();
            this.tt_donTu = new HashSet<tt_donTu>();
            this.tt_donTu1 = new HashSet<tt_donTu>();
            this.tt_dsGiaoVienLop = new HashSet<tt_dsGiaoVienLop>();
            this.tt_dsLop = new HashSet<tt_dsLop>();
            this.tt_lichHoc = new HashSet<tt_lichHoc>();
            this.tt_nhatKyGD = new HashSet<tt_nhatKyGD>();
        }
    
        public int maLop { get; set; }
        public int maCT { get; set; }
        public int maHK { get; set; }
        public int maCN { get; set; }
        public string tenLop { get; set; }
        public long hocPhi { get; set; }
        public System.DateTime ngayKG { get; set; }
        public System.DateTime ngayKT { get; set; }
        public Nullable<int> thoiLuong { get; set; }
        public string thuHoc { get; set; }
        public string caHoc { get; set; }
        public Nullable<int> siSoHKT { get; set; }
        public Nullable<int> siSoMucTieu { get; set; }
        public string ghiChu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_bangDiem> tt_bangDiem { get; set; }
        public virtual tt_chiNhanh tt_chiNhanh { get; set; }
        public virtual tt_chuongTrinh tt_chuongTrinh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_danhGiaHS> tt_danhGiaHS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_dongHP> tt_dongHP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_donTu> tt_donTu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_donTu> tt_donTu1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_dsGiaoVienLop> tt_dsGiaoVienLop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_dsLop> tt_dsLop { get; set; }
        public virtual tt_khoaHoc tt_khoaHoc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_lichHoc> tt_lichHoc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_nhatKyGD> tt_nhatKyGD { get; set; }
    }
}
