using HealthCareProject.Models;

namespace WebapiProject.Authentication
{
    public interface IAuth
    {
        string GenerateToken(User user);
    }
}