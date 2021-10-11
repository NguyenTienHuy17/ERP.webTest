using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.ThanhPhos.Dtos;
using ERP.Dto;
using ERP.Storage;

namespace ERP.ThanhPhos.Exporting
{
    public class ThanhPhosExcelExporter : EpPlusExcelExporterBase, IThanhPhosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ThanhPhosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetThanhPhoForViewDto> thanhPhos)
        {
            return CreateExcelPackage(
                "ThanhPhos.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ThanhPhos"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("MaTP"),
                        L("TenTP"),
                        L("MoTa"),
                        L("ZipCode")

                        );

                    AddObjects(
                        sheet, 2, thanhPhos,
                        _ => _.ThanhPho.MaTP,
                        _ => _.ThanhPho.TenTP,
                        _ => _.ThanhPho.MoTa,
                        _ => _.ThanhPho.ZipCode
                        );

                });
        }
    }
}