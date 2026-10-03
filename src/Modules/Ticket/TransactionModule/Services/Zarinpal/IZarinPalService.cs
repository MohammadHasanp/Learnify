using TransactionModule.Services.Zarinpal.DTOs.Payment;
using TransactionModule.Services.Zarinpal.DTOs.Refound;
using TransactionModule.Services.Zarinpal.DTOs.UnVerification;
using TransactionModule.Services.Zarinpal.DTOs.Verification;

namespace TransactionModule.Services.Zarinpal;

public interface IZarinPalService
{
    Task<PaymentResponseData> CreatePaymentRequest(int amount, string description, string callBackUrl, string? mobile = null, string? email = null);
    Task<FinallyVerificationResponse> CreateVerificationRequest(string authority, int price);
    Task<UnVerificationFinallyResponse> GetUnVerificationRequests();
    Task<ReFoundResponse> ReFoundRequest(string authority);

    //SandBox
    Task<FinallyVerificationResponse> SandBox_CreateVerificationRequest(string authority, int price);
    Task<PaymentResponseData> SandBox_CreatePaymentRequest(int amount, string description, string callBackUrl, string? mobile = null, string? email = null);
}