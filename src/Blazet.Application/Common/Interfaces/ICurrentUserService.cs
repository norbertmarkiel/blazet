namespace Blazet.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; set; }
        string? UserName { get; set; }
    }
}
