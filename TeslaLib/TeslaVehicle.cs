using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using TeslaLib.Converters;
using TeslaLib.Models;

namespace TeslaLib
{
    public class TeslaVehicle
    {

        #region Properties

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "option_codes")]
        [JsonConverter(typeof(VehicleOptionsConverter))]
        public VehicleOptions Options { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "vehicle_id")]
        public int VehicleId { get; set; }

        [JsonProperty(PropertyName = "vin")]
        public string Vin { get; set; }

        [JsonProperty(PropertyName = "tokens")]
        public List<string> Tokens { get; set; }

        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleState State { get; set; }

        [JsonIgnore]
        public RestClient Client { get; set; }

        #endregion

        #region State and Settings

        public async Task<ResponseWrapper<bool>> LoadMobileEnabledStatus()
        {
            return await Client
                .Get<bool>($"vehicles/{Id}/mobile_enabled")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ChargeStateStatus>> LoadChargeStateStatus()
        {
            return await Client
                .Post<ChargeStateStatus>($"vehicles/{Id}/data_request/charge_state")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ClimateStateStatus>> LoadClimateStateStatus()
        {
            return await Client
                .Post<ClimateStateStatus>($"vehicles/{Id}/data_request/climate_state")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<DriveStateStatus>> LoadDriveStateStatus()
        {
            return await Client
                .Post<DriveStateStatus>($"vehicles/{Id}/data_request/drive_state")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<GuiSettingsStatus>> LoadGuiStateStatus()
        {
            return await Client
                .Post<GuiSettingsStatus>($"vehicles/{Id}/data_request/gui_settings")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<VehicleStateStatus>> LoadVehicleStateStatus()
        {
            return await Client
                .Post<VehicleStateStatus>($"vehicles/{Id}/data_request/vehicle_state")
                .ConfigureAwait(false);
        }

        #endregion

        #region Commands

        public async Task<ResponseWrapper<TeslaVehicle>> WakeUp()
        {
            return await Client
                .Post<TeslaVehicle>($"vehicles/{Id}/wake_up")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> OpenChargePortDoor()
        {
            return await Client
                .Post<ResultStatus>($"vehicles/{Id}/command/charge_port_door_open")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> SetChargeLimitToStandard()
        {
            return await Client
                .Post<ResultStatus>($"vehicles/{Id}/command/charge_standard")
                .ConfigureAwait(false);
        }

        // Don't use this very often as it damages the battery.
        public async Task<ResponseWrapper<ResultStatus>> SetChargeLimitToMaxRange()
        {
            return await Client
                .Post<ResultStatus>($"vehicles/{Id}/command/charge_max_range")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> SetChargeLimit(int stateOfChargePercent)
        {
            return await Client
                .Post<ResultStatus>($"vehicles/{Id}/command/set_charge_limit?state=set&percent={stateOfChargePercent}")
                .ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> StartCharge()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/charge_start").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> StopCharge()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/charge_stop").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> FlashLights()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/flash_lights").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> HonkHorn()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/honk_horn").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> UnlockDoors()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/door_unlock").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> LockDoors()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/door_lock").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> SetTemperatureSettings(int driverTemp = 17, int passengerTemp = 17)
        {
            var TEMP_MAX = 32;
            var TEMP_MIN = 17;

            driverTemp = Math.Max(driverTemp, TEMP_MIN);
            driverTemp = Math.Min(driverTemp, TEMP_MAX);

            passengerTemp = Math.Max(passengerTemp, TEMP_MIN);
            passengerTemp = Math.Min(passengerTemp, TEMP_MAX);

            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/set_temps?driver_temp={driverTemp}&passenger_temp={passengerTemp}").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> StartHvac()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/auto_conditioning_start").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> StopHvac()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/auto_conditioning_stop").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> SetPanoramicRoofLevel(PanoramicRoofState roofState, int percentOpen = 0)
        {

            var uri = $"vehicles/{Id}/command/sun_roof_control?state={roofState.GetEnumValue()}";

            if (roofState == PanoramicRoofState.MOVE)
            {
                uri += $"&percent={percentOpen}";
            }

            return await Client.Post<ResultStatus>(uri).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> RemoteStart(string password)
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/remote_start_drive?password={password}").ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> OpenFrontTrunk() => await OpenTrunk("front").ConfigureAwait(false);

        public async Task<ResponseWrapper<ResultStatus>> OpenRearTrunk() => await OpenTrunk("rear").ConfigureAwait(false);

        public async Task<ResponseWrapper<ResultStatus>> OpenTrunk(string trunkType)
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/trunk_open", new
            {
                which_trunk = trunkType
            }).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> DisableValetMode() => await SetValetMode(false).ConfigureAwait(false);

        public async Task<ResponseWrapper<ResultStatus>> EnableValetMode(int password) => await SetValetMode(true, password).ConfigureAwait(false);

        public async Task<ResponseWrapper<ResultStatus>> SetValetMode(bool enabled, int password = 0)
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/set_valet_mode", new
            {
                on = enabled,
                password
            }).ConfigureAwait(false);
        }

        public async Task<ResponseWrapper<ResultStatus>> ResetValetPin()
        {
            return await Client.Post<ResultStatus>($"vehicles/{Id}/command/reset_valet_pin").ConfigureAwait(false);
        }

        #endregion

        public string LoadStreamingValues(string values)
        {
            return null;

            //values = "speed,odometer,soc,elevation,est_heading,est_lat,est_lng,power,shift_state,range,est_range";
            //string response = webClient.DownloadString(Path.Combine(TESLA_SERVER, string.Format("stream/{0}/?values={1}", vehicle.VehicleId, values)));
            //return response;
        }
    }
}