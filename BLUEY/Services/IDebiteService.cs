﻿using BLUEY.Models;

namespace BLUEY.Services
{
    public interface IDebiteService
    {
        public List<LCTOFISConsServ> GetDebit();
        public LCTOFISConsServ setDebitM(LCTOFISConsServ lctofisconsserv);
    }
}
