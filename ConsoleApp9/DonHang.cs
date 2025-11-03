namespace DeTaiQLDH
{
    public class DonHang
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public DateTime NgayTaoDon { get; set; }
        public List<MonAn> DanhSachMon { get; set; }
        public bool PhuongThucThanhToan { get; set; }
        public string TrangThai { get; set; }
        public DonHang()
        {
            DanhSachMon = new List<MonAn>();
        }
        public void ThemMon(MonAn mon)
        {
            DanhSachMon.Add(mon);
        }

        public decimal TongTien()
        {
            decimal tong = 0;
            foreach (var mon in DanhSachMon)
            {
                tong += mon.Gia;
            }
            return tong;
        }
        public void HienThiDonHang()
        {
            Console.WriteLine("\n ===== THÔNG TIN ĐƠN HÀNG =====");
            Console.WriteLine($"Mã đơn hàng: {ID}");
            Console.WriteLine($"Tên khách hàng: {CustomerName}");
            Console.WriteLine($"Ngày tạo đơn: {NgayTaoDon}");
            Console.WriteLine("\n---- Món đã chọn ----");
            foreach (var mon in DanhSachMon)
            {
                Console.WriteLine($"{mon.TenMon} - {mon.Gia:N0} VND");
            }
            Console.WriteLine($"Tổng tiền đơn hàng:{TongTien():N0} VND");
            Console.WriteLine($"Hình thức thanh toán: {(PhuongThucThanhToan ? "Chuyển khoản" : "Tiền mặt")}");
            Console.WriteLine("===============================================================================");
        }
    }
}