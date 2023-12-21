import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NgForm } from "@angular/forms";
// import configurl from '../../assets/config.json';
import { JwtHelperService } from "@auth0/angular-jwt";
import { ToastrService } from "ngx-toastr";
import configurl from "../../../assets/config/config.json";
import { UserSessionService } from "src/app/layout/user-session.service";

@Component({
  selector: "login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent {
  invalidLogin?: boolean;

  url = configurl.apiServer.url + "/api/authentication/";

  constructor(
    private router: Router,
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    public userService: UserSessionService,
    private toastr: ToastrService
  ) {}

  public login = (form: NgForm) => {
    const credentials = JSON.stringify(form.value);
    this.http
      .post(this.url + "login", credentials, {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
        }),
      })
      .subscribe(
        (response) => {
          const token = (<any>response).token;
          localStorage.setItem("jwt", token);
          this.userService.getAccountBootstrap().subscribe();
          this.invalidLogin = false;
          this.toastr.success("Logged In successfully");
          this.router.navigate(["/dashboard"]);
        },
        (err) => {
          this.invalidLogin = true;
          this.toastr.error("Logged In fail");
        }
      );
  };

  isUserAuthenticated() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    } else {
      return false;
    }
  }
}
