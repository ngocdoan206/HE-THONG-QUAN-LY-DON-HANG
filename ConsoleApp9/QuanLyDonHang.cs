using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;
namespace DeTaiQLDH
{
    public class QuanLyDonHang
    {
        private List<DonHang> ListDonHang = null;

        //Gọi danh sách đơn hàng ra
        public QuanLyDonHang()
        {
            ListDonHang = new List<DonHang>();
        }
        

        //Tạo đơn hàng
        public void TaoDonHang()
        {
            DonHang dh = new DonHang();
            Console.Write("Nhập mã ID đơn hàng: "); //cái này sau sửa lại thành tự cập nhật ID sau
            dh.ID = int.Parse(Console.ReadLine()!);// Nhận mã ID từ bàn phím

            Console.Write("Nhập tên khách hàng: ");// Yêu cầu nhập tên khách hàng
            dh.CustomerName = Console.ReadLine()!;// Nhận tên khách hàng từ bàn phím

            DateTime ngayTao = DateTime.Now;// Lưu thời gian hiện tại làm ngày tạo đơn
            //Gọi menu từ class MenuMonAn
            MenuMon menuMon = new MenuMon();
            menuMon.HienThiMenu(); // Hiển thị menu ra màn hình
            // Cho người dùng chọn món
            Console.Write("\nNhập mã món muốn chọn (ngăn bằng dấu cách): ");// yêu cầu người dùng nhập mã món ngăn bằng dấu cách
            string input = Console.ReadLine()!;//Nhận mã món từ bàn phím
            string[] choice = input.Split(" ");// chia chuỗi đó thành từng mã món riêng bằng dấu cách
            // Thêm món được chọn vào đơn hàng
            List<MonAn> danhSachMon = menuMon.LayDanhSachMon();
            foreach (string maMon in choice)
            {
            MonAn mon = danhSachMon.Find (m => m.IDMon.Equals(maMon, StringComparison.OrdinalIgnoreCase));//Tìm trong danh sách xem có món nào có mã trùng với maMon không (không phân biệt hoa thường)
            }
        //Chọn hình thức thanh toán
            Console.Write("Chọn hình thức thanh toán (1: Tiền mặt, 2: Chuyển khoản): ");
            string phuongThuc = Console.ReadLine()!;
            dh.PhuongThucThanhToan = phuongThuc == "2";//Nếu nhập 2 đặt PhuongThucThanhToan = true (chuyển khoản), nếu nhập 1 là false (tiền mặt)                                          
        //Mặc định trạng thái là "Đang xử lý";
            dh.TrangThai = "Đang xử lý";
        //Thêm đơn hàng vào danh sách
            ListDonHang.Add(dh);//Thêm đơn hàng vừa tạo vào danh sách quản lý
            Console.WriteLine("\nĐơn hàng đã được tạo thành công!");
            dh.HienThiDonHang();//in thông báo thành công và hiển thị thông tin chi tiết đơn hàng ra màn hình
        }


