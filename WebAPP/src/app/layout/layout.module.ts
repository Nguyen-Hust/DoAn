import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CommonModule, DatePipe } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { LayoutComponent } from "./layout.component";
import { HomepageComponent } from "../pages/homepage/homepage.component";
import { ThietBiYTeComponent } from "../pages/thiet-bi-y-te/thiet-bi-y-te.component";
import { LayoutRoutingModule } from "./layout-routing.module";
import { NgZorroAntdModule } from "../sharded/ng-zorro-antd.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BrowserModule } from "@angular/platform-browser";
import {
  StepBackwardOutline,
  CaretLeftOutline,
  SettingOutline,
} from "@ant-design/icons-angular/icons";
import { IconDefinition } from "@ant-design/icons-angular";
import { NzIconModule } from "ng-zorro-antd/icon";
import { FormBuilderComponent } from "./form-builder.component";
import { PhongBanComponent } from "../pages/phong-ban/phong-ban.component";
import { NhanSuComponent } from "../pages/nhan-su/nhan-su.component";
import { LoaiThietBiComponent } from "../pages/loai-thiet-bi/loai-thiet-bi.component";
import { PhieuSuaChuaComponent } from "../pages/phieu-sua-chua/phieu-sua-chua.component";
import { NzToolTipModule } from "ng-zorro-antd/tooltip";
import { PhieuBaoDuongComponent } from "../pages/phieu-bao-duong/phieu-bao-duong.component";
import { HttpClientModule } from "@angular/common/http";
import { PhieuNhapXuatComponent } from "../pages/phieu-nhap-xuat/phieu-nhap-xuat.component";
import { PhieuXuatComponent } from "../pages/phieu-xuat/phieu-xuat.component";
import { DanhSachThietBiComponent } from "../pages/danh-sach-thiet-bi/danh-sach-thiet-bi.component";
import { PhieuBanGiaoComponent } from "../pages/phieu-ban-giao/phieu-ban-giao.component";
import { PhieuThuHoiComponent } from "../pages/phieu-thu-hoi/phieu-thu-hoi.component";
import { BaoCaoNhapComponent } from "../pages/bao-cao-nhap/bao-cao-nhap.component";
import { BaoCaoXuatComponent } from "../pages/bao-cao-xuat/bao-cao-xuat.component";
import { SpinnerComponent } from "./spinner/spinner.component";
import { BaoCaoBaoDuongComponent } from "../pages/bao-cao-bao-duong/bao-cao-bao-duong.component";
import { BaoCaoSuaChuaComponent } from "../pages/bao-cao-sua-chua/bao-cao-sua-chua.component";

const icons: IconDefinition[] = [
  StepBackwardOutline,
  CaretLeftOutline,
  SettingOutline,
];

@NgModule({
  declarations: [
    LayoutComponent,
    HomepageComponent,
    ThietBiYTeComponent,
    FormBuilderComponent,
    LoaiThietBiComponent,
    PhongBanComponent,
    NhanSuComponent,
    PhieuSuaChuaComponent,
    PhieuBaoDuongComponent,
    PhieuNhapXuatComponent,
    PhieuXuatComponent,
    DanhSachThietBiComponent,
    PhieuBanGiaoComponent,
    PhieuThuHoiComponent,
    BaoCaoNhapComponent,
    BaoCaoXuatComponent,
    BaoCaoBaoDuongComponent,
    BaoCaoSuaChuaComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    LayoutRoutingModule,
    NgZorroAntdModule,
    RouterModule,
    NzIconModule.forChild(icons),
    HttpClientModule,
  ],
  providers: [DatePipe],
})
export class LayOutModule {}
