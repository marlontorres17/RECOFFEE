using AutoMapper;
using Entity.DTO.Operational;
using Entity.Model.Operational;
using Repository.Operational.Interface;
using Service.Operational.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Operational.Implements
{
    public class PersonBenefitService : IPersonBenefitService
    {
        private readonly IPersonBenefitRepository _personBenefitRepository;
        private readonly IMapper _mapper;

        public PersonBenefitService(IPersonBenefitRepository personBenefitRepository, IMapper mapper)
        {
            _personBenefitRepository = personBenefitRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonBenefitDto>> GetAll()
        {
            var personBenefits = await _personBenefitRepository.GetAll();
            return _mapper.Map<IEnumerable<PersonBenefitDto>>(personBenefits);
        }

        public async Task<PersonBenefitDto> GetById(int id)
        {
            var personBenefit = await _personBenefitRepository.GetById(id);
            return _mapper.Map<PersonBenefitDto>(personBenefit);
        }

        public async Task Add(PersonBenefitDto personBenefitDto)
        {
            var personBenefit = _mapper.Map<PersonBenefit>(personBenefitDto);
            await _personBenefitRepository.Add(personBenefit);
        }

        public async Task Update(PersonBenefitDto personBenefitDto)
        {
            var personBenefit = _mapper.Map<PersonBenefit>(personBenefitDto);
            await _personBenefitRepository.Update(personBenefit);
        }

        public async Task Delete(int id)
        {
            await _personBenefitRepository.Delete(id);
        }
    }
}
