import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from "@angular/common";
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
import { IconDefinition } from '@ant-design/icons-angular';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { FormBuilderComponent } from './form-builder.component';
import { PhongBanComponent } from '../pages/phong-ban/phong-ban.component';
import { NhanSuComponent } from '../pages/nhan-su/nhan-su.component';
import { LoaiThietBiComponent } from "../pages/loai-thiet-bi/loai-thiet-bi.component";

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
    NhanSuComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    LayoutRoutingModule,
    NgZorroAntdModule,
    RouterModule,
    NzIconModule.forChild(icons),
  ],
  providers: [],
})
export class LayOutModule {}
