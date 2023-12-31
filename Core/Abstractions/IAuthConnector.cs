using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Core.Abstractions;

public interface IAuthConnector
{
    public bool Available { get; }
    public Task<Result<AuthModel?>> AccessAsync(LoginModel login);
    public Task<Result<AuthModel?>> RefreshAsync(Guid id, string refreshToken);
}