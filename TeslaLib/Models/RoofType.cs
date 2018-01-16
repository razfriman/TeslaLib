using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum RoofType
    {
        [EnumMember(Value = "Colored")]
        COLORED,

        [EnumMember(Value = "None")]
        NONE,

        [EnumMember(Value = "Black")]
        BLACK
    }
}
