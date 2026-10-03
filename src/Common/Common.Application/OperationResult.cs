namespace Common.Application;


public class OperationResult<TData>
{
    public const string SuccessMessage = "عملیات با موفقیت انجام شد";
    public const string ErrorMessage = "عملیات با شکست مواجه شد";
    public const string NotFoundMessage = "موردی یافت نشد";

    public string Message { get; set; } = null!;
    public string? Title { get; set; }
    public EnOperationResultStatus Status { get; set; }
    public TData Data { get; set; } = default!;

    public static OperationResult<TData> Success(TData data) =>
         new()
         {
             Status = EnOperationResultStatus.Success,
             Message = SuccessMessage,
             Data = data,
         };

    public static OperationResult<TData> NotFound() =>
         new()
         {
             Status = EnOperationResultStatus.NotFound,
             Title = "NotFound",
             Data = default(TData),
             Message = NotFoundMessage,
         };

    public static OperationResult<TData> Error(string message = ErrorMessage) =>

         new()
         {
             Status = EnOperationResultStatus.Error,
             Title = "مشکلی در عملیات رخ داده",
             Data = default(TData),
             Message = message
         };

    public static OperationResult<TData> Error(TData? data, string message = ErrorMessage) =>

     new()
     {
         Status = EnOperationResultStatus.Error,
         Title = "مشکلی در عملیات رخ داده",
         Data = data,
         Message = message
     };
}
public class OperationResult
{
    public const string SuccessMessage = "عملیات با موفقیت انجام شد";
    public const string ErrorMessage = "عملیات با شکست مواجه شد";
    public const string NotFoundMessage = "موردی یافت نشد";

    public string Message { get; set; } = null!;
    public string? Title { get; set; }
    public EnOperationResultStatus Status { get; set; }

    public static OperationResult Error() =>
         new()
         {
             Status = EnOperationResultStatus.Error,
             Message = ErrorMessage,
         };

    public static OperationResult NotFound(string message) =>
         new()
         {
             Status = EnOperationResultStatus.NotFound,
             Message = NotFoundMessage,
         };

    public static OperationResult NotFound() =>
          new()
          {
              Status = EnOperationResultStatus.NotFound,
              Message = NotFoundMessage,
          };

    public static OperationResult Error(string message) =>
         new()
         {
             Status = EnOperationResultStatus.Error,
             Message = message,
         };

    public static OperationResult Error(string message, EnOperationResultStatus status) =>
        new()
        {
            Status = status,
            Message = message,
        };
    public static OperationResult Success() =>
         new()
         {
             Status = EnOperationResultStatus.Success,
             Message = SuccessMessage,
         };

    public static OperationResult Success(string message) =>
        new()
        {
            Status = EnOperationResultStatus.Success,
            Message = message,
        };
}


public enum EnOperationResultStatus
{
    Error = 10,
    Success = 200,
    NotFound = 404
}
