using System;
namespace StructuralMemberDesignandAnalysis.Classes

{
    public class Louver : Member
    {
        public Louver() { }

        public class LouverBlade : ReadySection
        {




            /// <summary>
            /// Note to my tyseer , bro , Here the Db Will create the LoverBlade Object 
            /// I Mean it Will the Give the Properties of the Blade Section by the name of the section UR Job BABy.
            /// 
            /// </summary>
            public string Model { get; set; }
            /// <summary>
            /// Note tutu habibi : this the stuff u must specify to be albe to calculate the stuff for the ouver blade
            /// </summary>
            /// <param name="model">Model.</param>
            /// <param name="material">Material.</param>
            /// <param name="ix">Ix.</param>
            /// <param name="iy">Iy.</param>
            /// <param name="ic">Ic.</param>
            /// <param name="sx">Sx.</param>
            /// <param name="sy">Sy.</param>
            /// <param name="sc">Sc.</param>
            /// <param name="allowedServiceabilityDeflection">Allowed serviceability deflection.</param>
            /// <param name="area">Area.</param>
            /// <param name="depth">Depth.</param>
            /// <param name="fy">Fy.</param>
            /// <param name="wTr">W tr.</param>
            /// <param name="γ">Γ.</param>
            /// <param name="deflectionLimit">Deflection limit.</param>
            /// <param name="horizentalPressure">Horizental pressure.</param>
            /// <param name="lmax">Lmax.</param>
            /// <param name="e">E.</param>
            public LouverBlade(string model, string material, float ix, float iy, float ic, float sx, float sy, float sc, float allowedServiceabilityDeflection, float area, float depth, float fy, float wTr, float γ, float deflectionLimit, float horizentalPressure, float lmax, float e)
            {
                Model = model;
                Material = material;
                Ix = ix;
                Iy = iy;
                Ic = ic;
                Sx = sx;
                Sy = sy;
                Sc = sc;
                AllowedServiceabilityDeflection = allowedServiceabilityDeflection;
                Area = area;
                Depth = depth;
                Fy = fy;
                WTr = wTr;
                this.γ = γ;
                DeflectionLimit = deflectionLimit;
                HorizentalPressure = horizentalPressure;
                Lmax = lmax;
                E = e;
            }

            public string CalculationType { get; set; }

            public float Ix { get; set; }
            public float Iy { get; set; }
            public float Ic { get; set; }

            public float Sx { get; set; }
            public float Sy { get; set; }
            public float Sc { get; set; }

            /// <summary>
            /// change according to code of design
            /// </summary>
            /// <value>The allowed serviceability deflection.</value>
            public float AllowedServiceabilityDeflection { get; set; }


            public float Area { get; set; }
            public float Depth { get; set; }

            /// <summary>
            /// design  
            /// </summary>
            /// <value>The fy.</value>
            public float Fy { get; set; }
            /// <summary>
            /// tributary width
            /// </summary>
            /// <value>The WTr.</value>
            public float WTr { get; set; }
            /// <summary>
            ///  is Unit Weight.
            /// unit is Weight / Volume
            /// </summary>
            /// <value>The γ.</value>
            public float γ { get; set; }


            /// <summary>
            /// accoring to code what is the limit for deflection 
            /// </summary>
            /// <value>The deflection limit.</value>
            public float DeflectionLimit { get; set; }

            /// <summary>
            /// like wind Pressure 
            /// Unit : Stress / Area
            /// </summary>
            /// <value>The horizental pressure.</value>
            public float HorizentalPressure { get; set; }


            public float Lmax { get; set; }

            public float E { get; set; }


            public bool NeedBladeSupport { get; set; }
            public float SupportSpaceing { get; set; }
            float DistributedHorizentalLoad { get; set; }

            /// <summary>
            /// Maxim. Bending Moment  occuring
            /// </summary>
            /// <value>The max bending moment.</value>
            float MaxBendingMoment { get; set; }

            /// <summary>
            /// Nominal Flexural Strength               
            /// </summary>
            float Mny { get; set; }
            float Mnx { get; set; }
            float Mnc { get; set; }


            float MinimumMomentCapacity { get; set; }


            float Δy { get; set; }
            float Δx { get; set; }
            float Δc { get; set; }
            float MaxDeflection { get; set; }



            /// <summary>
            /// Allowable Continuous Length  
            /// </summary>
            /// <value>The lady.</value>
            float Lady { get; set; }
            float Ladx { get; set; }
            float Ladc { get; set; }

            float MaximumAllowableContinuousLengthLoadGenerated { get; set; }
            float MinimumContinuousLengthServiceabilityGenrated { get; set; }

