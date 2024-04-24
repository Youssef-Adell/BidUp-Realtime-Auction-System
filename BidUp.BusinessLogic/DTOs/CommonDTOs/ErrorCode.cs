namespace BidUp.BusinessLogic.DTOs.CommonDTOs;

public enum ErrorCode
{
    AUTH_VIOLATE_REGISTER_RULES,
    AUTH_INVALID_USERNAME_OR_PASSWORD,
    AUTH_ACCOUNT_IS_LOCKED_OUT,
    AUTH_EMAIL_NOT_CONFIRMED,
    AUTH_INVALID_REFRESH_TOKEN,
    USER_INPUT_INVALID_SYNTAX
}