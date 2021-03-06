using RestFit.DataAccess.Abstract;

namespace RestFit.API.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = string.Empty;
            return user;
        }
    }
}
