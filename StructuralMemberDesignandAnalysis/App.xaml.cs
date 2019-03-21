using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StructuralMemberDesignandAnalysis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

    }


//    static void AddLouverUI()
//    {
//        Louver CurrentLouver = new Louver(60 * 12, 12 * 6, 4);

//        Louver.LouverAnalysis louverAnalysis = new Louver.LouverAnalysis(ref CurrentLouver);

//        louverAnalysis.RunAnalysis();
//        LouverLoad louverLoad = new LouverLoad(65);
//        Louver.LouverBlade.LouverBladeProperty louverBladePro = new Louver.LouverBlade.LouverBladeProperty(
//        "EA-413 WIND DRIVEN SIGHT PROOF", "Aluminu", 0.036f
//        , 1.965f, 0.016f, 0.0615f, 0.6764f, 0.032f, 0.66f, 4, 10100, 9600, 3.5f, 0.098f);
//        Louver.LouverBlade louverBlade = new Louver.LouverBlade(180, (60 * 12), louverBladePro, louverLoad, CurrentLouver, Classes.MemberDesignText.CalculationType.GetCalculationStepsType);

//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesNorthBase =
//        new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.375f, 34f, 2f, 4475f, 1520, 3070, 1685);
//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesNorthAlt =
//new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.190f, 0.75f, 0.75f, 920f, 306.6666667f, 1465, 488.3333333f);
//        Louver.LouverAnchors.ConnectionType NorthBaseConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesNorthBase, " 3 / 8 KWIK BOLT TZ ");
//        Louver.LouverAnchors.ConnectionType NorthAltConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesNorthAlt, "#10-16 KWIK FLEX SELF DRILLING");




//        Louver.LouverAnchors NorthlouverAnchors = new Louver.LouverAnchors("A12", Classes.MemberDesignText.LouverStrings.NorthSideConnection
//            , "Cracked Concrete", " 0.1875 Thick Alum.Angle", NorthBaseConnection
//            , NorthAltConnection, louverLoad, CurrentLouver.NorthSideLoadRatio
//            , ref CurrentLouver);


//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesSouthBase =
//        new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.375f, 34f, 2f, 4475f, 1520, 3070, 1685);
//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesSouthAlt =
//new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.190f, 0.75f, 0.75f, 920f, 306.6666667f, 1465, 488.3333333f);
//        Louver.LouverAnchors.ConnectionType SouthBaseConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesSouthBase, " 3 / 8 KWIK BOLT TZ ");
//        Louver.LouverAnchors.ConnectionType SouthAltConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesSouthAlt, "#10-16 KWIK FLEX SELF DRILLING");

//        Louver.LouverAnchors SouthlouverAnchors = new Louver.LouverAnchors("A12", Classes.MemberDesignText.LouverStrings.SouthSideConnection
//           , "Cracked Concrete", " 0.1875 Thick Alum.Angle", SouthBaseConnection
//           , SouthAltConnection, louverLoad, CurrentLouver.SouthSideLoadRatio
//           , ref CurrentLouver);

//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesEastBase =
//                 new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.25f, 21, 21, 1700f, 310, 2250, 400);
//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesEastAlt =
//new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.190f, 0.75f, 0.75f, 920f, 306.6666667f, 1465, 488.3333333f);

//        Louver.LouverAnchors.ConnectionType EastBaseConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesEastBase, "1/4  KWIK CON II +");
//        Louver.LouverAnchors.ConnectionType EastAltConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesEastAlt, "#10-16 KWIK FLEX SELF DRILLING");

//        Louver.LouverAnchors EastlouverAnchors = new Louver.LouverAnchors("D14", Classes.MemberDesignText.LouverStrings.EastSideConnection
//           , "Cracked Concrete", " 0.1875 Thick Alum.Angle", EastBaseConnection
//           , EastAltConnection, louverLoad, CurrentLouver.EastSideLoadRatio
//           , ref CurrentLouver);

//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesWestBase =
//                           new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.25f, 21, 21, 1700f, 310, 2250, 400);
//        Louver.LouverAnchors.ConnectionType.AnchorBoltProperties anchorBoltPropertiesWestAlt =
//new Louver.LouverAnchors.ConnectionType.AnchorBoltProperties(0.190f, 0.75f, 0.75f, 920f, 306.6666667f, 1465, 488.3333333f);

//        Louver.LouverAnchors.ConnectionType WestBaseConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesWestBase, "1/4  KWIK CON II +");
//        Louver.LouverAnchors.ConnectionType WestAltConnection = new Louver.LouverAnchors.ConnectionType(anchorBoltPropertiesWestAlt, "#10-16 KWIK FLEX SELF DRILLING");

//        Louver.LouverAnchors WestlouverAnchors = new Louver.LouverAnchors("D14", Classes.MemberDesignText.LouverStrings.WestSideConnection
//           , "Cracked Concrete", " 0.1875 Thick Alum.Angle", WestBaseConnection
//           , WestAltConnection, louverLoad, CurrentLouver.WestSideLoadRatio
//           , ref CurrentLouver);



//        CurrentLouver.CreateLouver(louverLoad, louverBlade, NorthlouverAnchors, EastlouverAnchors, WestlouverAnchors, SouthlouverAnchors, louverAnalysis);

//        // on event button
//        CurrentLouver.SetUpMember();
//    }

}
