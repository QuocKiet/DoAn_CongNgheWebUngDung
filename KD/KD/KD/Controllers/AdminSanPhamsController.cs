using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KD.Models;

namespace KD.Controllers
{
    public class AdminSanPhamsController : Controller
    {
        private SonyEntities db = new SonyEntities();

        // GET: AdminSanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMucSanPham).Include(s => s.HinhSanPham);
            return View(sanPhams.ToList());
        }

        // GET: AdminSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: AdminSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.IdDanhMucSanPham = new SelectList(db.DanhMucSanPhams, "IdDanhMucSanPham", "TenDanhMucSanPham");
            ViewBag.IdHinhSanPham = new SelectList(db.HinhSanPhams, "IdHinhSanPham", "FileHinhSanPham");
            return View();
        }

        // POST: AdminSanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSanPham,IdDanhMucSanPham,IdHinhSanPham,TenSanPham,MoTaSanPham,GiaSanPham")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDanhMucSanPham = new SelectList(db.DanhMucSanPhams, "IdDanhMucSanPham", "TenDanhMucSanPham", sanPham.IdDanhMucSanPham);
            ViewBag.IdHinhSanPham = new SelectList(db.HinhSanPhams, "IdHinhSanPham", "FileHinhSanPham", sanPham.IdHinhSanPham);
            return View(sanPham);
        }

        // GET: AdminSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDanhMucSanPham = new SelectList(db.DanhMucSanPhams, "IdDanhMucSanPham", "TenDanhMucSanPham", sanPham.IdDanhMucSanPham);
            ViewBag.IdHinhSanPham = new SelectList(db.HinhSanPhams, "IdHinhSanPham", "FileHinhSanPham", sanPham.IdHinhSanPham);
            return View(sanPham);
        }

        // POST: AdminSanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSanPham,IdDanhMucSanPham,IdHinhSanPham,TenSanPham,MoTaSanPham,GiaSanPham")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDanhMucSanPham = new SelectList(db.DanhMucSanPhams, "IdDanhMucSanPham", "TenDanhMucSanPham", sanPham.IdDanhMucSanPham);
            ViewBag.IdHinhSanPham = new SelectList(db.HinhSanPhams, "IdHinhSanPham", "FileHinhSanPham", sanPham.IdHinhSanPham);
            return View(sanPham);
        }

        // GET: AdminSanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: AdminSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
