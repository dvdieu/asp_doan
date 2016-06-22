using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Areas.Admin.Models
{
    public class Poco_ChiTiet_Order
    {
        public string SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

    }
}