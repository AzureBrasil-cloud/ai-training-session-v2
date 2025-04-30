using PowerPilotChat.Application;

namespace ContosoAcai.Application;

public static class Errors
{
    public static Error None = new(Guid.Empty, string.Empty);
    public static Error NullValue = new(ErrorCodes.NullValue.Code, ErrorCodes.NullValue.Message);
    public static Error NotBeNullOrEmpty(string property) => 
        new(
            ErrorCodes.BeNullOrEmpty.Code, 
            ErrorCodes.BeNullOrEmpty.Message,
            new Dictionary<string, object>
            {
                {"property", property}
            });
    
    public static Error EntityExists(string entityName, string id) => 
        new(
            ErrorCodes.EntityExists.Code, 
            ErrorCodes.EntityExists.Message,
            new Dictionary<string, object>
            {
                {"entityName", entityName},
                {"id", id},
            });
    public static Error EntityNotFound(string entityName, string id) => 
        new(
            ErrorCodes.EntityNotFound.Code, 
            ErrorCodes.EntityNotFound.Message,
            new Dictionary<string, object>
            {
                {"entityName", entityName},
                {"id", id},
            });
    
    public static Error InvalidPersonalIdentifier() => 
        new(
            ErrorCodes.InvalidPersonalIdentifier.Code, 
            ErrorCodes.InvalidPersonalIdentifier.Message,
            new Dictionary<string, object>());
    
    public static Error GreaterThanZero(string property) =>
        new(
            ErrorCodes.GreaterThanZero.Code,
            ErrorCodes.GreaterThanZero.Message,
            new Dictionary<string, object>
            {
                { "property", property }
            });
    
    public static Error UnableToCreateRemoteAgent() =>
        new(
            ErrorCodes.UnableToCreateRemoteAgent.Code,
            ErrorCodes.UnableToCreateRemoteAgent.Message,
            new Dictionary<string, object>());
    
    public static Error UnableToCreateRemoteThread() =>
        new(
            ErrorCodes.UnableToCreateRemoteThread.Code,
            ErrorCodes.UnableToCreateRemoteThread.Message,
            new Dictionary<string, object>());
    
    public static Error InvalidEmail(string email) =>
        new(
            ErrorCodes.InvalidEmail.Code,
            ErrorCodes.InvalidEmail.Message,
            new Dictionary<string, object>
            {
                { "email", email }
            });
    
    public static Error Unexpected() =>
        new(
            ErrorCodes.Unexpected.Code,
            ErrorCodes.Unexpected.Message,
            new Dictionary<string, object>());
    
    public static Error UnableToCreateRemoteFile() =>
        new(
            ErrorCodes.UnableToCreateRemoteFile.Code,
            ErrorCodes.UnableToCreateRemoteFile.Message,
            new Dictionary<string, object>());
    
    public static Error InvalidFileExtension() =>
        new(
            ErrorCodes.InvalidFileExtension.Code,
            ErrorCodes.InvalidFileExtension.Message,
            new Dictionary<string, object>());
    
    public static Error UnableToCreateRemoteDocumentConnector() =>
        new(
            ErrorCodes.UnableToCreateRemoteDocumentConnector.Code,
            ErrorCodes.UnableToCreateRemoteDocumentConnector.Message,
            new Dictionary<string, object>());
    
    public static Error UnableToUpdateRemoteDocumentConnector() =>
        new(
            ErrorCodes.UnableToUpdateRemoteDocumentConnector.Code,
            ErrorCodes.UnableToUpdateRemoteDocumentConnector.Message,
            new Dictionary<string, object>());
    
    public static Error UnableToDeleteRemoteFile() =>
        new(
            ErrorCodes.UnableToDeleteRemoteFile.Code,
            ErrorCodes.UnableToDeleteRemoteFile.Message,
            new Dictionary<string, object>());
    
    public static Error UnableRemoveFileFromDocumentConnector() =>
        new(
            ErrorCodes.UnableRemoveFileFromDocumentConnector.Code,
            ErrorCodes.UnableRemoveFileFromDocumentConnector.Message,
            new Dictionary<string, object>());
    
    public static Error UnableAddFileFromDocumentConnector() =>
        new(
            ErrorCodes.UnableAddFileFromDocumentConnector.Code,
            ErrorCodes.UnableAddFileFromDocumentConnector.Message,
            new Dictionary<string, object>());
    
    public static Error InvalidEnum(string enumValue) =>
        new(
            ErrorCodes.InvalidEnum.Code,
            ErrorCodes.InvalidEnum.Message,
            new Dictionary<string, object>
            {
                {"enum", enumValue}
            });
}