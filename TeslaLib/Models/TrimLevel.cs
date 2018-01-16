using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum TrimLevel
    {
        [EnumMember(Value = "00")]
        STANDARD,

        //[EnumMember(Value = "01")]
        //PERFORMANCE,

        [EnumMember(Value = "02")]
        SIGNATURE_PERFORMANCE
    }
}
