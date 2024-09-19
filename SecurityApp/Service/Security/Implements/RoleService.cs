using AutoMapper;
using Entity.DTO.Security;
using Entity.Model.Security;
using Repository.Security.Interface;
using Service.Security.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAll()
        {
            var roles = await _roleRepository.GetAll();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetById(int id)
        {
            var role = await _roleRepository.GetById(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task Add(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _roleRepository.Add(role);
        }

        public async Task Update(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            await _roleRepository.Update(role);
        }

        public async Task DeleteLogically(int id)
        {
            await _roleRepository.DeleteLogically(id);
        }

        public async Task DeletePhysically(int id)
        {
            await _roleRepository.DeletePhysically(id);
        }
    }
}