            public void CheckLoverBladeSupportNeed()
            {
                // simple Supported BEam Assump

                DistributedHorizentalLoad = HorizentalPressure * WTr;
                MaxBendingMoment = DistributedHorizentalLoad * Lmax * Lmax;

                Mny = Fy * Sy;
                Mnx = Fy * Sx;
                Mnc = Fy * Sc;

                // check Moment 
                MinimumMomentCapacity = Math.Min(Mny, Math.Min(Mnx, Mnx));


                Lady = (float)Math.Sqrt((8 * Mny) / DistributedHorizentalLoad);// this called casting bro cuz the value  will be double not float
                Ladx = (float)Math.Sqrt((8 * Mnx) / DistributedHorizentalLoad);// this called casting bro cuz the value  will be double not float
                Ladc = (float)Math.Sqrt((8 * Mnc) / DistributedHorizentalLoad);// this called casting bro cuz the value  will be double not float

                MaximumAllowableContinuousLengthLoadGenerated = Math.Min(Lady, Math.Min(Ladc, Ladx));


                // servicbility check
                Δy = 5 * DistributedHorizentalLoad * ((Lmax * Lmax * Lmax * Lmax) / (E * Iy * 384));
                Δx = 5 * DistributedHorizentalLoad * ((Lmax * Lmax * Lmax * Lmax) / (E * Ix * 384));
                Δc = 5 * DistributedHorizentalLoad * ((Lmax * Lmax * Lmax * Lmax) / (E * Ic * 384));

                MaxDeflection = Math.Max(Δy, Math.Max(Δx, Δc));

                CheckSpacingMoment();

            }



            void CheckSpacingMoment()
            {
                if (MaximumAllowableContinuousLengthLoadGenerated > Lmax)
                {
                    NeedBladeSupport = false;

                    CheckSpacingDeflection();

                }
                else
                {

                    // Fail 

                    NeedBladeSupport = true;

                    CheckSpacingDeflection();

                }



            }

            void CheckSpacingDeflection()
            {

                    LouverFilterData FilterData = new LouverFilterData();

                    if (MaxDeflection < AllowedServiceabilityDeflection)
                    {

                        // Succeeded
                        NeedBladeSupport = false;


                        FilterData.CheckAddibilityLouverobject(this);


                    }
                    else
                    {

                        // fail 
                        NeedBladeSupport = true;

                        FilterData.CheckAddibilityLouverobject(this.ShallowCopy());// since there is not object with in our louverblade

                        //do again wtih different length
                        DecreaseLourverLength();

                    if (CalculationType == MemberDesignText.CalculationType.GetCheckCalculationType)
                        return;

                        CheckLoverBladeSupportNeed();


                    }









            }

            // here the we decrease the Length
            void DecreaseLourverLength()
            { 
            // try again by dividing the length by two till u get the goal we all want
            Lmax = Lmax / 2;

            }

            void ReportResult(string ExporterType)
            {





            }

            public override void SetUpSubMember()
            {
                CheckLoverBladeSupportNeed();
            }



            public LouverBlade ShallowCopy()
            {
                LouverBlade LouverBlade = (LouverBlade)this.MemberwiseClone();
               
                return LouverBlade;
            }

        }


        public override void AnalyticCompressionCapacity()
        {
        }

        public override void AnalyticMomentCapacity()
        {
        }

        public override void AnalyticShearCapacity()
        {
        }

        public override void AnalyticTensionCapacity()
        {
        }

        public override void DesignCompressionCapacity()
        {
        }

        public override void DesignMomentCapacity()
        {
        }

        public override void DesignShearCapacity()
        {
        }

        public override void DesignTensionCapacity()
        {
        }




        public class LouverAnchors : Connection
        {

            /// <summary>
            /// 3/8" KWIK BOLT TZ   , #10-16 KWIK FLEX SELF DRILLING  ... ETC                                                  
            /// </summary>
            /// <value>The type of the connection.</value>
            public string ConnectionType { get; set; }

            /// <summary>
            /// where u get the info from the drawings
            /// </summary>
            /// <value>The mark up refrence.</value>
            public string MarkUpRefrence { get; set; }


            /// <summary>
            ///This Equal to Side in xls
            /// </summary>
            /// <value>The connection location.</value>
            public string ConnectionLocation { get; set; }

           

            /// <summary>
            /// GMC , Cracked Concrete
            /// </summary>
            /// <value>The base material.</value>
            public string BaseMaterial { get; set; }

            /// <summary>
            /// #10-16 KWIK FLEX SELF DRILLING Etc                           
            /// </summary>
            /// <value>The alt material.</value>
            public string AltMaterial { get; set; }





            public override void SetUpSubMember()
            {
                throw new NotImplementedException();
            }
        }

        public class BuildUpLouver : BuildUpSetion
        {
            public override void SetUpSubMember()
            {
                throw new NotImplementedException();
            }
        }

        public class ReadySectionLouver : ReadySection
        {
            public override void SetUpSubMember()
            {
                throw new NotImplementedException();
            }
        }

    }

}