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
  pageIndex = 1;
  pageSize = 10;
  total = 0;

  dsThietBi: any[] = [];
  dsNhanSu: any[] = [];

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuSuaChuaService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService
  ) {}

  ngOnInit() {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      chiTietThietBiId: ["", [Validators.required]],
      lyDo: ["", [Validators.required]],
    });

    this.service.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
    });

    this.nhanSuService.getList({ filter: "" }).subscribe((val) => {
      this.dsNhanSu = val.items;
    });

    this.getList();
  }

  getList(pageIndex = 1) {
    this.pageIndex = pageIndex;
    var body = {
      date: this.date,
      maxResultCount: this.pageSize,
      skipCount: (this.pageIndex - 1) * this.pageSize,
    };
    this.service.getList(body).subscribe((val) => {
      this.danhSach = val.items;
      this.total = val.totalCount;
    });
  }

  getTenThietBi(id) {
    return this.dsThietBi.find((_) => _.id == id).ma;
  }

  getTenNhanSu(id) {
    return this.dsNhanSu.find((_) => _.id == id).ten;
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
    this.service
      .create({ ...input, nhanVienId })
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(() => {
        this.getList();
        this.form.reset();
        this.isShowModal = false;
        this.toastr.success("Data Saved Successfully");
      });
  }

  delete(id: number) {
    this.modal.confirm({
      nzTitle: "Xác nhận xóa",
      nzContent: `Bạn có muốn xóa phiếu không`,
      nzOnOk: () =>
        this.service.delete(id).subscribe(() => {
          this.toastr.success("Data Deleted Successfully");
          this.getList();
        }),
    });
  }

  approve(id) {
    this.modal.confirm({
      nzTitle: "Xác nhận duyệt",
      nzContent: `Bạn có muốn duyệt phiếu không`,
      nzOnOk: () =>
        this.service.approve(id).subscribe((val) => {
          if (val.isSuccessful) {
            this.toastr.success("Duyệt phiếu thành công");
            this.getList();
          } else {
            this.toastr.error(val.errorMessage);
          }
        }),
    });
  }

  deny(id) {
    this.modal.confirm({
      nzTitle: "Xác nhận từ chối",
      nzContent: `Bạn có muốn từ chối phiếu không`,
      nzOnOk: () =>
        this.service.deny(id).subscribe((val) => {
          if (val.isSuccessful) {
            this.toastr.success("Từ chối phiếu thành công");
            this.getList();
          } else {
            this.toastr.error(val.errorMessage);
          }
        }),
    });
  }

  completed(id) {
    this.modal.confirm({
      nzTitle: "Xác nhận hoàn thành",
      nzContent: `Bạn muốn xác nhận thiết bị sửa xong không`,
      nzOnOk: () =>
        this.service.completed(id).subscribe((val) => {
          if (val.isSuccessful) {
            this.toastr.success("Xác nhận phiếu thành công");
            this.getList();
          } else {
            this.toastr.error(val.errorMessage);
          }
        }),
    });
  }
}
