import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomepageComponent } from "../pages/homepage/homepage.component";
import { ThietBiYTeComponent } from "../pages/thiet-bi-y-te/thiet-bi-y-te.component";
import { LoaiThietBiComponent } from "../pages/loai-thiet-bi/loai-thiet-bi.component";

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
    ]),
  ],
})
export class LayoutRoutingModule {}
