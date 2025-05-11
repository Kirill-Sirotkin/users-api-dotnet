using Microsoft.AspNetCore.Mvc;
using users_api_dotnet.Entities;
using users_api_dotnet.Services;

namespace users_api_dotnet.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase {
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService) {
        _usersService = usersService;
    }

    [HttpPost]
    public ActionResult<UserDto> CreateUser(CreateUserDto newUserData) {
        var newUser = _usersService.CreateUser(
            newUserData.Login, 
            newUserData.Password, 
            newUserData.Name, 
            newUserData.Gender,
            newUserData.Birthday,
            newUserData.isAdmin    
        );

        if (newUser is null) { return BadRequest("User already exists"); }
        return Ok(newUser);
    }
}
