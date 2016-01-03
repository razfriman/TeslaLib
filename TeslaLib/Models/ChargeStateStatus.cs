using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public class ChargeStateStatus
    {
        [JsonProperty(PropertyName = "charging_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChargingState ChargingState { get; set; }

        [JsonProperty(PropertyName = "battery_current")]
        public double? BatteryCurrent { get; set; }

        [JsonProperty(PropertyName = "battery_heater_on")]
        public bool? IsBatteryHeaterOn { get; set; }

        [JsonProperty(PropertyName = "battery_level")]
        public int BatteryLevel { get; set; }

        [JsonProperty(PropertyName = "battery_range")]
        public double BatteryRange { get; set; }

        [JsonProperty(PropertyName = "charge_enable_request")]
        public bool IsChargeEnableRequest { get; set; }

        [JsonProperty(PropertyName = "charge_limit_soc")]
        public int ChargeLimitSoc { get; set; }

        [JsonProperty(PropertyName = "charge_limit_soc_max")]
        public int ChargeLimitSocMax { get; set; }

        [JsonProperty(PropertyName = "charge_limit_soc_min")]
        public int ChargeLimitSocMin { get; set; }

        [JsonProperty(PropertyName = "charge_limit_soc_std")]
        public int ChargeLimitSocStd { get; set; }

        [JsonProperty(PropertyName = "charge_port_door_open")]
        public bool? IsChargePortDoorOpen { get; set; }

        [JsonProperty(PropertyName = "charge_rate")]
        public double ChargeRate { get; set; }

        // No longer returned as of Jan 2016.
        //[JsonProperty(PropertyName = "charge_starting_range")]
        //public int? ChargeStartingRange { get; set; }

        //[JsonProperty(PropertyName = "charge_starting_soc")]
        //public int? ChargeStartingSoc { get; set; }

        [JsonProperty(PropertyName = "charge_to_max_range")]
        public bool IsChargeToMaxRange { get; set; }

        [JsonProperty(PropertyName = "charger_actual_current")]
        public int? ChargerActualCurrent { get; set; }

        [JsonProperty(PropertyName = "charger_pilot_current")]
        public int? ChargerPilotCurrent { get; set; }

        [JsonProperty(PropertyName = "charger_power")]
        public int? ChargerPower { get; set; }

        [JsonProperty(PropertyName = "charger_voltage")]
        public int? ChargerVoltage { get; set; }           // null when a car is starting to charge.

        [JsonProperty(PropertyName = "est_battery_range")]
        public double EstimatedBatteryRange { get; set; }

        [JsonProperty(PropertyName = "fast_charger_present")]
        public bool IsUsingSupercharger { get; set; }

        [JsonProperty(PropertyName = "ideal_battery_range")]
        public double IdealBatteryRange { get; set; }

        [JsonProperty(PropertyName = "max_range_charge_counter")]
        public int MaxRangeChargeCounter { get; set; }

        [JsonProperty(PropertyName = "not_enough_power_to_heat")]
        public bool? IsNotEnoughPowerToHeat { get; set; }

        [JsonProperty(PropertyName = "scheduled_charging_pending")]
        public bool ScheduledChargingPending { get; set; }

        [JsonProperty(PropertyName = "scheduled_charging_start_time")]
        public DateTime? ScheduledChargingStartTime { get; set; }

        [JsonProperty(PropertyName = "time_to_full_charge")]
        public double? TimeUntilFullCharge { get; set; }

        [JsonProperty(PropertyName = "user_charge_enable_request")]
        public bool? IsUserChargeEnableRequest { get; set; }

        // Updates to Tesla API's
        // Updated at an unknown time

        [JsonProperty(PropertyName = "trip_charging")]
        public bool? IsTripCharging { get; set; }

        [JsonProperty(PropertyName = "charger_phases")]
        public int? ChargerPhases { get; set; }

        [JsonProperty(PropertyName = "motorized_charge_port")]
        public bool? IsMotorizedChargePort { get; set; }

        // Seen values "\u003Cinvalid\u003E"
        [JsonProperty(PropertyName = "fast_charger_type")]
        public String FastChargerType { get; set; }

        [JsonProperty(PropertyName = "usable_battery_level")]
        public int? UsableBatteryLevel { get; set; }

        [JsonProperty(PropertyName = "charge_energy_added")]
        public double? ChargeEnergyAdded { get; set; }

        [JsonProperty(PropertyName = "charge_miles_added_rated")]
        public double? ChargeMilesAddedRated { get; set; }

        [JsonProperty(PropertyName = "charge_miles_added_ideal")]
        public double? ChargeMilesAddedIdeal { get; set; }

        [JsonProperty(PropertyName = "eu_vehicle")]
        public bool IsEUVehicle { get; set; }

        // Updates to Tesla API's around December 2015:
        // Updated firmware from v7.0 (2.7.56) to v7(2.9.12) Some new fields added:

        [JsonProperty(PropertyName = "charge_port_latch")]
        public String ChargePortLatch { get; set; }  // "Engaged"

        [JsonProperty(PropertyName = "charge_current_request")]
        public int? ChargeCurrentRequest { get; set; }  // amps

        [JsonProperty(PropertyName = "charge_current_request_max")]
        public int? ChargeCurrentRequestMax { get; set; }  // amps

        [JsonProperty(PropertyName = "managed_charging_active")]
        public bool? ManagedChargingActive { get; set; }

        [JsonProperty(PropertyName = "managed_charging_user_canceled")]
        public bool? ManagedChargingUserCanceled { get; set; }

        [JsonProperty(PropertyName = "managed_charging_start_time")]
        public DateTime? ManagedChargingStartTime { get; set; }
    }

    public enum ChargingState
    {
        [EnumMember(Value = "Complete")]
        Complete,

        [EnumMember(Value = "Charging")]
        Charging,

        [EnumMember(Value = "Disconnected")]
        Disconnected,

        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "NotCharging")]
        NotCharging,

        [EnumMember(Value = "Starting")]
        Starting,
    }
}
