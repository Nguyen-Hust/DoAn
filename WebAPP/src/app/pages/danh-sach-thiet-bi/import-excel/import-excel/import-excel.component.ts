import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';
import configurl from "../../../../../assets/config/config.json";
import { DanhSachThietBiService } from '../../danh-sach-thiet-bi.service';
import { PhieuNhapXuatService } from 'src/app/pages/phieu-nhap-xuat/phieu-nhap-xuat.service';
import { NhanSuService } from 'src/app/pages/nhan-su/nhan-su.service';
import { finalize } from 'rxjs';
import { LoaderService } from 'src/app/services/loader.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Component({
  selector: 'app-import-excel',
  templateUrl: './import-excel.component.html',
  styleUrls: ['./import-excel.component.css']
})
export class ImportExcelComponent implements OnInit {
  @ViewChild('fileInput') fileInput;
  datatable:  any[] = [];
  dataErrorTable: any[] = [];
  dataError: any[] = [];
  dsThietBi: any[] = [];
  dsNhanSu: any[] = [];
  dsKhoa: any[] = [];
  showDataTable = false;
  showError = false;
  constructor(
    private drawerRef: NzDrawerRef,
    private nhapXuatService: PhieuNhapXuatService,
    private service: DanhSachThietBiService,
    private nhanSuService: NhanSuService,
    private loadingService: LoaderService,
    private notification: NzNotificationService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {

    this.nhapXuatService.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
    });

    this.nhapXuatService.getDanhSachNhanSu().subscribe((val) => {
      this.dsNhanSu = val;
    });

    this.nhanSuService.getAllKhoa().subscribe((val) => {
      this.dsKhoa = val;
    });
  }

  uploadFile() {
    let fileToUpload = this.fileInput.nativeElement.files[0];
    let formData = new FormData();
    formData.append('files', fileToUpload);

    this.service.UploadExcel(formData).subscribe((result: any) => {
      var data: any[] = [];
      var dataError: any[] = [];
      this.dataError = [];
      if(result?.validDtos.length > 0)
      {
        this.showDataTable = true;
        result?.validDtos.forEach((item: any) => {
          data.push(item?.data);
        })
      }
      if(result?.inValidDtos.length > 0 ) {
        this.showError = true;
        result?.inValidDtos.forEach((item: any) => {
          dataError.push(item?.data);
          this.dataError = [...this.dataError, ...item?.invalidErrors];
        })
      }
      this.datatable = data;
      this.dataErrorTable = dataError;
    });
  }

  getTenThietBi(id) {
    return (id != null && id != undefined && id >= 1) ? this.dsThietBi.find((_) => _.id == id).ten : "";
  }
  getKhoa(id) {
    return (id != null && id != undefined && id >= 1) ? this.dsKhoa.find((_) => _.id == id).ten : "";
  }
  getNhanVien(id) {
    return (id != null && id != undefined && id >=  1) ? this.dsNhanSu.find((_) => _.id == id).ten : "";
  }

  import() {
    this.loadingService.setLoading(true);
    this.service.createList(this.datatable)
    .pipe(finalize(() => this.loadingService.setLoading(false)))
    .subscribe(data => {
      if(data.isSuccessful) {
        this.notification.create("success", "Thành công", "import danh sách thiết bị thành công");
        this.drawerRef.close();
      }else {
        this.notification.create("error", "Thất bại", "import danh sách thiết bị thất bại");
      }
    })
  }
}
