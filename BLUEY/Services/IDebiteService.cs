using BLUEY.Models;
using System.Data;

namespace BLUEY.Services
{
    public interface IDebiteService
    {
        public List<LCTOFISConsServ> GetDebit();
        public List<LCTOFISConsServ> GetDebit(string empresa, string datain, string dataout);
        public bool setDebitM(LCTOFISConsServ lctofisconsserv);
    }
}
