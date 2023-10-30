import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { ChiTietPhieuNhapXuatDto, PhieuNhapXuatDto } from 'src/app/models/PhieuNhapXuatDto';
import { PhieuNhapXuatService } from './phieu-nhap-xuat.service';
import { NzTableQueryParams } from 'ng-zorro-antd/table';

@Component({
  selector: 'app-phieu-nhap-xuat',
  templateUrl: './phieu-nhap-xuat.component.html',
  styleUrls: ['./phieu-nhap-xuat.component.css']
})
export class PhieuNhapXuatComponent implements OnInit {
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

  listChiTietThietBi: ChiTietPhieuNhapXuatDto[] = [];
  
  constructor(
    private formbulider: FormBuilder,
    private service: PhieuNhapXuatService,
    // private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService
  ) {}
  ngOnInit(): void {
    this.service.getDanhSachThietBi().subscribe((val) => {
      console.log(val);
      this.dsThietBi = val;
    });
    this.service.getDanhSachNhanSu().subscribe((val) => {
      console.log(val);
      this.dsNhanSu = val;
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
      chiTietPhieuNhapXuatDtos: this.formbulider.array([])
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
    this.service.getList(body).subscribe((val) => {
      this.danhSach = val.items;
      this.total = val.totalCount;
    });
  }

  openModalCreatePhieuNhap() {
    this.reset();
    this.isShowModal = true;
    this.title = "Thêm mới phiếu nhập";
  }

  openModalCreatePhieuXuat() {
    this.reset();
    this.isShowModal = true;
    this.title = "Thêm mới phiếu xuất";
  }

  add() {
    var item: ChiTietPhieuNhapXuatDto = { id: 0 };
    this.listChiTietThietBi.push(item);
    this.listChiTietThietBi = [...this.listChiTietThietBi];
  }

  deleteDetail(i) {
    this.listChiTietThietBi.splice(i, 1);
    this.listChiTietThietBi = [...this.listChiTietThietBi];
  }

  onQueryParamsChange(data: NzTableQueryParams) {
    if (this.pageIndex != data.pageIndex || this.pageSize != data.pageSize) {
      this.pageIndex = data.pageIndex;
      this.pageSize = data.pageSize;
    }
  }

  reset() {
    this.isDetail = false;
    this.listChiTietThietBi = [];
  }
}