        //Cập nhật đơn hàng
        public void CapNhatDonHang()
        {
            Console.Write("Nhập ID đơn hàng cần cập nhật: ");//Yêu cầu người dùng nhập vào mã đơn hàng
            if (!int.TryParse(Console.ReadLine(), out int idCanCapNhat))//Chuyển chuỗi ng dùng nhập thành số nguyên
                                                                        // Nếu người dùng nhập không phải là số nguyên hợp lệ, thì báo lỗi và dừng việc cập nhật đơn hàng.
            {
                Console.WriteLine("ID không hợp lệ!");
                return;
            }
            DonHang donHangCanSua = ListDonHang.Find(dh => dh.ID == idCanCapNhat);
            if (donHangCanSua.ID == 0 && donHangCanSua.CustomerName == null) //Kiểm tra mã đơn hàng có bằng 0 hay tên khách hàng có bị null (chưa có giá trị) hay không
            {
                Console.WriteLine("Không tìm thấy đơn hàng!"); //Nếu điều kiện đúng in ra không tìm thấy đơn hàng và dừng việc cập nhật
                return;
            }
            // Nếu như đơn hàng tồn tại thì
            Console.WriteLine($"Mã đơn hàng: {donHangCanSua.ID}");// In ra mã đơn hàng
            Console.WriteLine($"Khách hàng: {donHangCanSua.CustomerName}");// In tên khách hàng
            Console.WriteLine($"Ngày tạo: {donHangCanSua.NgayTaoDon}");// In ngày tạo
            Console.WriteLine($"Trạng thái: {donHangCanSua.TrangThai}");// In trạng thái đơn hàng
            Console.WriteLine("Danh sách món:");// In danh sách món đã tạo

            foreach (var mon in donHangCanSua.DanhSachMon)//Duyệt qua từng món ăn trong danh sách món
                Console.WriteLine($"- {mon.TenMon} ({mon.Gia:N0} VND)");//Rồi in ra tên từng món ăn
            Console.WriteLine("Bạn muốn cập nhật phần nào?");//Sau khi in xong danh sách món thì hỏi ng dùng muốn cập nhật phần nào
            Console.WriteLine("1. Tên khách hàng");//Cập nhật lại tên
            Console.WriteLine("2. Danh sách món");//Cập nhật lại danh sách món
            Console.WriteLine("3. Trạng thái đơn hàng");//Cập nhật lại trạng thái của đơn hàng
            Console.Write("Chọn: ");
            string userchoice = Console.ReadLine();//Đọc lựa chọn người dùng nhập vào từ bàn phím

            switch (userchoice)
            {
                case "1"://Chọn cập nhật lại tên KH
                    Console.Write("Nhập tên khách hàng mới: ");
                    string newName = Console.ReadLine();//Lưu tên khách hàng mới mà ng dùng vừa nhập vào biến newName.
                    if (!string.IsNullOrWhiteSpace(newName))//Nếu như tên KH vừa nhập không phải khoảng trắng, không rỗng 
                    {
                        donHangCanSua.CustomerName = newName;//Thì cập nhật lại tên KH trong đơn hàng bằng tên mới ng dùng vừa nhập.
                        Console.WriteLine("Cập nhật tên khách hàng thành công!");
                    }
                    else Console.WriteLine("Tên không được để trống!");//Nếu đk sai thì in dòng này
                    break;

                case "2"://Cập nhật danh sách món
                    donHangCanSua.DanhSachMon.Clear();//Xóa toàn bộ các món cũ trong đơn hàng trước khi thêm món mới.
                    Console.WriteLine("Nhập danh sách món mới:");
                    int thutumon = 1;//Dùng để đánh số thứ tự món ăn. Mỗi khi thêm một món mới, thutumon sẽ tăng lên.
                    while (true)//Đây là vòng lặp vô hạn, vì chưa biết ng dùng muốn nhập bao nhiêu món.Và nó chỉ dừng lại khi ng dùng không nhập tên món nữa (để trống và nhấn Enter).
                    {
                        Console.Write($"Tên món {thutumon}: ");
                        string tenMon = Console.ReadLine();//Nhận tên món ăn ng dùng nhập vào từ bàn phím.
                        if (string.IsNullOrWhiteSpace(tenMon))
                            break;//Kiểm tra nếu ng dùng để trống, hoặc nhập toàn khoảng trắng thì thoát khỏi vòng lặp (dừng nhập món).

                        Console.Write("Giá món: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal giaMon))//Đọc giá món người dùng nhập, chuyển chuỗi nhập được sang decimal
                            giaMon = 0;//Nếu người dùng nhập đúng kiểu số → giaMon nhận giá trị đó.
                                       //Nếu người dùng nhập sai → TryParse() trả về false → giaMon = 0 (gán giá mặc định là 0 để tránh lỗi)

                        donHangCanSua.DanhSachMon.Add(new MonAn("M" + thutumon.ToString("D2"), tenMon, giaMon));//Tạo một món ăn mới gồm stt, tên món, giá sau đó thêm nó vào danh sách của đơn hàng.
                        thutumon++;
                    }
                    Console.WriteLine("Cập nhật danh sách món thành công!");//In thông báo cập nhật thành công
                    break;

                case "3": //Cập nhật trạng thái đơn hàng
                    Console.WriteLine("Chọn trạng thái mới:");
                    Console.WriteLine("1. Hoàn tất");
                    Console.WriteLine("2. Đã hủy");
                    Console.WriteLine("3. Đang xử lý");
                    Console.Write("Chọn: ");
                    string trangthai = Console.ReadLine();//Đọc chuỗi người dùng nhập từ bàn phím và lưu vào biến status.
                    switch (trangthai)
                    {
                        case "1":
                            donHangCanSua.TrangThai = "Hoàn tất";
                            break;//Nếu ng dùng nhập "1" → thay đổi trạng thái đơn hàng thành Hoàn tất.
                        case "2":
                            donHangCanSua.TrangThai = "Đã hủy";
                            break;//Nếu nhập "2" → trạng thái là Đã hủy.
                        case "3":
                            donHangCanSua.TrangThai = "Đang xử lý";
                            break;//Nếu nhập "3" → trạng thái là Đang xử lý.
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ!");
                            return;//Nếu nhập khác → in ra thông báo lỗi “Lựa chọn không hợp lệ!” và return để thoát khỏi hàm, không cập nhật gì.
                    }
                    Console.WriteLine("Cập nhật trạng thái thành công!");//Sau khi cập nhật hợp lệ, chương trình in thông báo xác nhận cho ng dùng.
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");//Trường hợp người dùng nhập ngoài “1”, “2”, “3” ở switch (trangthai) ban đầu, thì chương trình báo lỗi “Lựa chọn không hợp lệ!”.
                    break;
            }
            // Ghi lại thay đổi vào danh sách
            for (int i = 0; i < ListDonHang.Count; i++)//Tìm trong danh sách xem đơn nào có cùng mã số (ID) với đơn hàng mà ng dùng vừa cập nhật.
            {
                if (ListDonHang[i].ID == donHangCanSua.ID)//So sánh mã của đơn hàng trong danh sách với mã của đơn vừa cập nhật. Nếu trùng, nghĩa là đây chính là đơn hàng cần thay thế trong danh sách.
                {
                    ListDonHang[i] = donHangCanSua;//Cập nhật dữ liệu mới vào đúng vị trí trong danh sách
                    break;
                }
            }
        }


        //Xóa đơn hàng
        public void XoaDonHang()
        {
            Console.Write("\n Nhập mã đơn hàng cần xóa: ");
            int maXoa = int.Parse(Console.ReadLine());

            var donCanXoa = ListDonHang.FirstOrDefault(d => d.ID == maXoa);

            if (donCanXoa != null)
            {
                Console.Write($"Bạn có chắc chắn muốn xóa đơn hàng {maXoa} của {donCanXoa.CustomerName} không? (y/n): " );
                string confirm = Console.ReadLine();

                if (confirm == "y")
                {
                    ListDonHang.Remove(donCanXoa);
                    Console.WriteLine("Đơn hàng đã được xóa thành công!");
                }
                else
                {
                    Console.WriteLine("Hủy thao tác xóa đơn hàng");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy đơn hàng");
            }
        }
        

        //Hiển thị danh sách đơn hàng
        public void HienThiTatCaDonHang()
        {
            if (ListDonHang.Count == 0)
            {
                Console.WriteLine("\nHiện chưa có đơn hàng nào trong hệ thống.");
                return;
            }
            HienThiDanhSachDonHang.HienThi(ListDonHang); // Gọi phần hiển thị danh sách
        }

        //Tìm kiếm đơn hàng
        public void TimKiemDonHang()
        {
            Console.WriteLine("===TÌM KIẾM ĐƠN HÀNG===");
            Console.WriteLine("1. Tìm kiếm theo MÃ ĐƠN HÀNG");
            Console.WriteLine("2. Tìm kiếm theo TÊN KHÁCH HÀNG");
            Console.WriteLine("3. Tìm kiếm theo MÓN ĂN");
            Console.Write("Chọn chức năng: ");
            string luachon = Console.ReadLine();

            if (!int.TryParse(luachon, out int luaChon))
            {
                Console.WriteLine("Lựa chọn không hợp lệ!");
                return;
            }

            List<DonHang> ketQua = new List<DonHang>();

            switch (luaChon)
            {
                case 1:
                    Console.WriteLine("Nhập ID đơn hàng cần tìm: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    ketQua = ListDonHang.Where(dh => dh.ID == id).ToList();
                    break;
                case 2:
                    Console.Write("Nhập tên khách hàng cần tìm: ");
                    string ten = Console.ReadLine()!;
                    ketQua = ListDonHang
                        .Where(dh => dh.CustomerName.Contains(ten, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;
                case 3:
                     Console.Write("Nhập tên món ăn cần tìm: ");
                    string mon = Console.ReadLine()!;
                    ketQua = ListDonHang
                        .Where(dh => dh.DanhSachMon.Any(m => m.TenMon.Contains(mon, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    return;
            }

            if (ketQua.Count == 0)
                Console.WriteLine("Không tìm thấy đơn hàng nào phù hợp!");
            else
            {
                Console.WriteLine($"\nTìm thấy {ketQua.Count} đơn hàng:");
                foreach (var dh in ketQua)
                    dh.HienThiDonHang();
            }
        }
    }
}




