using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum RoofType
    {
        [EnumMember(Value = "Colored")]
        Colored,

        [EnumMember(Value = "None")]
        None,

        [EnumMember(Value = "Black")]
        Black
    }
}
