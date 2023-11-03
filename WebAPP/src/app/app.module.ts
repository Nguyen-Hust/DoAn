import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppComponent } from "./app.component";

import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from "./guards/auth-guard.service";
import { HomepageComponent } from "./pages/homepage/homepage.component";
import { LoginComponent } from "./pages/login/login.component";
import { ToastrModule } from "ngx-toastr";
import { LayoutComponent } from "./layout/layout.component";
import { registerLocaleData } from "@angular/common";
import vi from "@angular/common/locales/en";
registerLocaleData(vi);
import { provideNzI18n, vi_VN } from "ng-zorro-antd/i18n";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NzIconModule } from "ng-zorro-antd/icon";
import { SpinnerComponent } from "./layout/spinner/spinner.component";

//all components routes
const routes: Routes = [
  {
    path: "",
    redirectTo: "/dashboard",
    pathMatch: "full",
  },
  {
    path: "",
    component: LayoutComponent,
    children: [
      {
        path: "",
        loadChildren: () =>
          import("./layout/layout.module").then((m) => m.LayOutModule),
      },
    ],
    canActivate: [AuthGuard],
  },
  { path: "login", component: LoginComponent },
  { path: "**", redirectTo: "/dashboard", pathMatch: "full" },
];

//function is use to get jwt token from local storage
export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [AppComponent, LoginComponent, SpinnerComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NzIconModule,
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7299"],
        disallowedRoutes: [],
      },
    }),
    ToastrModule.forRoot(),
  ],
  providers: [AuthGuard, provideNzI18n(vi_VN)],
  bootstrap: [AppComponent],
})
export class AppModule {}
