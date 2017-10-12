using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TeslaLib.Models
{
    // As of August 2017, a 2014 Model S returns:
    // {"response":{"api_version":3,"autopark_state":"unavailable","autopark_state_v2":"unavailable","calendar_supported":true,
    // "car_type":"s","car_version":"2017.32 9ea02cb","center_display_state":0,"dark_rims":false,"df":0,"dr":0,
    // "exterior_color":"Red","ft":0,"has_spoiler":true,"locked":true,"notifications_supported":true,"odometer":32490.440953,
    // "parsed_calendar_supported":true,"perf_config":"P2","pf":0,"pr":0,"rear_seat_heaters":0,"rear_seat_type":0,
    // "remote_start":false,"remote_start_supported":true,"rhd":false,"roof_color":"None","rt":0,"seat_type":0,
    // "spoiler_type":"Passive","sun_roof_installed":1,"sun_roof_percent_open":0,"sun_roof_state":"unknown",
    // "third_row_seats":"None","timestamp":1503881911969,"valet_mode":false,"vehicle_name":"Hope Bringer","wheel_type":"Base19"}}
    public class VehicleStateStatus
    {
        [JsonProperty(PropertyName = "df")]
        public bool IsDriverFrontDoorOpen { get; set; }

        [JsonProperty(PropertyName = "dr")]
        public bool IsDriverRearDoorOpen { get; set; }

        [JsonProperty(PropertyName = "pf")]
        public bool IsPassengerFrontDoorOpen { get; set; }

        [JsonProperty(PropertyName = "pr")]
        public bool IsPassengerRearDoorOpen { get; set; }

        [JsonProperty(PropertyName = "ft")]
        public bool IsFrontTrunkOpen { get; set; }

        [JsonProperty(PropertyName = "rt")]
        public bool IsRearTrunkOpen { get; set; }

        [JsonProperty(PropertyName = "car_version")]
        public string CarVersion { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool IsLocked { get; set; }

        [JsonProperty(PropertyName = "sun_roof_installed")]
        public bool HasPanoramicRoof { get; set; }

        [JsonProperty(PropertyName = "sun_roof_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PanoramicRoofState PanoramicRoofState { get; set; }

        [JsonProperty(PropertyName = "sun_roof_percent_open")]
        public int PanoramicRoofPercentOpen { get; set; }

        [JsonProperty(PropertyName = "dark_rims")]
        public bool HasDarkRims { get; set; }

        [JsonProperty(PropertyName = "wheel_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WheelType WheelType { get; set; }

        [JsonProperty(PropertyName = "has_spoiler")]
        public bool HasSpoiler { get; set; }

        [JsonProperty(PropertyName = "roof_color")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RoofType RoofColor { get; set; }

        [JsonProperty(PropertyName = "perf_config")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PerformanceConfiguration PerformanceConfiguration { get; set; }

        // Updates to Tesla API's around December 2015:
        // Updated firmware from v7.0 (2.7.56) to v7(2.9.12) Some new fields added:

        [JsonProperty(PropertyName = "car_type")]
        public String CarType { get; set; }   // "s"

        [JsonProperty(PropertyName = "third_row_seats")]
        public String ThirdRowSeats { get; set; }   // "None"

        // Fields that exist as of August 2017:

        [JsonProperty(PropertyName = "odometer")]
        public Double Odometer { get; set; }  // Value is in miles, regardless of the car's UI settings.

        // Note: We should use the TeslaColor enum here, but this returns values like "Red" vs. "MULTICOAT_RED"
        [JsonProperty(PropertyName = "exterior_color")]
        //[JsonConverter(typeof(StringEnumConverter))]
        public /*TeslaColor*/String ExteriorColor { get; set; }
    }

    public enum PanoramicRoofState
    {
        [EnumMember(Value = "Open")]
        OPEN,

        [EnumMember(Value = "Comfort")]
        COMFORT,

        [EnumMember(Value = "Vent")]
        VENT,

        [EnumMember(Value = "Close")]
        CLOSE,

        [EnumMember(Value = "Move")]
        MOVE,

        [EnumMember(Value = "Unknown")]
        UNKNOWN,

        // As of September 2017, we started seeing "closed" and "moving" as values.
        [EnumMember(Value = "Closed")]
        Closed = CLOSE,

        [EnumMember(Value = "Moving")]
        Moving = MOVE,

    }

    public enum WheelType
    {

        [EnumMember(Value = "Base19")]
        BASE_19,

        [EnumMember(Value = "Silver21")]
        SILVER_21,

        [EnumMember(Value = "Charcoal21")]
        CHARCOAL_21,

        CHARCOAL_PERFORMANCE_21
    }

    public enum RoofType
    {
        [EnumMember(Value = "Colored")]
        COLORED,

        [EnumMember(Value = "None")]
        NONE,

        [EnumMember(Value = "Black")]
        BLACK
    }

    public enum PerformanceConfiguration
    {
        [EnumMember(Value = "Base")]
        BASE,

        [EnumMember(Value = "Sport")]
        SPORT,

        [EnumMember(Value = "P2")]
        P2
    }
}