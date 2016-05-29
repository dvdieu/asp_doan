using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models.Helper
{
    public class Poco_Product_Page
    {
        List<tbl_SanPhams> danhSachSanPham;
        int soPage;
        int curPage;
        int maSanPham;
        public List<tbl_SanPhams> DanhSachSanPham
        {
            get
            {
                return danhSachSanPham;
            }

            set
            {
                danhSachSanPham = value;
            }
        }

        public int SoPage
        {
            get
            {
                return soPage;
            }

            set
            {
                soPage = value;
            }
        }

        public int CurPage
        {
            get
            {
                return curPage;
            }

            set
            {
                curPage = value;
            }
        }

        public int MaSanPham
        {
            get
            {
                return maSanPham;
            }

            set
            {
                maSanPham = value;
            }
        }
    }
}