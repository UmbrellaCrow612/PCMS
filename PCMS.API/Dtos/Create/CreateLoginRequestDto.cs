using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO for the user login request.
    /// The request type for the "/login" endpoint based on the ASP.Net Core Identity Routes Source Code.
    /// </summary>
    public record CreateLoginRequestDto
    {
        [Required]
        public required string UserName { get; init; }

        [Required]
        public required string Password { get; init; }

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