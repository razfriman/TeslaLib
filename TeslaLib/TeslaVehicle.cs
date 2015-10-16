using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
            var request = new RestRequest("vehicles/{id}/charge_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ChargeStateStatus>(json.ToString());

            return data;
        }

        public ClimateStateStatus LoadClimateStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/climate_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ClimateStateStatus>(json.ToString());

            return data;
        }

        public DriveStateStatus LoadDriveStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/drive_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<DriveStateStatus>(json.ToString());

            return data;
        }

        public GuiSettingsStatus LoadGuiStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/gui_settings");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<GuiSettingsStatus>(json.ToString());

            return data;
        }

        public VehicleStateStatus LoadVehicleStateStatus()
        {
            var request = new RestRequest("vehicles/{id}/vehicle_state");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Get(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<VehicleStateStatus>(json.ToString());

            return data;
        }

        #endregion

        #region Commands

        public ResultStatus WakeUp()
        {
            var request = new RestRequest("vehicles/{id}/wake_up");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus OpenChargePortDoor()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_port_door_open");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus SetChargeLimitToStandard(int percent = 50)
        {
            var request = new RestRequest("vehicles/{id}/command/charge_standard");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus SetChargeLimitToMaxRange(int percent = 50)
        {
            var request = new RestRequest("vehicles/{id}/command/charge_max_range");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus SetChargeLimit(int percent = 50)
        {
            var request = new RestRequest("vehicles/{id}/command/set_charge_limit?percent={limit_value}");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddParameter("limit_value", percent, ParameterType.UrlSegment);
            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus StartCharge()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_start");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus StopCharge()
        {
            var request = new RestRequest("vehicles/{id}/command/charge_stop");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus FlashLights()
        {
            var request = new RestRequest("vehicles/{id}/command/flash_lights");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus HonkHorn()
        {
            var request = new RestRequest("vehicles/{id}/command/honk_horn");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus UnlockDoors()
        {
            var request = new RestRequest("vehicles/{id}/command/door_unlock");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus LockDoors()
        {
            var request = new RestRequest("vehicles/{id}/command/door_lock");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
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
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus StartHVAC()
        {
            var request = new RestRequest("vehicles/{id}/command/auto_conditioning_start");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus StopHVAC()
        {
            var request = new RestRequest("vehicles/{id}/command/auto_conditioning_stop");
            request.AddParameter("id", Id, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
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
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus RemoteStart(string password)
        {
            var request = new RestRequest("vehicles/{id}/command/remote_start_drive?password={password}");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddParameter("password", password, ParameterType.UrlSegment);

            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

            return data;
        }

        public ResultStatus OpenFrontTrunk()
        {
            return OpenTrunk("front");
        }

        public ResultStatus OpenRearTrunk()
        {
            return OpenTrunk("rear");
        }

        public ResultStatus OpenTrunk(string trunkType)
        {
            var request = new RestRequest("vehicles/{id}/command/trunk_open");
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            request.AddBody(new
            {
                which_trunk = trunkType
            });
            var response = Client.Post(request);
            var json = JObject.Parse(response.Content)["response"];
            var data = JsonConvert.DeserializeObject<ResultStatus>(json.ToString());

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

    public enum VehicleState
    {
        [EnumMember(Value = "Online")]
        ONLINE,

        [EnumMember(Value = "Asleep")]
        ASLEEP,
    }
}