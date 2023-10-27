import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomepageComponent } from '../pages/homepage/homepage.component';
import { ThietBiYTeComponent } from '../pages/thiet-bi-y-te/thiet-bi-y-te.component';
import { PhongBanComponent } from '../pages/phong-ban/phong-ban.component';
import { NhanSuComponent } from '../pages/nhan-su/nhan-su.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: 'dashboard',
        component: HomepageComponent,
      },
      {
        path: 'thiet-bi-y-te',
        component: ThietBiYTeComponent,
      },
      {
        path: 'phong-ban',
        component: PhongBanComponent,
      },
      {
        path: 'nhan-su',
        component: NhanSuComponent,
      },
    ]),
  ],
})
export class LayoutRoutingModule {}
