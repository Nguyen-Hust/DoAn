import { Component, OnInit } from '@angular/core';
import { PhieuSuaChuaDto } from 'src/app/models/PhieuSuaChuaDto';
import { PhieuSuaChuaService } from '../phieu-sua-chua/phieu-sua-chua.service';
import { NhanSuService } from '../nhan-su/nhan-su.service';
import { ToastrService } from 'ngx-toastr';
import { NzModalService } from 'ng-zorro-antd/modal';
import { LoaderService } from 'src/app/services/loader.service';
import { finalize } from 'rxjs';
import { BaoCaoService } from '../bao-cao-nhap/bao-cao.service';
import { NzTableQueryParams } from 'ng-zorro-antd/table';

@Component({
  selector: 'app-bao-cao-sua-chua',
  templateUrl: './bao-cao-sua-chua.component.html',
  styleUrls: ['./bao-cao-sua-chua.component.css']
})
export class BaoCaoSuaChuaComponent implements OnInit{
  danhSach: PhieuSuaChuaDto[];

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
    private service: PhieuSuaChuaService,
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
    this.baoCaoservice.BaoCaoSuaChua(body)
    .pipe(finalize(() => this.loadingService.setLoading(false)))
    .subscribe((response: any) => {
      const blob = new Blob([response], { type: response.type });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = 'bao-cao-sua-chua.xlsx';
      link.click();
      window.URL.revokeObjectURL(url);
    })
  }
}
