using System;
namespace StructuralMemberDesignandAnalysis.Classes.HelperMemberDesignAnalysis
{
    public class HelperResultExporter
    {


        // Pattern Design Singlton
        private static HelperResultExporter helperResultExporterinstance = null;


        private HelperResultExporter()
        {
        }

        public static HelperResultExporter HelperResultExporterInstance
        {
            get
            {
                if (helperResultExporterinstance == null)
                {
                    helperResultExporterinstance = new HelperResultExporter();
                }
                return helperResultExporterinstance;
            }


        }

        public void DisposeInstance()
        {
            try
            {
                helperResultExporterinstance = null;

                // THis topic Is Called Garbage Collection BRO // Watch about interesting
                GC.Collect();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }




        public void ExportObjects()
        {
            switch (MemberDesignSettings.MemberDesignSettingsInstance.ExporterType)
            {
                case MemberDesignText.ExportersTypes.ExcelExporterType:


                    //ExcelExporter.LouverBladeExcelSheet
                    break;
                case MemberDesignText.ExportersTypes.PDFExporterType:

                    break;
                default:
                    Console.WriteLine("if not Suit do this");
                    break;
            }
        }


      

    }
}
