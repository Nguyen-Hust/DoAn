import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { finalize } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { ThietBiYTeDto } from "src/app/models/ThietBiYTeDto";
import { PhieuBanGiaoService } from "./phieu-ban-giao.service";
import { NzModalService } from "ng-zorro-antd/modal";
import { LoaiThietBiService } from "../loai-thiet-bi/loai-thiet-bi.service";
import { PhieuSuaChuaDto } from "src/app/models/PhieuSuaChuaDto";
import { NhanSuService } from "../nhan-su/nhan-su.service";
import { PhieuBanGiaoDto } from "src/app/models/PhieuBanGiaoDto";
import { LoaderService } from "src/app/services/loader.service";
import { NzTableQueryParams } from "ng-zorro-antd/table";

@Component({
  selector: "app-phieu-ban-giao",
  templateUrl: "./phieu-ban-giao.component.html",
  styleUrls: ["./phieu-ban-giao.component.css"],
})
export class PhieuBanGiaoComponent implements OnInit {
  danhSach: PhieuBanGiaoDto[];
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

  checked = false;
  listOfCurrentPageData: readonly any[] = [];
  setOfCheckedId = new Set<number>();
  disabled = false;

  constructor(
    private formbulider: FormBuilder,
    private service: PhieuBanGiaoService,
    private nhanSuService: NhanSuService,
    private toastr: ToastrService,
    private modal: NzModalService,
    private loadingService: LoaderService
  ) {}

  ngOnInit() {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      nhanVienNhan: ["", [Validators.required]],
    });
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
    this.form.reset();
    this.form.get("nhanVienNhan")?.setValue("");
    this.form.get("id")?.setValue(0);
  }

  save() {
    if (this.setOfCheckedId.size == 0) {
      this.toastr.error("Phải chọn thiết bị cần bảo dưỡng");
      return;
    }
    if (this.form.invalid) {
      this.toastr.error("Cần nhập đủ thông tin");
      return;
    }
    this.isConfirmLoading = true;
    this.create();
  }

  create() {
    const input = this.form.value;
    this.loadingService.setLoading(true);
    this.service
      .create({
        ...input,
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
        this.service.getDanhSachThietBi().subscribe((val) => {
          this.dsThietBi = val;
        });
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
        this.title = "Xem chi tiết";
        this.setOfCheckedId = new Set<number>(val.danhSachThietBi);
        this.form.get("nhanVienNhan")?.disable();
        this.form.patchValue(val);
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
