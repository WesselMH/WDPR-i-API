
using Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public abstract class ValidationController : ControllerBase
{

    protected readonly WesselWestSideContext _dbContext;

    public ValidationController(WesselWestSideContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected string GetUserIdFromJWT()
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId"));

        if (claim == null)
        {
            throw new UnauthorizedAccessException("Invalid JWT token.");
        }

        return claim.Value;
    }

    protected Account GetUserFromJWT()
    {
        var userId = GetUserIdFromJWT();
        IdentityUser? user = _dbContext.Users.FirstOrDefault(x => x.Id.Equals(userId));

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid JWT token.");
        }

        return (Account)user;
    }

}