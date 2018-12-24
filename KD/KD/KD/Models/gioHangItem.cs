using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KD.Models
{
    public class gioHangItem
    {
        public SanPham sanPham { get; set; }
        public int soLuong { get; set; }
        public gioHangItem() { }
        public gioHangItem(SanPham sanPham, int soLuong)
        {
            this.sanPham = sanPham;
            this.soLuong = soLuong;
        }
    }
}