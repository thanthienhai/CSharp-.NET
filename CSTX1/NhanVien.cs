using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTX1
{
    class NhanVien:TinhLuong
    {
        private string MaNV { get; set; }
        private string ChucVu { get; set; }

        public double PhuCapChucVu()
        {
            if (this.ChucVu.Equals("Giám đốc"))
                return 0.5;
            else if (this.ChucVu.Equals("Trưởng phòng") || this.ChucVu.Equals("Phó giám đốc"))
                return 0.4;
            else if (this.ChucVu.Equals("Phó phòng"))
                return 0.3;
            else
                return 0;
        }

        public override double Luong()
        {
            return (HeSoLuong + PhuCapChucVu()) * luongCoBan;
        }
        public void Nhap()
        {
            Console.WriteLine("Nhap ma nhan vien: ");
            MaNV = Console.ReadLine();
            base.Nhap();
            int chonChucVu;
            do
            {
                Console.WriteLine("Chọn chức vụ:\n1. Giám đốc\n2. Trưởng phòng\n3. Phó giám đốc\n4. Phó phòng\n5. Các chức vụ khác");
                chonChucVu = int.Parse(Console.ReadLine());
                if (chonChucVu == 1) ChucVu = "Giám đốc";
                else if (chonChucVu == 2) ChucVu = "Trưởng phòng";
                else if (chonChucVu == 3) ChucVu = "Phó giám đốc";
                else if (chonChucVu == 4) ChucVu = "Phó phòng";
                else ChucVu = "Các chức vụ khác";
            } while (chonChucVu != 1 && chonChucVu != 2 && chonChucVu != 3 && chonChucVu != 4 && chonChucVu != 5);
        }
        public void Xuat()
        {
            Console.Write($"{MaNV, 10}");
            base.Xuat();
            Console.Write($"{ChucVu,20}, {PhuCapChucVu(),15}, {Luong(),15}");
        }
        public void TieuDeX()
        {
            Console.Write($"{"Mã nhân viên",10}");
            TieuDe();
            Console.Write($"{"Chức vụ",20}, {"Phụ cấp",15}, {"Lương",15}");
        }
    }
}
