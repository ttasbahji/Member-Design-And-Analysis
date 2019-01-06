using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StructuralMemberDesignandAnalysis.Classes
{
    public class LouverPassData

    {

        public LouverPassData() { }


        // this will be key and the object to iterate
        public static ObservableCollection<object> LouverList = new ObservableCollection<object>();


        public static ObservableCollection<Member> MemberList = new ObservableCollection<Member>();

        public void CheckAddibilityLouverObject(object SubLouverMember)
        {

            switch (SubLouverMember)
            {
                case Louver.LouverBlade LouverBlade:


                    switch (LouverBlade.CalculationType)
                    {
                        case MemberDesignText.CalculationType.GetCalculationStepsType:
                            LouverList.Add(LouverBlade);
                            break;
                        case MemberDesignText.CalculationType.GetCheckCalculationType:

                            LouverList.Add(LouverBlade);

                            break;
                        case MemberDesignText.CalculationType.GetFinalSolutionCalculationType:

                            if (!LouverBlade.NeedBladeSupport)
                            {

                                LouverList.Add(LouverBlade);

                            }
                            break;
                        default:
                            Console.WriteLine("Nothing");
                            break;
                    }







                    break;
                case Louver.BuildUpLouver BuildUpLouver:




                    break;
               
                case Louver.LouverAnchors LouverAnchors:




                    break;
                default:
                    Console.WriteLine("if not Suit do this");
                    break;
            }


        }



    }
}
