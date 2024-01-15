import { Injectable } from "@angular/core";
import configurl from "../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { ThongTinChiTietThietBiDto } from "src/app/models/PhieuNhapXuatDto";

@Injectable({
  providedIn: "root",
})
export class DanhSachThietBiService {
  url = configurl.apiServer.url + "/api/DanhSachThietBi/";
  constructor(private http: HttpClient) {}
  getList(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list", body, httpHeaders);
  }

  getListByUser(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "get-list-by-user", body, httpHeaders);
  }
  update(input: any): Observable<ThongTinChiTietThietBiDto> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<ThongTinChiTietThietBiDto>(
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
  getById(id: number): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<any>(this.url + "get-by-id?id=" + id, httpHeaders);
  }

  UploadExcel(formData: FormData) {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        ContentType: "multipart/form-data",
        Accept: "application/json",
      }),
    };
    return this.http.post(this.url + "UploadExcel", formData, httpHeaders);
  }

  createList(input: any[]): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.post<any>(this.url + "create-list", input, httpHeaders);
  }

  getAllMa(): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
    };
    return this.http.get<any>(this.url + "get-all-ma", httpHeaders);
  }
}
