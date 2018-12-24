using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KD.Models;


namespace KD.Controllers
{
    public class LoginController : Controller
    {
        private SonyEntities db = new SonyEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            if (Request.Cookies["MyCookie"] != null)
            {
                var c = new HttpCookie("MyCookie");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Login()
        {
            var c = new HttpCookie("MyCookie");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(NguoiDung user)
        {
            if (ModelState.IsValid)
            {
                var obj = db.NguoiDungs.
                    Where(a => a.TenDangNhap.Equals(user.TenDangNhap) && a.MatKhau.Equals(user.MatKhau) && a.IdKieuNguoiDung==2).
                    FirstOrDefault();
                if (obj != null)
                {
                    Session["TaiKhoan"] = obj.TenDangNhap.ToString();
                    return RedirectToAction("Index", "AdminDanhMucSanPhams");
                }
                else
                {
                    ViewBag.Error = "Sai tên tài khoản hoặc mật khẩu. Vui lòng nhập lại.";
                    return View();
                }
            }
            return RedirectToAction("Login", "Login");
        }

    }

}
