import { Component, OnInit } from '@angular/core';
import { LichSuBanGiaoDto } from 'src/app/models/LichSuBanGiaoDto';
import { LichSuBanGiaoService } from './lich-su-ban-giao.service';
import { LoaderService } from 'src/app/services/loader.service';
import { finalize } from 'rxjs';
import { PhieuNhapXuatService } from '../../phieu-nhap-xuat/phieu-nhap-xuat.service';
import { ThongTinChiTietThietBiDto } from 'src/app/models/PhieuNhapXuatDto';
import { DanhSachThietBiService } from '../../danh-sach-thiet-bi/danh-sach-thiet-bi.service';
import { NzTableQueryParams } from 'ng-zorro-antd/table';

@Component({
  selector: 'app-lich-su-ban-giao',
  templateUrl: './lich-su-ban-giao.component.html',
  styleUrls: ['./lich-su-ban-giao.component.css']
})
export class LichSuBanGiaoComponent implements OnInit {
  danhSach: ThongTinChiTietThietBiDto[];
  filter = "";
  pageIndex = 1;
  pageSize = 10;
  total = 0;
  dsNhanSu: any[] = [];
  dsThietBi: any[] = [];

  filteredOptions: string[] = [];

  constructor(
    private service: DanhSachThietBiService,
    private loadingService: LoaderService,
    private servicePhieuNhap: PhieuNhapXuatService,
  ) {}

  ngOnInit() {
    this.servicePhieuNhap.getDanhSachNhanSu().subscribe((val) => {
      this.dsNhanSu = val;
    });

    this.servicePhieuNhap.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
    });
    this.getList();
  }

  getList() {
    var body = {
      filter: this.filter,
      maxResultCount: this.pageSize,
      skipCount: (this.pageIndex - 1) * this.pageSize,
    };
    this.loadingService.setLoading(true);
    this.service
      .getListByUser(body)
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

  getTenNhanSu(id) {
    return this.dsNhanSu.find((_) => _.id == id).ten;
  }

  getTenThietBi(id) {
    return id != null || id != undefined
      ? this.dsThietBi.find((_) => _.id == id)?.ten
      : "";
  }
}
