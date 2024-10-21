


using Dapper;

namespace BLUEY.Models.Repositories
{
    public class TABELACTBFISLCTOCTBRepository : BaseRepository, ITABELACTBFISLCTOCTBRepository
    {
        public TABELACTBFISLCTOCTBRepository(IConfiguration configuration) : base(configuration)
        {

        }
        
        public List<TABELACTBFISLCTOCTB> GetByEmpCtbfis(int CodigoEmpresa, int CodigoTabCTBFIS)
        {
            string query = @"SELECT 
                            A.CODIGOEMPRESA,
                            B.CONTACTB,
                            A.CODIGOHISTCTB,
                            A.COMPLHISTCTB

                            FROM TABELACTBFISLCTOCTB AS A,
                            TABELACTBFISLCTOCTBCONTA AS B
                            WHERE A.CODIGOEMPRESA  = B.CODIGOEMPRESA
                            AND A.CODIGOTABCTBFIS = B.CODIGOTABCTBFIS
                            AND A.CODIGOEMPRESA = @CODIGOEMPRESA
                            AND A.NATURLCTOCTB = 1
                            AND A.CONTACTB  is null
                            AND A.CODIGOTABCTBFIS = @CODIGOTABCTBFIS";
            return _conFirebird.Query<TABELACTBFISLCTOCTB>(query, new { CODIGOEMPRESA = CodigoEmpresa, CODIGOTABCTBFIS = CodigoTabCTBFIS }).ToList();
        }
    }
}
