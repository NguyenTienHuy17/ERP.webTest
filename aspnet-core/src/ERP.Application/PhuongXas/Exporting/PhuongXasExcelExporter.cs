using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.PhuongXas.Dtos;
using ERP.Dto;
using ERP.Storage;

namespace ERP.PhuongXas.Exporting
{
    public class PhuongXasExcelExporter : EpPlusExcelExporterBase, IPhuongXasExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PhuongXasExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPhuongXaForViewDto> phuongXas)
        {
            return CreateExcelPackage(
                "PhuongXas.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PhuongXas"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("MaPhuong"),
                        L("TenPhuong"),
                        L("SoDan"),
                        L("ChuTichPhuong"),
                        (L("ThanhPho")) + L("MaTP")
                        );

                    AddObjects(
                        sheet, 2, phuongXas,
                        _ => _.PhuongXa.MaPhuong,
                        _ => _.PhuongXa.TenPhuong,
                        _ => _.PhuongXa.SoDan,
                        _ => _.PhuongXa.ChuTichPhuong,
                        _ => _.ThanhPhoMaTP
                        );

                });
        }
    }
}