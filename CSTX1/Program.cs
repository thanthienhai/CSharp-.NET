using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTX1
{
    class Program
    {
        public static List<NhanVien> dsnv = new List<NhanVien>();
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            int luaChon;
            do
            {
                Console.WriteLine("========MENU========");
                Console.WriteLine("1. Thêm");
                Console.WriteLine("2. Hiển thị danh sách");
                Console.WriteLine("3. Sắp xếp");
                Console.WriteLine("4. Thoát!");

                Console.WriteLine("Mời nhập lựa chọn: ");
                luaChon = int.Parse(Console.ReadLine());

                switch (luaChon)
                {
                    case 1:
                        try
                        {
                            them();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            hienThiDS();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            sapXep();
                            Console.WriteLine("Danh sách sau khi sắp xếp:");
                            hienThiDS();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Kết thúc chương trình !!!");
                        break;
                }
            } while (luaChon != 4);
        }
        public static void them()
        {
            NhanVien nv = new NhanVien();
            nv.Nhap();
            foreach(var item in dsnv)
            {
                if (nv.Equals(item))
                {
                    throw new Exception("Nha vien da ton tai !!!");
                }
            }
            dsnv.Add(nv);
            Console.WriteLine("Them nhan vien thanh cong");
        }
        public static void hienThiDS()
        {
            if (dsnv.Count == 0)
            {
                throw new Exception("Danh sach nhan vien rong !!!");
            }
            NhanVien.TieuDe();
            foreach(NhanVien nv in dsnv)
            {
                nv.Xuat();
            }
        }

        public static void sapXep()
        {
            dsnv.Sort();
        }
    }
}
