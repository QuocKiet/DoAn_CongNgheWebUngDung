using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KD.Models;
using PagedList;
using PagedList.Mvc;

namespace KD.Controllers
{
    public class SanPhamController : Controller
    {
        SonyEntities db = new SonyEntities();
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult sanPhamMoi()
        {
            List<SanPham> items = db.SanPhams.Include("HinhSanPham").OrderByDescending(p => p.IdSanPham).Take(8).ToList();
            return PartialView(items);
        }

        public ViewResult listSanPham(int? page)
        {
            if (page == null || page.Value < 1)
            {
                page = 1;
            }
            IPagedList<SanPham> items = db.SanPhams.Include("HinhSanPham").OrderByDescending(p => p.IdSanPham).ToPagedList(page.Value, 20);
            ViewBag.SanPhams = items;
            return View();
        }

        public ActionResult ChiTiet(int? id)
        {
            if (id == null || id < 0)
            {
                RedirectToAction("Index", "Home");
            }
            var item = db.SanPhams.Include("DanhMucSanPham").Include("HinhSanPham").SingleOrDefault(p => p.IdSanPham == id);
            if (item == null)
                throw new Exception("Sản phẩm không tồn tại");
            return View(item);

        }

        public ViewResult sanPhamTheoLoai(int idL, int? page)
        {
            if (page == null || page.Value < 0)
                page = 1;

            DanhMucSanPham danhMuc = db.DanhMucSanPhams.Find(idL);
            IPagedList<SanPham> items = db.SanPhams.Include("DanhMucSanPham").Include("HinhSanPham").Where(p => p.IdDanhMucSanPham == idL).OrderByDescending(p => p.IdSanPham).ToPagedList(page.Value, 20);
            ViewBag.SanPhams = items;
            ViewBag.DanhMucSanPhamId = idL;
            return View("listSanPham");
        }

        public ActionResult TimKiem(FormCollection f, int? page)
        {
            if (page == null || page.Value < 0)
            {
                page = 1;
            }
            string query = f["textBoxTimKiem"].ToString();
            IPagedList<SanPham> items = db.SanPhams.Include("DanhMucSanPham").Include("HinhSanPham").Where(p => p.TenSanPham.Contains(query)).OrderByDescending(p => p.IdSanPham).ToPagedList(page.Value, 20);
            ViewBag.SanPhams = items;
            ViewBag.TimKiem = query;
            return View("listSanPham");
        }
    }
}