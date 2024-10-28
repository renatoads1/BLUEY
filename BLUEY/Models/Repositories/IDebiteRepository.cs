namespace BLUEY.Models.Repositories
{
    public interface IDebiteRepository
    {
        public List<LCTOFISConsServ> Get();
        public bool SetDebitMR(LCTOFISConsServ lctofisconsserv);

    }
}
