using System;
using System.Globalization;

namespace OrderManagement
{
    enum TrangThaiDonHang
    {
        Moi = 1,
        DangGiao,
        HoanThanh,
        Huy
    }

    class DonHang
    {
        public string MaDon { get; set; }
        public string TenKhach { get; set; }
        public DateTime NgayDat { get; set; }
        public double TongTien { get; set; }
        public TrangThaiDonHang TrangThai { get; set; }

        // ===== HÀM HỖ TRỢ =====
        private string ReadNonEmpty(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                    Console.WriteLine("❌ Không được để trống! Vui lòng nhập lại.");
            } while (string.IsNullOrEmpty(input));
            return input;
        }

        // ===== NHẬP =====
        public void Nhap()
        {
            MaDon = ReadNonEmpty("Nhập mã đơn hàng: ");
            TenKhach = ReadNonEmpty("Nhập tên khách hàng: ");

            // --- Ngày đặt ---
            while (true)
            {
                Console.Write("Nhập ngày đặt (dd/MM/yyyy): ");
                string input = Console.ReadLine()?.Trim();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime ngay))
                {
                    NgayDat = ngay;
                    break;
                }
                Console.WriteLine("❌ Sai định dạng ngày! Ví dụ: 10/10/2025");
            }

            // --- Tổng tiền ---
            while (true)
            {
                Console.Write("Nhập tổng tiền: ");
                if (double.TryParse(Console.ReadLine(), out double tien) && tien >= 0)
                {
                    TongTien = tien;
                    break;
                }
                Console.WriteLine("❌ Tổng tiền không hợp lệ! Vui lòng nhập lại (≥ 0)");
            }

            // --- Trạng thái ---
            while (true)
            {
                Console.Write("Chọn trạng thái (1.Mới  2.Đang giao  3.Hoàn thành  4.Hủy): ");
                if (int.TryParse(Console.ReadLine(), out int tt) && tt >= 1 && tt <= 4)
                {
                    TrangThai = (TrangThaiDonHang)tt;
                    break;
                }
                Console.WriteLine("❌ Lựa chọn không hợp lệ! Chỉ nhập 1–4.");
            }
        }

        // ===== XUẤT =====
        public void Xuat()
        {
            Console.WriteLine($"{MaDon,-10} | {TenKhach,-15} | {NgayDat:dd/MM/yyyy} | {TongTien,10:C0} | {TrangThai}");
        }
    }
}