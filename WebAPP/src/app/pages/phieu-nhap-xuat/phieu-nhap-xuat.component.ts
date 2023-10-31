import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ToastrService } from 'ngx-toastr';
import { PhieuNhapXuatDto, ThongTinChiTietThietBiDto } from 'src/app/models/PhieuNhapXuatDto';
import { PhieuNhapXuatService } from './phieu-nhap-xuat.service';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { NhanSuService } from '../nhan-su/nhan-su.service';
import { finalize } from 'rxjs';

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
  dsKhoa: any[] = [];

  listChiTietThietBi: ThongTinChiTietThietBiDto[] = [] ;
  
  constructor(
    private formbulider: FormBuilder,
    private service: PhieuNhapXuatService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService
  ) {}
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
    var body = {
      filter: this.filter,
      maxResultCount: this.pageSize,
      skipCount: (this.pageIndex - 1) * this.pageSize,
    };
    this.service.getListPhieuNhap(body).subscribe((val) => {
      this.danhSach = val.items;
      this.total = val.totalCount;
    });
  }

  openModalCreatePhieuNhap() {
    this.reset();
    this.isShowModal = true;
    this.title = "Thêm mới phiếu nhập";
    this.form.get('loaiPhieu')?.patchValue(1); 
  }
  getTenNhanSu(id) {
    return this.dsNhanSu.find((_) => _.id == id).ten;
  }

  openModalUpdate(data) {
    this.getById(data.id);
    this.isShowModal = true;
    this.title = `Sửa: ${data.name}`;
  }

  delete(id: number) {
    this.modal.confirm({
      nzTitle: "Xác nhận xóa",
      nzContent: `Bạn có muốn xóa phiếu nhập này không`,
      nzOnOk: () =>
        this.service.delete(id).subscribe(() => {
          this.toastr.success("Data Deleted Successfully");
          this.getList();
        }),
    });
  }

  getById(id: number) {
    this.service.getById(id).subscribe((result) => {
      this.id = result.id;
      this.listChiTietThietBi  = [];
      if(result.thongTinChiTietThietBiDtos != null && result.thongTinChiTietThietBiDtos.length > 0) {
        this.listChiTietThietBi = result.thongTinChiTietThietBiDtos;
      }
      this.form.get("ngayNhapXuat")?.patchValue(result.ngayNhapXuat);
      this.form.patchValue(result);
    });
  }

  add() {
    var item: ThongTinChiTietThietBiDto = {
      id: 0,
      ma: '',
      thietBiYTeId: 0,
      ngayNhap: null,
      xuatXu: '',
      namSX: null,
      hangSanXuat: '',
      tinhTrang: null,
      khoaId: null,
      nhanVienId: null,
      serial: '',
      model: '',
      giaTien: null,
      thoiGianBaoDuong: null
    };
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

  save() {
    const input = this.form.value;
    input.thongTinChiTietThietBiDtos = this.listChiTietThietBi;
    if (this.form.invalid) {
      this.toastr.error("Cần nhập đủ thông tin");
      return;
    }
    this.isConfirmLoading = true;
    if (input.id) {
      this.update();
    } else {
      this.create();
    }
  }

  create() {
    const input = this.form.value;
    this.service
      .create(input)
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(() => {
        this.getList();
        this.form.reset();
        this.isShowModal = false;
        this.toastr.success("Data Saved Successfully");
      });
  }

  update() {
    const input = this.form.value;
    this.service
      .update(input)
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(() => {
        this.toastr.success("Data Updated Successfully");
        this.form.reset();
        this.isShowModal = false;
        this.getList();
      });
  }
}
