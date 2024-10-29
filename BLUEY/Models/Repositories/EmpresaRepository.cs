

using Dapper;

namespace BLUEY.Models.Repositories
{
    public class EmpresaRepository :BaseRepository, IEmpresaRepository
    {
        public EmpresaRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Empresa> GetAllEmpresa()
        {
            var query = @"select A.CODIGOEMPRESA,A.CODIGOESTAB,B.NOMEEMPRESA,A.INSCRFEDERAL,A.DATAENCERATIV from estab as A, empresa as B where A.CODIGOEMPRESA = B.CODIGOEMPRESA AND A. TIPOINSCR  = 2 AND A.CODIGOESTAB = 1";
            var result = _conFirebird.Query<Empresa>(query).ToList();
            return result;
        }

        public Empresa GetById(string inscFed)
        {
            throw new NotImplementedException();
        }
    }
}
