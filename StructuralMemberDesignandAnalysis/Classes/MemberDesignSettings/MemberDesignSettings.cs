using System;
using System.IO;

namespace StructuralMemberDesignandAnalysis.Classes
{
    public class MemberDesignSettings
    {

        public string ExporterType { set; get; }


        //for Future
        //public string LangaugeType { set; get; }

        public string UnitsType { set; get; }

        // default Value Set to be the Desktop bro
        public string ExportedFileFolderPath { set; get; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // default 
        public string ExportedFileName { set; get; } = "Result";

        // default 
        //public  string FullExportedFilePath { set; get; } = 
        public string FullExportedFilePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ExportedFileName);
            }
            set
            {

                FullExportedFilePath = value;

            }

        }


        // Pattern Design Singlton
        private static MemberDesignSettings MemberDesignSettingsinstance = null;


        private MemberDesignSettings()
        {
        }

        public static MemberDesignSettings MemberDesignSettingsInstance
        {
            get
            {
                if (MemberDesignSettingsinstance == null)
                {
                    MemberDesignSettingsinstance = new MemberDesignSettings();
                }
                return MemberDesignSettingsinstance;
            }


        }

        public void DisposeInstance()
        {
            try
            {
                MemberDesignSettingsinstance = null;

                // THis topic Is Called Garbage Collection BRO // Watch about interesting
                GC.Collect();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }







    }
}
