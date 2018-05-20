using System;
using Newtonsoft.Json;
using TeslaLib.Converters;

namespace TeslaLib.Models
{
    // Represents all the info for logging in, including a token, a type, and a creation time & expiration time
    // for the token.  Times are in Unix times (seconds from 1970).
    // Example:
    // {"access_token":64 hex characters,"token_type":"bearer","expires_in":7776000,"created_at":1451181413}
    public class LoginToken
    {
        /// <summary>
        /// Gets the access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; }

        /// <summary>
        /// Gets the type of the <see cref="AccessToken"/>.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; }

        /// <summary>
        /// Gets the expiry duration in seconds of the <see cref="AccessToken"/>.
        /// </summary>
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; }

        /// <summary>
        /// Gets the expiry duration of the <see cref="AccessToken"/>.
        /// </summary>
        [JsonIgnore]
        public TimeSpan ExpiresInTimespan => TimeSpan.FromSeconds(ExpiresIn);

        /// <summary>
        /// Gets the UTC <see cref="DateTime"/> when the <see cref="AccessToken"/> expires.
        /// </summary>
        [JsonIgnore]
        public DateTime ExpiresUtc => UnixTimeConverter.ToDateTime(CreatedAt + ExpiresIn);

        /// <summary>
        /// Gets the Epoch timestamp when the <see cref="AccessToken"/> was issued.
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; }

        /// <summary>
        /// Gets the UTC <see cref="DateTime"/> when the <see cref="AccessToken"/> was issued.
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedUtc => UnixTimeConverter.ToDateTime(CreatedAt);

        /// <summary>
        /// Gets the refresh token that can be used to acquire a new <see cref="AccessToken"/>.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; }
    }
}
