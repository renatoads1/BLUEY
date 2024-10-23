namespace BLUEY.Models.Repositories
{
    public interface IDebiteRepository
    {
        public List<LCTOFISConsServ> Get();
        public LCTOFISConsServ SetDebitMR(LCTOFISConsServ lctofisconsserv);

    }
}
