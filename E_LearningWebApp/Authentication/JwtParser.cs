using System.Security.Claims;
using System.Text.Json;

namespace E_LearningWebApp.Authentication;

public class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJWT(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];

        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        ExtractRolesFromJWT(claims, keyValuePairs);

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

        return claims;
    }

    private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
    {
        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

        if (roles is not null)
        {
            var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');

            if (parsedRoles.Length > 1)
            {
                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        if (base64.Length % 4 == 2)
        {
            base64 += "==";
        }
        else if (base64.Length % 4 == 3)
        {
            base64 += "=";
        }
        return Convert.FromBase64String(base64);
    }
}
