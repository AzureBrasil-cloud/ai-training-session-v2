namespace PowerPilotChat.Application;

public class ErrorCodes
{
    public static (Guid Code, string Message) BeNullOrEmpty = (Guid.Parse("7C254CFC-19BB-4A64-A640-28E6F2127277"), "The '{property}' must not be null or empty");
    public static (Guid Code, string Message) EntityNotFound = (Guid.Parse("143C4702-5E1A-493E-BF1C-91321431C366"), "Entity '{entityName}' with Id '{id}' not found");
    public static (Guid Code, string Message) EntityExists = (Guid.Parse("BCDAC5E7-B84A-4BA6-BFA8-D6628F567D1E"), "The '{entityName}' with Id '{id}' already exists");
    public static (Guid Code, string Message) NullValue = (Guid.Parse("1D238A51-F3A5-4C20-8D4F-CEC9C2848634"), "The '{property}' must not be null");
    public static (Guid Code, string Message) InvalidPersonalIdentifier = (Guid.Parse("B0914510-4216-4208-A738-6E9612C07E1F"), "The personal identifier is invalid");
    public static (Guid Code, string Message) GreaterThanZero = (Guid.Parse("6C8E0B49-880D-4ACE-88AB-EA4DAE3362E8"), "The '{property}' must be greater to 0");
    public static (Guid Code, string Message) UnableToCreateRemoteAgent = (Guid.Parse("64074AB4-DFD0-4866-A55F-EB2BD4850FB0"), "Unable to create remote agent");
    public static (Guid Code, string Message) UnableToCreateRemoteThread = (Guid.Parse("FA5F27DE-081C-4BDF-92E0-7A392DF0797F"), "Unable to create remote thread");
    public static (Guid Code, string Message) InvalidEmail = (Guid.Parse("095D37F0-DE91-4DBD-B505-6AB76A4ADA61"), "The e-mail '{email}' is invalid");
    public static (Guid Code, string Message) Unexpected = (Guid.Parse("C43B8E75-54F6-4546-A545-FDC9B7564745"), "Unexpected error");
    public static (Guid Code, string Message) InvalidFileExtension = (Guid.Parse("263838C1-ACD5-43CE-B844-5537D016474F"), "The file extension is invalid");
    public static (Guid Code, string Message) UnableToCreateRemoteDocumentConnector = (Guid.Parse("D66245DC-3EA1-45A1-BCAD-327FC646D8D0"), "Unable to create remote document connector");
    public static (Guid Code, string Message) UnableToUpdateRemoteDocumentConnector = (Guid.Parse("250C5335-FE5C-4CF0-BF64-2FA610D5F1D1"), "Unable to update remote document connector");
    public static (Guid Code, string Message) UnableToCreateRemoteFile = (Guid.Parse("D66245DC-3EA1-45A1-BCAD-327FC646D8D0"), "Unable to create the remote file");
    public static (Guid Code, string Message) UnableToDeleteRemoteFile = (Guid.Parse("D66245DC-3EA1-45A1-BCAD-327FC646D8D0"), "Unable to delete the remote file");
    public static (Guid Code, string Message) UnableRemoveFileFromDocumentConnector = (Guid.Parse("C2A531D9-FC34-417F-9AAC-7EA9E9610AAA"), "Unable to remove the file from the document connector");
    public static (Guid Code, string Message) UnableAddFileFromDocumentConnector = (Guid.Parse("12298596-0EE6-4225-BC30-7D3D8AAB9EAC"), "Unable to add the file from the document connector");
    public static (Guid Code, string Message) UnableToUpdateRemoteAgentConnectors = (Guid.Parse("E4073D2A-F88C-4003-BD4B-3E0BAA9795EA"), "Unable to update remote agent connectors");
}