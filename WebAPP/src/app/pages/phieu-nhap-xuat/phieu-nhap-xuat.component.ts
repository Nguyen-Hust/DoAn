import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { NzModalService } from "ng-zorro-antd/modal";
import { ToastrService } from "ngx-toastr";
import {
  PhieuNhapXuatDto,
  ThongTinChiTietThietBiDto,
} from "src/app/models/PhieuNhapXuatDto";
import { PhieuNhapXuatService } from "./phieu-nhap-xuat.service";
import { NzTableQueryParams } from "ng-zorro-antd/table";
import { NhanSuService } from "../nhan-su/nhan-su.service";
import { finalize } from "rxjs";
import { LoaderService } from "src/app/services/loader.service";
import { en_US, NzI18nService } from "ng-zorro-antd/i18n";
import { formatDate } from "@angular/common";

@Component({
  selector: "app-phieu-nhap-xuat",
  templateUrl: "./phieu-nhap-xuat.component.html",
  styleUrls: ["./phieu-nhap-xuat.component.css"],
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
  filter = "";

  dsThietBi: any[] = [];
  dsNhanSu: any[] = [];
  dsKhoa: any[] = [];
  dateFormat = "dd/MM/yyyy";

  listChiTietThietBi: ThongTinChiTietThietBiDto[] = [];
  isView = false;

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuNhapXuatService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService,
    private loadingService: LoaderService,
    private i18n: NzI18nService
  ) {}
  ngOnInit(): void {
    this.i18n.setLocale(en_US);
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
      ma: ["", [Validators.required]],
      ngayNhapXuat: new FormControl(new Date()),
      nhaCungCap: [""],
      nguoiDaiDien: [""],
      nhaVienId: [0],
      soLuong: [0],
      tongTien: [0],
      ghiChu: [""],
      loaiPhieu: [0],
      thongTinChiTietThietBiDtos: this.formbulider.array([]),
    });

    this.form.controls["soLuong"].disable();
    this.form.controls["tongTien"].disable();
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
    this.service
      .getListPhieuNhap(body)
      .pipe(finalize(() => this.loadingService.setLoading(false)))
      .subscribe((val) => {
        this.danhSach = val.items;
        this.total = val.totalCount;
      });
  }

  openModalCreatePhieuNhap() {
    this.reset();
    this.isShowModal = true;
    this.isView = false;
    this.title = "Thêm mới phiếu nhập";
    this.form.enable();
    this.form.get("loaiPhieu")?.patchValue(1);
    this.form
      .get("ngayNhapXuat")
      ?.patchValue(formatDate(new Date(), "yyyy-MM-dd", "en"));
  }
  getTenNhanSu(id) {
    return this.dsNhanSu.find((_) => _.id == id).ten;
  }

  openModalUpdate(data) {
    this.getById(data.id);
    this.isShowModal = true;
    this.isView = true;
    this.title = `Xem chi tiết: ${data.ma}`;
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
      this.listChiTietThietBi = [];
      if (
        result.thongTinChiTietThietBiDtos != null &&
        result.thongTinChiTietThietBiDtos.length > 0
      ) {
        this.listChiTietThietBi = result.thongTinChiTietThietBiDtos;
      }
      this.form.patchValue(result);
      this.form
        .get("ngayNhapXuat")
        ?.patchValue(
          formatDate(new Date(result.ngayNhapXuat), "yyyy-MM-dd", "en")
        );
      this.form.disable();
    });
  }

  add() {
    var item: ThongTinChiTietThietBiDto = {
      id: 0,
      ma: "",
      thietBiYTeId: 0,
      ngayNhap: null,
      xuatXu: "",
      namSX: 0,
      hangSanXuat: "",
      tinhTrang: null,
      khoaId: null,
      nhanVienId: null,
      serial: "",
      model: "",
      giaTien: null,
      thoiGianBaoDuong: null,
      daXuat: null,
    };
    this.listChiTietThietBi.push(item);
    this.listChiTietThietBi = [...this.listChiTietThietBi];
    this.form.controls["soLuong"].patchValue(this.listChiTietThietBi.length);
  }

  deleteDetail(i) {
    this.listChiTietThietBi.splice(i, 1);
    this.listChiTietThietBi = [...this.listChiTietThietBi];
    this.form.controls["soLuong"].patchValue(this.listChiTietThietBi.length);
  }

  onQueryParamsChange(data: NzTableQueryParams) {
    if (this.pageIndex != data.pageIndex || this.pageSize != data.pageSize) {
      this.pageIndex = data.pageIndex;
      this.pageSize = data.pageSize;
    }
  }

  reset() {
    this.form.reset();
    this.form.get("id")?.patchValue(0);
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
    this.create();
  }

  create() {
    const input = this.form.value;
    input.soLuong = this.listChiTietThietBi.length;
    input.tongTien = 0;
    this.listChiTietThietBi.forEach((item) => {
      input.tongTien = input.tongTien + Number(item.giaTien);
    });
    input.nhaVienId = 0;
    this.service
      .create(input)
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(
        (val) => {
          if (val.isSuccessful) {
            this.getList();
            this.form.reset();
            this.isShowModal = false;
            this.toastr.success("Data Saved Successfully");
          } else {
            this.toastr.error(val.errorMessage);
          }
        },
        (error) => {
          if (error.status == 400) {
            this.toastr.error("Cần nhập đủ thông tin");
          }
        }
      );
  }

  onBlurMethod() {
    var total = 0;
    this.listChiTietThietBi.forEach((item) => {
      total = total + Number(item.giaTien);
    });

    this.form.controls["tongTien"].patchValue(total);
  }
}
