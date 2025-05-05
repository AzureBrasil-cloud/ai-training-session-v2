namespace ContosoAcai.Application.Orders.Models.Requests;

public record CreateAudioPreOrdersRequest(
    string UserEmail,
    string AudioName,
    string AudioExtension,
    Stream Content);