namespace BLUEY.Models.Repositories
{
    public interface ITABELACTBFISLCTOCTBRepository
    {
        public List<TABELACTBFISLCTOCTB> GetByEmpCtbfis(int CODIGOEMPRESA, int CODIGOTABCTBFIS);

    }
}
