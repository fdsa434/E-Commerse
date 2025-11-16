using E_Commers.Domain.Exceptions;
using E_Commers.Domain.Identity;
using E_commerse.Shared.DTOS.IdentityDtos;
using E_Commerse.ServiceAbstraction.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerse.Serviceimplemention.Serviceimplemetition.AuthorizationService
{
    public class AuthorizationService(UserManager<ApplicationUser> usermanager,IConfiguration con ) : IAuthorizationService
    {
        public async Task<UserReturnDto> LoginAsync(LoginDto dto)
        {
            var us = await usermanager.FindByEmailAsync(dto.Email);
            if(us is not null)
            {
                var flag = await usermanager.CheckPasswordAsync(us, dto.Password);
                if (flag)
                {
                    
                    return new UserReturnDto()
                    {
                        email = us.Email,
                        username = us.UserName,
                        token = await CreateTokenAsync(us)
                    };
                }
                else
                {
                    throw new Exception("SomeThing Went Wrong!");
                }
            }
            else
            {
                throw new UserNotFoundEX();

            }
        }

        public async Task<UserReturnDto> RegisetrAsync(RegisterDto dto)
        {
            var user = new ApplicationUser()
            {
                UserName = dto.Username,
                displayname = dto.DisplayName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
            };
            var create = await usermanager.CreateAsync(user);
            if (create.Succeeded)
            {
                return new UserReturnDto()
                {
                    email = user.Email,
                    token = await CreateTokenAsync(user),
                    username = user.UserName
                };
            }
            else
            {
                var error = create.Errors.Select(p => p.Description).ToList();
                throw new BadRequestEX(error);
            }
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claim = new List<Claim>()
          {

              new Claim(ClaimTypes.Name,user.UserName),
              new Claim (ClaimTypes.Email,user.Email),
              new Claim (ClaimTypes.HomePhone,user.PhoneNumber),
          };
            var roles = await usermanager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(con.GetSection("Jwt")["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
             issuer: con.GetSection("Jwt")["Issuer"],
             audience: con.GetSection("Jwt")["Audience"],
             claims: claim,
             expires: DateTime.Now.AddHours(3),
             signingCredentials: creds);
            var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenstring;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var user = await usermanager.FindByEmailAsync(email);
            return user != null;
        }
        public async Task<UserReturnDto?> GetUserByEmailAsync(string email)
        {
            var user = await usermanager.FindByEmailAsync(email);

            if (user == null)
                return null;

            return new UserReturnDto
            {
                email = user.Email,
                username = user.UserName,
                token = await CreateTokenAsync(user)
            };
        }
        public async Task<UserAddressDto?> GetAddressByEmailAsync(string email)
        {
            var user = await usermanager.Users
                .Include(u => u.address) // Include navigation property
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.address == null)
                return null;

            return new UserAddressDto
            {
                FName = user.address.fname,
                LName = user.address.lname,
                Street = user.address.street,
                Country = user.address.country
            };
        }
        public async Task<UserAddressUpdateDto?> UpdateAddressAsync(string email, UserAddressUpdateDto dto)
        {
            var user = await usermanager.Users
                .Include(u => u.address)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            if (user.address == null)
            {
                user.address = new Address
                {
                    id = Guid.NewGuid().ToString(),
                    userid = user.Id,
                    fname = dto.FName,
                    lname = dto.LName,
                    street = dto.Street,
                    country = dto.Country,
                    user = user
                };
            }
            else
            {
                user.address.fname = dto.FName;
                user.address.lname = dto.LName;
                user.address.street = dto.Street;
                user.address.country = dto.Country;
            }

            await usermanager.UpdateAsync(user);

            return dto;
        }


    }
}

