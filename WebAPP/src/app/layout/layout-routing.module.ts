import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomepageComponent } from '../pages/homepage/homepage.component';
import { ThietBiYTeComponent } from '../pages/thiet-bi-y-te/thiet-bi-y-te.component';

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
    ]),
  ],
})
export class LayoutRoutingModule {}
