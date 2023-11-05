import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ThongTinChiTietThietBiDto } from 'src/app/models/PhieuNhapXuatDto';
import { DanhSachThietBiService } from './danh-sach-thiet-bi.service';
import { LoaiThietBiService } from '../loai-thiet-bi/loai-thiet-bi.service';
import { ToastrService } from 'ngx-toastr';
import { NzModalService } from 'ng-zorro-antd/modal';
import { PhieuNhapXuatService } from '../phieu-nhap-xuat/phieu-nhap-xuat.service';
import { NhanSuService } from '../nhan-su/nhan-su.service';
import { finalize } from 'rxjs';
import { LoaderService } from 'src/app/services/loader.service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';


@Component({
  selector: 'app-danh-sach-thiet-bi',
  templateUrl: './danh-sach-thiet-bi.component.html',
  styleUrls: ['./danh-sach-thiet-bi.component.css']
})
export class DanhSachThietBiComponent implements OnInit {
  danhSach: ThongTinChiTietThietBiDto[];
  form: FormGroup;

  id = 0;
  title = "";
  isShowModal = false;
  isConfirmLoading = false;
  filter = "";
  pageIndex = 1;
  pageSize = 10;
  total = 0;
  dsThietBi: any[] = [];
  dsNhanSu: any[] = [];
  dsKhoa: any[] = [];

  constructor(
    private formbulider: FormBuilder,
    private service: DanhSachThietBiService,
    private nhapXuatService: PhieuNhapXuatService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService,
    private loadingService: LoaderService,
    private drawerService: NzDrawerService,
  ) {}
  ngOnInit(): void {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      ma: ["", [Validators.required]],
      ngayNhap: [null],
      xuatXu: [""],
      namSX: [0],
      hangSanXuat: [""],
      tinhTrang: [0],
      khoaId: [0],
      nhanVienId: [0],
      serial: [""],
      model: [""],
      giaTien: [0],
      thoiGianBaoDuong: [0]
    });

    this.nhapXuatService.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
    });

    this.nhapXuatService.getDanhSachNhanSu().subscribe((val) => {
      this.dsNhanSu = val;
    });

    this.nhanSuService.getAllKhoa().subscribe((val) => {
      this.dsKhoa = val;
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
    this.service.getList(body).pipe(finalize(() => this.loadingService.setLoading(false))).subscribe((val) => {
      this.danhSach = val.items;
      this.total = val.totalCount;
    });
  }

  getTenThietBi(id) {
    return (id != null || id != undefined) ? this.dsThietBi.find((_) => _.id == id).ten : "";
  }
  getKhoa(id) {
    return (id != null || id != undefined) ? this.dsKhoa.find((_) => _.id == id).ten : "";
  }
  getNhanVien(id) {
    return (id != null || id != undefined) ? this.dsNhanSu.find((_) => _.id == id).ten : "";
  }

  delete(id: number) {
    this.modal.confirm({
      nzTitle: "Xác nhận xóa",
      nzContent: `Bạn có muốn xóa thong tin chi tiết thiết bị này không`,
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
      this.form.patchValue(result);
    });
  }

  openModalUpdate(data) {
    this.getById(data.id);
    this.isShowModal = true;
    this.title = `Sửa:`;
  }

  save() {
    const input = this.form.value;
    if (this.form.invalid) {
      this.toastr.error("Cần nhập đủ thông tin");
      return;
    }
    this.isConfirmLoading = true;
    if (input.id) {
      this.update();
    }
  }

  update() {
    const input = this.form.value;
    this.loadingService.setLoading(true)
    this.service
      .update(input)
      .pipe(finalize(() => (this.isConfirmLoading = false,
        this.loadingService.setLoading(false))))
      .subscribe(() => {
        this.toastr.success("Data Updated Successfully");
        this.form.reset();
        this.isShowModal = false;
        this.getList();
      });
  }


  importExcel() {
    console.log("test");
  }

}
