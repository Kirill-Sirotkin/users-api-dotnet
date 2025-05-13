using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using users_api_dotnet.Entities;
using users_api_dotnet.Services;

namespace users_api_dotnet.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase {
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService) {
        _usersService = usersService;
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    public ActionResult<UserDto> CreateUser([FromBody] CreateUserDto newUserData) {
        var userLoginFromClaim = User.FindFirst("Login")?.Value;

        if (userLoginFromClaim is null) {
            return Unauthorized("One or more JWT claims missing");
        }

        var newUser = _usersService.CreateUser(
            newUserData.Login, 
            newUserData.Password, 
            newUserData.Name, 
            newUserData.Gender,
            newUserData.Birthday,
            newUserData.isAdmin,
            userLoginFromClaim   
        );

        if (newUser is null) { return BadRequest("User already exists"); }
        var userDto = new UserDto(newUser);
        return Created("api/v1/users/get-by-login", userDto);
    }

    [HttpPatch("data")]
    public ActionResult<UserDto> UpdateUserData([FromBody] UpdateUserDataDto data) {
        var userIdFromClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoleFromClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        var userActiveFromClaim = User.FindFirst("Active")?.Value;
        var userLoginFromClaim = User.FindFirst("Login")?.Value;

        if (userIdFromClaim is null || userActiveFromClaim is null || userRoleFromClaim is null || userLoginFromClaim is null) {
            return Unauthorized("One or more JWT claims missing");
        }

        if (userRoleFromClaim != "ADMIN") {
            if (userActiveFromClaim != "REVOKED") {
                if (userIdFromClaim != data.Guid.ToString()) {
                    return Forbid();
                }
            } else {
                return Unauthorized("User revoked");
            }
        }

        var user = _usersService.UpdateUserData(data.Guid, data.Name, data.Gender, data.Birthday, userLoginFromClaim);

        if (user is null) { return BadRequest("Failed to update user data"); }
        var userDto = new UserDto(user);
        return Ok(userDto);
    }

    [HttpPatch("password")]
    public ActionResult<UserDto> UpdateUserPassword([FromBody] UpdateUserPasswordDto data) {
        var userIdFromClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoleFromClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        var userActiveFromClaim = User.FindFirst("Active")?.Value;
        var userLoginFromClaim = User.FindFirst("Login")?.Value;

        if (userIdFromClaim is null || userActiveFromClaim is null || userRoleFromClaim is null || userLoginFromClaim is null) {
            return Unauthorized("One or more JWT claims missing");
        }

        if (userRoleFromClaim != "ADMIN") {
            if (userActiveFromClaim != "REVOKED") {
                if (userIdFromClaim != data.Guid.ToString()) {
                    return Forbid();
                }
            } else {
                return Unauthorized("User revoked");
            }
        }

        var user = _usersService.UpdateUserPassword(data.Guid, data.Password, userLoginFromClaim);

        if (user is null) { return BadRequest("Failed to update user password"); }
        var userDto = new UserDto(user);
        return Ok(userDto);
    }

    [HttpPatch("login")]
    public ActionResult<UserDto> UpdateUserLogin([FromBody] UpdateUserLoginDto data) {
        var userIdFromClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoleFromClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        var userActiveFromClaim = User.FindFirst("Active")?.Value;
        var userLoginFromClaim = User.FindFirst("Login")?.Value;

        if (userIdFromClaim is null || userActiveFromClaim is null || userRoleFromClaim is null || userLoginFromClaim is null) {
            return Unauthorized("One or more JWT claims missing");
        }

        if (userRoleFromClaim != "ADMIN") {
            if (userActiveFromClaim != "REVOKED") {
                if (userIdFromClaim != data.Guid.ToString()) {
                    return Forbid();
                }
            } else {
                return Unauthorized("User revoked");
            }
        }

        var user = _usersService.UpdateUserLogin(data.Guid, data.Login, userLoginFromClaim);

        if (user is null) { return BadRequest("Failed to update user login"); }
        var userDto = new UserDto(user);
        return Ok(userDto);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("active")]
    public ActionResult<IEnumerable<UserDto>> GetActiveUsers() {
        var users = _usersService.GetActiveUsers();
        var usersDto = users.Select(u => new UserDto(u)).ToList();
        return Ok(usersDto);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("by-login")]
    public ActionResult<UserDto> GetUserByLogin([FromQuery] string login) {
        if (string.IsNullOrWhiteSpace(login)) { return BadRequest("Login query parameter required"); }

        var user = _usersService.GetUserByLogin(login);
        if (user is null) { return BadRequest("User with that login not found"); }
        var userDto = new UserDto(user);
        return Ok(userDto);
    }

    [AllowAnonymous]
    [HttpPost("by-login-password")]
    public ActionResult<UserWithJwtDto> GetUserByLoginAndPassword([FromBody] LoginPasswordDto data) {
        var user = _usersService.GetUserByLoginAndPassword(data.Login, data.Password);
        if (user is null) { return Unauthorized("Login or password invalid"); }
        var jwt = _usersService.GenerateToken(user);
        var userWithJwtDto = new UserWithJwtDto(user, jwt);
        if (!userWithJwtDto.IsActive) { return Unauthorized("User revoked"); }

        return Ok(userWithJwtDto);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("age")]
    public ActionResult<IEnumerable<UserDto>> GetUsersOlderThanAge([FromQuery] string age) {
        if (string.IsNullOrWhiteSpace(age)) { return BadRequest("Age query parameter required"); }
        int ageNum;
        if (int.TryParse(age, out ageNum)) { return BadRequest("Age query parameter must be an integer"); }

        var users = _usersService.GetUsersOlderThanAge(ageNum);
        var usersDto = users.Select(u => new UserDto(u)).ToList();
        return Ok(usersDto);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete("login")]
    public ActionResult<UserDto> RevokeUserByLogin([FromBody] RevokeUserDto data) {
        var userLoginFromClaim = User.FindFirst("Login")?.Value;

        if (userLoginFromClaim is null) {
            return Unauthorized("One or more JWT claims missing");
        }

        var user = _usersService.RevokeUserByLogin(data.Login, userLoginFromClaim, data.IsDeleteFully);
        if (user is null) { return BadRequest("Failed to revoke user"); }
        var userDto = new UserDto(user);
        return Ok(userDto);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPatch("restore")]
    public ActionResult<UserDto> RestoreUserByLogin([FromBody] LoginDto data) {
        var userLoginFromClaim = User.FindFirst("Login")?.Value;

        if (userLoginFromClaim is null) {
            return Unauthorized("One or more JWT claims missing");
        }

        var user = _usersService.RestoreUserByLogin(data.Login, userLoginFromClaim);
        if (user is null) { return BadRequest("Failed to restore user"); }
        var userDto = new UserDto(user);
        return Ok(userDto);
    }

    [AllowAnonymous]
    [HttpPost("helper")]
    public ActionResult<LoginPasswordDto> HelperAddMockAdmin() {
        return Created("api/v1/users/get-by-login", _usersService.HelperAddMockAdmin());
    }
}
