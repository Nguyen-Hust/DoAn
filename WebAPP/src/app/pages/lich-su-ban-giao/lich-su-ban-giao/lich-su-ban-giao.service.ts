import { Injectable } from "@angular/core";
import configurl from "../../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root",
  })

  export class LichSuBanGiaoService {
    url = configurl.apiServer.url + "/api/LichSuBanGiao/";
    constructor(private http: HttpClient) {}
    getList(body): Observable<any> {
      const httpHeaders = {
        headers: new HttpHeaders({
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
      };
      return this.http.post<any>(this.url + "get-list", body, httpHeaders);
    }
  }
  