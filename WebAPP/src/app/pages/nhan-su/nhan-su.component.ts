import { ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { NhanSuDto } from "src/app/models/NhanSuDto";
import { NhanSuService } from "./nhan-su.service";
import { ToastrService } from "ngx-toastr";
import { NzModalService } from "ng-zorro-antd/modal";
import { finalize } from "rxjs";
import { KhoaDto } from "src/app/models/KhoaDto";
import { LoaderService } from "src/app/services/loader.service";
import { NzTableQueryParams } from "ng-zorro-antd/table";

@Component({
  selector: "app-nhan-su",
  templateUrl: "./nhan-su.component.html",
  styleUrls: ["./nhan-su.component.css"],
})
export class NhanSuComponent implements OnInit {
  danhSach: NhanSuDto[];
  listKhoa: KhoaDto[];
  form: FormGroup;
  formAccount: FormGroup;

  id = 0;
  title = "";
  isShowModal = false;
  isConfirmLoading = false;
  filter = "";
  pageIndex = 1;
  pageSize = 10;
  total = 0;

  isShowModalAccount = false;

  constructor(
    private formbulider: FormBuilder,
    private service: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService,
    private cdr: ChangeDetectorRef,
    private loadingService: LoaderService
  ) {}

  ngOnInit() {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      ma: ["", [Validators.required]],
      ten: ["", [Validators.required]],
      sdt: [""],
      email: ["", [Validators.required]],
      khoaId: [0, [Validators.required]],
      diaChi: [""],
      accountId: [""],
      laQuanLyThietBi: [false],
    });
    this.formAccount = this.formbulider.group({
      email: ["", [Validators.required]],
      userName: ["", [Validators.required]],
      password: ["", [Validators.required]],
      nhanVienId: [null, [Validators.required]],
      loaiAccount: ["user", [Validators.required]],
    });
    this.getListKhoa();
    this.getList();
  }

  getListKhoa() {
    this.service.getAllKhoa().subscribe((data) => {
      this.listKhoa = data;
    });
  }

  getList() {
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
        this.cdr.markForCheck();
      });
  }

  onQueryParamsChange(data: NzTableQueryParams) {
    if (this.pageIndex != data.pageIndex || this.pageSize != data.pageSize) {
      this.pageIndex = data.pageIndex;
      this.pageSize = data.pageSize;
      this.getList();
    }
  }

  delete(id: number, ten) {
    this.modal.confirm({
      nzTitle: "Xác nhận xóa",
      nzContent: `Bạn có muốn xóa nhân viên: <b>${ten}</b> không`,
      nzOnOk: () => {
        this.loadingService.setLoading(true);
        this.service
          .delete(id)
          .pipe(finalize(() => this.loadingService.setLoading(false)))
          .subscribe(() => {
            this.toastr.success("Data Deleted Successfully");
            this.getList();
          });
      },
    });
  }

  getById(id: number) {
    this.loadingService.setLoading(true);
    this.service
      .getById(id)
      .pipe(finalize(() => this.loadingService.setLoading(false)))
      .subscribe((result) => {
        this.id = result.id;
        this.form.patchValue(result);
        if (result.accountId == null) {
          this.form.get("accountId")?.setValue("");
        }
      });
  }

  openModalCreate() {
    this.isShowModal = true;
    this.title = "Thêm mới";
    this.form.reset();
    this.form.get("khoaId")?.setValue("");
    this.form.get("sdt")?.setValue("");
    this.form.get("diaChi")?.setValue("");
    this.form.get("id")?.setValue(0);
  }
  openModalUpdate(data) {
    this.getById(data.id);
    this.isShowModal = true;
    this.title = `Sửa: ${data.ten}`;
  }

  openModalAccount(data) {
    this.formAccount.get("email")?.setValue(data.email);
    this.formAccount.get("nhanVienId")?.setValue(data.id);
    if (data.laQuanLyThietBi)
      this.formAccount.get("loaiAccount")?.setValue("manager");
    else this.formAccount.get("loaiAccount")?.setValue("user");
    this.isShowModalAccount = true;
  }

  register() {
    const input = this.formAccount.getRawValue();
    this.loadingService.setLoading(true);
    if (input.loaiAccount == "user") {
      this.service
        .registerUser(input)
        .pipe(finalize(() => this.loadingService.setLoading(false)))
        .subscribe((val) => {
          if (val.isSuccessful) {
            this.toastr.success("Cấp tài khoản thành công");
            this.getList();
            this.formAccount.reset();
            this.isShowModalAccount = false;
          } else {
            this.toastr.error(val.errorMessage);
          }
        });
    } else {
      this.service
        .registerAdmin(input)
        .pipe(finalize(() => this.loadingService.setLoading(false)))
        .subscribe((val) => {
          if (val.isSuccessful) {
            this.toastr.success("Cấp tài khoản thành công");
            this.getList();
            this.formAccount.reset();
            this.isShowModalAccount = false;
          } else {
            this.toastr.error(val.errorMessage);
          }
        });
    }
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
    } else {
      this.create();
    }
  }

  create() {
    const input = this.form.value;
    input.accountId = "";
    this.loadingService.setLoading(true);
    this.service
      .create(input)
      .pipe(
        finalize(() => {
          this.isConfirmLoading = false;
          this.loadingService.setLoading(false);
        })
      )
      .subscribe((val) => {
        if (val.isSuccessful) {
          this.getList();
          this.form.reset();
          this.isShowModal = false;
          this.toastr.success("Data Saved Successfully");
        } else {
          this.toastr.error(val.errorMessage);
        }
      });
  }

  update() {
    const input = this.form.value;
    this.loadingService.setLoading(true);
    this.service
      .update(input)
      .pipe(
        finalize(() => {
          this.isConfirmLoading = false;
          this.loadingService.setLoading(false);
        })
      )
      .subscribe((val) => {
        if (val.isSuccessful) {
          this.toastr.success("Data Updated Successfully");
          this.form.reset();
          this.isShowModal = false;
          this.getList();
        } else {
          this.toastr.error(val.errorMessage);
        }
      });
  }

  getTenKhoa(id) {
    if(id != null || id != undefined) {
      var khoa = this.listKhoa.find(_ => _.id == id);
      return khoa?.ten;
    }
    return "";
  }
}
