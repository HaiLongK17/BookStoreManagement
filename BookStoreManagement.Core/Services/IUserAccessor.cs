namespace BookStoreManagement.Core.Services
{
    public interface IUserAccessor
    {
        string GetUserEmail();
        string GetUserDisplayName(string userId);
    }
}
