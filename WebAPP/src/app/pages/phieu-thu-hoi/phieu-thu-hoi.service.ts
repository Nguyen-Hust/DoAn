import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import configurl from "../../../assets/config/config.json";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuBanGiaoDto } from "src/app/models/PhieuBanGiaoDto";
import { PhieuThuHoiDto } from "src/app/models/PhieuThuHoiDto";

@Injectable({
  providedIn: "root",
})
export class PhieuThuHoiService {
  url = configurl.apiServer.url + "/api/PhieuThuHoi/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(input: any): Observable<PhieuThuHoiDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<PhieuThuHoiDto>(
      this.url + "create",
      input,
      httpHeaders
    );
  }

  getById(id: number): Observable<PhieuThuHoiDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<PhieuThuHoiDto>(
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
