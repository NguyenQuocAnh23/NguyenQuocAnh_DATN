using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "SDT không được để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ khổng được để trống")]
        public string Address { get; set; }
        public string Email { get; set; }
        public int TypePayment { get; set; }
    }
}