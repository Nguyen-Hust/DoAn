import { Injectable } from "@angular/core";
import configurl from "../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { NhanSuDto } from "src/app/models/NhanSuDto";

@Injectable({
  providedIn: "root",
})
export class NhanSuService {
  url = configurl.apiServer.url + "/api/NhanVien/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(nhanSu: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "create", nhanSu, httpHeaders);
  }
  update(nhanSu: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(
      this.url + "update?id=" + nhanSu.id,
      nhanSu,
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
  getById(id: number): Observable<NhanSuDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<NhanSuDto>(
      this.url + "get-by-id?id=" + id,
      httpHeaders
    );
  }

  getAllKhoa(): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "Get-all-khoa", "", httpHeaders);
  }

  registerUser(input: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(
      configurl.apiServer.url + "/api/authentication/register",
      input,
      httpHeaders
    );
  }

  registerAdmin(input: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(
      configurl.apiServer.url + "/api/authentication/register-admin",
      input,
      httpHeaders
    );
  }
}
