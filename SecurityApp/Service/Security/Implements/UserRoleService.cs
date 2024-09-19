using AutoMapper;
using Entity.DTO.Security;
using Entity.Model.Security;
using Repository.Security.Interface;
using Service.Security.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Implements
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRoleDto>> GetAll()
        {
            var userRoles = await _userRoleRepository.GetAll();
            return _mapper.Map<IEnumerable<UserRoleDto>>(userRoles);
        }

        public async Task<UserRoleDto> GetById(int id)
        {
            var userRole = await _userRoleRepository.GetById(id);
            return _mapper.Map<UserRoleDto>(userRole);
        }

        public async Task Add(UserRoleDto userRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDto);
            await _userRoleRepository.Add(userRole);
        }

        public async Task Update(UserRoleDto userRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDto);
            await _userRoleRepository.Update(userRole);
        }

        public async Task DeleteLogically(int id)
        {
            await _userRoleRepository.DeleteLogically(id);
        }

        public async Task DeletePhysically(int id)
        {
            await _userRoleRepository.DeletePhysically(id);
        }
    }
}
