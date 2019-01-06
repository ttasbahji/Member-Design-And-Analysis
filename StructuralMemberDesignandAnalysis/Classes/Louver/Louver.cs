using System;
namespace StructuralMemberDesignandAnalysis.Classes

{
    public class Louver : Member
    {
        public Louver() { }

        public Louver(LouverLoad louverLoad, LouverBlade loverBlade
        , LouverAnchors northLouverAnchor, LouverAnchors eastLouverAnchor
            , LouverAnchors westLouverAnchor, LouverAnchors southLouverAnchor)
        {
            LouverLoad = louverLoad;
            LoverBlade = loverBlade;
            NorthLouverAnchor = northLouverAnchor;
            EastLouverAnchor = eastLouverAnchor;
            WestLouverAnchor = westLouverAnchor;
            SouthLouverAnchor = southLouverAnchor;
        }


        // two Way , One Way
        public String LouverLoadType;


       
        LouverLoad LouverLoad;
        LouverBlade LoverBlade;
        LouverAnchors NorthLouverAnchor;
        LouverAnchors EastLouverAnchor;
        LouverAnchors WestLouverAnchor;
        LouverAnchors SouthLouverAnchor;

        public class LouverBlade : ReadySection
        {

            LouverLoad CurrentLouverLoad { set; get; }
            LouverBladeProperty CurrentLouverBladeProperty { set; get; }

          
            public LouverBlade(float allowedServiceabilityDeflection, float deflectionLimit, float lmax, LouverBladeProperty CurrentLouverBlade,LouverLoad currentLouverLoad)
            {
               
              
                AllowedServiceabilityDeflection = allowedServiceabilityDeflection;
              
                DeflectionLimit = deflectionLimit;
                Lmax = lmax;

                CurrentLouverLoad = currentLouverLoad;
                HorizentalPressure=currentLouverLoad.HorizentalDistributedLoad;
                this.CurrentLouverBladeProperty = CurrentLouverBlade;
            }



            public string CalculationType { get; set; }

          

            /// <summary>
            /// change according to code of design
            /// </summary>
            /// <value>The allowed serviceability deflection.</value>
            public float AllowedServiceabilityDeflection { get; set; }


          


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

                DistributedHorizentalLoad = HorizentalPressure * CurrentLouverBladeProperty.WTr;
                MaxBendingMoment = DistributedHorizentalLoad * Lmax * Lmax;

                Mny = CurrentLouverBladeProperty.Fy * CurrentLouverBladeProperty.Sy;
                Mnx = CurrentLouverBladeProperty.Fy * CurrentLouverBladeProperty.Sx;
                Mnc = CurrentLouverBladeProperty.Fy * CurrentLouverBladeProperty.Sc;

                // check Moment 
                MinimumMomentCapacity = Math.Min(Mny, Math.Min(Mnx, Mnx));


                Lady = (float)Math.Sqrt((8 * Mny) / DistributedHorizentalLoad);// this called casting bro cuz the value  will be double not float
                Ladx = (float)Math.Sqrt((8 * Mnx) / DistributedHorizentalLoad);// this called casting bro cuz the value  will be double not float
                Ladc = (float)Math.Sqrt((8 * Mnc) / DistributedHorizentalLoad);// this called casting bro cuz the value  will be double not float

                MaximumAllowableContinuousLengthLoadGenerated = Math.Min(Lady, Math.Min(Ladc, Ladx));


                // servicbility check
                Δy = 5 * DistributedHorizentalLoad * ((Lmax * Lmax * Lmax * Lmax) / (CurrentLouverBladeProperty.E * CurrentLouverBladeProperty.Iy * 384));
                Δx = 5 * DistributedHorizentalLoad * ((Lmax * Lmax * Lmax * Lmax) / (CurrentLouverBladeProperty.E * CurrentLouverBladeProperty.Ix * 384));
                Δc = 5 * DistributedHorizentalLoad * ((Lmax * Lmax * Lmax * Lmax) / (CurrentLouverBladeProperty.E * CurrentLouverBladeProperty.Ic * 384));

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

