namespace Restaurant.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}