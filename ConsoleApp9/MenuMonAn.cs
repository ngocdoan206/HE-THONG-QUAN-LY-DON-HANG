namespace DeTaiQLDH
{
    public class MenuMon
    {
        List<MonAn> menu = new List<MonAn>()//Tạo danh sách các món ăn
        {
            new MonAn("M01","Cơm tấm sườn", 35000), new MonAn("M02", "Phở bò", 40000), new MonAn("M03", "Bún bò Huế", 35000), new MonAn("M04", "Mì cay", 55000)
        };
    public void HienThiMenu ()
        {
            //In danh sách menu cho người dùng xem, mỗi dòng gồm mã món, tên món và giá
            Console.WriteLine ("\n----DANH SÁCH MÓN ĂN----");
             foreach (var mon in menu)
            {
                Console.WriteLine($"{mon.IDMon} - {mon.TenMon,-25} - {mon.Gia:N0} VND");
            }
    
        }
        public List<MonAn> LayDanhSachMon()
        {
            return menu;
        }
    }
}