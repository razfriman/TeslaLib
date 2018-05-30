using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TeslaLib.Models;

namespace TeslaLib
{
    public static class LoginTokenCache
    {
        private const string CacheFileName = "TeslaLoginTokenCache.cache";

        private static readonly TimeSpan ExpirationTimeWindow = TimeSpan.FromDays(1);
        private static readonly Dictionary<string, LoginToken> Tokens = new Dictionary<string, LoginToken>();
        private static readonly object CacheLock = new object();
        private static volatile bool _haveReadCacheFile;

        private static void ReadCacheFile()
        {
            Tokens.Clear();

            if (!File.Exists(CacheFileName))
            {
                return;
            }

            var serializer = new JsonSerializer();

            using (var reader = File.OpenText(CacheFileName))
            {
                var jsonReader = new JsonTextReader(reader);
                while (!reader.EndOfStream)
                {
                    var emailAddress = reader.ReadLine();
                    var token = serializer.Deserialize<LoginToken>(jsonReader);

                    if (emailAddress != null)
                    {
                        Tokens.Add(emailAddress, token);
                    }
                }
            }
        }

        private static void WriteCacheFile()
        {
            // Note: On an Android device, we either don't have a legal path or we simply can't write anything.
            // Presumably the same behavior will happen with an IPhone.  Can we use isolated storage on Mono?
            try
            {
                using (var writer = File.CreateText(CacheFileName))
                {
                    var serializer = new JsonSerializer();

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

        public static LoginToken GetToken(string emailAddress)
        {
            lock (CacheLock)
            {
                if (!_haveReadCacheFile)
                {
                    ReadCacheFile();
                    _haveReadCacheFile = true;
                }

                if (!Tokens.TryGetValue(emailAddress, out var token))
                {
                    return null;
                }

                if (DateTime.Now + ExpirationTimeWindow >= token.ExpiresUtc)
                {
                    Tokens.Remove(emailAddress);
                    WriteCacheFile();
                    token = null;
                }

                return token;
            }
        }

        public static void AddToken(string emailAddress, LoginToken token)
        {
            lock (CacheLock)
            {
                Tokens[emailAddress] = token;
                WriteCacheFile();
            }
        }

        public static void ClearCache()
        {
            lock (CacheLock)
            {
                Tokens.Clear();
                File.Delete(CacheFileName);
            }
        }
    }
}
