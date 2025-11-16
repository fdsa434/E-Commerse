using E_commerse.Shared.DTOS.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.ServiceAbstraction.IService
{
    public interface IAuthorizationService
    {
        //username/email/token
        public Task<UserReturnDto> LoginAsync(LoginDto dto);
        public Task<UserReturnDto> RegisetrAsync(RegisterDto dto);
        public Task<bool> CheckEmailExistsAsync(string email);
        public Task<UserReturnDto?> GetUserByEmailAsync(string email);
        public Task<UserAddressDto?> GetAddressByEmailAsync(string email);
        public Task<UserAddressUpdateDto?> UpdateAddressAsync(string email, UserAddressUpdateDto dto);

    }
}
