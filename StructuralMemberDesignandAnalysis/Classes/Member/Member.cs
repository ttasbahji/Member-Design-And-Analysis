using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructuralMemberDesignandAnalysis.Classes;

namespace StructuralMemberDesignandAnalysis.Classes

{
   public abstract class Member : DesignMemberInterface, AnalyticMemberInterface , MemberCycleInterface
    {
      public  Member() { }
        public float Width { get; set; }
        public float Height { get; set; }



        public abstract class ReadySection : SubMemberCycleInterface
        {
            public ReadySection()
            {
            }

            public string Material { get; set; }
            public string Code { get; set; }

            public abstract void SetUpSubMember();
        }


        public abstract class BuildUpSetion : SubMemberCycleInterface
        {
            public BuildUpSetion()
            {
            }

            public string Material { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public float Ix { get; set; }
            public float Iy { get; set; }
            public float Sx { get; set; }
            public float Sy { get; set; }
            public float E { get; set; }
            public float UltimateShearStress { get; set; }
            public float UltimateTensileStress { get; set; }
            public float UltimateCompressionStress { get; set; }
            public float DesignShearStress { get; set; }
            public float DesignTensileStress { get; set; }
            public float DesignCompressionStress { get; set; }

            public abstract void SetUpSubMember();
        }
        public abstract class Connection : SubMemberCycleInterface
        {
            public Connection()
            {
            }

            public string Material { get; set; }
            public float UltimateShearStress { get; set; }
            public float UltimateTensileStress { get; set; }
            public float UltimateCompressionStress { get; set; }
            public float DesignShearStress { get; set; }
            public float DesignTensileStress { get; set; }
            public float DesignCompressionStress { get; set; }

            public abstract void SetUpSubMember();
        }
        public abstract void DesignMomentCapacity();
        public abstract void DesignShearCapacity();
        public abstract void DesignTensionCapacity();
        public abstract void DesignCompressionCapacity();
        public abstract void AnalyticMomentCapacity();
        public abstract void AnalyticShearCapacity();
        public abstract void AnalyticTensionCapacity();
        public abstract void AnalyticCompressionCapacity();
        public abstract void SetUpMember();
    }
}
