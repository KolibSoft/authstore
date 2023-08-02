using System.Security.Claims;

namespace KolibSoft.AuthStore.Core.Abstractions;

public interface ITokenGenerator
{
    public string Generate(IEnumerable<Claim> claims, TimeSpan ttl);
}