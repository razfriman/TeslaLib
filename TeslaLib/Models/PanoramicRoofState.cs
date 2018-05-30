using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum PanoramicRoofState
    {
        [EnumMember(Value = "Open")]
        Open,

        [EnumMember(Value = "Comfort")]
        Comfort,

        [EnumMember(Value = "Vent")]
        Vent,

        [EnumMember(Value = "Close")]
        Close,

        [EnumMember(Value = "Move")]
        Move,

        [EnumMember(Value = "Unknown")]
        Unknown,

        // As of September 2017, we started seeing "closed" and "moving" as values.
        [EnumMember(Value = "Closed")]
        Closed = Close,

        [EnumMember(Value = "Moving")]
        Moving = Move

    }
}
