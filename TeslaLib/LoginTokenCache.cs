using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TeslaLib.Converters;
using TeslaLib.Models;

namespace TeslaLib
{
    internal static class LoginTokenCache
    {
        private const String CacheFileName = "TeslaLoginTokenCache.cache";
        // Make sure the token from the cache is valid for this long.
        private static readonly TimeSpan ExpirationTimeWindow = TimeSpan.FromDays(1);

        private static Dictionary<String, LoginToken> Tokens = new Dictionary<String, LoginToken>();
        private static volatile bool haveReadCacheFile = false;
        private static Object cacheLock = new Object();

        private static void ReadCacheFile()
        {
            Tokens.Clear();
            if (!File.Exists(CacheFileName))
                return;

            JsonSerializer serializer = new JsonSerializer();
            using(StreamReader reader = File.OpenText(CacheFileName))
            {
                JsonReader jsonReader = new JsonTextReader(reader);
                String emailAddress = null;
                while (!reader.EndOfStream)
                {
                    emailAddress = reader.ReadLine();
                    LoginToken token = serializer.Deserialize<LoginToken>(jsonReader);
                    Tokens.Add(emailAddress, token);
                }
            }
        }

        private static void WriteCacheFile()
        {
            // Note: On an Android device, we either don't have a legal path or we simply can't write anything.
            // Presumably the same behavior will happen with an IPhone.  Can we use isolated storage on Mono?
            try
            {
                using (StreamWriter writer = File.CreateText(CacheFileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    foreach (var pair in Tokens)
                    {
                        writer.WriteLine(pair.Key);
                        serializer.Serialize(writer, pair.Value);
                        writer.WriteLine();
                    }
                }
            }
            catch (UnauthorizedAccessException)
            { }
        }

        public static LoginToken GetToken(String emailAddress)
        {
            lock (cacheLock)
            {
                if (!haveReadCacheFile)
                {
                    ReadCacheFile();
                    haveReadCacheFile = true;
                }

                LoginToken token;
                if (!Tokens.TryGetValue(emailAddress, out token))
                {
                    return null;
                }

                // Ensure the LoginToken is still valid.
                DateTime expirationTime = token.CreatedAt.ToLocalTime() + UnixTimeConverter.FromUnixTimeSpan(token.ExpiresIn);
                if (DateTime.Now + ExpirationTimeWindow >= expirationTime)
                {
                    Tokens.Remove(emailAddress);
                    WriteCacheFile();
                    token = null;
                }
                return token;
            }
        }

        public static void AddToken(String emailAddress, LoginToken token)
        {
            lock (cacheLock)
            {
                Tokens[emailAddress] = token;
                WriteCacheFile();
            }
        }

        public static void ClearCache()
        {
            lock (cacheLock)
            {
                Tokens.Clear();
                File.Delete(CacheFileName);
            }
        }
    }
}
