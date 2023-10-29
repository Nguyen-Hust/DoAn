import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import configurl from "../../assets/config/config.json";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.css"],
})
export class LayoutComponent {
  userInfo: any;
  url = configurl.apiServer.url + "/api/authentication/";
  isVisible = false;

  constructor(private router: Router, private http: HttpClient) {
    // if (localStorage.getItem("jwt")) {
    //   this.http
    //     .get(this.url + "get-user-info", {
    //       headers: new HttpHeaders({
    //         "Content-Type": "application/json",
    //         Authorization: `Bearer ${localStorage.getItem("jwt")}`,
    //       }),
    //     })
    //     .subscribe((response) => {
    //       this.isVisible = true;
    //       this.userInfo = response;
    //     });
    // }
  }
  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/login"]);
  };
}
