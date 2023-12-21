export class PhieuThuHoiDto {
  id: number;
  ma: string;
  nhanVienId: number;
  createTime: Date | null;
  tongThietBi: number | null;
  danhSachThietBiId: number[];
  danhSachThietBi: any[];
}
