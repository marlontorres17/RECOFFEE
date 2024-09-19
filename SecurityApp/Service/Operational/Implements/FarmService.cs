using AutoMapper;
using Entity.DTO.Operational;
using Entity.Model.Operational;
using Repository.Operational.Interface;
using Service.Operational.Interface;

namespace Service.Operational.Implements
{
    public class FarmService : IFarmService
    {
        private readonly IFarmRepository _farmRepository;
        private readonly IMapper _mapper;

        public FarmService(IFarmRepository farmRepository, IMapper mapper)
        {
            _farmRepository = farmRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FarmDto>> GetAll()
        {
            var farms = await _farmRepository.GetAll();
            return _mapper.Map<IEnumerable<FarmDto>>(farms);
        }

        public async Task<FarmDto> GetById(int id)
        {
            var farm = await _farmRepository.GetById(id);
            return _mapper.Map<FarmDto>(farm);
        }

        public async Task Add(FarmDto farmDto)
        {
            var farm = _mapper.Map<Farm>(farmDto);
            await _farmRepository.Add(farm);
        }

        public async Task Update(FarmDto farmDto)
        {
            var farm = _mapper.Map<Farm>(farmDto);
            await _farmRepository.Update(farm);
        }

        // Eliminación lógica
        public async Task DeleteLogically(int id)
        {
            await _farmRepository.DeleteLogically(id);
        }

        // Eliminación física
        public async Task DeletePhysically(int id)
        {
            await _farmRepository.DeletePhysically(id);
        }
    }
}
