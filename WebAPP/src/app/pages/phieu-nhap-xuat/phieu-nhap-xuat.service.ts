import { Injectable } from "@angular/core";
import configurl from "../../../assets/config/config.json";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { NhanSuDto } from "src/app/models/NhanSuDto";
import { PhieuNhapXuatDto } from "src/app/models/PhieuNhapXuatDto";

@Injectable({
    providedIn: "root",
  })

  export class PhieuNhapXuatService {
    url = configurl.apiServer.url + "/api/PhieuNhapXuat/";
    constructor(private http: HttpClient) {}
    getListPhieuNhap(body): Observable<any> {
        const httpHeaders = {
          headers: new HttpHeaders({
            Authorization: `Bearer ${localStorage.getItem("jwt")}`,
          }),
        };
        return this.http.post<any>(this.url + "get-list-phieu-nhap", body, httpHeaders);
    }

    getListPhieuXuat(body): Observable<any> {
      const httpHeaders = {
        headers: new HttpHeaders({
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
      };
      return this.http.post<any>(this.url + "get-list-phieu-xuat", body, httpHeaders);
  }

    create(productData: any): Observable<PhieuNhapXuatDto> {
      const httpHeaders = {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
      };
      return this.http.post<PhieuNhapXuatDto>(
        this.url + "create",
        productData,
        httpHeaders
      );
    }

    update(product: any): Observable<PhieuNhapXuatDto> {
      const httpHeaders = {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
      };
      return this.http.post<PhieuNhapXuatDto>(
        this.url + "update?id=" + product.id,
        product,
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
    getById(id: number): Observable<PhieuNhapXuatDto> {
      const httpHeaders = {
        headers: new HttpHeaders({
          Authorization: `Bearer ${localStorage.getItem("jwt")}`,
        }),
      };
      return this.http.get<PhieuNhapXuatDto>(
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