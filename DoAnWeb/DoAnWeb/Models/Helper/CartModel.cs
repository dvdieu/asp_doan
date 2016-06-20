using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models.Helper
{
    public class CartItemModel
    {

        public CartItem Item { get; set; }
        public tbl_SanPhams Product { get; set; }
    }
}