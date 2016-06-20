using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Models;
namespace DoAnWeb.Areas.Admin.Models
{
    public class Poco_DonHang_IndexPage
    {
        public string MaDonHang { get; set; }
        public string NgayLapPhieu { get; set; }
        public string GioLap { get; set; }
        public string TenKhachHang { get; set; }
        public string TongSoLuong { get; set; }
        public decimal TongTien { get; set; }
        public bool TinhTrangThanhToan { get; set; }
        public bool TinhTrangGiaoHang{ get; set; }
    }
}