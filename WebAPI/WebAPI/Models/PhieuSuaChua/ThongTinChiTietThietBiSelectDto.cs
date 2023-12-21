namespace WebAPI.Models.PhieuSuaChua
{
    public class ThongTinChiTietThietBiSelectDto
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        public int? NhanVienId { get; set; }
        public bool? DaXuat { get; set; }
    }
}
