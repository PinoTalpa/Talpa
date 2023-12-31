﻿using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_DAL.Entities;
using Talpa_DAL.Interfaces;
using Talpa_DAL.Repositories;

namespace Talpa_BLL.Services
{
    public class UserActivityDateService : IUserActivityDateService
    {
        public readonly IUserActivityDateRepository _userActivityDateRepository;

        public UserActivityDateService(IUserActivityDateRepository userActiryDateRepository)
        {
            _userActivityDateRepository = userActiryDateRepository;
        }

        public async Task<Models.UserActivityDate> AddUserActivityDateAsync(Models.UserActivityDate userActivityDate)
        {
            UserActivityDateDto newUserActivityDate = new()
            {
                UserId = userActivityDate.UserId,
                ActivityDateId = userActivityDate.ActivityDateId,
                IsAvailable = userActivityDate.IsAvailable
            };

            bool userCreated = await _userActivityDateRepository.CreateUserActivityDateAsync(newUserActivityDate);

            if (!userCreated)
            {
                return userActivityDate;
            }

            return userActivityDate;
        }
    }
}
