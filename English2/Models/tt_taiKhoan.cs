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
    
    public partial class tt_taiKhoan
    {
        public int maTV { get; set; }
        public string tkThanhVien { get; set; }
        public string matKhau { get; set; }
        public System.DateTime ngayCap { get; set; }
        public bool duocSD { get; set; }
        public Nullable<int> capDo { get; set; }
        public string quyenHan { get; set; }
        public string ipTruyCap { get; set; }
        public System.DateTime tcGanNhat { get; set; }
        public string mac { get; set; }
        public string trangMacDinh { get; set; }
        public string ghiChu { get; set; }
    
        public virtual tt_thanhVien tt_thanhVien { get; set; }
    }
}