using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Models
{
    public class Poco_Page_ThemSanPham
    {
        public List<tbl_LoaiSanPhams> DanhSachLoaiSanPham { get; set; }
        public List<tbl_NhaSanXuats> DanhSachNhaCungCap { get; set; }
    }
}