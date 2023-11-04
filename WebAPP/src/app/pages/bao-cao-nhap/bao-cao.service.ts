import { Injectable } from "@angular/core";
import configurl from "../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root",
  })
export class BaoCaoService {
    url = configurl.apiServer.url + "/api/BaoCao/";
    constructor(private http: HttpClient) {}
    BaoCaoNhap(body): Observable<any> {
        const httpHeaders = {
          headers: new HttpHeaders({
            Authorization: `Bearer ${localStorage.getItem("jwt")}`,
          }),
          responseType: 'blob' as 'json',
        };
        return this.http.post(this.url + "bao-cao-nhap", body, httpHeaders);
    }

    BaoCaoXuat(body): Observable<any> {
      const httpHeaders = {
        headers: new HttpHeaders({
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
        responseType: 'blob' as 'json',
      };
      return this.http.post(this.url + "bao-cao-xuat", body, httpHeaders);
  }

  ExportPdfPhieuNhapXuat(id: number): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
      responseType: 'blob' as 'json',
    };
    return this.http.get(this.url + "export-pdf-phieu-nhap-xuat?id=" + id, httpHeaders);
  }

  BaoCaoBaoDuong(body): Observable<any> {
    const httpHeaders = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem("jwt")}`,
      }),
      responseType: 'blob' as 'json',
    };
    return this.http.post(this.url + "bao-cao-bao-duong", body, httpHeaders);
}

BaoCaoSuaChua(body): Observable<any> {
  const httpHeaders = {
    headers: new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem("jwt")}`,
    }),
    responseType: 'blob' as 'json',
  };
  return this.http.post(this.url + "bao-cao-sua-chua", body, httpHeaders);
}
}