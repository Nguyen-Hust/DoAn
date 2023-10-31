namespace WebAPI.Models.PhieuBanGiao
{
    public class PhieuBanGiaoDto
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        public int? NhanVienId { get; set; }
        public int NhanVienNhan { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? TongThietBi { get; set; }
        public List<int> DanhSachThietBi { get; set; }
    }
}
