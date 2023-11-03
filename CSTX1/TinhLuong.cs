using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTX1
{
    class TinhLuong
    {
        private string HoTen { get; set; }
        private string DiaChi { get; set; }
        public float HeSoLuong { get; set; }
        public static double luongCoBan = 1000000;
        public TinhLuong()
        {

        }
        public TinhLuong(string hoTen, string diaChi, float heSoLuong)
        {
            HoTen = hoTen;
            DiaChi = diaChi;
            HeSoLuong = heSoLuong;
        }
        public virtual double Luong()
        {
            return this.HeSoLuong * luongCoBan;
        }
        
        public void Nhap()
        {
            Console.Write("Nhap ho va ten: ");
            HoTen = Console.ReadLine();
            Console.Write("Nhap dia chi: ");
            DiaChi = Console.ReadLine();
            Console.Write("Nhap he so luong: ");
            HeSoLuong = int.Parse(Console.ReadLine());
        }
        public void Xuat()
        {
            Console.Write($"{HoTen,15} {DiaChi,20} {HeSoLuong,3}");
        }
        public static void TieuDe()
        {
            Console.Write($"{"Ho ten",15} {"Dia chi",20} {"HS Luong",15}");
        }
    }
}
