using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.UI;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order

        public ActionResult Index(int? page)
        {
            var items = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 15;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //    var item = db.Orders.Find(id);
            //    if (item != null)
            //    {
            //        db.Orders.Remove(item);
            //        db.SaveChanges();
            //        return Json(new { success = true });
            //    }
            //    return Json(new { success = false });
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var item = db.Orders.Find(id);
                    if (item != null)
                    {
                        // Update product quantities
                        foreach (var detail in item.OrderDetails)
                        {
                            var product = db.Products.Find(detail.ProductId);
                            if (product != null)
                            {
                                product.Quantity += detail.Quantity;
                            }
                        }

                        db.Orders.Remove(item);
                        db.SaveChanges();
                        transaction.Commit();
                        return Json(new { success = true });
                    }
                    return Json(new { success = false });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log the exception
                    return Json(new { success = false, message = "An error occurred while deleting the order." });
                }
            }
        }
    }
}