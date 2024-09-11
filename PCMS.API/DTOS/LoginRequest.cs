using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for the user login request.
    /// The request type for the "/login" endpoint based on the ASP.Net Core Identity Routes Source Code.
    /// </summary>
    public record LoginRequest
    {
        /// <summary>
        /// The user's email address which acts as a user name.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; } = string.Empty;

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; } = string.Empty;

        /// <summary>
        /// The optional two-factor authenticator code. This may be required for users who have enabled two-factor authentication.
        /// This is not required if a <see cref="TwoFactorRecoveryCode"/> is sent.
        /// </summary>
        public string? TwoFactorCode { get; init; }

        /// <summary>
        /// An optional two-factor recovery code from <see cref="TwoFactorResponse.RecoveryCodes"/>.
        /// This is required for users who have enabled two-factor authentication but lost access to their <see cref="TwoFactorCode"/>.
        /// </summary>
        public string? TwoFactorRecoveryCode { get; init; }
    }
}