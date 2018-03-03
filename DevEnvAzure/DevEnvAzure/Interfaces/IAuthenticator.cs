using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace DevEnvAzure
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri);
        Task<AuthenticationResult> ReAuthenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri);
    }
}
