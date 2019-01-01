using System;
namespace StructuralMemberDesignandAnalysis.Classes.Louver

{
    public class Louver : Member
    {
        public Louver() { }



        public override void AnalyticCompressionCapacity()
        {
            throw new NotImplementedException();
        }

        public override void AnalyticMomentCapacity()
        {
            throw new NotImplementedException();
        }

        public override void AnalyticShearCapacity()
        {
            throw new NotImplementedException();
        }

        public override void AnalyticTensionCapacity()
        {
            throw new NotImplementedException();
        }

        public override void DesignCompressionCapacity()
        {
            throw new NotImplementedException();
        }

        public override void DesignMomentCapacity()
        {
            throw new NotImplementedException();
        }

        public override void DesignShearCapacity()
        {
            throw new NotImplementedException();
        }

        public override void DesignTensionCapacity()
        {
            throw new NotImplementedException();
        }


       

        public class LouverAnchors : Member.Connection
        {


        }

        public class BuildUpLouver : Member.BuildUpSetion
        {


        }

        public class ReadSectionLouver : Member.ReadySection
        {


        }

    }

}