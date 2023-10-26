import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { finalize } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { LoaiThietBiService } from "./loai-thiet-bi.service";
import { NzModalService } from "ng-zorro-antd/modal";

@Component({
  selector: "app-loai-thiet-bi",
  templateUrl: "./loai-thiet-bi.component.html",
  styleUrls: ["./loai-thiet-bi.component.css"],
})
export class LoaiThietBiComponent implements OnInit {
  danhSach: ThietBiYTeDto[];
  form: FormGroup;

  id = 0;
  title = "";
  isShowModal = false;
  isConfirmLoading = false;
  filter = "";
  pageIndex = 1;
  pageSize = 10;
  total = 0;

  constructor(
    private formbulider: FormBuilder,
    private service: LoaiThietBiService,
    private toastr: ToastrService,
    private modal: NzModalService
  ) {}

  ngOnInit() {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      ma: ["", [Validators.required]],
      ten: ["", [Validators.required]],
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

  openModalCreate() {
    this.isShowModal = true;
    this.title = "Thêm mới";
    this.form.reset();
    this.form.get("id")?.setValue(0);
  }

  openModalUpdate(data) {
    this.getById(data.id);
    this.isShowModal = true;
    this.title = `Sửa: ${data.name}`;
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

  getById(id: number) {
    this.service.getById(id).subscribe((result) => {
      this.id = result.id;
      this.form.patchValue(result);
    });
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

  delete(id: number, ten) {
    this.modal.confirm({
      nzTitle: "Xác nhận xóa",
      nzContent: `Bạn có muốn xóa loại thiết bị: <b>${ten}</b> không`,
      nzOnOk: () =>
        this.service.delete(id).subscribe(() => {
          this.toastr.success("Data Deleted Successfully");
          this.getList();
        }),
    });
  }
}
