//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_PhieuOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_PhieuOrders()
        {
            this.tbl_ChiTietOrders = new HashSet<tbl_ChiTietOrders>();
        }
    
        public int PhieuOrderID { get; set; }
        public System.DateTime NgayLapPhieu { get; set; }
        public int NguoiSuDungID { get; set; }
        public int TongSoLuong { get; set; }
        public decimal TongTien { get; set; }
        public Nullable<bool> TinhTrangThanhToan { get; set; }
        public Nullable<bool> TinhTrangGiaoHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public Nullable<bool> DaXoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ChiTietOrders> tbl_ChiTietOrders { get; set; }
        public virtual tbl_NguoiSuDungs tbl_NguoiSuDungs { get; set; }
    }
}
