import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { finalize } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuThuHoiService } from "./phieu-thu-hoi.service";
import { NzModalService } from "ng-zorro-antd/modal";
import { LoaiThietBiService } from "../loai-thiet-bi/loai-thiet-bi.service";
import { PhieuSuaChuaDto } from "src/app/models/PhieuSuaChuaDto";
import { NhanSuService } from "../nhan-su/nhan-su.service";
import { PhieuThuHoiDto } from "src/app/models/PhieuThuHoiDto";

@Component({
  selector: "app-phieu-thu-hoi",
  templateUrl: "./phieu-thu-hoi.component.html",
  styleUrls: ["./phieu-thu-hoi.component.css"],
})
export class PhieuThuHoiComponent implements OnInit {
  danhSach: PhieuThuHoiDto[];

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

  checked = false;
  listOfCurrentPageData: readonly any[] = [];
  setOfCheckedId = new Set<number>();
  disabled = false;

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuThuHoiService,
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
      filter: this.filter,
      maxResultCount: this.pageSize,
      skipCount: (this.pageIndex - 1) * this.pageSize,
    };
    this.service.getList(body).subscribe((val) => {
      this.danhSach = val.items;
      this.total = val.totalCount;
    });
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
      .create({
        id: 0,
        ma: "",
        danhSachThietBi: Array.from(this.setOfCheckedId),
      })
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(() => {
        this.getList();
        this.isShowModal = false;
        this.toastr.success("Data Saved Successfully");
      });
  }

  xemChiTiet(id) {
    this.service.getById(id).subscribe((val) => {
      this.disabled = true;
      this.isShowModal = true;
      this.title = "Xem chi tiết";
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