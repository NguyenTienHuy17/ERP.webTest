using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.PhuongXas.Dtos
{
    public class GetPhuongXaForEditOutput
    {
        public CreateOrEditPhuongXaDto PhuongXa { get; set; }

        public string ThanhPhoMaTP { get; set; }

    }
}