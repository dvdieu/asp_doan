using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DoAnWeb.Models.Helper
{
    public class Poco_Index_Page
    {
        List<tbl_SanPhams> danhSachSanPhamMoiNhat;
        List<tbl_SanPhams> danhSachSanPhamXemNhieuNhat;
        List<tbl_SanPhams> danhSachSanPhamBanChayNhat;

        public List<tbl_SanPhams> DanhSachSanPhamMoiNhat
        {
            get
            {
                return danhSachSanPhamMoiNhat;
            }

            set
            {
                danhSachSanPhamMoiNhat = value;
            }
        }
        public List<tbl_SanPhams> DanhSachSanPhamXemNhieuNhat
        {
            get
            {
                return danhSachSanPhamXemNhieuNhat;
            }

            set
            {
                danhSachSanPhamXemNhieuNhat = value;
            }
        }
        public List<tbl_SanPhams> DanhSachSanPhamBanChayNhat
        {
            get
            {
                return danhSachSanPhamBanChayNhat;
            }

            set
            {
                danhSachSanPhamBanChayNhat = value;
            }
        }
    }
}