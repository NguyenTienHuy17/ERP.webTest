using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.ThanhPhos.Dtos
{
    public class GetThanhPhoForEditOutput
    {
        public CreateOrEditThanhPhoDto ThanhPho { get; set; }

    }
}