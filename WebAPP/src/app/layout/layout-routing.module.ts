import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomepageComponent } from "../pages/homepage/homepage.component";
import { ThietBiYTeComponent } from "../pages/thiet-bi-y-te/thiet-bi-y-te.component";
import { LoaiThietBiComponent } from "../pages/loai-thiet-bi/loai-thiet-bi.component";
import { PhongBanComponent } from "../pages/phong-ban/phong-ban.component";
import { NhanSuComponent } from "../pages/nhan-su/nhan-su.component";
import { PhieuSuaChuaComponent } from "../pages/phieu-sua-chua/phieu-sua-chua.component";
import { PhieuBaoDuongComponent } from "../pages/phieu-bao-duong/phieu-bao-duong.component";
import { PhieuNhapXuatComponent } from "../pages/phieu-nhap-xuat/phieu-nhap-xuat.component";
import { PhieuXuatComponent } from "../pages/phieu-xuat/phieu-xuat.component";
import { DanhSachThietBiComponent } from "../pages/danh-sach-thiet-bi/danh-sach-thiet-bi.component";
import { PhieuBanGiaoComponent } from "../pages/phieu-ban-giao/phieu-ban-giao.component";
import { PhieuThuHoiComponent } from "../pages/phieu-thu-hoi/phieu-thu-hoi.component";
import { BaoCaoNhapComponent } from "../pages/bao-cao-nhap/bao-cao-nhap.component";
import { BaoCaoXuatComponent } from "../pages/bao-cao-xuat/bao-cao-xuat.component";
import { BaoCaoBaoDuongComponent } from "../pages/bao-cao-bao-duong/bao-cao-bao-duong.component";
import { BaoCaoSuaChuaComponent } from "../pages/bao-cao-sua-chua/bao-cao-sua-chua.component";
import { LichSuBanGiaoComponent } from "../pages/lich-su-ban-giao/lich-su-ban-giao/lich-su-ban-giao.component";
import { ThongTinThietBiComponent } from "../pages/thong-tin-thiet-bi/thong-tin-thiet-bi.component";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "dashboard",
        component: HomepageComponent,
      },
      {
        path: "thiet-bi-y-te",
        component: ThietBiYTeComponent,
      },
      {
        path: "loai-thiet-bi",
        component: LoaiThietBiComponent,
      },
      {
        path: "phong-ban",
        component: PhongBanComponent,
      },
      {
        path: "nhan-su",
        component: NhanSuComponent,
      },
      {
        path: "phieu-sua-chua",
        component: PhieuSuaChuaComponent,
      },
      {
        path: "phieu-ban-giao",
        component: PhieuBanGiaoComponent,
      },
      {
        path: "phieu-thu-hoi",
        component: PhieuThuHoiComponent,
      },
      {
        path: "phieu-bao-duong",
        component: PhieuBaoDuongComponent,
      },
      {
        path: "phieu-nhap",
        component: PhieuNhapXuatComponent,
      },
      {
        path: "phieu-xuat",
        component: PhieuXuatComponent,
      },
      {
        path: "danh-sach-thiet-bi",
        component: DanhSachThietBiComponent,
      },
      {
        path: "bao-cao-nhap",
        component: BaoCaoNhapComponent,
      },
      {
        path: "bao-cao-xuat",
        component: BaoCaoXuatComponent,
      },
      {
        path: "bao-cao-bao-duong",
        component: BaoCaoBaoDuongComponent,
      },
      {
        path: "bao-cao-sua-chua",
        component: BaoCaoSuaChuaComponent,
      },
      {
        path: "lich-su-ban-giao",
        component: LichSuBanGiaoComponent,
      },
    ]),
  ],
})
export class LayoutRoutingModule {}
