using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Models;
namespace DoAnWeb.Areas.Admin.Models
{
    public class Poco_DonHang_ChiTiet
    {
        public Poco_Info_DonHang ThongTinDonHang { get; set; }
        public Poco_Info_KhachHang ThongTinKhachHang { get; set; }
        public List<Poco_ChiTiet_Order> ChiTietDonHang { get; set; }
    }
}