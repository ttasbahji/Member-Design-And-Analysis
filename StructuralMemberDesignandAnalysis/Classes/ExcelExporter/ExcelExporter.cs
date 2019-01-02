using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace StructuralMemberDesignandAnalysis.Classes
{
    public class ExcelExporter
    {

        private ExcelExporter()
        {

        }

        // Pattern Design Singlton
        private static ExcelExporter ExcelExporterinstance = null;

        public static ExcelExporter ExcelExporterInstance
        {
            get
            {
                if (ExcelExporterinstance == null)
                {
                    ExcelExporterinstance = new ExcelExporter();
                }
                return ExcelExporterinstance;
            }


        }

        public void DisposeInstance()
        {
            try
            {
                ExcelExporterinstance = null;

                // THis topic Is Called Garbage Collection BRO // Watch about interesting
                GC.Collect();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }




        public class LouverExcelWorkBook
        {
            Application Application;
            Workbook LouverWorkBook;

            // Pattern Design Singlton
            private static LouverExcelWorkBook LouverExcelWorkBookinstance = null;


            private LouverExcelWorkBook()
            {
                Application = new Application();

                LouverWorkBook = Application.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            }

            public static LouverExcelWorkBook LouverExcelWorkBookInstance
            {
                get
                {
                    if (LouverExcelWorkBookinstance == null)
                    {
                        LouverExcelWorkBookinstance = new LouverExcelWorkBook();
                    }
                    return LouverExcelWorkBookinstance;
                }


            }

            public void DisposeInstance()
            {
                try
                {
                    LouverExcelWorkBookinstance = null;

                    // THis topic Is Called Garbage Collection BRO // Watch about interesting
                    GC.Collect();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex);
                }
            }



            private void GenerateBladeLouverSheet(Louver.LouverBlade LouverBlade)
            {
                Application app = new Application();
                app.Visible = true;
                app.WindowState = XlWindowState.xlMaximized;

                Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet ws = wb.Worksheets[1] as Worksheet;
                DateTime currentDate = DateTime.Now;

                ws.Range["A1:A3"].Value = "Who is number one? 🙂";
                ws.Range["A4"].Value = "vitoshacademy.com";
                ws.Range["A5"].Value = currentDate;
                ws.Range["B6"].Value = "Tommorow's date is: =>";
                ws.Range["C6"].FormulaLocal = "= A5 + 1";
                ws.Range["A7"].FormulaLocal = "=SUM(D1:D10)";
                for (int i = 1; i <= 10; i++)
                    ws.Range["D" + i].Value = i * 2;

                wb.SaveAs("C:\\Temp\\vitoshacademy.xlsx");

            }
            private void GenerateBuildUpLouverSheet(Louver.BuildUpLouver BuildUpLouver)
            {
                //todo
            }
            private void GenerateReadySectionLouverSheet(Louver.ReadySectionLouver ReadySectionLouver)
            {
                //todo


            }
            private void GenerateLouverAnchorsSheet(Louver.LouverAnchors LouverAnchors)
            {

                //todo

            }

            public void GenerateExcelSheetSublouverMember(object SubLouverMember)
            {

                switch (SubLouverMember)
                {
                    case Louver.LouverBlade LouverBlade:


                        GenerateBladeLouverSheet(SubLouverMember as Louver.LouverBlade);

                        break;
                    case Louver.BuildUpLouver BuildUpLouver:


                        GenerateBuildUpLouverSheet(SubLouverMember as Louver.BuildUpLouver);


                        break;
                    case Louver.ReadySectionLouver ReadySectionLouver:


                        GenerateReadySectionLouverSheet(SubLouverMember as Louver.ReadySectionLouver);


                        break;
                    case Louver.LouverAnchors LouverAnchors:


                        GenerateLouverAnchorsSheet(SubLouverMember as Louver.LouverAnchors);


                        break;
                    default:
                        Console.WriteLine("if not Suit do this");
                        break;
                }



            }
        }


        void SaveWorkBook(Workbook CurrentWorkBook, string FullPathWorkBook)
        {

            CurrentWorkBook.SaveAs(MemberDesignSettings.MemberDesignSettingsInstance.FullExportedFilePath);


        }
    }
}

