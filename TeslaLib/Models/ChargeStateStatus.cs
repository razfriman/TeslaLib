using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TeslaLib.Models
{
    /*
    * {"response":
    * {"charging_state":"Charging",
    * "charge_limit_soc":81,
    * "charge_limit_soc_std":90,
    * "charge_limit_soc_min":50,
    * "charge_limit_soc_max":100,
    * "charge_to_max_range":false,
    * "battery_heater_on":false,
    * "not_enough_power_to_heat":false,
    * "max_range_charge_counter":0,
    * "fast_charger_present":false,
    * "fast_charger_type":"<invalid>",
    * "battery_range":212.05,
    * "est_battery_range":222.92,
    * "ideal_battery_range":245.31,
    * "battery_level":80,
    * "usable_battery_level":80,
    * "battery_current":9.2,
    * "charge_energy_added":11.36,
    * "charge_miles_added_rated":38.5,
    * "charge_miles_added_ideal":44.5,
    * "charger_voltage":242,
    * "charger_pilot_current":40,
    * "charger_actual_current":17,
    * "charger_power":4,
    * "time_to_full_charge":0.08,
    * "trip_charging":false,
    * "charge_rate":9.1,
    * "charge_port_door_open":true,
    * "motorized_charge_port":false,
    * "scheduled_charging_start_time":null,
    * "scheduled_charging_pending":false,
    * "user_charge_enable_request":null,
    * "charge_enable_request":true,
    * "eu_vehicle":false,
    * "charger_phases":1,
    * "charge_port_latch":"Engaged",
    * "charge_current_request":40,
    * "charge_current_request_max":40,
    * "managed_charging_active":false,
    * "managed_charging_user_canceled":false,
    * "managed_charging_start_time":null}}
    * 
    * 
    * // While SuperCharging
    *     "charging_state": "Charging",
    "battery_current": 206.7,
    "battery_heater_on": false,
    "battery_level": 47,
    "battery_range": 125.08,
    "charge_enable_request": true,
    "charge_limit_soc": 95,
    "charge_limit_soc_max": 100,
    "charge_limit_soc_min": 50,
    "charge_limit_soc_std": 90,
    "charge_port_door_open": true,
    "charge_rate": 342.6,
    "charge_to_max_range": true,
    "charger_actual_current": 0,
    "charger_pilot_current": 0,
    "charger_power": 78,
    "charger_voltage": 377,
    "est_battery_range": 117.77,
    "fast_charger_present": true,
    "ideal_battery_range": 144.71,
    "max_range_charge_counter": 0,
    "not_enough_power_to_heat": false,
    "scheduled_charging_pending": false,
    "scheduled_charging_start_time": null,
    "time_to_full_charge": 0.83,
    "user_charge_enable_request": null,
    "trip_charging": false,
    "charger_phases": null,
    "motorized_charge_port": false,
    "fast_charger_type": "Tesla",
    "usable_battery_level": 47,
    "charge_energy_added": 29.44,
    "charge_miles_added_rated": 100.0,
    "charge_miles_added_ideal": 115.5,
    "eu_vehicle": false,
    "charge_port_latch": "Engaged",
    "charge_current_request": 80,
    "charge_current_request_max": 80,
    "managed_charging_active": false,
    "managed_charging_user_canceled": false,
    "managed_charging_start_time": null
    */
    public class ChargeStateStatus
    {
        // Note: the ChargingState started coming back as null around June 2017, coinciding with a significant
        // Tesla software update.  They apparently upgraded from Linux kernel 2.6.36 to 4.4.35.  They may have changed
        // a lot of Tesla's software stack too.
        [JsonProperty(PropertyName = "charging_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChargingState? ChargingState { get; set; }

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
        public bool? IsUsingSupercharger { get; set; }

        [JsonProperty(PropertyName = "ideal_battery_range")]
        public double IdealBatteryRange { get; set; }

        [JsonProperty(PropertyName = "max_range_charge_counter")]
        public int MaxRangeChargeCounter { get; set; }

        [JsonProperty(PropertyName = "not_enough_power_to_heat")]
        public bool? IsNotEnoughPowerToHeat { get; set; }

        [JsonProperty(PropertyName = "scheduled_charging_pending")]
        public bool ScheduledChargingPending { get; set; }

        // This is a Unix time value in seconds from 1970 in UTC.
        [JsonProperty(PropertyName = "scheduled_charging_start_time")]
        public long? ScheduledChargingStartTime { get; set; }

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
}
