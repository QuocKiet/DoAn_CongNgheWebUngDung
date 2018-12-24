using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KD.Models;

namespace KD.Controllers
{
    public class DanhMucSanPhamController : Controller
    {
        SonyEntities db = new SonyEntities();
        // GET: DanhMucSanPham
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult MenuLoai()
        {
            List<DanhMucSanPham> items = db.DanhMucSanPhams.ToList();
            //return PartialView("_MenuLoai", items);
            return PartialView(items);
        }
    }
}