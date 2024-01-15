import { Injectable } from "@angular/core";
import configurl from "../../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { LichSuBanGiaoDto } from "src/app/models/LichSuBanGiaoDto";

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

    getListByThietBi(id: number): Observable<LichSuBanGiaoDto[]> {
      const httpHeaders = {
        headers: new HttpHeaders({
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
      };
      return this.http.get<LichSuBanGiaoDto[]>(
        this.url + "get-lich-su-by-thiet-bi?id=" + id, 
        httpHeaders
        );
    }
  }
  