using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }

        //public void AddToCart(ShoppingCartItem item, int Quantity, string Size)
        //{
        //    var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId && x.Size == Size);
        //    if (checkExits != null)
        //    {
        //        checkExits.Quantity += Quantity;
        //        checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
        //    }
        //    else
        //    {
        //        item.Size = Size; // Lưu kích thước vào mục hàng
        //        Items.Add(item);
        //    }
        //}
        //public void AddToCart(ShoppingCartItem item, int Quantity, string Size)
        //{
        //    var existingProduct = Items.FirstOrDefault(x => x.ProductId == item.ProductId);

        //    if (existingProduct != null)
        //    {
        //        // Kiểm tra xem kích thước của sản phẩm đã tồn tại có khác với kích thước mới không
        //        if (!string.Equals(existingProduct.Size, Size, StringComparison.OrdinalIgnoreCase))
        //        {
        //            // Nếu kích thước khác, cập nhật kích thước mới cho sản phẩm đã tồn tại
        //            existingProduct.Size = Size;
        //            // Cập nhật số lượng và tổng giá tiền của sản phẩm
        //            existingProduct.Quantity += Quantity;
        //            existingProduct.TotalPrice = existingProduct.Price * existingProduct.Quantity;
        //        }
        //        else
        //        {
        //            // Nếu kích thước giống nhau, chỉ cập nhật số lượng và tổng giá tiền của sản phẩm
        //            existingProduct.Quantity += Quantity;
        //            existingProduct.TotalPrice = existingProduct.Price * existingProduct.Quantity;
        //        }
        //    }
        //    else
        //    {
        //        // Nếu sản phẩm không tồn tại trong giỏ hàng, thêm sản phẩm mới vào giỏ hàng với kích thước mới
        //        item.Size = Size;
        //        Items.Add(item);
        //    }
        //}
        public void AddToCart(ShoppingCartItem item, int Quantity, string Size)
        {
            var existingProduct = Items.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existingProduct != null)
            {
                // Kiểm tra xem kích thước của sản phẩm đã tồn tại có khác với kích thước mới không
                if (!string.Equals(existingProduct.Size, Size, StringComparison.OrdinalIgnoreCase))
                {
                    // Nếu kích thước khác, cập nhật kích thước mới cho sản phẩm đã tồn tại
                    existingProduct.Size = Size;
                }

                // Cập nhật số lượng và tổng giá tiền của sản phẩm theo số lượng mới
                existingProduct.Quantity = Quantity;
                existingProduct.TotalPrice = existingProduct.Price * Quantity;
            }
            else
            {
                // Nếu sản phẩm không tồn tại trong giỏ hàng, thêm sản phẩm mới vào giỏ hàng với kích thước mới
                item.Size = Size;
                Items.Add(item);
            }
        }


        public void Remove(int id)
        {
            var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                Items.Remove(checkExits);
            }
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                checkExits.Quantity = quantity;
                checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
            }
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(x => x.TotalPrice);
        }
        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            Items.Clear();
        }

    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}