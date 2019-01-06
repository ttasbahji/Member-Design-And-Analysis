using System;
namespace StructuralMemberDesignandAnalysis.Classes

{
    public class Louver : Member
    {
        LouverAnalysis CurrentLouverAnalysis;
        public float γ { get; set; }

        public float Thickness { get; set; }

        public float NorthSideLoadRatio { get; set; } = 1;
        public float SouthSideLoadRatio { get; set; } = 1;
        public float EastSideLoadRatio { get; set; } = 1;
        public float WestSideLoadRatio { get; set; } = 1;

        public Louver(LouverLoad louverLoad, LouverBlade loverBlade
        , LouverAnchors northLouverAnchor, LouverAnchors eastLouverAnchor
            , LouverAnchors westLouverAnchor, LouverAnchors southLouverAnchor
            , LouverAnalysis CurrentLouverAnalysis)
        {
            LouverLoad = louverLoad;
            LoverBlade = loverBlade;

            this.CurrentLouverAnalysis = CurrentLouverAnalysis;

            NorthLouverAnchor = northLouverAnchor;
            EastLouverAnchor = eastLouverAnchor;
            WestLouverAnchor = westLouverAnchor;
            SouthLouverAnchor = southLouverAnchor;

        }


        // two Way , One Way
        public string LouverLoadType;



        LouverLoad LouverLoad;
        LouverBlade LoverBlade;
        LouverAnchors NorthLouverAnchor;
        LouverAnchors EastLouverAnchor;
        LouverAnchors WestLouverAnchor;
        LouverAnchors SouthLouverAnchor;

        public class LouverBlade : ReadySection
        {

            public LouverLoad CurrentLouverLoad { set; get; }
            public LouverBladeProperty CurrentLouverBladeProperty { set; get; }
            public Louver CurrentLouver { set; get; }


            public LouverBlade(float allowedServiceabilityDeflection
            , float deflectionLimit, float lmax
                , LouverBladeProperty CurrentLouverBlade
                , LouverLoad currentLouverLoad, Louver CurrentLouver)
            {


                AllowedServiceabilityDeflection = allowedServiceabilityDeflection;

                DeflectionLimit = deflectionLimit;
                Lmax = lmax;

                CurrentLouverLoad = currentLouverLoad;
                HorizentalPressure = currentLouverLoad.HorizentalDistributedLoad;
                this.CurrentLouverBladeProperty = CurrentLouverBlade;

                this.CurrentLouver = CurrentLouver;
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
                // pass the UnitWeight
                CurrentLouver.γ = this.CurrentLouverBladeProperty.γ;
                CheckLoverBladeSupportNeed();
            }



