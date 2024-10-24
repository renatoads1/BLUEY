﻿

using BLUEY.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BLUEY.Models.Repositories
{
    public class DebiteRepository : BaseRepository, IDebiteRepository
    {
        private readonly ApplicationDbContext _contextM;
        public DebiteRepository(ApplicationDbContext context, IConfiguration configuration) : base(configuration)
        {
            _contextM = context;

        }

        public List<LCTOFISConsServ> Get()
        {
            string sql2 = @"SELECT 
                            EMPRESA_,
                            FILIAL,
                            COD_PESSOA,
                            INSCR_FEDERAL,
                            NOME,
                            CFOP,
                            TABELA,
                            MOVIMENTO
                            FROM (select 
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
                            OR  B.CODIGOCFOP LIKE '2556%'
                            OR  B.CODIGOCFOP LIKE '1406%'
                            OR  B.CODIGOCFOP LIKE '2406%'
                            OR  B.CODIGOCFOP LIKE '1551%'
                            OR  B.CODIGOCFOP LIKE '2551%')

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
                            OR  B.CODIGOCFOP LIKE '2556%'
                            OR  B.CODIGOCFOP LIKE '1406%'
                            OR  B.CODIGOCFOP LIKE '2406%'
                            OR  B.CODIGOCFOP LIKE '1551%'
                            OR  B.CODIGOCFOP LIKE '2551%'))
                            GROUP BY 1,2,3,4,5,6,7,8";
            try
            {
                var r = _conFirebird.Query<LCTOFISConsServ>(sql2).ToList();
                return r;
            }
            catch (Exception ex) {
                //_logger.LogError("Erro ao conectar com Firebird");
                List<LCTOFISConsServ> lst = new List<LCTOFISConsServ>();
                return lst;
             }
            
        }

        public LCTOFISConsServ SetDebitMR(LCTOFISConsServ lctofisconsserv)
        {
            try
            {
                // Verifique se o registro já existe no banco de dados.
                // Substitua os critérios de comparação (Ex: COD_PESSOA) pelos campos relevantes para sua aplicação.
                bool exists = _contextM.LCTOFISConsServs
                                .Any(x => x.COD_PESSOA == lctofisconsserv.COD_PESSOA &&
                                          x.FILIAL == lctofisconsserv.FILIAL);

                _contextM.Add(lctofisconsserv);
                _contextM.SaveChanges();
                return lctofisconsserv;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                LCTOFISConsServ l = new LCTOFISConsServ();
                return l;
                
            }

        }
    }
}
