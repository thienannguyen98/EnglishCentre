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
    
    public partial class tt_dongHP
    {
        public int maLop { get; set; }
        public string maHS { get; set; }
        public string soBienLai { get; set; }
        public long soTien { get; set; }
        public int lanThu { get; set; }
        public long mienGiam { get; set; }
    
        public virtual tt_hocSinh tt_hocSinh { get; set; }
        public virtual tt_lopHoc tt_lopHoc { get; set; }
    }
}
