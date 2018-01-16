using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum TeslaColor
    {
        [EnumMember(Value = "BSB")]
        BLACK,

        [EnumMember(Value = "BCW")]
        WHITE,

        [EnumMember(Value = "MSS")]
        SILVER,

        [EnumMember(Value = "MTG")]
        METALLIC_DOLPHIN_GREY,

        [EnumMember(Value = "MAB")]
        METALLIC_BROWN,

        [EnumMember(Value = "MMB")]
        METALLIC_BLUE,

        [EnumMember(Value = "MSG")]
        METALLIC_GREEN,

        [EnumMember(Value = "PSW")]
        PEARL_WHITE,

        [EnumMember(Value = "PMR")]
        MULTICOAT_RED,
        //Red = MULTICOAT_RED,

        [EnumMember(Value = "PSR")]
        SIGNATURE_RED,
    }
}
