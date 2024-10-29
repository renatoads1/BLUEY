using BLUEY.Models;
using BLUEY.Models.Repositories;
using BLUEY.Models.ViewModels;
using BLUEY.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLUEY.Controllers
{

    public class DebiteController : BaseController
    {
        private readonly IDebiteRepository _debiteRepository;
        private readonly IDebiteService _debiteService;
        private readonly IEmpresaRepository _empresaRepository;

        public DebiteController(IEmpresaRepository empresaRepository, ILogger<BaseController> logger, IDebiteRepository debiteRepository, IDebiteService debiteService):base(logger)
        {
            _empresaRepository = empresaRepository;
            _debiteRepository = debiteRepository;
            _debiteService = debiteService;
        }


        // GET: DebiteController
        public ActionResult Index()
        {
            var result = _empresaRepository.GetAllEmpresa();
            ViewBag.Empresas = result;
            //var debit = _debiteService.GetDebit();
            //return View(debit);
            return View();
        }

        [HttpPost]
        [Route("GetDebit")]
        public ActionResult GetDebit(int empresa, DateTime datain, DateTime dataout)
        {
            var result = _empresaRepository.GetAllEmpresa();
            ViewBag.Empresas = result;

            var emp = empresa.ToString();
            var din =  datain.ToString("dd.MM.yyyy"); 
            var dout = dataout.ToString("dd.MM.yyyy");

            var debit = _debiteService.GetDebit(emp, din, dout);
            return View("Index",debit);
        }

        [HttpPost]
        [Route("/SetDebit")]
        public async Task<JsonResult> SetDebit([FromBody] LctofisconsServViewModel lctofisconsservviewmodel) 
        {
            LCTOFISConsServ lcto = new LCTOFISConsServ();
            lcto.EMPRESA_ = lctofisconsservviewmodel.EMPRESA_;
            lcto.FILIAL = lctofisconsservviewmodel.FILIAL;
            lcto.COD_PESSOA = lctofisconsservviewmodel.COD_PESSOA;
            lcto.INSCR_FEDERAL = lctofisconsservviewmodel.INSCR_FEDERAL.ToString();
            lcto.NOME = lctofisconsservviewmodel.NOME;
            lcto.CFOP = lctofisconsservviewmodel.CFOP;
            lcto.TABELA = lctofisconsservviewmodel.TABELA;
            lcto.MOVIMENTO = lctofisconsservviewmodel.TABELA.ToString();
            lcto.CONTACADASTRADA = lctofisconsservviewmodel.TABELACTBFISLCTOCTB;

            _debiteService.setDebitM(lcto);

            return new JsonResult(true);
        }

        // GET: DebiteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DebiteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DebiteController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DebiteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DebiteController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DebiteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DebiteController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
