
export class PhieuNhapXuatDto 
{
    id: number;
    ma: string;
    ngayNhapXuat: Date;
    nhaCungCap: string;
    nguoiDaiDien: string;
    nhanVienId: number;
    soLuong: number | null;
    tongTien: number| null;
    ghiChu: string;
    loaiPhieu: number;
    chiTietPhieuNhapXuatDtos: ChiTietPhieuNhapXuatDto[] | null;
}

export class ChiTietPhieuNhapXuatDto {
    id: number;
    phieuNhapXuatId?: number;
    chiTietThietBiId?: number;
    giaTien?: number | null;
}