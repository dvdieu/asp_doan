using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models.Helper
{
    public class Cart
    {

        public List<CartItem> Items { get; set; }

        public Cart() {
            this.Items = new List<CartItem>();
        }

        public void Add(CartItem item)
        {
            var existedItem = this.Items.Where(i => i.ProID == item.ProID).FirstOrDefault();

            if (existedItem != null)
            {
                existedItem.Quantity += item.Quantity;

            }
            else
            { 
                this.Items.Add(item);
            }
        
        }

        public int GetTotal() {
            return this.Items.Sum(i => i.Quantity);
        }

        public void RemoveItem(int proId) {
            CartItem existedItem = Items
                .Where(i => i.ProID == proId)
                .FirstOrDefault();
            if(existedItem != null)
            {
                this.Items.Remove(existedItem);

            }
        }

        public void UpdateItem(int proId, int quantity)
        {
            CartItem existedItem = Items
                .Where(i => i.ProID == proId)
                .FirstOrDefault();
            if (existedItem != null)
            {
                existedItem.Quantity = quantity;

            }
        }
    }

    public class CartItem
    {
            public int ProID { get; set; }
            public int Quantity { get; set; }
    }
}