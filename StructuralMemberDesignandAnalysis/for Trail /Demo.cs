using System;
using StructuralMemberDesignandAnalysis.Classes.Louver;

namespace StructuralMemberDesignandAnalysis

{
    public class Demo{

        void Demo1()
        {
            // this is a deafult constructor 
            /// I mean here What do we need to create a Louver
            /// here nothing but it will be change according to louver
            Louver LoverDemo = new Louver();
            /// we Specify the create Connection 
            /// naming them As connection Kills me but if u want to change it ur choice . 
            /// however , u did not change it yesterday thus , I left it the same way
            Louver.Connection RightConnection = new Classes.Louver.Louver.Connection();
            Louver.Connection LeftConnection = new Classes.Louver.Louver.Connection();
            Louver.Connection UpperConnection = new Classes.Louver.Louver.Connection();
            Louver.Connection LowerConnection = new Classes.Louver.Louver.Connection();

            // here is the workers , function or methods , to calculate the stuff for us 
            LoverDemo.DesignTensionCapacity();
            LoverDemo.DesignMomentCapacity();
            LoverDemo.DesignShearCapacity();
            LoverDemo.DesignCompressionCapacity();


            /// hope u get me .


        }
    }

}