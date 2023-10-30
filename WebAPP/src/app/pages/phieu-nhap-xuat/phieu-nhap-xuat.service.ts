import { Injectable } from "@angular/core";
import configurl from "../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { NhanSuDto } from "src/app/models/NhanSuDto";

@Injectable({
    providedIn: "root",
  })

  export class PhieuNhapXuatService {
    url = configurl.apiServer.url + "/api/PhieuNhapXuat/";
    constructor(private http: HttpClient) {}
    getList(body): Observable<any> {
        const httpHeaders = {
          headers: new HttpHeaders({
            Authorization: `Bearer ${localStorage.getItem("jwt")}`,
          }),
        };
        return this.http.post<any>(this.url + "get-list", body, httpHeaders);
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

      getDanhSachNhanSu(): Observable<NhanSuDto[]> {
        const httpHeaders = {
          headers: new HttpHeaders({
            Authorization: `Bearer ${localStorage.getItem("jwt")}`,
          }),
        };
        return this.http.get<NhanSuDto[]>(
          this.url + "get-danh-sach-nhan-vien",
          httpHeaders
        );
      }
  }