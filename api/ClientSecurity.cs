using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public static class ClientSecurity
{
    private static readonly string secretKey = "5t378yfn525609t7nym8n0ym79fny79m0nm79guhyskhgyuegeusrtgliuhjrfgaiufrweily793ny793ny793ny973yn7";

    public static string GenerateToken()
    {
        var header = new Dictionary<string, object>()
        {
            { "alg", "HS256" },
            { "typ", "JWT" }
        };

        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long exp = now + 999600;

        var payload = new Dictionary<string, object>()
        {
            { "nbf", now },
            { "exp", exp },
            { "iss", "https://auth.rec.net" },
            { "client_id", "recnet" },
            { "role", "developer" },
            { "sub", "6226807" },
            { "auth_time", now - 3600 },
            { "idp", "local" },
            { "jti", GenerateRandomHex(16) },
            { "sid", GenerateRandomHex(16) },
            { "iat", now },
            { "scope", new List<string>
                {
                    "openid",
                    "rn.api",
                    "rn.commerce",
                    "rn.notify",
                    "rn.match.read",
                    "rn.chat",
                    "rn.accounts",
                    "rn.auth",
                    "rn.link",
                    "rn.clubs",
                    "rn.rooms",
                    "rn.discovery"
                }
            },
            { "amr", new List<string> { "mfa" } }
        };

        string headerJson = JsonConvert.SerializeObject(header);
        string payloadJson = JsonConvert.SerializeObject(payload);

        string encodedHeader = Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));
        string encodedPayload = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));

        string unsignedToken = $"{encodedHeader}.{encodedPayload}";

        string signature = CreateSignature(unsignedToken, secretKey);

        return $"{unsignedToken}.{signature}";
    }

    private static string CreateSignature(string unsignedToken, string secret)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secret);
        var messageBytes = Encoding.UTF8.GetBytes(unsignedToken);

        using var hmac = new HMACSHA256(keyBytes);
        byte[] hash = hmac.ComputeHash(messageBytes);

        return Base64UrlEncode(hash);
    }

    private static string Base64UrlEncode(byte[] input)
    {
        return Convert.ToBase64String(input)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    private static string GenerateRandomHex(int byteLength)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] bytes = new byte[byteLength];
        rng.GetBytes(bytes);
        return BitConverter.ToString(bytes).Replace("-", "");
    }
}
