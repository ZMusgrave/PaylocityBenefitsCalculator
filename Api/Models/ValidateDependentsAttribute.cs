using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class ValidateDependentsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IEnumerable<Dependent> dependents) return ValidationResult.Success;

        var partnerCount = dependents.Count(d =>
            d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner);
        
        if (partnerCount > 1) return new ValidationResult("An employee can have only one spouse or domestic partner.");


        return ValidationResult.Success;
    }
}