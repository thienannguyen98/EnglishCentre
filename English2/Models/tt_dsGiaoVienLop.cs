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
    
    public partial class tt_dsGiaoVienLop
    {
        public int maLop { get; set; }
        public int maTV { get; set; }
        public System.DateTime ngayNhanLop { get; set; }
        public System.DateTime ngayThoiDay { get; set; }
        public string dayThu { get; set; }
        public bool trangThai { get; set; }
        public string nhiemVu { get; set; }
    
        public virtual tt_lopHoc tt_lopHoc { get; set; }
        public virtual tt_thanhVien tt_thanhVien { get; set; }
    }
}
