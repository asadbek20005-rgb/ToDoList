using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ToDoList.Service.Helper;

public class UserHelperService : IUserHelperService
{

    private readonly IHttpContextAccessor? _httpContextAccessor;
    public UserHelperService(IHttpContextAccessor? httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        if (_httpContextAccessor.HttpContext?.User?.Claims == null)
        {
            throw new InvalidOperationException("HttpContext or User claims are not available.");
        }

        var nameIdentifierClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (nameIdentifierClaim == null || string.IsNullOrEmpty(nameIdentifierClaim))
        {
            throw new InvalidOperationException("NameIdentifier claim is not present.");
        }

        if (Guid.TryParse(nameIdentifierClaim, out var userId))
        {
            return userId;
        }
        else
        {
            throw new FormatException("Invalid GUID format for user ID.");
        }
    }


}
