using System;
using System.Collections.Generic;
using System.IO;

namespace OrderManagement
{
    class FileHandler
    {
        private readonly QuanLyDonHang ql;
        private const string filePath = "donhang.txt";

        public FileHandler(QuanLyDonHang ql)
        {
            this.ql = ql;
        }

        public void GhiFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Copy(filePath, filePath + ".bak", true); // backup
                }

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var dh in ql.LayDanhSach())
                    {
                        sw.WriteLine($"{dh.MaDon};{dh.TenKhach};{dh.NgayDat:dd/MM/yyyy};{dh.TongTien};{dh.TrangThai}");
                    }
                }
                Console.WriteLine("Đã ghi dữ liệu ra file thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi file: " + ex.Message);
            }
        }

        public void DocFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File dữ liệu không tồn tại.");
                    return;
                }

                List<DonHang> ds = new List<DonHang>();
                foreach (string line in File.ReadAllLines(filePath))
                {
                    var p = line.Split(';');
                    DonHang dh = new DonHang
                    {
                        MaDon = p[0],
                        TenKhach = p[1],
                        NgayDat = DateTime.ParseExact(p[2], "dd/MM/yyyy", null),
                        TongTien = double.Parse(p[3]),
                        TrangThai = Enum.Parse<TrangThaiDonHang>(p[4])
                    };
                    ds.Add(dh);
                }

                ql.GanDanhSach(ds);
                Console.WriteLine("Đã đọc dữ liệu từ file thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file: " + ex.Message);
            }
        }
    }
}