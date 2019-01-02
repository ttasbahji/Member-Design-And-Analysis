using System;
namespace StructuralMemberDesignandAnalysis.Classes

{
    public class Louver : Member
    {
        public Louver() { }

        public class LouverBlade

        {




            /// <summary>
            /// Note to my tyseer , bro , Here the Db Will create the LoverBlade Object 
            /// I Mean it Will the Give the Properties of the Blade Section by the name of the section UR Job BABy.
            /// 
            /// </summary>
            public string Model;
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

            public string Material { get; set; }

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

            public void SetUpLoverBlade()
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


                    NeedBladeSupport = true;


                    // try again by dividing the length by two till u get the goal we all want
                    Lmax = Lmax / 2;


                    SetUpLoverBlade();

                }



            }

            void CheckSpacingDeflection()
            {

                if (MaxDeflection < AllowedServiceabilityDeflection)
                {
                    NeedBladeSupport = false;

                    // here we will report Result what happend with u PC 
                    ReportResult();


                }
                else
                {
                    // try again by dividing the length by two till u get the goal we all want

                    NeedBladeSupport = true;

                    // here we will report Result what happend with u PC 
                    ReportResult();

                    Lmax = Lmax / 2;



                    SetUpLoverBlade();
                }



            }


            void ReportResult()
            {
                //todo 
            }


        }


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