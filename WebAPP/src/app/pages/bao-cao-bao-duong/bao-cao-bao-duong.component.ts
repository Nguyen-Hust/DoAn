import { Component, OnInit } from '@angular/core';
import { PhieuBaoDuongDto } from 'src/app/models/PhieuBaoDuongDto';
import { PhieuBaoDuongService } from '../phieu-bao-duong/phieu-bao-duong.service';
import { NhanSuService } from '../nhan-su/nhan-su.service';
import { ToastrService } from 'ngx-toastr';
import { NzModalService } from 'ng-zorro-antd/modal';
import { LoaderService } from 'src/app/services/loader.service';
import { finalize } from 'rxjs';
import { BaoCaoService } from '../bao-cao-nhap/bao-cao.service';
import { ThongKeDto } from 'src/app/models/ThongKeDto';
import { NzTableQueryParams } from 'ng-zorro-antd/table';

@Component({
  selector: 'app-bao-cao-bao-duong',
  templateUrl: './bao-cao-bao-duong.component.html',
  styleUrls: ['./bao-cao-bao-duong.component.css']
})
export class BaoCaoBaoDuongComponent implements OnInit  {
  danhSach: PhieuBaoDuongDto[];
  thongKe = new ThongKeDto;


  id = 0;
  title = "";
  isShowModal = false;
  isConfirmLoading = false;
  date = null;
  filter = "";
  pageIndex = 1;
  pageSize = 10;
  total = 0;

  dsThietBi: any[] = [];
  dsNhanSu: any[] = [];

  constructor(
    private service: PhieuBaoDuongService,
    private nhanSuService: NhanSuService,
    private baoCaoservice: BaoCaoService,
    private toastr: ToastrService,
    private modal: NzModalService,
    private loadingService: LoaderService
  ) {}

  ngOnInit() {
    this.service.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
    });

    this.nhanSuService.getList({ filter: "" }).subscribe((val) => {
      this.dsNhanSu = val.items;
    });

    this.getList();
    this.getThongKe();
  }

  getThongKe() {
    this.service.getThongKeBaoDuong().subscribe(val => {
      this.thongKe = val;
    })
  }

  getList() {
    var body = {
      date: this.date,
      maxResultCount: this.pageSize,
      skipCount: (this.pageIndex - 1) * this.pageSize,
      filter: this.filter,
    };
    this.loadingService.setLoading(true);
    this.service
      .getList(body)
      .pipe(finalize(() => this.loadingService.setLoading(false)))
      .subscribe((val) => {
        this.danhSach = val.items;
        this.total = val.totalCount;
      });
  }

  onQueryParamsChange(data: NzTableQueryParams) {
    if (this.pageIndex != data.pageIndex || this.pageSize != data.pageSize) {
      this.pageIndex = data.pageIndex;
      this.pageSize = data.pageSize;
      this.getList();
    }
  }

  getTenThietBi(id) {
    return this.dsThietBi.find((_) => _.id == id)?.ma;
  }

  getTenNhanSu(id) {
    return this.dsNhanSu.find((_) => _.id == id)?.ten;
  }

  exportExcel() {
    this.loadingService.setLoading(true);
    var body = {
      date: this.date,
      filter: this.filter,
    };
    this.baoCaoservice.BaoCaoBaoDuong(body)
    .pipe(finalize(() => this.loadingService.setLoading(false)))
    .subscribe((response: any) => {
      const blob = new Blob([response], { type: response.type });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = 'bao-cao-bao-duong.xlsx';
      link.click();
      window.URL.revokeObjectURL(url);
    })
  }
}
