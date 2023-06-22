namespace BookStoreManagement.Core.Constants
{
    public static class Errors
    {
        public const string ADD_FAILURE = "Failed to add new resource";
        public const string UPDATE_FAILURE = "Failed to update requested resource";
        public const string DELETE_FAILURE = "Failed to delete requested resource";
        public const string DELETE_PHOTO_FAILURE = "Failed to delete requested photo from cloud";
        public const string RESOURCE_NOTFOUND = "Could not find any resource with the requested ID";
        public const string INCORRECT_PASSWORD = "Password is incorrect";
        public const string GET_USER_FAILURE = "Failed to get current user";
        public const string INCORRECT_ACCOUNT = "Email or phone number is incorrect";
        public const string BAD_ACCOUNT = "Email has already exists";
        public const string BAD_REFRESH_TOKEN = "Refresh token is empty or invalid";
    }
}
