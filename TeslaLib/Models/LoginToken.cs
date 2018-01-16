using System;
using Newtonsoft.Json;

namespace TeslaLib.Models
{
    // Represents all the info for logging in, including a token, a type, and a creation time & expiration time
    // for the token.  Times are in Unix times (seconds from 1970).
    // Example:
    // {"access_token":64 hex characters,"token_type":"bearer","expires_in":7776000,"created_at":1451181413}
    public class LoginToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        // Returns a DateTime in UTC time.
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        // This should be a TimeSpan, but we can't deserialize it appropriately.  Instead, this is the number of seconds to add to CreatedAt.
        // I couldn't get this to convert using the JsonConverter attribute.
        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }
    }
}
