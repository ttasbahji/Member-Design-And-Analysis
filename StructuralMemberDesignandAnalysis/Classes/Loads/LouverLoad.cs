using System;
namespace StructuralMemberDesignandAnalysis.Classes
{
    public class LouverLoad
    {
        public LouverLoad(float horizentalDistributedLoad)
        {
            HorizentalDistributedLoad = horizentalDistributedLoad;
        }

       public float HorizentalDistributedLoad
         { set; get; }
      

    }
}
