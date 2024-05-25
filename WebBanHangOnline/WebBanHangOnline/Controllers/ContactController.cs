using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using System.Web.Helpers;

namespace WebBanHangOnline.Controllers
{
    public class ContactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LichSuDonHang()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);

                string email = user.Email;

                var items = db.Orders.Where(x => x.Email == email).ToList();
                return PartialView(items);
            }

            return PartialView();
        }
    }
}