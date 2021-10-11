using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.NhanSus.Dtos;
using ERP.Dto;
using ERP.Storage;

namespace ERP.NhanSus.Exporting
{
    public class NhanSusExcelExporter : EpPlusExcelExporterBase, INhanSusExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public NhanSusExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetNhanSuForViewDto> nhanSus)
        {
            return CreateExcelPackage(
                "NhanSus.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.Workbook.Worksheets.Add(L("NhanSus"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("MaNhanSu"),
                        L("TenNhanSu"),
                        L("PhongBan"),
                        L("ThamNien"),
                        L("Tuoi"),
                        L("QueQuan")
                        );

                    AddObjects(
                        sheet, 2, nhanSus,
                        _ => _.NhanSu.MaNhanSu,
                        _ => _.NhanSu.TenNhanSu,
                        _ => _.NhanSu.PhongBan,
                        _ => _.NhanSu.ThamNien,
                        _ => _.NhanSu.Tuoi,
                        _ => _.NhanSu.QueQuan
                        );

                });
        }
    }
}