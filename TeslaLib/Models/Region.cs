using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum Region
    {
        [EnumMember(Value = "NA")]
        USA,

        [EnumMember(Value = "NC")]
        CANADA
    }
}
