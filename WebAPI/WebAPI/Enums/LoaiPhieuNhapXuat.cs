using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Enums
{
	public enum  LoaiPhieuNhapXuat
	{
		[Display(Name = "Nhập")]
		Nhap = 1,
        [Display(Name = "Xuất")]
        Xuat = 1,

    }
}

