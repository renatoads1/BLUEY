

using Dapper;

namespace BLUEY.Models.Repositories
{
    public class DebiteRepository : BaseRepository, IDebiteRepository
    {
        public DebiteRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<LCTOFISConsServ> Get()
        {
            string sql = @"select 
                        A.CODIGOEMPRESA AS EMPRESA_,
                        A.CHAVELCTOFISENT AS CHAVE,
                        A.CODIGOESTAB AS FILIAL,
                        A.CODIGOPESSOA AS COD_PESSOA,
                        C.INSCRFEDERAL AS INSCR_FEDERAL,
                        C.NOMEPESSOA AS NOME,
                        B.VALORCONTABILIMPOSTO AS VALOR,
                        B.CODIGOCFOP AS CFOP,
                        B.CODIGOTABCTBFIS AS TABELA,
                        A.NUMERONF,
                        'ME' AS MOVIMENTO
                        from LCTOFISENT AS A,
                        LCTOFISENTCFOP AS B,
                        PESSOA AS C,
                        EMPRESA AS D
                        WHERE A.CODIGOEMPRESA = B.CODIGOEMPRESA
                        AND A.CODIGOESTAB = B.CODIGOESTAB
                        AND A.CHAVELCTOFISENT = B.CHAVELCTOFISENT
                        AND A.CODIGOPESSOA = C.CODIGOPESSOA
                        AND A.CODIGOEMPRESA = D.CODIGOEMPRESA
                        AND A.CODIGOEMPRESA = 203
                        AND A.DATALCTOFIS BETWEEN '01.05.2024' AND '31.05.2024'
                        AND B.CODIGOTABCTBFIS IS NOT NULL
                        AND (B.CODIGOCFOP LIKE '8%' 
                        OR B.CODIGOCFOP LIKE '1407%' 
                        OR B.CODIGOCFOP LIKE '1556%' 
                        OR B.CODIGOCFOP LIKE '2407%' 
                        OR  B.CODIGOCFOP LIKE '2556%')

                        UNION ALL

                        select 
                        A.CODIGOEMPRESA AS EMPRESA_,
                        B.CHAVELCTOFISENTRETIDO AS CHAVE,
                        A.CODIGOESTAB AS FILIAL,
                        A.CODIGOPESSOA AS COD_PESSOA,
                        C.INSCRFEDERAL AS INSCR_FEDERAL,
                        C.NOMEPESSOA AS NOME,
                        B.VALORCONTABIL AS VALOR,
                        B.CODIGOCFOP AS CFOP,
                        B.CODIGOTABCTBFIS AS TABELA,
                        A.NUMERONF,
                        'RE' AS MOVIMENTO
                        from LCTOFISENT AS A,
                        LCTOFISENTRETIDO AS B,
                        PESSOA AS C,
                        EMPRESA AS D
                        WHERE A.CODIGOEMPRESA = B.CODIGOEMPRESA
                        AND A.CODIGOESTAB = B.CODIGOESTAB
                        AND A.CHAVELCTOFISENT = B.CHAVELCTOFISENT
                        AND A.CODIGOPESSOA = C.CODIGOPESSOA
                        AND A.CODIGOEMPRESA = D.CODIGOEMPRESA
                        AND A.CODIGOEMPRESA = 203
                        AND A.DATALCTOFIS BETWEEN '01.05.2024' AND '31.05.2024'
                        AND B.CODIGOTABCTBFIS IS NOT NULL
                        AND (B.CODIGOCFOP LIKE '8%' 
                        OR B.CODIGOCFOP LIKE '1407%' 
                        OR B.CODIGOCFOP LIKE '1556%' 
                        OR B.CODIGOCFOP LIKE '2407%' 
                        OR  B.CODIGOCFOP LIKE '2556%')";

           var r = _conFirebird.Query<LCTOFISConsServ>(sql).ToList();
            return r;

        }
    }
}
