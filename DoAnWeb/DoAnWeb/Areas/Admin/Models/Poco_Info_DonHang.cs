using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Areas.Admin.Models
{
    public class Poco_Info_DonHang
    {
        public string MaDonHang { get; set; }
        public string NgayLapPhieu { get; set; }
        public string GioLap { get; set; }
        public string TongSoLuong { get; set; }
        public decimal TongTien { get; set; }
        public bool TinhTrangThanhToan { get; set; }
        public bool TinhTrangGiaoHang { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string SoDienThoaiNhanHang { get; set; }
    }
}