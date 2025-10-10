using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement
{
    class QuanLyDonHang
    {
        private List<DonHang> danhSach = new List<DonHang>();

        // === THÊM ===
        public void ThemDonHang()
        {
            DonHang dh = new DonHang();
            dh.Nhap();
            danhSach.Add(dh);
            Console.WriteLine("Đã thêm đơn hàng thành công!");
        }

        // === SỬA ===
        // === SỬA ===
public void SuaDonHang()
{
    Console.Write("Nhập mã đơn cần sửa: ");
    string ma = Console.ReadLine()?.Trim();
    DonHang dh = danhSach.FirstOrDefault(x => x.MaDon.Equals(ma, StringComparison.OrdinalIgnoreCase));

    if (dh == null)
    {
        Console.WriteLine("❌ Không tìm thấy đơn hàng có mã này.");
        return;
    }

    int chon;
    do
    {
        Console.WriteLine("\n===== CHỌN THÔNG TIN CẦN SỬA =====");
        Console.WriteLine("1. Mã đơn hàng");
        Console.WriteLine("2. Tên khách hàng");
        Console.WriteLine("3. Ngày đặt");
        Console.WriteLine("4. Tổng tiền");
        Console.WriteLine("5. Trạng thái");
        Console.WriteLine("0. Thoát chỉnh sửa");
        Console.Write("Chọn mục cần sửa: ");
        if (!int.TryParse(Console.ReadLine(), out chon)) chon = -1;

        switch (chon)
        {
            // ==== 1️⃣ SỬA MÃ ĐƠN ====
            case 1:
                Console.Write("Nhập mã đơn hàng mới: ");
                string maMoi = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(maMoi))
                {
                    Console.WriteLine("❌ Mã đơn hàng không được để trống!");
                }
                else if (danhSach.Any(x => x.MaDon.Equals(maMoi, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("❌ Mã đơn hàng này đã tồn tại! Vui lòng nhập mã khác.");
                }
                else
                {
                    dh.MaDon = maMoi;
                    Console.WriteLine("✅ Đã cập nhật mã đơn hàng.");
                }
                break;

            // ==== 2️⃣ SỬA TÊN KHÁCH ====
            case 2:
                Console.Write("Nhập tên khách hàng mới: ");
                string tenMoi = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(tenMoi))
                {
                    dh.TenKhach = tenMoi;
                    Console.WriteLine("✅ Đã cập nhật tên khách hàng.");
                }
                else
                    Console.WriteLine("❌ Tên khách hàng không hợp lệ!");
                break;

            // ==== 3️⃣ SỬA NGÀY ĐẶT ====
            case 3:
                Console.Write("Nhập ngày đặt mới (dd/MM/yyyy): ");
                while (true)
                {
                    string input = Console.ReadLine()?.Trim();
                    if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngay))
                    {
                        dh.NgayDat = ngay;
                        Console.WriteLine("✅ Đã cập nhật ngày đặt.");
                        break;
                    }
                    else
                        Console.Write("❌ Sai định dạng! Nhập lại (ví dụ: 10/10/2025): ");
                }
                break;

            // ==== 4️⃣ SỬA TỔNG TIỀN ====
            case 4:
                Console.Write("Nhập tổng tiền mới: ");
                while (true)
                {
                    string input = Console.ReadLine()?.Trim();
                    if (double.TryParse(input, out double tong) && tong >= 0)
                    {
                        dh.TongTien = tong;
                        Console.WriteLine("✅ Đã cập nhật tổng tiền.");
                        break;
                    }
                    else
                        Console.Write("❌ Tổng tiền không hợp lệ! Nhập lại (≥ 0): ");
                }
                break;

            // ==== 5️⃣ SỬA TRẠNG THÁI ====
            case 5:
                Console.WriteLine("Chọn trạng thái mới: 1.Mới  2.Đang giao  3.Hoàn thành  4.Hủy");
                while (true)
                {
                    string input = Console.ReadLine()?.Trim();
                    if (int.TryParse(input, out int tt) && tt >= 1 && tt <= 4)
                    {
                        dh.TrangThai = (TrangThaiDonHang)tt;
                        Console.WriteLine("✅ Đã cập nhật trạng thái đơn hàng.");
                        break;
                    }
                    else
                        Console.Write("❌ Lựa chọn không hợp lệ! Nhập lại (1-4): ");
                }
                break;

            // ==== THOÁT ====
            case 0:
                Console.WriteLine("➡ Thoát chỉnh sửa.");
                break;

            default:
                Console.WriteLine("❌ Lựa chọn không hợp lệ! Hãy chọn từ 0–5.");
                break;
        }

    } while (chon != 0);
}

        // === XÓA ===
        public void XoaDonHang()
        {
            Console.Write("Nhập mã đơn cần xóa: ");
            string ma = Console.ReadLine();
            DonHang dh = danhSach.FirstOrDefault(x => x.MaDon == ma);

            if (dh != null)
            {
                danhSach.Remove(dh);
                Console.WriteLine("Đã xóa đơn hàng.");
            }
            else Console.WriteLine("Không tìm thấy đơn hàng.");
        }

        // === TÌM KIẾM CHUNG ===
        public void TimKiemDonHang()
        {
            Console.Write("Nhập mã đơn hoặc tên khách cần tìm: ");
            string key = Console.ReadLine().ToLower();

            var ketQua = danhSach.Where(x => x.MaDon.ToLower().Contains(key) ||
                                             x.TenKhach.ToLower().Contains(key)).ToList();

            if (ketQua.Count > 0)
            {
                Console.WriteLine("Kết quả tìm kiếm:");
                ketQua.ForEach(x => x.Xuat());
            }
            else Console.WriteLine("Không tìm thấy đơn hàng nào.");
        }

        // === TÌM KIẾM THEO MÃ - DÙNG OUT ===
        public bool TimKiemDonHangTheoMa(string ma, out DonHang found)
        {
            foreach (var dh in danhSach)
            {
                if (dh.MaDon.Equals(ma, StringComparison.OrdinalIgnoreCase))
                {
                    found = dh;
                    return true;
                }
            }
            found = null;
            return false;
        }

        // === NẠP CHỒNG (OVERLOADING) HÀM TÌM KIẾM ===
        // 1️⃣ Tìm kiếm theo mã đơn hàng
        public void TimKiemDonHang(string ma)
        {
            var ketQua = danhSach.Where(x => x.MaDon.Equals(ma, StringComparison.OrdinalIgnoreCase)).ToList();

            if (ketQua.Count > 0)
            {
                Console.WriteLine("Kết quả tìm kiếm theo mã:");
                ketQua.ForEach(x => x.Xuat());
            }
            else Console.WriteLine("Không tìm thấy đơn hàng có mã này.");
        }

        // 2️⃣ Tìm kiếm theo tên khách hàng
        public void TimKiemDonHangTheoTen(string ten)
        {
            var ketQua = danhSach.Where(x => x.TenKhach.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (ketQua.Count > 0)
            {
                Console.WriteLine("Kết quả tìm kiếm theo tên khách:");
                ketQua.ForEach(x => x.Xuat());
            }
            else Console.WriteLine("Không tìm thấy khách hàng này.");
        }

        // === SẮP XẾP ===
        public void SapXepDonHang()
        {
            Console.WriteLine("Sắp xếp theo: 1. Tổng tiền  2. Ngày đặt");
            int chon = Convert.ToInt32(Console.ReadLine());

            if (chon == 1)
                danhSach = danhSach.OrderBy(x => x.TongTien).ToList();
            else if (chon == 2)
                danhSach = danhSach.OrderBy(x => x.NgayDat).ToList();

            Console.WriteLine("Danh sách sau khi sắp xếp:");
            danhSach.ForEach(x => x.Xuat());
        }

        // === THỐNG KÊ ===
        public void ThongKeDoanhThu()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Chưa có đơn hàng.");
                return;
            }

            double[,] thongKe = new double[4, 2]; // 4 trạng thái, 2 cột: SL, Tổng tiền
            foreach (var dh in danhSach)
            {
                int index = (int)dh.TrangThai - 1;
                thongKe[index, 0]++;
                thongKe[index, 1] += dh.TongTien;
            }

            Console.WriteLine("===== THỐNG KÊ DOANH THU THEO TRẠNG THÁI =====");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{(TrangThaiDonHang)(i + 1),-12} | SL: {thongKe[i, 0],3} | Doanh thu: {thongKe[i, 1],10:C0}");
            }

            // Gọi thử hàm có tham số mặc định để hiển thị tổng doanh thu
            double tong = TinhTongDoanhThu(); // mặc định bỏ qua đơn hủy
            Console.WriteLine($"\nTổng doanh thu (không tính đơn hủy): {tong:C0}");
        }

        // === HÀM CÓ THAM SỐ MẶC ĐỊNH ===
        // Nếu includeCancelled = false → bỏ qua đơn bị hủy
        // Nếu includeCancelled = true  → tính cả đơn bị hủy
        public double TinhTongDoanhThu(bool includeCancelled = false)
        {
            double tong = 0;
            foreach (var dh in danhSach)
            {
                if (includeCancelled || dh.TrangThai != TrangThaiDonHang.Huy)
                    tong += dh.TongTien;
            }
            return tong;
        }

        // === LẤY DANH SÁCH (cho FileHandler) ===
        public List<DonHang> LayDanhSach() => danhSach;

        // === Gán lại danh sách khi đọc file ===
        public void GanDanhSach(List<DonHang> ds) => danhSach = ds;
    }
}