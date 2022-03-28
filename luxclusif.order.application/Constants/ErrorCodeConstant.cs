using luxclusif.order.application.Models;

namespace luxclusif.order.application.Constants
{
    public static class ErrorCodeConstant
    {
        public static readonly ErrorModel Generic = new ErrorModel("0001", "Unfortunately an error occurred during the processing.");
        public static readonly ErrorModel Validation = new ErrorModel("0002", "Unfortunately your request do not pass in our validation process.");
        public static readonly ErrorModel ErrorOnSavingNewUser = new ErrorModel("0003", "Unfortunately an error occorred when saving the Order.");
        public static readonly ErrorModel NotificationValuesError = new ErrorModel("0004", "Error on creating a notification.");
    }
}
