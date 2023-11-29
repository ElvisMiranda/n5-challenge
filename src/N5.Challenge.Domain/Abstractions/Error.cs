namespace N5.Challenge.Domain.Abstractions
{
    public class Error
    {
        public static ErrorOr.Error None => ErrorOr.Error.Custom(CustomErrorType.None, string.Empty, string.Empty);

        public static ErrorOr.Error NullValue =>
            ErrorOr.Error.Custom(CustomErrorType.NullValue, "Error.NullValue", "Null value was provided");
    }
}
