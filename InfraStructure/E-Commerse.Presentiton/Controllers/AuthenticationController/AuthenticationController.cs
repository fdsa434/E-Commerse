using E_commerse.Shared.DTOS.IdentityDtos;
using E_Commerse.ServiceAbstraction.IsurvaceManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Presentiton.Controllers.AuthenticationController
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class AuthenticationController(IserviceManager service):ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserReturnDto> > Login(LoginDto dto)
        {
            var user=await service.AuthorizationService.LoginAsync(dto);
            return Ok(user);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserReturnDto>> Register(RegisterDto dto)
        {
            var user = await service.AuthorizationService.RegisetrAsync(dto);
            return Ok(user);
        }
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email)
        {
            var exists = await service.AuthorizationService.CheckEmailExistsAsync(email);
            return Ok(new { emailExists = exists });
        }
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserReturnDto>> GetCurrentUser([FromQuery] string email)
        {
            var user = await service.AuthorizationService.GetUserByEmailAsync(email);

            if (user == null)
                return Unauthorized("Invalid token or user not found");

            return Ok(user);
        }
        [HttpGet("GetCurrentUserAddress")]
        public async Task<ActionResult<UserAddressDto>> GetCurrentUserAddress([FromQuery] string email)
        {
            var address = await service.AuthorizationService.GetAddressByEmailAsync(email);

            if (address == null)
                return NotFound("Address not found for this user");

            return Ok(address);
        }
        [HttpPost("Update")]
        public async Task<ActionResult<UserAddressDto>> Update([FromBody] UserAddressUpdateDto dto)
        {
            var updated = await service.AuthorizationService.UpdateAddressAsync(dto.Email, dto);
            if (updated == null)
                return NotFound("User not found");

            // ارجع UserAddressDto بدون Email
            return Ok(new UserAddressDto
            {
                FName = updated.FName,
                LName = updated.LName,
                Street = updated.Street,
                Country = updated.Country
            });
        }

    }
}
