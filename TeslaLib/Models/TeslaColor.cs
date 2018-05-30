using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum TeslaColor
    {
        [EnumMember(Value = "BSB")]
        Black,

        [EnumMember(Value = "BCW")]
        White,

        [EnumMember(Value = "MSS")]
        Silver,

        [EnumMember(Value = "MTG")]
        MetallicDolphinGrey,

        [EnumMember(Value = "MAB")]
        MetallicBrown,

        [EnumMember(Value = "MMB")]
        MetallicBlue,

        [EnumMember(Value = "MSG")]
        MetallicGreen,

        [EnumMember(Value = "PSW")]
        PearlWhite,

        [EnumMember(Value = "PMR")]
        MulticoatRed,

        [EnumMember(Value = "PSR")]
        SignatureRed
    }
}
