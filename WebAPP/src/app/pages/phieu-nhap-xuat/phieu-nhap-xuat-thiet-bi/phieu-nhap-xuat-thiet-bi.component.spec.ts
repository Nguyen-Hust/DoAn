import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhieuNhapXuatThietBiComponent } from './phieu-nhap-xuat-thiet-bi.component';

describe('PhieuNhapXuatThietBiComponent', () => {
  let component: PhieuNhapXuatThietBiComponent;
  let fixture: ComponentFixture<PhieuNhapXuatThietBiComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PhieuNhapXuatThietBiComponent]
    });
    fixture = TestBed.createComponent(PhieuNhapXuatThietBiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
