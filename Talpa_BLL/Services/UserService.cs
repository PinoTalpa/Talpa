using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Interfaces;

namespace Talpa_BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Login(string userId, string name, string email)
        {
            await _userRepository.Login(userId, name, email );
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            UserDto user = await _userRepository.GetUserAsync(userId);

            if (user != null)
            {
                user = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    ProfileImage = user.ProfileImage
                };
            }

            return user;
        }



        public async Task<User> UpdateUserAsync(User userProfile)
        {
            if (string.IsNullOrEmpty(userProfile.Name))
            {
                userProfile.ErrorMessage = "User name or Image has an incorrect input.";
                return userProfile;
            }

            UserDto userDto = new()
            {
                Id = userProfile.Id,
                Name = userProfile.Name,
                ProfileImage = userProfile.ProfileImage,
            };

            bool isUserUpdated = await _userRepository.UpdateUserAsync(userDto);

            userProfile = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,    
                ProfileImage = userDto.ProfileImage,
            };

            if (!isUserUpdated)
            {
                userProfile.ErrorMessage = "There was an error while creating the suggestion!";
            }

            return userProfile;

        }

    }
}
