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
  pageIndex = 1;
  pageSize = 10;
  total = 0;

  dsThietBi: any[] = [];
  dsNhanSu: any[] = [];

  checked = false;
  listOfCurrentPageData: readonly any[] = [];
  setOfCheckedId = new Set<number>();

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuBaoDuongService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService
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
    this.setOfCheckedId = new Set<number>();
  }

  save() {
    if (this.setOfCheckedId.size == 0) {
      this.toastr.error("Phải chọn thiết bị cần bảo dưỡng");
      return;
    }
    this.isConfirmLoading = true;
    this.create();
  }

  create() {
    this.service
      .create({ id: 0, danhSachThietBi: Array.from(this.setOfCheckedId) })
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(() => {
        this.getList();
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
    const listOfEnabledData = this.listOfCurrentPageData.filter(
      ({ disabled }) => !disabled
    );
    this.checked = listOfEnabledData.every(({ id }) =>
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
