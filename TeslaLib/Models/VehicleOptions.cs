﻿using System;
using System.Linq;

namespace TeslaLib.Models
{
    public class VehicleOptions
    {
        public VehicleOptions(string optionCodes)
        {
            ParseOptionCodes(optionCodes);
        }

        public RoofType RoofType { get; set; }

        public Region Region { get; set; }

        public int YearModel { get; set; }

        public Model Model { get; set; }

        public TrimLevel TrimLevel { get; set; }

        public DriverSide DriverSide { get; set; }

        public bool IsPerformance { get; set; }

        public int BatterySize { get; set; }

        public TeslaColor Color { get; set; }

        public WheelType WheelType { get; set; }

        public InteriorDecor InteriorDecor { get; set; }

        public bool HasPowerLiftgate { get; set; }

        public bool HasNavigation { get; set; }

        public bool HasPremiumExteriorLighting { get; set; }

        public bool HasHomeLink { get; set; }

        public bool HasSatelliteRadio { get; set; }

        public bool HasPerformanceExterior { get; set; }

        public bool HasPerformancePowertrain { get; set; }

        public bool HasThirdRowSeating { get; set; }

        public bool HasAirSuspension { get; set; }

        public bool HasSuperCharging { get; set; }

        public bool HasTechPackage { get; set; }

        public bool HasAudioUpgrade { get; set; }

        public bool HasTwinChargers { get; set; }

        public bool HasHPWC { get; set; }

        public bool HasPaintArmor { get; set; }

        public bool HasParcelShelf { get; set; }

        // 02 = NEMA 14-50
        public int AdapterTypeOrdered { get; set; }

        public bool IsPerformancePlus { get; set; }

        public void ParseOptionCodes(string optionCodes)
        {
            // MS01,RENA,TM00,DRLH,PF00,BT85,PBCW,RFPO,WT19,IBMB,IDPB,TR00,SU01,SC01,TP01,AU01,CH00,HP00,PA00,PS00,AD02,X020,X025,X001,X003,X007,X011,X013

            var options = optionCodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string option in options)
            {
                switch (option)
                {
                    case "X001":
                        HasPowerLiftgate = true;
                        break;
                    case "X002":
                        HasPowerLiftgate = false;
                        break;
                    case "X003":
                        HasNavigation = true;
                        break;
                    case "X004":
                        HasNavigation = false;
                        break;
                    case "X007":
                        HasPremiumExteriorLighting = true;
                        break;
                    case "X008":
                        HasPremiumExteriorLighting = false;
                        break;
                    case "X011":
                        HasHomeLink = true;
                        break;
                    case "X012":
                        HasHomeLink = false;
                        break;
                    case "X013":
                        HasSatelliteRadio = true;
                        break;
                    case "X014":
                        HasSatelliteRadio = false;
                        break;
                    case "X019":
                        HasPerformanceExterior = true;
                        break;
                    case "X020":
                        HasPerformanceExterior = false;
                        break;
                    case "X024":
                        HasPerformancePowertrain = true;
                        break;
                    case "X025":
                        HasPerformancePowertrain = false;
                        break;
                    case "MDLX":
                        Model = Model.X;
                        break;
                }

                string value2 = option.Substring(2, 2);

                switch (option.Substring(0, 2))
                {
                    case "MS":
                        YearModel = int.Parse(value2);
                        Model = Model.S;
                        break;
                    case "RE":
                        Region = Extensions.ToEnum<Region>(value2);
                        break;
                    case "TM":
                        TrimLevel = Extensions.ToEnum<TrimLevel>(value2);
                        break;
                    case "DR":
                        DriverSide = Extensions.ToEnum<DriverSide>(value2);
                        break;
                    case "PF":
                        IsPerformance = int.Parse(value2) > 0;
                        break;
                    case "BT":
                        BatterySize = int.Parse(value2);
                        break;
                    case "RF":
                        switch (value2)
                        {
                            case "BC":
                                RoofType = RoofType.COLORED;
                                break;
                            case "PO":
                                RoofType = RoofType.NONE;
                                break;
                            case "BK":
                                RoofType = RoofType.BLACK;
                                break;
                        }

                        break;
                    case "WT":
                        switch (value2)
                        {
                            case "19":
                                WheelType = WheelType.BASE_19;
                                break;
                            case "21":
                                WheelType = WheelType.SILVER_21;
                                break;
                            case "SP":
                                WheelType = WheelType.CHARCOAL_21;
                                break;
                            case "SG":
                                WheelType = WheelType.CHARCOAL_PERFORMANCE_21;
                                break;
                        }

                        break;
                    case "ID":
                        InteriorDecor = Extensions.ToEnum<InteriorDecor>(value2);
                        break;
                    case "TR":
                        HasThirdRowSeating = int.Parse(value2) > 0;
                        break;
                    case "SU":
                        HasAirSuspension = int.Parse(value2) > 0;
                        break;
                    case "SC":
                        HasSuperCharging = int.Parse(value2) > 0;
                        break;
                    case "TP":
                        HasTechPackage = int.Parse(value2) > 0;
                        break;
                    case "AU":
                        HasAudioUpgrade = int.Parse(value2) > 0;
                        break;
                    case "CH":
                        HasTwinChargers = int.Parse(value2) > 0;
                        break;
                    case "HP":
                        HasHPWC = int.Parse(value2) > 0;
                        break;
                    case "PA":
                        HasPaintArmor = int.Parse(value2) > 0;
                        break;
                    case "PS":
                        HasParcelShelf = int.Parse(value2) > 0;
                        break;
                    case "AD":
                        break;
                    case "PX":
                        IsPerformancePlus = int.Parse(value2) > 0;
                        break;
                }

                string value3 = option.Substring(1, 3);
                switch (option.Substring(0, 1))
                {
                    case "P":
                        Color = Extensions.ToEnum<TeslaColor>(value3);
                        break;
                    case "I":
                        break;
                }
            }
        }
    }
}