﻿﻿using System;
using System.Collections.Generic;
using System.Text;
namespace DeTaiQLDH
{
    public class Menu
    {
        static void Main(string[] args)//Hàm main là điểm bắt đầu chạy chương trình
        {
            //Cho phép code chạy được tiếng Việt không bị lỗi
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            //Gọi các chức năng
            QuanLyDonHang quanLyDonHang = new QuanLyDonHang();
            //Tạo menu
            while (true)
            {
                Console.WriteLine("\nHỆ THỐNG QUẢN LÝ ĐƠN HÀNG");
                Console.WriteLine("*************************MENU**************************");
                Console.WriteLine("**  1. Thêm đơn hàng.                                **");
                Console.WriteLine("**  2. Cập nhật thông tin đơn hàng theo ID.          **");
                Console.WriteLine("**  3. Xóa đơn hàng theo ID.                         **");
                Console.WriteLine("**  4. Hiển thị danh sách đơn hàng                   **");
                Console.WriteLine("**  5. Tìm kiếm đơn hàng.                            **");
                Console.WriteLine("**  6. Sắp xếp đơn hàng.                             **");
                Console.WriteLine("**  7. Hiển thị danh sách sinh viên.                 **");
                Console.WriteLine("**  0. Thoát.                                        **");
                Console.WriteLine("*******************************************************");
                Console.Write("Nhập tùy chọn: ");
                int key = Convert.ToInt32(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        Console.WriteLine("1. Thêm đơn hàng.");
                        quanLyDonHang.TaoDonHang();
                        Console.WriteLine("Thêm đơn hàng thành công!");
                        break;
                    case 2:
                        Console.WriteLine("2. Cập nhật đơn hàng");
                        quanLyDonHang.CapNhatDonHang();
                        Console.WriteLine("Cập nhật đơn hàng thành công");
                        break;
                    case 3:
                        Console.WriteLine("3. Xóa đơn hàng");
                        quanLyDonHang.XoaDonHang();
                        break;
                    case 4:
                        Console.WriteLine("4. Hiển thị danh sách đơn hàng.");
                        quanLyDonHang.HienThiTatCaDonHang();
                        break;
                    case 5:
                        Console.WriteLine("5.Tìm kiếm đơn hàng");
                        quanLyDonHang.TimKiemDonHang();
                        break;
                    case 6:
                        Console.WriteLine("6. Sắp xếp đơn hàng.");
                        quanLyDonHang.SapXepDonHang();
                        break;
                }
            }
        }
    }
}