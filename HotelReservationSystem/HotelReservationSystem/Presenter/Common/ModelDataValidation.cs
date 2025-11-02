using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class ModelDataValidation
{
    public void Validate(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        bool isValid = Validator.TryValidateObject(model, context, results, true);

        if (!isValid)
        {
            string errorMessage = string.Join("\n- ", results.Select(r => r.ErrorMessage));
            errorMessage = "- " + errorMessage; 
            throw new Exception(errorMessage);
        }
    }
}
