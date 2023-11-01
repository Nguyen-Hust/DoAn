import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import configurl from "../../../assets/config/config.json";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuBanGiaoDto } from "src/app/models/PhieuBanGiaoDto";

@Injectable({
  providedIn: "root",
})
export class PhieuBanGiaoService {
  url = configurl.apiServer.url + "/api/PhieuBanGiao/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(input: any): Observable<PhieuBanGiaoDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<PhieuBanGiaoDto>(
      this.url + "create",
      input,
      httpHeaders
    );
  }

  getById(id: number): Observable<PhieuBanGiaoDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<PhieuBanGiaoDto>(
      this.url + "get-by-id?id=" + id,
      httpHeaders
    );
  }

  getDanhSachThietBi(): Observable<ThietBiYTeDto[]> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<ThietBiYTeDto[]>(
      this.url + "get-danh-sach-thiet-bi",
      httpHeaders
    );
  }
}
