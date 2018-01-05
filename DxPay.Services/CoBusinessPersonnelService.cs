using DxPay.Repositories;
using JMP.MDL;

namespace DxPay.Services
{
    public class CoBusinessPersonnelService : GenericService<CoBusinessPersonnel>, ICoBusinessPersonnelService
    {
        private readonly ICoBusinessPersonnelRepository _repository;
        public CoBusinessPersonnelService(ICoBusinessPersonnelRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public bool UpdatePwd(int id, string password)
        {
            return _repository.UpdatePwd(id, password);
        }

       
    }
}
