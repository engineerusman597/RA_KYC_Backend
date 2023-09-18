using Application.Dtos.Account;

namespace RA_KYC_BE.Application.Interfaces.TypedRepositories
{
    public interface IAuthRepository
    {
        //For signup logic
        Task<AuthResponse> SignUpAsync(SignUp model, string orgin);
        //For login logic
        Task<AuthResponse> LoginAsync(Login model);
        //for addroles logic
        Task<string> AssignRolesAsync(AssignRolesDto model);

        //for checking if the sent token is valid
        Task<AuthResponse> RefreshTokenCheckAsync(string token);
        // for revoking refreshrokens
        Task<bool> RevokeTokenAsync(string token);
        Task<string> ConfirmEmailAsync(string userId, string code);
    }
}