            public LouverBlade DeepCopy()
            {
                LouverBlade LouverBlade = (LouverBlade)this.MemberwiseClone();

                LouverBlade.CurrentLouverBladeProperty = new LouverBladeProperty(CurrentLouverBladeProperty.Model, CurrentLouverBladeProperty.Material
                , CurrentLouverBladeProperty.Ix, CurrentLouverBladeProperty.Iy, CurrentLouverBladeProperty.Ic, CurrentLouverBladeProperty.Sx, CurrentLouverBladeProperty.Sy,
                    CurrentLouverBladeProperty.Sc, CurrentLouverBladeProperty.Area, CurrentLouverBladeProperty.Depth, CurrentLouverBladeProperty.E, CurrentLouverBladeProperty.Fy
                , CurrentLouverBladeProperty.WTr, CurrentLouverBladeProperty.γ);


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
            public float SideLoadRatio { get; set; } = 1;

            public LouverAnchors(string markUpRefrence, string connectionLocation, string baseMaterial, string altMaterial
            , ConnectionType connectionTypeBase, ConnectionType connectionTypeAlt, LouverLoad CurrentLouverLoad, ref float SideLoadRatio, ref Louver CurrentLouver)
            {
                MarkUpRefrence = markUpRefrence;
                ConnectionLocation = connectionLocation;
                BaseMaterial = baseMaterial;
                AltMaterial = altMaterial;
                ConnectionTypeBase = connectionTypeBase;
                ConnectionTypeAlt = connectionTypeAlt;
                this.CurrentLouverLoad = CurrentLouverLoad;
                this.SideLoadRatio = SideLoadRatio;
                this.CurrentLouver = CurrentLouver;
            }


            // to from louver Side
            public ConnectionType ConnectionTypeAlt { get; set; }

            // to structure SIde
            public ConnectionType ConnectionTypeBase { get; set; }

            public float WeightLoadArea { get; set; }
            public float SurfacePressure { get; set; }

            /// will be Calculated from Analyze method

            public float ShearReaction { set; get; }

            public float NormalReaction { set; get; }

            public float MomentReaction { set; get; }
            public Louver CurrentLouver { set; get; }
            public float LoadCheckRatio { set; get; }


            /// <summary>
            /// where u get the info from the drawings
            /// </summary>
            /// <value>The mark up refrence.</value>
            public string MarkUpRefrence { get; set; }


            public float BaseInnerSpacing { get; set; }
            public float AltInnerSpacing { get; set; }

            public float BaseEdgeSpacing { get; set; }
            public float AltEdgeSpacing { get; set; }


            /// <summary>
            ///This Equal to Side in xls north ,east or west
            /// </summary>
            /// <value>The connection location.</value>
            public string ConnectionLocation { get; set; }

            public int RequiredBoltsNumberAlt { get; set; }
            public int RequiredBoltsNumberBase { get; set; }


            public float SingleBoltStrengthShearRatioAlt { get; set; }
            public float SingleBoltStrengthShearRatioBase { get; set; }

            public float SingleBoltStrengthNormalRatioAlt { get; set; }
            public float SingleBoltStrengthNormalRatioBase { get; set; }
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

                public ConnectionType(AnchorBoltProperties anchorBoltsProperties)
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

            void SetUpAppliedPressure()
            {
                WeightLoadArea = (CurrentLouver.γ) * (CurrentLouver.Thickness);
                SurfacePressure = CurrentLouverLoad.HorizentalDistributedLoad;


            }

            void AnalyzeLouverLoad()
            {
                switch (ConnectionLocation)
                {
                    case MemberDesignText.LouverStrings.NorthSideConnection:

                        DoNorthSideConnectionReactionCalculation();
                        break;
                    case MemberDesignText.LouverStrings.SouthSideConnection:
                        DoSouthSideConnectionReactionCalculation();
                        break;
                    case MemberDesignText.LouverStrings.EastSideConnection:
                        DoEastSideConnectionReactionCalculation();
                        break;
                    case MemberDesignText.LouverStrings.WestSideConnection:
                        DoWestSideConnectionReactionCalculation();
                        break;
                    default:
                        Console.WriteLine("if not Suit do this");
                        break;
                }






            }
            void DoNorthSideConnectionReactionCalculation()
            {
                NormalReaction = WeightLoadArea * CurrentLouver.Width * CurrentLouver.NorthSideLoadRatio * CurrentLouver.Height * (1 / 2);
                ShearReaction = SurfacePressure * CurrentLouver.Width * CurrentLouver.NorthSideLoadRatio * CurrentLouver.Height * (1 / 2);

            }

            void DoSouthSideConnectionReactionCalculation()
            {
                NormalReaction = WeightLoadArea * CurrentLouver.Width * CurrentLouver.SouthSideLoadRatio * CurrentLouver.Height * (1 / 2);
                ShearReaction = SurfacePressure * CurrentLouver.Width * CurrentLouver.SouthSideLoadRatio * CurrentLouver.Height * (1 / 2);


            }
            void DoEastSideConnectionReactionCalculation()
            {
                ShearReaction = WeightLoadArea * CurrentLouver.Width * CurrentLouver.EastSideLoadRatio * CurrentLouver.Height * (1 / 2);
                NormalReaction = SurfacePressure * CurrentLouver.Width * CurrentLouver.EastSideLoadRatio * CurrentLouver.Height * (1 / 2);


            }
            void DoWestSideConnectionReactionCalculation()
            {

                ShearReaction = WeightLoadArea * CurrentLouver.Width * CurrentLouver.WestSideLoadRatio * CurrentLouver.Height * (1 / 2);
                NormalReaction = SurfacePressure * CurrentLouver.Width * CurrentLouver.WestSideLoadRatio * CurrentLouver.Height * (1 / 2);

            }


            void CheckGroupBoltsStrength()
            {

                float ApproxmateRequiredBoltsNumberAlt = (ShearReaction / ConnectionTypeAlt.AnchorBoltProperty.FdS)
                + (NormalReaction / ConnectionTypeAlt.AnchorBoltProperty.FdT);

                // thus

                RequiredBoltsNumberAlt = (int)Math.Round((ApproxmateRequiredBoltsNumberAlt + 0.5));


                float ApproxmateRequiredBoltsNumberBase = (ShearReaction / ConnectionTypeBase.AnchorBoltProperty.FdS)
             + (NormalReaction / ConnectionTypeBase.AnchorBoltProperty.FdT);

                // thus

                RequiredBoltsNumberBase = (int)Math.Round((ApproxmateRequiredBoltsNumberBase + 0.5));


            }
            void CheckSingleBoltStrength()
            {

                SingleBoltStrengthShearRatioAlt = (ShearReaction / RequiredBoltsNumberAlt) / ConnectionTypeAlt.AnchorBoltProperty.FdS;
                SingleBoltStrengthShearRatioBase = (ShearReaction / RequiredBoltsNumberBase) / ConnectionTypeBase.AnchorBoltProperty.FdS;


                SingleBoltStrengthNormalRatioAlt = (NormalReaction / RequiredBoltsNumberAlt) / ConnectionTypeAlt.AnchorBoltProperty.FdT;
                SingleBoltStrengthNormalRatioBase = (NormalReaction / RequiredBoltsNumberBase) / ConnectionTypeBase.AnchorBoltProperty.FdT;



            }
            void CheckInnerAnhcorSpacing()
            {

                if (ConnectionLocation == MemberDesignText.LouverStrings.NorthSideConnection 
                || ConnectionLocation == MemberDesignText.LouverStrings.SouthSideConnection)
                {
                    float ExactBaseInnerSpacing = RequiredBoltsNumberBase / (CurrentLouver.Width);
                    float ExactAltInnerSpacing = RequiredBoltsNumberAlt / (CurrentLouver.Width);
                    // here we will write the Algo


                    BaseInnerSpacing = ExactBaseInnerSpacing;
                    AltInnerSpacing = ExactAltInnerSpacing;


                }
                else
                {
                    float ExactBaseInnerSpacing = RequiredBoltsNumberBase / (CurrentLouver.Height);
                    float ExactAltInnerSpacing = RequiredBoltsNumberAlt / (CurrentLouver.Height);

                    // here we will write the Algo

                    BaseInnerSpacing = ExactBaseInnerSpacing;
                    AltInnerSpacing = ExactAltInnerSpacing;



                }
            }
            void CheckEdgeAnchorSpacing()
            {


                if (ConnectionLocation == MemberDesignText.LouverStrings.NorthSideConnection
                || ConnectionLocation == MemberDesignText.LouverStrings.SouthSideConnection)
                {
                    float ExactBaseEdgeSpacing = (CurrentLouver.Width) - (RequiredBoltsNumberBase * BaseInnerSpacing);
                    float ExactAltInnerSpacing = (CurrentLouver.Width) - (RequiredBoltsNumberAlt *AltInnerSpacing) ;

                    // todo 
                    // here we will write the Algo
                    BaseEdgeSpacing = ExactBaseEdgeSpacing;
                    AltEdgeSpacing = ExactBaseEdgeSpacing;


                }
                else
                {
                    float ExactBaseEdgeSpacing = (CurrentLouver.Height) - (RequiredBoltsNumberBase * BaseInnerSpacing);
                    float ExactAltInnerSpacing = (CurrentLouver.Height) - (RequiredBoltsNumberAlt * AltInnerSpacing);

                    // here we will write the Algo
                    BaseEdgeSpacing = ExactBaseEdgeSpacing;
                    AltEdgeSpacing = ExactBaseEdgeSpacing;


                }

            }





            public override void SetUpSubMember()
            {
                SetUpAppliedPressure();
                AnalyzeLouverLoad();
                CheckGroupBoltsStrength();
                CheckSingleBoltStrength();
                CheckInnerAnhcorSpacing();
                CheckEdgeAnchorSpacing();

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

            this.LoverBlade.SetUpSubMember();
            this.NorthLouverAnchor.SetUpSubMember();
            this.SouthLouverAnchor.SetUpSubMember();
            this.EastLouverAnchor.SetUpSubMember();
            this.WestLouverAnchor.SetUpSubMember();


        }

        public class LouverAnalysis
        {


            public LouverAnalysis(ref Louver CurrentLover)
            {
                Width = CurrentLover.Width;
                Height = CurrentLover.Height;
                Thickness = CurrentLover.Thickness;


            }
            Louver CurrentLover { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public float Thickness { get; set; }
            public string DistributionLoadType { get; set; }
            public bool WidthIsLongest { get; set; } = false;

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

                    CurrentLover.NorthSideLoadRatio = 0.5f;
                    CurrentLover.SouthSideLoadRatio = 0.5f;
                    CurrentLover.WestSideLoadRatio = 1;
                    CurrentLover.EastSideLoadRatio = 1;


                }
                else if (DistributionLoadType == MemberDesignText.LouverStrings.OneWayLouverText && !WidthIsLongest)
                {
                    CurrentLover.NorthSideLoadRatio = 1;
                    CurrentLover.SouthSideLoadRatio = 1;
                    CurrentLover.WestSideLoadRatio = 0.5f;
                    CurrentLover.EastSideLoadRatio = 0.5f;

                }
                else
                {


                    CurrentLover.NorthSideLoadRatio = 0.5f;
                    CurrentLover.SouthSideLoadRatio = 0.5f;
                    CurrentLover.WestSideLoadRatio = 0.5f;
                    CurrentLover.EastSideLoadRatio = 0.5f;


                }



            }
            public void RunAnalysis()
            {
                CheckLouverType();

                GetLoadDistributionRatio();



            }


        }

    }

}