                LouverPassData FilterData = new LouverPassData();

                if (MaxDeflection < AllowedServiceabilityDeflection)
                {

                    // Succeeded
                    NeedBladeSupport = false;


                    FilterData.CheckAddibilityLouverObject(this);


                }
                else
                {

                    // fail 
                    NeedBladeSupport = true;

                    FilterData.CheckAddibilityLouverObject(this.DeepCopy());// since there is not object with in our louverblade

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



            public LouverBlade DeepCopy()
            {
                LouverBlade LouverBlade = (LouverBlade)this.MemberwiseClone();

                LouverBlade.CurrentLouverBladeProperty = new LouverBladeProperty(CurrentLouverBladeProperty.Model, CurrentLouverBladeProperty.Material
                , CurrentLouverBladeProperty.Ix, CurrentLouverBladeProperty.Iy, CurrentLouverBladeProperty.Ic, CurrentLouverBladeProperty.Sx,CurrentLouverBladeProperty.Sy,
                    CurrentLouverBladeProperty.Sc, CurrentLouverBladeProperty.Area, CurrentLouverBladeProperty.Depth, CurrentLouverBladeProperty.E,CurrentLouverBladeProperty.Fy
                , CurrentLouverBladeProperty.WTr,CurrentLouverBladeProperty.γ);

                LouverBlade.CurrentLouverLoad = new LouverLoad(CurrentLouverLoad.HorizentalDistributedLoad);


                return LouverBlade;
            }
            // change to this bro

            public class LouverBladeProperty
            {
                public LouverBladeProperty(string model, string material, float ix, float iy, float ic, float sx
                , float sy, float sc, float area, float depth, float e, float fy, float wTr, float γ)
                {
                    Model = model;
                    Material = material;
                    Ix = ix;
                    Iy = iy;
                    Ic = ic;
                    Sx = sx;
                    Sy = sy;
                    Sc = sc;
                    Area = area;
                    Depth = depth;
                    E = e;
                    Fy = fy;
                    WTr = wTr;
                    this.γ = γ;
                }



                /// <summary>
                /// Note to my tyseer , bro , Here the Db Will create the LoverBlade Object 
                /// I Mean it Will the Give the Properties of the Blade Section by the name of the section UR Job BABy.
                /// 
                /// </summary>
                public string Model { get; set; }

                public string Material { get; set; }

                public float Ix { get; set; }
                public float Iy { get; set; }
                public float Ic { get; set; }

                public float Sx { get; set; }
                public float Sy { get; set; }
                public float Sc { get; set; }


                public float Area { get; set; }
                public float Depth { get; set; }
                public float E { get; set; }

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
            }


            }


      

        public class LouverAnchors : Connection
        {

            LouverLoad CurrentLouverLoad; 

            public LouverAnchors(string markUpRefrence, string connectionLocation, string baseMaterial, string altMaterial
            , ConnectionType connectionTypeBase, ConnectionType connectionTypeAlt , LouverLoad CurrentLouverLoad)
            {
                MarkUpRefrence = markUpRefrence;
                ConnectionLocation = connectionLocation;
                BaseMaterial = baseMaterial;
                AltMaterial = altMaterial;
                ConnectionTypeBase = connectionTypeBase;
                ConnectionTypeAlt = connectionTypeAlt;

                this.CurrentLouverLoad = CurrentLouverLoad;
            }


            // to from louver Side
            public ConnectionType ConnectionTypeAlt { get; set; }

            // to structure SIde
            public ConnectionType ConnectionTypeBase { get; set; }


            /// will be Calculated from Analyze method

            public float ShearReaction { set; get; }

            public float NormalReaction{ set; get; }

            public float MomentReaction { set; get; }


            /// <summary>
            /// where u get the info from the drawings
            /// </summary>
            /// <value>The mark up refrence.</value>
            public string MarkUpRefrence { get; set; }


