import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import configurl from "../../../assets/config/config.json";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuSuaChuaDto } from "src/app/models/PhieuSuaChuaDto";

@Injectable({
  providedIn: "root",
})
export class PhieuSuaChuaService {
  url = configurl.apiServer.url + "/api/PhieuSuaChua/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(input: any): Observable<PhieuSuaChuaDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<PhieuSuaChuaDto>(
      this.url + "create",
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

  deny(id: number): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "deny?id=" + id, null, httpHeaders);
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

  getById(id: number): Observable<PhieuSuaChuaDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<PhieuSuaChuaDto>(
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
