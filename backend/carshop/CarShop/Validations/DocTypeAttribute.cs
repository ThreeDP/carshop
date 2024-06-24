using System.ComponentModel.DataAnnotations;

namespace CarShop.Validations;

public class DocTypeAttribute : ValidationAttribute {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrEmpty(value.ToString())) {
            return ValidationResult.Success;
        }
        var docType = value.ToString()?.ToUpper();   
        if (docType is not null && (docType != "CPF" || docType != "CNPJ")) {
            return new ValidationResult("Tipo inválido de documento.");
        }
        return ValidationResult.Success;
    }
}