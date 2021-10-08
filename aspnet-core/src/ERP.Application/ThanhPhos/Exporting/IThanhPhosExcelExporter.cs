using System.Collections.Generic;
using ERP.ThanhPhos.Dtos;
using ERP.Dto;

namespace ERP.ThanhPhos.Exporting
{
    public interface IThanhPhosExcelExporter
    {
        FileDto ExportToFile(List<GetThanhPhoForViewDto> thanhPhos);
    }
}