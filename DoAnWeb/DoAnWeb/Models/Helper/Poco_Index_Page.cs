using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DoAnWeb.Models.Helper
{
    public class Poco_Index_Page
    {
        public List<tbl_SanPhams> DanhSachSanPhamMoiNhat { get; set; }
        public List<tbl_SanPhams> DanhSachSanPhamXemNhieuNhat { get; set; }
        public List<tbl_SanPhams> DanhSachSanPhamBanChayNhat { get; set; }
    }
}