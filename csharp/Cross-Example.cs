using Jose; // https://github.com/dvsekhvalnov/jose-jwt
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace HMRestAPISample
{
    class MainClass
    {
        static HttpClient client = new HttpClient();

        // Replace the config with your snippet
        static string version = "1.2";
        //Remove header, footer and newlines from your private key in the snippet        
        static string private_key = "MIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgtzd1N1DQ4U3n0ShskaKQjtmOhEqBpx2B5DsAf6muc8OhRANCAAT9d1CpsA8Y8/IRzn4CrP5dvMT3yZrZciyGsJ7vZUz6EqUsaPnWdx8UZO8AOFMiy0vPO+yJtysybLk5/1CEGAnf";
        static string app_uri = "https://sandbox.rest-api.high-mobility.com/v3";
        static string app_id = "499063A8F468440EE94F9DF9";
        static string access_token = "2f5c1c9f-4ac0-49c4-82e9-d39800c68133";

        static string createJwtToken()
        {
            var payload = new Dictionary<string, object>()
                {
                    { "api_version", version },
                    { "app_id", app_id},
                    { "aud", app_uri},
                    { "iat", DateTimeOffset.Now.ToUnixTimeSeconds()},
                    { "jti", Guid.NewGuid()},
                    { "access_token", access_token}
                };

            ECDsa key = ECDsa.Create();
            key.ImportPkcs8PrivateKey(Convert.FromBase64String(private_key), out _);

            string token = Jose.JWT.Encode(payload, key, JwsAlgorithm.ES256);
            return token;
        }

        public static void Main(string[] args)
        {
            client.BaseAddress = new Uri(app_uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", createJwtToken());

            Console.WriteLine(client.DefaultRequestHeaders.ToString());
            HttpResponseMessage response = client.GetAsync(app_uri + "/vehicle_status").Result;
            string body = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(response.ToString());
            Console.WriteLine(body);

            Console.ReadLine();
        }
    }
}

