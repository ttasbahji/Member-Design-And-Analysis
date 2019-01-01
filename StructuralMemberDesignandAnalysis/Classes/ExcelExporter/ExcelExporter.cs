using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralMemberDesignandAnalysis.Classes
{
    public class ExcelExporter
    {
        public class LouverBladeExcelSheet
        {
            Louver.LouverBlade CalculatedLouverBlade;
            public LouverBladeExcelSheet(Louver.LouverBlade CalculatedLouverBlade)
            {

                this.CalculatedLouverBlade = CalculatedLouverBlade;

            }
            private void GenerateBladeLouverSheet()
            {
                //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //app.Visible = true;
                //app.WindowState = XlWindowState.xlMaximized;

                //Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                //Worksheet ws = wb.Worksheets[1];
                //DateTime currentDate = DateTime.Now;

                //ws.Range["A1:A3"].Value = "Who is number one? 🙂";
                //ws.Range["A4"].Value = "vitoshacademy.com";
                //ws.Range["A5"].Value = currentDate;
                //ws.Range["B6"].Value = "Tommorow's date is: =>";
                //ws.Range["C6"].FormulaLocal = "= A5 + 1";
                //ws.Range["A7"].FormulaLocal = "=SUM(D1:D10)";
                //for (int i = 1; i <= 10; i++)
                //    ws.Range["D" + i].Value = i * 2;

                //wb.SaveAs("C:\\Temp\\vitoshacademy.xlsx");

            }

        }
    }
}

