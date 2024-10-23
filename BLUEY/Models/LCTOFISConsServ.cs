namespace BLUEY.Models
{
    public class LCTOFISConsServ
    {
        public int Id { get; set; }
        public int EMPRESA_ { get; set; }
        public int CHAVE { get; set; }
        public int FILIAL { get; set; }
        public int COD_PESSOA { get; set; }
        public string INSCR_FEDERAL { get; set; }
        public string NOME { get; set; }
        public decimal VALOR { get; set; }
        public int CFOP { get; set; }
        public int TABELA { get; set; }
        public int NUMERONF { get; set; }
        public string MOVIMENTO { get; set; }
        public int CONTACONTABIL { get; set; }
        public List<TABELACTBFISLCTOCTB> TABELACTBFISLCTOCTB { get; set; }
        public string CONTACADASTRADA { get; set; }
    }
}
