﻿using Entity.DTO.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Security.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task Add(UserDto userDto);
        Task Update(UserDto userDto);
        Task Delete(int id);
    }
}
