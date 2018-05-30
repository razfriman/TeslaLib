using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum TrimLevel
    {
        [EnumMember(Value = "00")]
        Standard,

        //[EnumMember(Value = "01")]
        //Performance,

        [EnumMember(Value = "02")]
        SignaturePerformance
    }
}
