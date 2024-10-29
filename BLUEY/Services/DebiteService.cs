using BLUEY.Models;
using BLUEY.Models.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BLUEY.Services
{
    public class DebiteService : IDebiteService
    {
        private readonly ITABELACTBFISLCTOCTBRepository _tabelactbfislctoctbrepository;
        private readonly IDebiteRepository _debiteRepository;

        public DebiteService(ITABELACTBFISLCTOCTBRepository tabelactbfislctoctbrepository, IDebiteRepository debiteRepository)
        {
            _tabelactbfislctoctbrepository = tabelactbfislctoctbrepository;
            _debiteRepository = debiteRepository;
        }

        public List<LCTOFISConsServ> GetDebit()
        {
            var lctofisconsserv = _debiteRepository.Get();
            //aqui um lupi para carregar o outro dado
            foreach (var item in lctofisconsserv) {
                item.TABELACTBFISLCTOCTB = _tabelactbfislctoctbrepository.GetByEmpCtbfis(item.EMPRESA_,item.TABELA);
            }
            return lctofisconsserv;
        }
        public List<LCTOFISConsServ> GetDebit(string empresa, string datain, string dataout) {
            var lctofisconsserv = _debiteRepository.Get(empresa, datain, dataout);
            //aqui um lupi para carregar o outro dado
            foreach (var item in lctofisconsserv)
            {
                item.TABELACTBFISLCTOCTB = _tabelactbfislctoctbrepository.GetByEmpCtbfis(item.EMPRESA_, item.TABELA);
            }
            return  lctofisconsserv;
        }

        public bool setDebitM(LCTOFISConsServ lctofisconsserv)
        {
            return _debiteRepository.SetDebitMR(lctofisconsserv);
        }
    }
}
