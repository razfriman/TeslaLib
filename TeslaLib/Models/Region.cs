using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum Region
    {
        [EnumMember(Value = "NA")]
        Usa,

        [EnumMember(Value = "NC")]
        Canada
    }
}
