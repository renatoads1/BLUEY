namespace BLUEY.Models.Repositories
{
    public interface IDebiteRepository
    {
        public List<LCTOFISConsServ> Get();
        public List<LCTOFISConsServ> Get(string empresa, string datain, string dataout);

        public bool SetDebitMR(LCTOFISConsServ lctofisconsserv);

    }
}