            /// <summary>
            ///This Equal to Side in xls north ,east or west
            /// </summary>
            /// <value>The connection location.</value>
            public string ConnectionLocation { get; set; }



            ///note : bro when the user choose BaseMaterial or AltMaterial  (Gmc or concrete )the list of bolts or anchors will be generated according
            /// to his choice

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

            /// <summary>
            /// 3/8" KWIK BOLT TZ   , #10-16 KWIK FLEX SELF DRILLING  ... ETC                                                  
            /// </summary>
            /// <value>The type of the connection.</value>
            public class ConnectionType
            {

                public AnchorBoltProperties AnchorBoltProperty { set; get; }

                public ConnectionType( AnchorBoltProperties anchorBoltsProperties )
                {
                    AnchorBoltProperty = anchorBoltsProperties;

                }

               
                // From DB TUTu
                public class AnchorBoltProperties 
                {

                    public AnchorBoltProperties(float dbt, float lbt, float lbe, float fuT, float fdT, float fuS, float fdS)
                    {
                        Dbt = dbt;
                        Lbt = lbt;
                        Lbe = lbe;
                        FuT = fuT;
                        FdT = fdT;
                        FuS = fuS;
                        FdS = fdS;
                    }


                    // b is bolt
                    public float Dbt { get; set; }
                    public float Lbt { get; set; }
                    public float Lbe { get; set; }
                    public float FuT { get; set; }
                    public float FdT { get; set; }
                    public float FuS { get; set; }
                    public float FdS { get; set; }

                }


            }


            void AnalyzeLouverLoad()
            {


            }



            void CheckAnchorLoad()
            {





            }

            void CheckInnerAnhcorSpacing()
            {



            }
            void CheckEdgeAnchorSpacing()
            {



            }





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

        public override void SetUpMember()
        {


            this.NorthLouverAnchor.SetUpSubMember();
            this.SouthLouverAnchor.SetUpSubMember();
            this.EastLouverAnchor.SetUpSubMember();
            this.WestLouverAnchor.SetUpSubMember();
            this.LoverBlade.SetUpSubMember();


        }

        public class LouverFrame 
        {

            public float Width { get; set; }
            public float Height { get; set; }
            public float Thickness { get; set; }
            public string DistributionLoadType { get; set; }
            public float NorthSideLoadRatio { get; set; }
            public float SouthSideLoadRatio { get; set; }
            public float EastSideLoadRatio { get; set; }
            public float WestSideLoadRatio { get; set; }
            public bool WidthIsLongest { get; set; } = false;
            public float γ { get; set; }

            void CheckLouverType()
            {


                float LongestEdge = Math.Max(Height, Width);
                float ShorterEdge = Math.Min(Height, Width);

                if (Height < Width)
                {

                    WidthIsLongest = true;

                }
                var AspectRatio = LongestEdge / ShorterEdge;


                if (AspectRatio > 1)
                {

                    DistributionLoadType = MemberDesignText.LouverStrings.OneWayLouverText;
                }
                else
                {
                    DistributionLoadType = MemberDesignText.LouverStrings.TwoWayLouverText;


                }


            }

            void GetLoadDistributionRatio()
            {

                if (DistributionLoadType == MemberDesignText.LouverStrings.OneWayLouverText && WidthIsLongest)
                {

                    NorthSideLoadRatio = 0.5f;
                    SouthSideLoadRatio = 0.5f;
                    WestSideLoadRatio = 1;
                    EastSideLoadRatio = 1;


                }
                else if (DistributionLoadType == MemberDesignText.LouverStrings.OneWayLouverText && !WidthIsLongest)
                {
                    NorthSideLoadRatio = 1;
                    SouthSideLoadRatio = 1;
                    WestSideLoadRatio = 0.5f;
                    EastSideLoadRatio = 0.5f;

                }
                else
                {


                    NorthSideLoadRatio = 0.5f;
                    SouthSideLoadRatio = 0.5f;
                    WestSideLoadRatio = 0.5f;
                    EastSideLoadRatio = 0.5f;


                }



            }
        }

    }

}