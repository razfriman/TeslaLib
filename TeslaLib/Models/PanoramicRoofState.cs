using System.Runtime.Serialization;

namespace TeslaLib.Models
{
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
}
