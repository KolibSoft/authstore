using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KolibSoft.AuthStore.Core.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace KolibSoft.AuthStore.Core;

public class TokenGenerator : ITokenGenerator
{

    public byte[] Secret { get; }
    public string Issuer { get; }
    public string Audience { get; }

    public virtual string Generate(IEnumerable<Claim> claims, TimeSpan ttl)
    {
        var key = new SymmetricSecurityKey(Secret);
        var expires = DateTime.UtcNow.Add(ttl);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.WriteToken(token);
        return jwt;
    }

    public TokenGenerator(byte[] secret, string issuer = "*", string audidence = "*")
    {
        Secret = secret;
        Issuer = issuer;
        Audience = audidence;
    }

}