import { Injectable } from "@angular/core";
import configurl from "../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { KhoaDto } from "src/app/models/KhoaDto";

@Injectable({
  providedIn: "root",
})
export class PhongBanService {
  url = configurl.apiServer.url + "/api/PhongBan/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(khoa: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "create", khoa, httpHeaders);
  }
  update(khoa: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(
      this.url + "update?id=" + khoa.id,
      khoa,
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
  getById(id: number): Observable<KhoaDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<KhoaDto>(this.url + "get-by-id?id=" + id, httpHeaders);
  }
}
