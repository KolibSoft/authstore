using System.Security.Cryptography;
using System.Text;

namespace KolibSoft.AuthStore.Core.Utils;

public static class HashExtensions
{

    public static string GetHashString(this string @string)
    {
        var bytes = Encoding.UTF8.GetBytes(@string);
        var hash = SHA256.HashData(bytes);
        var hashString = string.Concat(hash.Select(x => x.ToString("x2")));
        return hashString;
    }

}