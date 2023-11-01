export class PhieuBanGiaoDto {
  id: number;
  ma: string;
  nhanVienId: number;
  nhanVienNhan: number | null;
  createTime: Date | null;
  tongThietBi: number | null;
  danhSachThietBi: number[];
}
