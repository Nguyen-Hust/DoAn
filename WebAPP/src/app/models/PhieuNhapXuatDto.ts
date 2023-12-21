export class PhieuNhapXuatDto {
  id: number;
  ma: string;
  ngayNhapXuat: Date;
  nhaCungCap: string;
  nguoiDaiDien: string;
  nhanVienId: number;
  soLuong: number | null;
  tongTien: number | null;
  ghiChu: string;
  loaiPhieu: number;
  thongTinChiTietThietBiDtos: ThongTinChiTietThietBiDto[] | null;
}

export class ThongTinChiTietThietBiDto {
  id: number | any;
  ma: string;
  thietBiYTeId: number;
  ngayNhap: Date | null;
  xuatXu: string;
  namSX?: number | null;
  hangSanXuat: string;
  tinhTrang: number | null;
  khoaId: number | null;
  nhanVienId: number | null;
  serial: string;
  model: string;
  giaTien: number | null;
  thoiGianBaoDuong: number | null;
  daXuat: boolean | null;
}
