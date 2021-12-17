using System.Collections.Generic;
using ERP.PhuongXas.Dtos;
using ERP.Dto;

namespace ERP.PhuongXas.Exporting
{
    public interface IPhuongXasExcelExporter
    {
        FileDto ExportToFile(List<GetPhuongXaForViewDto> phuongXas);
    }
}