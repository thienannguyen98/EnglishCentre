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
    
    public partial class tt_nhatKyGD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tt_nhatKyGD()
        {
            this.tt_diemDanh = new HashSet<tt_diemDanh>();
        }
    
        public int maNKGD { get; set; }
        public int maLop { get; set; }
        public int maPhong { get; set; }
        public int maCa { get; set; }
        public Nullable<int> maTV { get; set; }
        public System.DateTime ngayGN { get; set; }
        public string noiDungBG { get; set; }
        public string btVeNha { get; set; }
        public int troGiang { get; set; }
        public string nhanXet { get; set; }
        public string nguoiDayThe { get; set; }
    
        public virtual tt_caHoc tt_caHoc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tt_diemDanh> tt_diemDanh { get; set; }
        public virtual tt_lopHoc tt_lopHoc { get; set; }
        public virtual tt_phongHoc tt_phongHoc { get; set; }
        public virtual tt_thanhVien tt_thanhVien { get; set; }
    }
}
