import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import configurl from "../../../assets/config/config.json";
import { LoaiThietBiDto } from "src/app/models/LoaiThietBiDto";

@Injectable({
  providedIn: "root",
})
export class LoaiThietBiService {
  url = configurl.apiServer.url + "/api/LoaiThietBi/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(input: any): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "create", input, httpHeaders);
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
  getById(id: number): Observable<LoaiThietBiDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<LoaiThietBiDto>(
      this.url + "get-by-id?id=" + id,
      httpHeaders
    );
  }
}
