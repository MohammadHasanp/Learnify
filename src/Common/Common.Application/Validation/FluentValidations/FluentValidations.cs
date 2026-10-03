
using Common.Application.FileUtil.Validations;
using Common.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Common.Application.Validation.FluentValidations;

public static class FluentValidations
{
    public static IRuleBuilderOptionsConditions<T, TProperty> JustImageFile<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "شما فقط قادر به وارد کردن عکس میباشید") where TProperty : IFormFile? =>
         ruleBuilder.Custom((file, context) =>
         {
             if (file == null)
                 return;

             if (!FileValidation.IsImage(file))
             {
                 context.AddFailure(errorMessage);
             }
         });

    public static IRuleBuilderOptionsConditions<T, string> ValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = ValidationMessages.InvalidPhoneNumber) =>
          ruleBuilder.Custom((phoneNumber, context) =>
          {
              if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length is < 11 or > 11)
                  context.AddFailure(errorMessage);

          });

    public static IRuleBuilderOptionsConditions<T, TProperty> JustValidFile<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "فایل نامعتبر است") where TProperty : IFormFile =>
         ruleBuilder.Custom((file, context) =>
        {
            if (file == null)
                return;

            if (!FileValidation.IsValidFile(file))
            {
                context.AddFailure(errorMessage);
            }
        });

    public static IRuleBuilderOptionsConditions<T, TProperty> JustValidCompressFile<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder, string errorMessage = "فایل نامعتبر است") where TProperty : IFormFile? =>
        ruleBuilder.Custom((file, context) =>
        {
            if (file == null)
                return;

            if (!FileValidation.IsValidCompressFile(file))
            {
                context.AddFailure(errorMessage);
            }
        });

    public static IRuleBuilderOptionsConditions<T, string> ValidNationalCode<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = "کد ملی نامعتبر است") =>
        ruleBuilder.Custom((nationalCode, context) =>
        {
            if (!IranianNationalIdChecker.IsValid(nationalCode))
                context.AddFailure(errorMessage);
        });
}
