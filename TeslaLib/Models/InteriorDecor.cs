using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum InteriorDecor
    {
        [EnumMember(Value = "CF")]
        CARBON_FIBER,

        [EnumMember(Value = "LW")]
        LACEWOOD,

        [EnumMember(Value = "OM")]
        OBECHE_WOOD_MATTE,

        [EnumMember(Value = "OG")]
        OBECHE_WOOD_GLOSS,

        [EnumMember(Value = "PB")]
        PIANO_BLACK,
    }
}
