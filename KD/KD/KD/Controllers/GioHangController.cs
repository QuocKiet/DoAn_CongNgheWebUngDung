using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KD.Models;

namespace KD.Controllers
{
    public class GioHangController : Controller
    {
        SonyEntities db = new SonyEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            var gioHang = Session["GioHang"] as Cart;
            if (gioHang == null || gioHang.TongSanPham == 0)
            {
                RedirectToAction("Index", "Home");
            }
            return View(gioHang);
        }

        public ActionResult ThemSanPham(int id, int sl = 1)
        {
            var gioHang = Session["GioHang"] as Cart;
            if (gioHang == null)
            {
                gioHang = new Cart();
                Session["GioHang"] = gioHang;
            }
            var item = db.SanPhams.Find(id);
            var item2 = new gioHangItem(item, sl);
            gioHang.addItem(item2);
            return RedirectToAction("Index");
        }

        public ActionResult XoaSanPham(int id)
        {
            var gioHang = Session["GioHang"] as Cart;
            gioHang.remove(id);
            return RedirectToAction("Index");
        }

        public ActionResult ChinhSua(int id, int sl)
        {
            var gioHang = Session["GioHang"] as Cart;
            gioHang.edit(id, sl);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatHang(NguoiDung ng)
        {
            var gioHang = Session["GioHang"] as Cart;
            if (gioHang == null || gioHang.TongSanPham == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            int soHDTonTai = db.DonHangs.Count();
            int soCTDHTonTai = db.ChiTietDonHangs.Count();
            int soNgDung = db.NguoiDungs.Count();
            try
            {
                ng.IdNguoiDung = soNgDung++;
                ng.IdKieuNguoiDung = 1;
                ng.TenDangNhap = "nguoiDung";
                ng.MatKhau = "matkhau";
                db.NguoiDungs.Add(ng);
                DonHang dh = new DonHang();
                dh.IdDonHang = soHDTonTai + 1;
                dh.IdNguoiDung = ng.IdNguoiDung;
                dh.IdTinhTrangDonHang = 1;
                dh.NgayTaoDonHang = DateTime.Now;
                dh.NgayXuLyDonHang = DateTime.Now;
                dh.TrackingNumber =(soHDTonTai + 1).ToString();
                dh.IdGiaoDich = dh.TrackingNumber;
                db.DonHangs.Add(dh);      
                
                for(int i = 0; i < gioHang.getLsCart.Count; i++)
                {
                    ChiTietDonHang ct = new ChiTietDonHang();
                    ct.IdChiTietDonHang = soCTDHTonTai + i;
                    ct.IdSanPham = gioHang.getLsCart[i].sanPham.IdSanPham;
                    ct.IdDonHang = dh.IdDonHang;
                    ct.SoLuongSanPham = gioHang.getLsCart[i].soLuong;                     
                    db.ChiTietDonHangs.Add(ct);
                }
                
                db.SaveChanges();
                gioHang.clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                string a = "Error" + ex;
                return RedirectToAction("Index");
            }

        }
    }
}