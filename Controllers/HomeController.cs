using Barachi.Models;
using Barachi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Barachi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILookupService _lookupService;
        private readonly IDeleteService _deleteService;
        private readonly IKilldownService _killdownService;
        private readonly IRetapingService _retapingService;

        public HomeController(
            ILookupService lookupService,
            IDeleteService deleteService,
            IKilldownService killdownService,
            IRetapingService retapingService)
        {
            _lookupService = lookupService;
            _deleteService = deleteService;
            _killdownService = killdownService;
            _retapingService = retapingService;
        }

        public IActionResult Scan()
        {
            return View();
        }

        // GET: /Home/Delete?rbId=xxxx
        [HttpGet]
        public IActionResult Delete(string rbId)
        {
            var lookup = _lookupService.GetByRBID(rbId);

            if (lookup == null)
            {
                TempData["Error"] = "RBID not found.";
                return RedirectToAction("Scan");
            }

            var model = new DeleteViewModel
            {
                RBID = lookup.RBID,
                LotNumber = lookup.LotNumber,
                Type = lookup.Type
            };

            return View(model);
        }

        // [HttpGet]
        // public IActionResult Delete(string? rbId)
        // {
        //     if (string.IsNullOrWhiteSpace(rbId))
        //     {
        //         return RedirectToAction("Scan");
        //     }

        //     var lookup = _lookupService.GetByRBID(rbId);

        //     if (lookup == null)
        //     {
        //         TempData["Error"] = "RBID not found.";
        //         return RedirectToAction("Scan");
        //     }

        //     var model = new DeleteViewModel
        //     {
        //         RBID = lookup.RBID,
        //         LotNumber = lookup.LotNumber,
        //         Type = lookup.Type
        //     };

        //     return View(model);
        // }

        // POST: /Home/Delete
        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _deleteService.LogDelete(model);

            if (string.Equals(model.Type, "15mg", System.StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Killdown", new { rbId = model.RBID, lotNumber = model.LotNumber, type = model.Type });
            }

            return RedirectToAction("Retaping", new { rbId = model.RBID, lotNumber = model.LotNumber, type = model.Type });
        }

        // GET: /Home/Killdown?rbId=xxxx&lotNumber=xxxx&type=xxxx
        [HttpGet]
        public IActionResult Killdown(string rbId, string lotNumber, string type)
        {
            var instructions = _killdownService.GetInstructions(rbId, lotNumber, type);

            var model = new KilldownViewModel
            {
                RBID = rbId,
                LotNumber = lotNumber,
                Type = type,
                Instructions = instructions
            };

            return View(model);
        }

        // POST: /Home/Killdown  (user clicks "I've Completed the Killdown")
        [HttpPost]
        public IActionResult Killdown(KilldownViewModel model)
        {
            _killdownService.Acknowledge(model);

            TempData["Success"] = $"Killdown acknowledged for RBID {model.RBID}.";
            return RedirectToAction("Scan");
        }

        // GET: /Home/Retaping?rbId=xxxx&lotNumber=xxxx&type=xxxx
        [HttpGet]
        public IActionResult Retaping(string rbId, string lotNumber, string type)
        {
            var instructions = _retapingService.GetInstructions(rbId, lotNumber, type);

            var model = new RetapingViewModel
            {
                RBID = rbId,
                LotNumber = lotNumber,
                Type = type,
                Instructions = instructions
            };

            return View(model);
        }

        // POST: /Home/Retaping  (user clicks "I've Completed the Retaping")
        [HttpPost]
        public IActionResult Retaping(RetapingViewModel model)
        {
            _retapingService.Acknowledge(model);

            TempData["Success"] = $"Retaping acknowledged for RBID {model.RBID}.";
            return RedirectToAction("Scan");
        }
    }
}