using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum WheelType
    {

        [EnumMember(Value = "Base19")]
        BASE_19,

        [EnumMember(Value = "Silver21")]
        SILVER_21,

        [EnumMember(Value = "Charcoal21")]
        CHARCOAL_21,

        CHARCOAL_PERFORMANCE_21
    }
}
