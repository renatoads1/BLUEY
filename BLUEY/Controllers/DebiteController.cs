using BLUEY.Models;
using BLUEY.Models.Repositories;
using BLUEY.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BLUEY.Controllers
{
    public class DebiteController : BaseController
    {
        private readonly IDebiteRepository _debiteRepository;
        private readonly IDebiteService _debiteService;

        public DebiteController(ILogger<BaseController> logger, IDebiteRepository debiteRepository, IDebiteService debiteService):base(logger)
        {
            _debiteRepository = debiteRepository;
            _debiteService = debiteService;
        }


        // GET: DebiteController
        public ActionResult Index()
        {
            List<LCTOFISConsServ> debit = new List<LCTOFISConsServ>();
            debit = _debiteService.GetDebit();
            //debit = _debiteRepository.Get();
            return View(debit);
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
