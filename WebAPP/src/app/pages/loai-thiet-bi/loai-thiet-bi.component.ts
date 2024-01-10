import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { finalize } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { LoaiThietBiService } from "./loai-thiet-bi.service";
import { NzModalService } from "ng-zorro-antd/modal";
import { LoaderService } from "src/app/services/loader.service";
import { NzTableQueryParams } from "ng-zorro-antd/table";

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
    private modal: NzModalService,
    private loadingService: LoaderService
  ) {}

  ngOnInit() {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      ma: ["", [Validators.required]],
      ten: ["", [Validators.required]],
    });
    this.getList();
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
      });
  }

  onQueryParamsChange(data: NzTableQueryParams) {
    if (this.pageIndex != data.pageIndex || this.pageSize != data.pageSize) {
      this.pageIndex = data.pageIndex;
      this.pageSize = data.pageSize;
      this.getList();
    }
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
    this.loadingService.setLoading(true);
    this.service
      .getById(id)
      .pipe(
        finalize(() => {
          this.loadingService.setLoading(false);
        })
      )
      .subscribe((result) => {
        this.id = result.id;
        this.form.patchValue(result);
      });
  }

  create() {
    const input = this.form.value;
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

  delete(id: number, ten) {
    this.modal.confirm({
      nzTitle: "Xác nhận xóa",
      nzContent: `Bạn có muốn xóa loại thiết bị: <b>${ten}</b> không`,
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
}
