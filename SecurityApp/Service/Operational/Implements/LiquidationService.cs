using AutoMapper;
using Entity.DTO.Operational;
using Entity.Model.Operational;
using Repository.Operational.Interface;
using Service.Operational.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Operational.Implements
{
    public class LiquidationService : ILiquidationService
    {
        private readonly ILiquidationRepository _liquidationRepository;
        private readonly IMapper _mapper;

        public LiquidationService(ILiquidationRepository liquidationRepository, IMapper mapper)
        {
            _liquidationRepository = liquidationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LiquidationDto>> GetAll()
        {
            var liquidations = await _liquidationRepository.GetAll();
            return _mapper.Map<IEnumerable<LiquidationDto>>(liquidations);
        }

        public async Task<LiquidationDto> GetById(int id)
        {
            var liquidation = await _liquidationRepository.GetById(id);
            return _mapper.Map<LiquidationDto>(liquidation);
        }

        public async Task Add(LiquidationDto liquidationDto)
        {
            var liquidation = _mapper.Map<Liquidation>(liquidationDto);
            await _liquidationRepository.Add(liquidation);
        }

        public async Task Update(LiquidationDto liquidationDto)
        {
            var liquidation = _mapper.Map<Liquidation>(liquidationDto);
            await _liquidationRepository.Update(liquidation);
        }

        public async Task Delete(int id)
        {
            await _liquidationRepository.Delete(id);
        }
    }
}
