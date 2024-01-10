import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { finalize } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuBaoDuongService } from "./phieu-bao-duong.service";
import { NzModalService } from "ng-zorro-antd/modal";
import { LoaiThietBiService } from "../loai-thiet-bi/loai-thiet-bi.service";
import { PhieuSuaChuaDto } from "src/app/models/PhieuSuaChuaDto";
import { NhanSuService } from "../nhan-su/nhan-su.service";
import { PhieuBaoDuongDto } from "src/app/models/PhieuBaoDuongDto";
import { LoaderService } from "src/app/services/loader.service";
import { NzTableQueryParams } from "ng-zorro-antd/table";

@Component({
  selector: "app-phieu-bao-duong",
  templateUrl: "./phieu-bao-duong.component.html",
  styleUrls: ["./phieu-bao-duong.component.css"],
})
export class PhieuBaoDuongComponent implements OnInit {
  danhSach: PhieuBaoDuongDto[];

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
  dsNhanSu: any[] = [];

  checked = false;
  listOfCurrentPageData: readonly any[] = [];
  setOfCheckedId = new Set<number>();
  disabled = false;

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuBaoDuongService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService,
    private loadingService: LoaderService
  ) {}

  ngOnInit() {
    this.service.getDanhSachThietBi().subscribe((val) => {
      this.dsThietBi = val;
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
    this.disabled = false;
    this.isShowModal = true;
    this.title = "Thêm mới";
    this.id = 0;
    this.setOfCheckedId = new Set<number>();
  }

  openModalUpdate(data) {
    this.getById(data.id);
    this.isShowModal = true;
    this.title = `Sửa: ${data.ma}`;
  }

  getById(id: number) {
    this.loadingService.setLoading(true);
    this.service
      .getById(id)
      .pipe(
        finalize(() => {
          this.loadingService.setLoading(false);
          this.isConfirmLoading = false;
        })
      )
      .subscribe((result) => {
        this.id = result.id;
        this.setOfCheckedId = new Set<number>(result.danhSachThietBi);
      });
  }

  save() {
    if (this.setOfCheckedId.size == 0) {
      this.toastr.error("Phải chọn thiết bị cần bảo dưỡng");
      return;
    }
    this.isConfirmLoading = true;
    if (this.id == 0) {
      this.create();
    } else {
      this.update();
    }
  }

  create() {
    this.loadingService.setLoading(true);
    this.service
      .create({
        id: 0,
        danhSachThietBi: Array.from(this.setOfCheckedId),
        ma: "",
      })
      .pipe(
        finalize(() => {
          this.isConfirmLoading = false;
          this.loadingService.setLoading(false);
        })
      )
      .subscribe(() => {
        this.getList();
        this.isShowModal = false;
        this.toastr.success("Data Saved Successfully");
      });
  }

  update() {
    this.loadingService.setLoading(true);
    this.service
      .update({
        id: this.id,
        danhSachThietBi: Array.from(this.setOfCheckedId),
        ma: "",
      })
      .pipe(
        finalize(() => {
          this.isConfirmLoading = false;
          this.loadingService.setLoading(false);
        })
      )
      .subscribe((val) => {
        if (val.isSuccessful) {
          this.toastr.success("Data Updated Successfully");
          this.isShowModal = false;
          this.getList();
        } else {
          this.toastr.error(val.errorMessage);
        }
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

  xemChiTiet(id) {
    this.loadingService.setLoading(true);
    this.service
      .getById(id)
      .pipe(finalize(() => this.loadingService.setLoading(false)))
      .subscribe((val) => {
        this.disabled = true;
        this.isShowModal = true;
        this.title = `Xem chi tiết: ${val.ma}`;
        this.setOfCheckedId = new Set<number>(val.danhSachThietBi);
      });
  }

  updateCheckedSet(id: number, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }

  onCurrentPageDataChange(listOfCurrentPageData: readonly any[]): void {
    this.listOfCurrentPageData = listOfCurrentPageData;
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    this.checked = this.listOfCurrentPageData.every(({ id }) =>
      this.setOfCheckedId.has(id)
    );
  }

  onItemChecked(id: number, checked: boolean): void {
    this.updateCheckedSet(id, checked);
    this.refreshCheckedStatus();
  }

  onAllChecked(checked: boolean): void {
    this.listOfCurrentPageData
      .filter(({ disabled }) => !disabled)
      .forEach(({ id }) => this.updateCheckedSet(id, checked));
    this.refreshCheckedStatus();
  }
}
