import { Component, OnInit } from "@angular/core";
import { formatDate } from "@angular/common";
import { DanhSachThietBiService } from "../danh-sach-thiet-bi/danh-sach-thiet-bi.service";
import { PhieuNhapXuatService } from "../phieu-nhap-xuat/phieu-nhap-xuat.service";
import { NhanSuService } from "../nhan-su/nhan-su.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-thong-tin-thiet-bi",
  templateUrl: "./thong-tin-thiet-bi.component.html",
})
export class ThongTinThietBiComponent implements OnInit {
  thietbi: any = null;
  dsNhanSu: any[] = [];
  dsKhoa: any[] = [];
  constructor(
    private route: ActivatedRoute,
    private service: DanhSachThietBiService,
    private nhanSuService: NhanSuService,
    private nhapXuatService: PhieuNhapXuatService
  ) {}
  ngOnInit(): void {
    this.nhapXuatService.getDanhSachNhanSu().subscribe((val) => {
      this.dsNhanSu = val;
    });

    this.nhanSuService.getAllKhoa().subscribe((val) => {
      this.dsKhoa = val;
    });
    this.getById();
  }

  getById() {
    const id = Number(this.route.snapshot.paramMap.get("id"));
    this.service.getById(id).subscribe((result) => {
      this.thietbi = result;
    });
  }

  getKhoa(id) {
    return id != null || id != undefined
      ? this.dsKhoa.find((_) => _.id == id)?.ten
      : "";
  }
  getNhanVien(id) {
    return id != null || id != undefined
      ? this.dsNhanSu.find((_) => _.id == id)?.ten
      : "";
  }
}
