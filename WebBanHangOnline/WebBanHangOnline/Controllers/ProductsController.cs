//using PagedList;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.UI;
//using WebBanHangOnline.Models;
//using WebBanHangOnline.Models.EF;

//namespace WebBanHangOnline.Controllers
//{
//    public class ProductsController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();
//        // GET: Products
//        public ActionResult Index(int? page, string SearchText)
//        {
//            var pageSize = 12;
//            if (page == null)
//            {
//                page = 1;
//            }

//            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);

//            if (!string.IsNullOrEmpty(SearchText))
//            {
//                items = items.Where(p => p.Title.Contains(SearchText) || p.Price.ToString().Contains(SearchText) || p.Alias.Contains(SearchText) || p.PriceSale.ToString().Contains(SearchText));
//            }

//            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
//            items = items.ToPagedList(pageIndex, pageSize);

//            ViewBag.PageSize = pageSize;
//            ViewBag.Page = page;
//            ViewBag.SearchText = SearchText;

//            return View(items);
//        }


//        public ActionResult Detail(string alias, int id)
//        {
//            var item = db.Products.Find(id);
//            if (item != null)
//            {
//                db.Products.Attach(item);
//                item.ViewCount = item.ViewCount + 1;
//                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
//                db.SaveChanges();
//            }

//            return View(item);
//        }

//        public ActionResult ProductCategory(string alias, int id, int? page)
//        {
//            var pageSize = 12;
//            if (page == null)
//            {
//                page = 1;
//            }

//            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

//            IEnumerable<Product> items = db.Products;

//            if (id > 0)
//            {
//                items = items.Where(x => x.ProductCategoryId == id);
//            }
//            var cate = db.ProductCategories.Find(id);
//            if (cate != null)
//            {
//                ViewBag.CateName = cate.Title;
//            }

//            items = items.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);

//            ViewBag.PageSize = pageSize;
//            ViewBag.Page = page;

//            return View(items);
//        }


//        public ActionResult Partial_ItemByCateId()
//        {
//            var items = db.Products.Where(x => x.IsActive).Take(100).ToList();
//            return PartialView(items);
//        }
//    }
//}

//using PagedList;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;
//using WebBanHangOnline.Models;
//using WebBanHangOnline.Models.EF;
//using System.Globalization;

//namespace WebBanHangOnline.Controllers
//{
//    public class ProductsController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Products
//        public ActionResult Index(int? page, string SearchText)
//        {
//            var pageSize = 12;
//            if (page == null)
//            {
//                page = 1;
//            }

//            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);

//            if (!string.IsNullOrEmpty(SearchText))
//            {
//                SearchText = NormalizeString(SearchText.Trim());
//                var searchTerms = SearchText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//                items = items.Where(p => searchTerms.All(term =>
//                    NormalizeString(p.Title).Contains(term) ||
//                    p.Price.ToString().Contains(term) ||
//                    p.PriceSale.ToString().Contains(term)
//                ));
//            }

//            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
//            items = items.ToPagedList(pageIndex, pageSize);

//            ViewBag.PageSize = pageSize;
//            ViewBag.Page = page;
//            ViewBag.SearchText = SearchText;

//            return View(items);
//        }

//        public ActionResult Detail(string alias, int id)
//        {
//            var item = db.Products.Find(id);
//            if (item != null)
//            {
//                db.Products.Attach(item);
//                item.ViewCount = item.ViewCount + 1;
//                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
//                db.SaveChanges();
//            }

//            return View(item);
//        }

//        public ActionResult ProductCategory(string alias, int id, int? page)
//        {
//            var pageSize = 12;
//            if (page == null)
//            {
//                page = 1;
//            }

//            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

//            IEnumerable<Product> items = db.Products;

//            if (id > 0)
//            {
//                items = items.Where(x => x.ProductCategoryId == id);
//            }
//            var cate = db.ProductCategories.Find(id);
//            if (cate != null)
//            {
//                ViewBag.CateName = cate.Title;
//            }

//            items = items.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);

//            ViewBag.PageSize = pageSize;
//            ViewBag.Page = page;

//            return View(items);
//        }

//        public ActionResult Partial_ItemByCateId()
//        {
//            var items = db.Products.Where(x => x.IsActive).Take(100).ToList();
//            return PartialView(items);
//        }

//        private string NormalizeString(string text)
//        {
//            var normalizedString = text.Normalize(NormalizationForm.FormD);
//            var stringBuilder = new StringBuilder();

//            foreach (var c in normalizedString)
//            {
//                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
//                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
//                {
//                    stringBuilder.Append(c);
//                }
//            }

//            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
//        }
//    }
//}

using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using System.Globalization;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(int? page, string SearchText)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }

            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);

            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchText = NormalizeString(SearchText.Trim());
                var searchTerms = SearchText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                items = items.Where(p => searchTerms.All(term =>
                    NormalizeString(p.Title).Contains(term) ||
                    p.Price.ToString().Contains(term) ||
                    p.PriceSale.ToString().Contains(term)
                ));
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchText = SearchText;

            return View(items);
        }

        private string NormalizeString(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
        }


        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }

            return View(item);
        }

        public ActionResult ProductCategory(string alias, int id, int? page)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            IEnumerable<Product> items = db.Products;

            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id);
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            items = items.OrderByDescending(x => x.Id).ToPagedList(pageIndex, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(items);
        }

        public ActionResult Partial_ItemByCateId()
        {
            var items = db.Products.Where(x => x.IsActive).Take(100).ToList();
            return PartialView(items);
        }

        
    }
}
