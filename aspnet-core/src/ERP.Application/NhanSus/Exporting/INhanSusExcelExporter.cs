using System.Collections.Generic;
using ERP.NhanSus.Dtos;
using ERP.Dto;

namespace ERP.NhanSus.Exporting
{
    public interface INhanSusExcelExporter
    {
        FileDto ExportToFile(List<GetNhanSuForViewDto> nhanSus);
    }
}