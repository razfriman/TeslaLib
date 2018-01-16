using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
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
        public string VIN { get; set; }

        [JsonProperty(PropertyName = "tokens")]
        public List<string> Tokens { get; set; }

        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleState State { get; set; }

        [JsonIgnore]
        public RestClient Client { get; set; }

        #endregion

        #region State and Settings

        public bool LoadMobileEnabledStatus()
        {
            var request = new RestRequest("vehicles/{id}/mobile_enabled");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = bool.Parse(json.ToString());

            return data;
        }

        public ChargeStateStatus LoadChargeStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/data_request/charge_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            return ParseResult<ChargeStateStatus>(response);
        }

        public ClimateStateStatus LoadClimateStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/data_request/climate_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            return ParseResult<ClimateStateStatus>(response);
        }

        public DriveStateStatus LoadDriveStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/data_request/drive_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            return ParseResult<DriveStateStatus>(response);
        }

        public GuiSettingsStatus LoadGuiStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/data_request/gui_settings");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            return ParseResult<GuiSettingsStatus>(response);
        }

        public VehicleStateStatus LoadVehicleStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/data_request/vehicle_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            return ParseResult<VehicleStateStatus>(response);
        }

        #endregion

        #region Commands

        public VehicleState WakeUp()
        {
            var request = new RestRequest("vehicles/{id}/wake_up");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<TeslaVehicle>(json.ToString());
            return data?.State ?? VehicleState.Asleep;
        }

        public ResultStatus OpenChargePortDoor()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_port_door_open");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus SetChargeLimitToStandard()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_standard");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        // Don't use this very often as it damages the battery.
        public ResultStatus SetChargeLimitToMaxRange()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_max_range");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus SetChargeLimit(int stateOfChargePercent)
        {
            var request = new RestRequest("vehicles/{id}/command/set_charge_limit", Method.POST);
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            /*  This throws an exception - RestSharp serializes this out incorrectly, perhaps?  
            request.AddBody(new
            {
                state = "set",
                percent = socPercent
            });
            */
            request.AddParameter("state", "set");
            request.AddParameter("percent", stateOfChargePercent.ToString());

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus StartCharge()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_start");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus StopCharge()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_stop");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus FlashLights()
        {
            var request = new RestRequest("vehicles/{id}/command/flash_lights");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus HonkHorn()
        {
            var request = new RestRequest("vehicles/{id}/command/honk_horn");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus UnlockDoors()
        {
            var request = new RestRequest("vehicles/{id}/command/door_unlock");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus LockDoors()
        {
            var request = new RestRequest("vehicles/{id}/command/door_lock");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus SetTemperatureSettings(int driverTemp = 17, int passengerTemp = 17)
        {
            int TEMP_MAX = 32;
            int TEMP_MIN = 17;

            driverTemp = Math.Max(driverTemp, TEMP_MIN);
            driverTemp = Math.Min(driverTemp, TEMP_MAX);

            passengerTemp = Math.Max(passengerTemp, TEMP_MIN);
            passengerTemp = Math.Min(passengerTemp, TEMP_MAX);


            var request = new RestRequest("vehicles/{id}/command/set_temps?driver_temp={driver_degC}&passenger_temp={pass_degC}");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddParameter("driver_degC", driverTemp, ParameterType.UrlSegment);
            request.AddParameter("pass_degC", passengerTemp, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus StartHVAC()
        {
            var request = new RestRequest("vehicles/{id}/command/auto_conditioning_start");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus StopHVAC()
        {
            var request = new RestRequest("vehicles/{id}/command/auto_conditioning_stop");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus SetPanoramicRoofLevel(PanoramicRoofState roofState, int percentOpen = 0)
        {
            var request = new RestRequest("vehicles/{id}/command/sun_roof_control?state={state}&percent={percent}");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddParameter("state", roofState.GetEnumValue(), ParameterType.UrlSegment);

            if (roofState == PanoramicRoofState.MOVE)
            {
                request.AddParameter("percent", percentOpen, ParameterType.UrlSegment);
            }

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus RemoteStart(string password)
        {
            var request = new RestRequest("vehicles/{id}/command/remote_start_drive?password={password}");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddParameter("password", password, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus OpenFrontTrunk() => OpenTrunk("front");

        public ResultStatus OpenRearTrunk() => OpenTrunk("rear");

        public ResultStatus OpenTrunk(string trunkType)
        {
            var request = new RestRequest("vehicles/{id}/command/trunk_open");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddBody(new
            {
                which_trunk = trunkType
            });

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus DisableValetMode() => SetValetMode(false);

        public ResultStatus EnableValetMode(int password) => SetValetMode(true, password);

        public ResultStatus SetValetMode(bool enabled, int password = 0)
        {
            var request = new RestRequest("vehicles/{id}/command/set_valet_mode");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddBody(new
            {
                on = enabled,
                password
            });

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        public ResultStatus ResetValetPin()
        {
            var request = new RestRequest("vehicles/{id}/command/reset_valet_pin");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            return ParseResult<ResultStatus>(response);
        }

        private T ParseResult<T>(IRestResponse response)
        {
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<T>(json.ToString());
            return data;
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