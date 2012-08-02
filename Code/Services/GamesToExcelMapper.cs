using System.Collections.Generic;
using System.IO;
using Domain;
using OfficeOpenXml;

namespace Services
{
    public class GamesToExcelMapper
    {
        public byte[] Map(IEnumerable<Game> games)
        {
            var result = new MemoryStream();
            var pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add("games");

            CreateHeader(ws);

            int row = 2;
            foreach (Game game in games)
            {
                ws.Cells[row, 1].Value = game.Id.ToString();
                ws.Cells[row, 2].Value = game.Slot.StartDateTime.ToString("MM/dd/yyyy");
                ws.Cells[row, 3].Value = game.Slot.StartDateTime.ToString("hh:mm tt");
                ws.Cells[row, 4].Value = string.Format("U{0}", game.Age);
                ws.Cells[row, 5].Value = game.Level.ToString();
                ws.Cells[row, 6].Value = game.Gender;
                ws.Cells[row, 7].Value = game.Slot.Field.Description;
                ws.Cells[row, 8].Value = game.Team1 != null ? game.Team1.FullName : string.Empty;
                ws.Cells[row, 9].Value = game.Team2 != null ? game.Team2.FullName : string.Empty;
                ws.Cells[row, 10].Value = game.Activity;
                ws.Cells[row, 11].Value = 3;
                ws.Cells[row, 12].Value = string.Empty;
                ws.Cells[row, 13].Value = game.Notes;

                row++;
            }
            pack.SaveAs(result);

            return result.ToArray();
        }

        private void CreateHeader(ExcelWorksheet ws)
        {
            ws.Cells[1, 1].Value = "GameNum";
            ws.Cells[1, 2].Value = "GameDate";
            ws.Cells[1, 3].Value = "GameTime";
            ws.Cells[1, 4].Value = "GameAge";
            ws.Cells[1, 5].Value = "GameLevel";
            ws.Cells[1, 6].Value = "Gender";
            ws.Cells[1, 7].Value = "Location";
            ws.Cells[1, 8].Value = "HomeTeam";
            ws.Cells[1, 9].Value = "AwayTeam";
            ws.Cells[1, 10].Value = "GameDescription";
            ws.Cells[1, 11].Value = "CrewSize";
            ws.Cells[1, 12].Value = "CrewDescription";
            ws.Cells[1, 13].Value = "Notes";
        }
    }
}