using Microsoft.VisualBasic.FileIO;

namespace DeTaiQLDH
{
    public class MonAn//Khai báo class MonAn
    {
        public string IDMon { get; set; }// Thuộc tính lưu ID món ăn
        public string TenMon { get; set; }// Thuộc tính lưu tên món ăn
        public decimal Gia { get; set; }// Thuộc tính lưu giá món ăn
        public MonAn(string ma, string tenMon, decimal gia) 
        //Hàm tự động chạy khi tạo đối tượng MonAn.
        // Nhận 3 tham số (ma, ten, gia) để gán cho các thuộc tính
        {
            IDMon = ma;
            TenMon = tenMon;
            Gia = gia;
        }
    }
}