import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PhongBanService } from './phong-ban.service';
import { ToastrService } from 'ngx-toastr';
import { NzModalService } from 'ng-zorro-antd/modal';
import { KhoaDto } from 'src/app/models/KhoaDto';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-phong-ban',
  templateUrl: './phong-ban.component.html',
  styleUrls: ['./phong-ban.component.css']
})
export class PhongBanComponent implements OnInit {
  danhSach: KhoaDto[];
  form: FormGroup;

  id = 0;
  title = '';
  isShowModal = false;
  isConfirmLoading = false;
  filter = '';
  pageIndex = 1;
  pageSize = 10;
  total = 0;
  constructor(
    private formbulider: FormBuilder,
    private service: PhongBanService,
    private toastr: ToastrService,
    private modal: NzModalService
  ) {}

  ngOnInit() {
    this.form = this.formbulider.group({
      id: [0, [Validators.required]],
      ma: ['', [Validators.required]],
      ten: ['', [Validators.required]],
      sdt: ['', [Validators.required]],
      email: ['', [Validators.required]],
      truongKhoaId: [0],
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
    this.title = 'Thêm mới';
    this.form.reset();
    this.form.get('id')?.setValue(0);
  }
  openModalUpdate(data) {
    this.getById(data.id);
    this.isShowModal = true;
    this.title = `Sửa: ${data.name}`;
  }

  save() {
    const input = this.form.value;
    if (this.form.invalid) {
      this.toastr.error('Cần nhập đủ thông tin');
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
    input.truongKhoaId = 0;
    this.service
      .create(input)
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(() => {
        this.getList();
        this.form.reset();
        this.isShowModal = false;
        this.toastr.success('Data Saved Successfully');
      });
  }

  update() {
    const input = this.form.value;
    this.service
      .update(input)
      .pipe(finalize(() => (this.isConfirmLoading = false)))
      .subscribe(() => {
        this.toastr.success('Data Updated Successfully');
        this.form.reset();
        this.isShowModal = false;
        this.getList();
      });
  }

  delete(id: number, ten) {
    this.modal.confirm({
      nzTitle: 'Xác nhận xóa',
      nzContent: `Bạn có muốn xóa khoa: <b>${ten}</b> không`,
      nzOnOk: () =>
        this.service.delete(id).subscribe(() => {
          this.toastr.success('Data Deleted Successfully');
          this.getList();
        }),
    });
  }

  getById(id: number) {
    this.service.getById(id).subscribe((result) => {
      this.id = result.id;
      this.form.patchValue(result);
    });
  }
}
