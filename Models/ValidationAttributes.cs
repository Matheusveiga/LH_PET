using System.ComponentModel.DataAnnotations;
using LH_PET.Services;

namespace LH_PET.Models
{
    /// <summary>
    /// Atributo de validação para CPF
    /// </summary>
    public class ValidateCPFAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || value is not string cpf)
            {
                return ValidationResult.Success;
            }

            var validationService = validationContext.GetService(typeof(IValidationService)) as IValidationService;
            if (validationService == null)
            {
                return new ValidationResult("Serviço de validação não disponível.");
            }

            if (!validationService.IsValidCPF(cpf))
            {
                return new ValidationResult("CPF inválido.");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Atributo de validação para CNPJ
    /// </summary>
    public class ValidateCNPJAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || value is not string cnpj)
            {
                return ValidationResult.Success;
            }

            var validationService = validationContext.GetService(typeof(IValidationService)) as IValidationService;
            if (validationService == null)
            {
                return new ValidationResult("Serviço de validação não disponível.");
            }

            if (!validationService.IsValidCNPJ(cnpj))
            {
                return new ValidationResult("CNPJ inválido.");
            }

            return ValidationResult.Success;
        }
    }
}
