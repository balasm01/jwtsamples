using System.Collections.Generic;

namespace JWTSamples
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
