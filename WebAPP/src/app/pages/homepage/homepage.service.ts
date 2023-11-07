import { Injectable } from "@angular/core";
import configurl from "../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { DoashBoardTotalDto } from "src/app/models/DoashBoardTotalDto";
import { DashBoardChartDto } from "src/app/models/DashBoardChartDto";

@Injectable({
    providedIn: "root",
  })

  export class HomePageService {
    url = configurl.apiServer.url + "/api/DashBoard/";
    constructor(private http: HttpClient) {}

    getDashBoard(): Observable<DoashBoardTotalDto> {
        const httpHeaders = {
          headers: new HttpHeaders({
            Authorization: `Bearer ${localStorage.getItem("jwt")}`,
          }),
        };
        return this.http.get<DoashBoardTotalDto>(
          this.url + "get-dash-board",
          httpHeaders
        );
      }

      getDashBoardChart(): Observable<DashBoardChartDto[]> {
        const httpHeaders = {
          headers: new HttpHeaders({
            Authorization: `Bearer ${localStorage.getItem("jwt")}`,
          }),
        };
        return this.http.get<DashBoardChartDto[]>(
          this.url + "get-dashbaord-chart",
          httpHeaders
        );
      }
  }