namespace CafeGameApi.ConfigModels;

public static class AppConstants
{
    /// <summary>
    ///     Validation related constants
    /// </summary>
    public static class Pagination
    {
        /// <summary>
        ///     Max allowed file path length
        /// </summary>
        public const int Limit = 20;
    }

    public static class SMSValidation
    {
        /// <summary>
        ///     Time SMS Is Valid In Seconds
        /// </summary>
        public const int IsValidInSeconds = 300;

        public const int MaximumConcurrentEntries = 10;
    }

    public static class UserRoles
    {
        public const string Banned = "Banned";
        public const string NotRegisteredUser = "NotRegisteredUser";
        public const string RegisteredUser = "RegisteredUser";
        public const string Admin = "Admin";
    }
}