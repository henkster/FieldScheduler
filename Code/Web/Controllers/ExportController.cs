using System;
using System.Data.Objects;
using System.Linq;
using System.Web.Mvc;
using Services;

namespace Web.Controllers
{
    public class ExportController : ApplicationController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Games()
        {
            var games = Context.Games.Where(
                g =>
                !g.CanceledOn.HasValue &&
                EntityFunctions.TruncateTime(g.Slot.StartDateTime) >= EntityFunctions.TruncateTime(DateTime.Now));

            byte[] workbook = new GamesToExcelMapper().Map(games);

            return new FileContentResult(workbook, "application/excel")
                       {
                           FileDownloadName = "TSC_Games.xlsx"
                       };
        }
    }
}
