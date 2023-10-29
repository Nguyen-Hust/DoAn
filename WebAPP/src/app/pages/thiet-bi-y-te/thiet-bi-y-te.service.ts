import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import configurl from "../../../assets/config/config.json";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";

@Injectable({
  providedIn: "root",
})
export class ThietBiYTeService {
  url = configurl.apiServer.url + "/api/ThietBiYTe/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }
  create(input: any): Observable<ThietBiYTeDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<ThietBiYTeDto>(
      this.url + "create",
      input,
      httpHeaders
    );
  }
  update(input: any): Observable<ThietBiYTeDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<ThietBiYTeDto>(
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
  getById(id: number): Observable<ThietBiYTeDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<ThietBiYTeDto>(
      this.url + "get-by-id?id=" + id,
      httpHeaders
    );
  }
}
