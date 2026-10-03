using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionModule.Enum
{
    public enum PaymentGateway
    {
        ZarinPal
    }


    public enum TransactionStatus
    {
        Pending = 0,
        PaymentSuccess = 1,
        PaymentError = 2,
        CancelPayment = 3
    }

    public enum TransactionFor
    {
        CourseOrder = 1,
    }
}
