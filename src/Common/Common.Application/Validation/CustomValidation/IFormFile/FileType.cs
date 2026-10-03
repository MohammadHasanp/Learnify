#nullable enable
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.Application.Validation.CustomValidation.IFormFile
{
    public class FileTypeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _type;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType">Example : png</param>
        public FileTypeAttribute(string fileType)
        {
            _type = fileType;
        }
        public override bool IsValid(object? value)
        {
            var fileInput = value as Microsoft.AspNetCore.Http.IFormFile;
            if (fileInput == null) return true;


            var fileType = Path.GetExtension(fileInput.FileName);
            return fileType == _type;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.TryAdd("data-val", "true");
            context.Attributes.Add("fileType", _type);
            context.Attributes.Add("accept", $".{_type}");
            context.Attributes.Add("data-val-fileType", ErrorMessage);
        }
    }
}