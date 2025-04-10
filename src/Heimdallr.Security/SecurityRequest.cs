using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Heimdallr.Security
{
    public class SecurityRequest : IValidatableObject
    {
        /// <summary>
        /// Service name for which the password is generated
        /// </summary>
        [Required]
        public string ServiceName { get; set; }

        /// <summary>
        /// Service account or password ID
        /// </summary>
        [Required]
        public string CommonName { get; set; }

        /// <summary>
        /// Master password to generate an idempotent password based on the service name and common name and the dictionary used
        /// </summary>
        [MinLength(8)]
        [Required]
        public string MasterPassword { get; set; }

        /// <summary>
        /// Numeric dictionary for password generation
        /// </summary>
        public bool HasNumeric { get; set; } = true;

        /// <summary>
        /// Letter dictionary for password generation
        /// </summary>
        public bool HasLetters { get; set; } = true;

        /// <summary>
        /// Symbolic dictionary for password generation
        /// </summary>
        public bool HasSpecialSymbols { get; set; } = true;

        /// <summary>
        /// Length of generated password
        /// </summary>
        [Required]
        public int Length { get; set; } = 16;

        /// <summary>
        /// Password version (if you need a new password for the same service and account)
        /// </summary>
        [Required]
        public int Version { get; set; } = 1;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!HasNumeric && !HasLetters && !HasSpecialSymbols)
                return
                [
                    new ValidationResult(
                        "The following fields cannot be simultaneously false",
                        new[] {nameof(HasNumeric),nameof(HasLetters),nameof(HasSpecialSymbols)})
                ];

            if(Length < 8 || Length > 32 )
                return
                [
                    new ValidationResult(
                        "Length cannot less than 8 and more than 32",
                        new[] {nameof(Length)})
                ];

            return [ValidationResult.Success];
        }
    }
}