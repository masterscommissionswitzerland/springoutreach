using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace SpringOutreach.CustomAttribute
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessageWrongType());
                }
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessageNoFile());
        }

        private string GetErrorMessageWrongType()
        {
            return "Es sind nur Dateien vom Typ .pdf erlaubt!";
        }

        private string GetErrorMessageNoFile()
        {
            return "Bitte wählen sie eine Datei aus!";
        }
    }
}