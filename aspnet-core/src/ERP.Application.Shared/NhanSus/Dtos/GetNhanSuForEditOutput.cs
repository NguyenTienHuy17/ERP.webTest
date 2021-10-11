using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.NhanSus.Dtos
{
    public class GetNhanSuForEditOutput
    {
        public CreateOrEditNhanSuDto NhanSu { get; set; }

    }
}