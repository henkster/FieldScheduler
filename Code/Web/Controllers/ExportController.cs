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

        public ActionResult ForRefereeAssignor()
        {
            var games = Context.Games.Where(
                g =>
                !g.CanceledOn.HasValue &&
                g.AreRefereesNeeded &&
                EntityFunctions.TruncateTime(g.Slot.StartDateTime) >= EntityFunctions.TruncateTime(DateTime.Now)).
                OrderBy(g => g.Slot.StartDateTime);

            byte[] workbook = new GamesToExcelMapper().Map(games);

            return new FileContentResult(workbook, "application/excel")
                       {
                           FileDownloadName = "TSC_Games_Referee.xlsx"
                       };
        }

        public ActionResult FullReport()
        {
            var games = Context.Games.Where(
                g =>
                !g.CanceledOn.HasValue).
                OrderBy(g => g.Slot.StartDateTime);

            byte[] workbook = new GamesToExcelMapper().Map(games);

            return new FileContentResult(workbook, "application/excel")
            {
                FileDownloadName = "TSC_Games_Full.xlsx"
            };
        }
    }
}
