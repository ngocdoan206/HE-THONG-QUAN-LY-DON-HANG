using System;

namespace OrderManagement
{
    class Menu
    {
        public static void HienThiMenu()
        {
            QuanLyDonHang ql = new QuanLyDonHang();
            FileHandler file = new FileHandler(ql);

            int chon;
            do
            {
                Console.WriteLine("===== HỆ THỐNG QUẢN LÝ ĐƠN HÀNG =====");
                Console.WriteLine("1. Thêm đơn hàng");
                Console.WriteLine("2. Sửa thông tin đơn hàng");
                Console.WriteLine("3. Xóa đơn hàng");
                Console.WriteLine("4. Tìm kiếm đơn hàng");
                Console.WriteLine("5. Sắp xếp danh sách đơn hàng");
                Console.WriteLine("6. Thống kê doanh thu");
                Console.WriteLine("7. Lưu dữ liệu ra file");
                Console.WriteLine("8. Đọc dữ liệu từ file");
                Console.WriteLine("0. Thoát chương trình");
                Console.WriteLine("=====================================");
                Console.Write("Chọn chức năng: ");
                chon = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (chon)
                {
                    case 1: ql.ThemDonHang(); break;
                    case 2: ql.SuaDonHang(); break;
                    case 3: ql.XoaDonHang(); break;
                    case 4: ql.TimKiemDonHang(); break;
                    case 5: ql.SapXepDonHang(); break;
                    case 6: ql.ThongKeDoanhThu(); break;
                    case 7: file.GhiFile(); break;
                    case 8: file.DocFile(); break;
                    case 0: Console.WriteLine("Thoát chương trình."); break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
                }

                Console.WriteLine();
            } while (chon != 0);
        }
    }
}