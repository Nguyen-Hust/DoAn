import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import configurl from "../../../assets/config/config.json";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuBaoDuongDto } from "src/app/models/PhieuBaoDuongDto";
import { ThongKeDto } from "src/app/models/ThongKeDto";

@Injectable({
  providedIn: "root",
})
export class PhieuBaoDuongService {
  url = configurl.apiServer.url + "/api/PhieuBaoDuong/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(input: any): Observable<PhieuBaoDuongDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<PhieuBaoDuongDto>(
      this.url + "create",
      input,
      httpHeaders
    );
  }

  update(input: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(
      this.url + "update?id=" + input.id,
      input,
      httpHeaders
    );
  }

  delete(id: number): Observable<number> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<number>(
      this.url + "delete?id=" + id,
      null,
      httpHeaders
    );
  }

  approve(id: number): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(
      this.url + "approve?id=" + id,
      null,
      httpHeaders
    );
  }

  completed(id: number): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(
      this.url + "completed?id=" + id,
      null,
      httpHeaders
    );
  }

  getById(id: number): Observable<PhieuBaoDuongDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<PhieuBaoDuongDto>(
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

  getThongKeBaoDuong(): Observable<ThongKeDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<ThongKeDto>(
      this.url + "get-total-phieu-bao-duong",
      httpHeaders
    );
  }
}
