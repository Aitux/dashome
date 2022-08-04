using Dashome.Domain.Models.Dtos;

namespace Dashome.Application.Features.Auth.Queries.GetMe;

public class GetMeResult
{
    public UserDto User { get; set; }
}