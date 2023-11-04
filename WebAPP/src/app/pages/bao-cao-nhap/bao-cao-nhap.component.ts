import { Component, OnInit } from '@angular/core';
import { PhieuNhapXuatDto, ThongTinChiTietThietBiDto } from 'src/app/models/PhieuNhapXuatDto';
import { PhieuNhapXuatService } from '../phieu-nhap-xuat/phieu-nhap-xuat.service';
import { NhanSuService } from '../nhan-su/nhan-su.service';
import { ToastrService } from 'ngx-toastr';
import { NzModalService } from 'ng-zorro-antd/modal';
import { BaoCaoService } from './bao-cao.service';
import { HttpResponse } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { LoaderService } from 'src/app/services/loader.service';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-bao-cao-nhap',
  templateUrl: './bao-cao-nhap.component.html',
  styleUrls: ['./bao-cao-nhap.component.css']
})
export class BaoCaoNhapComponent implements OnInit {
  danhSach: PhieuNhapXuatDto[];
  form: FormGroup;

  id = 0;
  title = "";
  isShowModal = false;
  isConfirmLoading = false;
  pageIndex = 1;
  pageSize = 10;
  total = 0;
  filter = '';
  isDetail = false;

  dsThietBi: any[] = [];
  dsNhanSu: any[] = [];
  dsKhoa: any[] = [];

  listChiTietThietBi: ThongTinChiTietThietBiDto[] = [] ;

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuNhapXuatService,
    private nhanSuService: NhanSuService,
    private baoCaoservice: BaoCaoService,
    private modal: NzModalService,
    private loadingService: LoaderService
  ) { }
  ngOnInit(): void {
    this.service.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
    });
    this.service.getDanhSachNhanSu().subscribe((val) => {
      this.dsNhanSu = val;
    });
    this.nhanSuService.getAllKhoa().subscribe((val) => {
      this.dsKhoa = val;
    });
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      ma: ['', [Validators.required]],
      ngayNhapXuat: new FormControl(new Date()), 
      nhaCungCap: ['', [Validators.required]],
      nguoiDaiDien: ['', [Validators.required]],
      nhaVienId: [0, [Validators.required]],
      soLuong: [0],
      tongTien: [0],
      ghiChu: [""],
      loaiPhieu: [0],
      thongTinChiTietThietBiDtos: this.formbulider.array([])
    });
    this.getList();

  }

  getList(pageIndex = 1) {
    this.pageIndex = pageIndex;
    this.loadingService.setLoading(true);
    var body = {
      filter: this.filter,
      maxResultCount: this.pageSize,
      skipCount: (this.pageIndex - 1) * this.pageSize,
    };
    this.service.getListPhieuNhap(body)
    .pipe(finalize(() => this.loadingService.setLoading(false)))
    .subscribe((val) => {
      this.danhSach = val.items;
      this.total = val.totalCount;
    });
  }

  getTenNhanSu(id) {
    return this.dsNhanSu.find((_) => _.id == id).ten;
  }

  getById(id: number) {
    this.loadingService.setLoading(true);
    this.isShowModal = true;
    this.title = `Xem chi tiết`;
    this.isDetail = true;
    this.service.getById(id)
    .pipe(finalize(() => this.loadingService.setLoading(false)))
    .subscribe((result) => {
      this.id = result.id;
      this.listChiTietThietBi  = [];
      if(result.thongTinChiTietThietBiDtos != null && result.thongTinChiTietThietBiDtos.length > 0) {
        this.listChiTietThietBi = result.thongTinChiTietThietBiDtos;
      }
      this.form.get("ngayNhapXuat")?.patchValue(result.ngayNhapXuat);
      this.form.patchValue(result);
    });
  }

  onQueryParamsChange(data: NzTableQueryParams) {
    if (this.pageIndex != data.pageIndex || this.pageSize != data.pageSize) {
      this.pageIndex = data.pageIndex;
      this.pageSize = data.pageSize;
    }
  }

  exportExcel() {
    this.loadingService.setLoading(true);
    var body = {
      filter: this.filter
    };
    this.baoCaoservice.BaoCaoNhap(body)
    .pipe(finalize(() => this.loadingService.setLoading(false)))
    .subscribe((response: any) => {
      const blob = new Blob([response], { type: response.type });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = 'bao-cao-nhap.xlsx';
      link.click();
      window.URL.revokeObjectURL(url);
    })
  }

  XuatPdf(id: number) {
    this.loadingService.setLoading(true);
    this.baoCaoservice.ExportPdfPhieuNhapXuat(id)
    .pipe(finalize(() => this.loadingService.setLoading(false)))
    .subscribe((response: any) => {
      const blob = new Blob([response], { type: response.type });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = 'bao-cao-chi-tiet-nhap.xlsx';
      link.click();
      window.URL.revokeObjectURL(url);
    })
  }
}
