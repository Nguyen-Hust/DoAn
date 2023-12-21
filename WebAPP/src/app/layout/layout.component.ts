import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import configurl from "../../assets/config/config.json";
import { UserSessionService } from "./user-session.service";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.css"],
})
export class LayoutComponent {
  userInfo: any;
  url = configurl.apiServer.url + "/api/authentication/";

  constructor(
    private router: Router,
    private http: HttpClient,
    public userService: UserSessionService
  ) {
    if (localStorage.getItem("jwt")) {
      this.userService.getAccountBootstrap().subscribe();
    }
  }
  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/login"]);
  };
}
