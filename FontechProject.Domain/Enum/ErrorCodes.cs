namespace FontechProject.Domain.Enum;

public enum ErrorCodes
{
    InternalServerError = 10,
    
    ReportNotFound = 1,
    
    ReportAlreadyExists = 2,
    
    UserNotFound = 11,
    
    UserAlreadyExists = 12,
    
    UserUnauthorizedAccess = 13,
    
    UserAlreadyExistsThisRole = 14,
    
    ReportsNotFound = 0,
    
    PasswordNotEqualsPasswordConfirm = 21,
    
    PasswordIsWrong = 23,
    
    RoleAlreadyExists = 31,
    
    RoleNotFound = 32,
    
    
}