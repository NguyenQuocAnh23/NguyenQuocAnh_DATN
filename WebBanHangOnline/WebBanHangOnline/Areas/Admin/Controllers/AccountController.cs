using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Account

        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }

            //// Lấy danh sách tất cả người dùng từ cơ sở dữ liệu và sắp xếp theo Id
            //var users = db.Users.OrderBy(x => x.Id).ToList();

            // Lấy danh sách tất cả người dùng từ cơ sở dữ liệu và sắp xếp theo CreatedDate
            var users = db.Users.OrderByDescending(x => x.CreatedDate).ToList();

            // Tạo một từ điển để lưu trữ UserId và danh sách vai trò của mỗi người dùng
            Dictionary<string, IList<string>> userRolesDict = new Dictionary<string, IList<string>>();

            // Lặp qua từng người dùng để lấy danh sách vai trò của mỗi người dùng
            foreach (var user in users)
            {
                // Lấy danh sách các vai trò của người dùng hiện tại
                var roles = UserManager.GetRoles(user.Id);

                // Thêm UserId và danh sách vai trò vào từ điển
                userRolesDict.Add(user.Id, roles);
            }

            // Truyền danh sách vai trò của từng người dùng vào ViewBag
            ViewBag.UserRoles = userRolesDict;

            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var paginatedUsers = users.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(paginatedUsers);
        }

        //// Lấy danh sách tất cả người dùng từ cơ sở dữ liệu
        //    var users = db.Users.ToList();

        //    // Tạo một từ điển (dictionary) để lưu trữ UserId và danh sách các vai trò của mỗi người dùng
        //    Dictionary<string, IList<string>> userRolesDict = new Dictionary<string, IList<string>>();

        //    // Lặp qua từng người dùng để lấy danh sách vai trò của mỗi người dùng
        //    foreach (var user in users)
        //    {
        //        // Lấy danh sách các vai trò của user hiện tại
        //        var roles = UserManager.GetRoles(user.Id);

        //        // Thêm UserId và danh sách vai trò vào từ điển
        //        userRolesDict.Add(user.Id, roles);
        //    }

        //    // Truyền danh sách vai trò của từng người dùng vào ViewBag
        //    ViewBag.UserRoles = userRolesDict;

        //    return View(users);


        //public ActionResult Index(int? page)
        //{
        //    //IEnumerable<News> items = db.News.OrderByDescending(x => x.Id);
        //    //var pageSize = 10;
        //    //if (page == null)
        //    //{
        //    //    page = 1;
        //    //}
        //    //var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
        //    //items = items.ToPagedList(pageIndex, pageSize);
        //    //ViewBag.PageSize = pageSize;
        //    //ViewBag.Page = page;
        //    //var atems = db.Users.ToList();
        //    //return View(atems);

        //    IEnumerable<News> items = db.News.OrderByDescending(x => x.Id);
        //    var pageSize = 10;
        //    if (page == null)
        //    {
        //        page = 1;
        //    }
        //    var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
        //    items = items.ToPagedList(pageIndex, pageSize);
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.Page = page;

        //    // Lấy danh sách tất cả users từ database
        //    var users = db.Users.ToList();

        //    // Tạo một dictionary để lưu UserId và danh sách roles của mỗi user
        //    Dictionary<string, IList<string>> userRolesDict = new Dictionary<string, IList<string>>();

        //    // Lặp qua từng user để lấy danh sách roles của mỗi user
        //    foreach (var user in users)
        //    {
        //        // Lấy danh sách các roles của user hiện tại
        //        var roles = UserManager.GetRoles(user.Id);

        //        // Thêm UserId và danh sách roles vào dictionary
        //        userRolesDict.Add(user.Id, roles);
        //    }

        //    // Truyền danh sách roles của từng user vào ViewBag
        //    ViewBag.UserRoles = userRolesDict;

        //    return View(users);

        //}

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai");
                    return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Fullname = model.FullName,
                    Phone = model.Phone,
                    CreatedDate= DateTime.Now,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}