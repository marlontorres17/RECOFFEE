using AutoMapper;
using Entity.DTO.Security;
using Entity.Model.Security;
using Repository.Security.Interface;
using Service.Security.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Implements
{
    public class RoleViewService : IRoleViewService
    {
        private readonly IRoleViewRepository _roleViewRepository;
        private readonly IMapper _mapper;

        public RoleViewService(IRoleViewRepository roleViewRepository, IMapper mapper)
        {
            _roleViewRepository = roleViewRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleViewDto>> GetAll()
        {
            var roleViews = await _roleViewRepository.GetAll();
            return _mapper.Map<IEnumerable<RoleViewDto>>(roleViews);
        }

        public async Task<RoleViewDto> GetById(int id)
        {
            var roleView = await _roleViewRepository.GetById(id);
            return _mapper.Map<RoleViewDto>(roleView);
        }

        public async Task Add(RoleViewDto roleViewDto)
        {
            var roleView = _mapper.Map<RoleView>(roleViewDto);
            await _roleViewRepository.Add(roleView);
        }

        public async Task Update(RoleViewDto roleViewDto)
        {
            var roleView = _mapper.Map<RoleView>(roleViewDto);
            await _roleViewRepository.Update(roleView);
        }

        public async Task DeleteLogically(int id)
        {
            await _roleViewRepository.DeleteLogically(id);
        }

        public async Task DeletePhysically(int id)
        {
            await _roleViewRepository.DeletePhysically(id);
        }
    }
}
