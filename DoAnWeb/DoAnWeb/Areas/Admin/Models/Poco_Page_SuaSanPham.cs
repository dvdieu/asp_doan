using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Models
{
    public class Poco_Page_SuaSanPham
    {
        public List<tbl_LoaiSanPhams> DanhSachLoaiSanPham { get; set; }
        public List<tbl_NhaSanXuats> DanhSachNhaSanXuat { get; set; }
        public tbl_SanPhams SanPham { get; set; }
    }
}