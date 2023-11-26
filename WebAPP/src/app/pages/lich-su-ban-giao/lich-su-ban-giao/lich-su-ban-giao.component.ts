import { Component, OnInit } from '@angular/core';
import { LichSuBanGiaoDto } from 'src/app/models/LichSuBanGiaoDto';
import { LichSuBanGiaoService } from './lich-su-ban-giao.service';
import { LoaderService } from 'src/app/services/loader.service';
import { finalize } from 'rxjs';
import { PhieuNhapXuatService } from '../../phieu-nhap-xuat/phieu-nhap-xuat.service';

@Component({
  selector: 'app-lich-su-ban-giao',
  templateUrl: './lich-su-ban-giao.component.html',
  styleUrls: ['./lich-su-ban-giao.component.css']
})
export class LichSuBanGiaoComponent implements OnInit {
  danhSach: LichSuBanGiaoDto[];
  filter = "";
  pageIndex = 1;
  pageSize = 10;
  total = 0;
  dsNhanSu: any[] = [];

  constructor(
    private service: LichSuBanGiaoService,
    private loadingService: LoaderService,
    private servicePhieuNhap: PhieuNhapXuatService,
  ) {}

  ngOnInit() {
    this.servicePhieuNhap.getDanhSachNhanSu().subscribe((val) => {
      this.dsNhanSu = val;
    });
    this.getList();
  }

  getList(pageIndex = 1) {
    this.pageIndex = pageIndex;
    var body = {
      filter: this.filter,
      maxResultCount: this.pageSize,
      skipCount: (this.pageIndex - 1) * this.pageSize,
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

  getTenNhanSu(id) {
    return this.dsNhanSu.find((_) => _.id == id).ten;
  }
}
