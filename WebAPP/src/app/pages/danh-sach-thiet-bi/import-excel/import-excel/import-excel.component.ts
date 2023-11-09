import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';
import configurl from "../../../../../assets/config/config.json";
import { DanhSachThietBiService } from '../../danh-sach-thiet-bi.service';

@Component({
  selector: 'app-import-excel',
  templateUrl: './import-excel.component.html',
  styleUrls: ['./import-excel.component.css']
})
export class ImportExcelComponent implements OnInit {
  @ViewChild('fileInput') fileInput;
  constructor(
    private drawerRef: NzDrawerRef,
    private service: DanhSachThietBiService,
  ) {}

  ngOnInit(): void {
  }

  uploadFile() {
    let fileToUpload = this.fileInput.nativeElement.files[0];
    let formData = new FormData();
    formData.append('files', fileToUpload);

    this.service.UploadExcel(formData).subscribe(result => {
      console.log(result.toString());
    });
  }
}
