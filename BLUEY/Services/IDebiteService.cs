﻿using BLUEY.Models;

namespace BLUEY.Services
{
    public interface IDebiteService
    {
        public List<LCTOFISConsServ> GetDebit();
        public bool setDebitM(LCTOFISConsServ lctofisconsserv);
    }
}
