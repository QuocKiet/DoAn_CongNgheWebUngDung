using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KD.Models
{
    public class Cart
    {
        private List<gioHangItem> lsCartItem = new List<gioHangItem>();
        public List<gioHangItem> getLsCart
        {
            get { return lsCartItem; }
        }

        public void addItem(gioHangItem item)
        {
            var checkExisting = getLsCart.Find(p => p.sanPham.IdSanPham == item.sanPham.IdSanPham);
            if (checkExisting == null)
            {
                lsCartItem.Add(item);
            }
            else
            {
                item.soLuong++;
            }
        }

        public void remove(int id)
        {
            var item = getLsCart.Find(p => p.sanPham.IdSanPham == id);
            lsCartItem.Remove(item);
        }

        public void clear()
        {
            lsCartItem.Clear();
        }

        public void edit(int id, int sl)
        {
            var item = lsCartItem.Find(p => p.sanPham.IdSanPham == id);
            item.soLuong = sl;
        }

        public int TongSanPham
        {
            get { return lsCartItem.Count; }
        }

        public int TongGiaTri
        {
            get
            {
                int kq = 0;
                kq = lsCartItem.Sum(p => p.sanPham.GiaSanPham * p.soLuong);
                return kq;
            }
        }
    }
}