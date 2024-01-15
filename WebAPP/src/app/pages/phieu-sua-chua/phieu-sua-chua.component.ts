import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { finalize } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuSuaChuaService } from "./phieu-sua-chua.service";
import { NzModalService } from "ng-zorro-antd/modal";
import { LoaiThietBiService } from "../loai-thiet-bi/loai-thiet-bi.service";
import { PhieuSuaChuaDto } from "src/app/models/PhieuSuaChuaDto";
import { NhanSuService } from "../nhan-su/nhan-su.service";
import { LoaderService } from "src/app/services/loader.service";
import { UserSessionService } from "src/app/layout/user-session.service";
import { NzTableQueryParams } from "ng-zorro-antd/table";

@Component({
  selector: "app-phieu-sua-chua",
  templateUrl: "./phieu-sua-chua.component.html",
  styleUrls: ["./phieu-sua-chua.component.css"],
})
export class PhieuSuaChuaComponent implements OnInit {
  danhSach: PhieuSuaChuaDto[];
  form: FormGroup;

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
  dsThietBiSuaChua: any[] = [];
  dsNhanSu: any[] = [];

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuSuaChuaService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService,
    private loadingService: LoaderService,
    public userService: UserSessionService
  ) {}

  ngOnInit() {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      chiTietThietBiId: ["", [Validators.required]],
      lyDo: ["", [Validators.required]],
    });

    this.service.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
      this.dsThietBiSuaChua = val.filter(
        (_) =>
          _.daXuat != true && this.userService.user?.nhanVienId == _.nhanVienId
      );
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

  openModalCreate() {
    this.isShowModal = true;
    this.title = "Thêm mới";
    this.form.reset();
    this.form.get("chiTietThietBiId")?.setValue("");
    this.form.get("id")?.setValue(0);
  }

  save() {
    if (this.form.invalid) {
      this.toastr.error("Cần nhập đủ thông tin");
      return;
    }
    this.isConfirmLoading = true;
    this.create();
  }

  create() {
    const input = this.form.value;
    const nhanVienId = this.dsThietBi.find(
      (_) => _.id == input.chiTietThietBiId
    )?.nhanVienId;
    this.loadingService.setLoading(true);
    this.service
      .create({ ...input, nhanVienId, ma: "" })
      .pipe(
        finalize(() => {
          this.isConfirmLoading = false;
          this.loadingService.setLoading(false);
        })
      )
      .subscribe(() => {
        this.getList();
        this.form.reset();
        this.isShowModal = false;
        this.toastr.success("Data Saved Successfully");
      });
  }

  delete(data) {
    this.modal.confirm({
      nzTitle: "Xác nhận xóa",
      nzContent: `Bạn có muốn xóa phiếu <b>'${data.ma}'</b> không`,
      nzOnOk: () => {
        this.loadingService.setLoading(true);
        this.service
          .delete(data.id)
          .pipe(finalize(() => this.loadingService.setLoading(false)))
          .subscribe(() => {
            this.toastr.success("Data Deleted Successfully");
            this.getList();
          });
      },
    });
  }

  approve(id) {
    this.modal.confirm({
      nzTitle: "Xác nhận duyệt",
      nzContent: `Bạn có muốn duyệt phiếu không`,
      nzOnOk: () => {
        this.loadingService.setLoading(true);
        this.service
          .approve(id)
          .pipe(finalize(() => this.loadingService.setLoading(false)))
          .subscribe((val) => {
            if (val.isSuccessful) {
              this.toastr.success("Duyệt phiếu thành công");
              this.getList();
            } else {
              this.toastr.error(val.errorMessage);
            }
          });
      },
    });
  }

  deny(id) {
    this.modal.confirm({
      nzTitle: "Xác nhận từ chối",
      nzContent: `Bạn có muốn từ chối phiếu không`,
      nzOnOk: () => {
        this.loadingService.setLoading(true);
        this.service
          .deny(id)
          .pipe(finalize(() => this.loadingService.setLoading(false)))
          .subscribe((val) => {
            if (val.isSuccessful) {
              this.toastr.success("Từ chối phiếu thành công");
              this.getList();
            } else {
              this.toastr.error(val.errorMessage);
            }
          });
      },
    });
  }

  completed(id) {
    this.modal.confirm({
      nzTitle: "Xác nhận hoàn thành",
      nzContent: `Bạn muốn xác nhận thiết bị sửa xong không`,
      nzOnOk: () => {
        this.loadingService.setLoading(true);
        this.service
          .completed(id)
          .pipe(finalize(() => this.loadingService.setLoading(false)))
          .subscribe((val) => {
            if (val.isSuccessful) {
              this.toastr.success("Xác nhận phiếu thành công");
              this.getList();
            } else {
              this.toastr.error(val.errorMessage);
            }
          });
      },
    });
  }
}
