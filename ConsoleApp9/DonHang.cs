using System;
using System.Text;
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

        // Đọc một chuỗi ngày với auto-format dd/MM/yyyy, chấp nhận số và '/'
        private string ReadDateInput()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return sb.ToString();
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        // Xóa ký tự cuối cùng
                        sb.Remove(sb.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    char c = key.KeyChar;

                    // Chỉ chấp nhận chữ số hoặc '/'
                    if (char.IsDigit(c))
                    {
                        // giới hạn tổng độ dài (dd/MM/yyyy => 10)
                        if (sb.Length < 10)
                        {
                            sb.Append(c);
                            Console.Write(c);

                            // Tự động chèn '/' sau khi nhập 2 và 4 chữ số (đếm theo số chữ số đã nhập + dấu)
                            // Khi length == 2 hoặc 5 (0-based length), sau 2 ký tự ta chèn '/'
                            if (sb.Length == 2 || sb.Length == 5)
                            {
                                if (sb.Length < 10) // vẫn còn chỗ cho '/'
                                {
                                    sb.Append('/');
                                    Console.Write('/');
                                }
                            }
                        }
                    }
                    else if (c == '/')
                    {
                        // Nếu người dùng tự gõ '/', chỉ cho phép khi vị trí cần '/' (sau 2 hoặc 5 ký tự)
                        if (sb.Length == 2 || sb.Length == 5)
                        {
                            if (sb.Length < 10)
                            {
                                sb.Append('/');
                                Console.Write('/');
                            }
                        }
                        // nếu vị trí không hợp lệ thì bỏ qua ký tự '/'
                    }
                    else
                    {
                        // bỏ qua ký tự lạ (có thể báo beep nếu muốn)
                        // Console.Beep();
                    }
                }
            }
        }

        public void Nhap()
        {
            // === Nhập mã đơn ===
            do
            {
                Console.Write("Nhập mã đơn hàng: ");
                MaDon = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(MaDon))
                    Console.WriteLine("❌ Mã đơn hàng không được để trống!");
            } while (string.IsNullOrEmpty(MaDon));

            // === Nhập tên khách ===
            do
            {
                Console.Write("Nhập tên khách hàng: ");
                TenKhach = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(TenKhach))
                    Console.WriteLine("❌ Tên khách hàng không được để trống!");
            } while (string.IsNullOrEmpty(TenKhach));

            // === Nhập ngày đặt (dùng ReadDateInput để tránh "loạn" khi gõ/paste) ===
            while (true)
            {
                Console.Write("Nhập ngày đặt (dd/MM/yyyy): ");
                string raw = ReadDateInput()?.Trim();

                // Loại bỏ khoảng trắng thừa và những '/' lặp
                raw = raw?.Replace(" ", "");
                if (string.IsNullOrEmpty(raw))
                {
                    Console.WriteLine("❌ Không được để trống! Vui lòng nhập lại.");
                    continue;
                }

                // Chuẩn hóa (nếu người dùng vô tình gõ //)
                while (raw.Contains("//")) raw = raw.Replace("//", "/");

                // Nếu người dùng nhập ddMMyyyy (không có /) thì tự chèn
                if (raw.Length == 8 && raw.IndexOf('/') == -1)
                {
                    raw = raw.Insert(2, "/").Insert(5, "/"); // dd/MM/yyyy
                }

                if (DateTime.TryParseExact(raw, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime ngay))
                {
                    NgayDat = ngay;
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Sai định dạng ngày! Ví dụ đúng: 10/10/2025");
                }
            }

            // === Nhập tổng tiền ===
            while (true)
            {
                Console.Write("Nhập tổng tiền: ");
                string input = Console.ReadLine()?.Trim();
                if (double.TryParse(input, out double tien) && tien >= 0)
                {
                    TongTien = tien;
                    break;
                }
                else
                    Console.WriteLine("❌ Tổng tiền không hợp lệ! Vui lòng nhập lại (chỉ nhập số, ≥ 0)");
            }

            // === Nhập trạng thái ===
            while (true)
            {
                Console.WriteLine("Chọn trạng thái: 1.Mới  2.Đang giao  3.Hoàn thành  4.Hủy");
                string input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out int tt) && tt >= 1 && tt <= 4)
                {
                    TrangThai = (TrangThaiDonHang)tt;
                    break;
                }
                else
                    Console.WriteLine("❌ Lựa chọn không hợp lệ! Chỉ nhập 1, 2, 3 hoặc 4.");
            }
        }

        public void Xuat()
        {
            Console.WriteLine($"{MaDon,-10} | {TenKhach,-15} | {NgayDat:dd/MM/yyyy} | {TongTien,10:C0} | {TrangThai}");
        }
    }
}