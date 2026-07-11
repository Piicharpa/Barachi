using System;
using System.Linq;
using Barachi.Data;
using Barachi.Models;

namespace Barachi.Services
{
    // ---------- Lookup Service (scan -> DB check for lots/type) ----------
    public interface ILookupService
    {
        BarachiEntity GetByRBID(string rbId);
    }

    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _context;

        public LookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BarachiEntity GetByRBID(string rbId)
        {
            return _context.Barachi.FirstOrDefault(b => b.RBID == rbId);
        }
    }

    // ---------- Delete Service ----------
    public interface IDeleteService
    {
        void LogDelete(DeleteViewModel model);
    }

    public class DeleteService : IDeleteService
    {
        private readonly ApplicationDbContext _context;

        public DeleteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void LogDelete(DeleteViewModel model)
        {
            var nextStep = string.Equals(model.Type, "15mg", StringComparison.OrdinalIgnoreCase)
                ? "Killdown"
                : "Retaping";

            var log = new BarachiDeleteLog
            {
                RBID = model.RBID,
                PIC = model.PIC,
                Reasons = model.Reasons,
                DeletedDate = DateTime.Now,
                NextStep = nextStep
            };

            _context.BarachiDeleteLogs.Add(log);

            var record = _context.Barachi.FirstOrDefault(b => b.RBID == model.RBID);
            if (record != null)
            {
                record.Status = "Deleted";
                record.ModifiedDate = DateTime.Now;
            }

            _context.SaveChanges();
        }
    }

    // ---------- Killdown Service ----------
    public interface IKilldownService
    {
        string GetInstructions(string rbId, string lotNumber, string type);
        void Acknowledge(KilldownViewModel model);
    }

    public class KilldownService : IKilldownService
    {
        private readonly ApplicationDbContext _context;

        public KilldownService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetInstructions(string rbId, string lotNumber, string type)
        {
            // TODO: replace with real business rule / lookup table for killdown locations.
            return $"Killdown all chips for Lot {lotNumber} (RBID: {rbId}) at the designated 15mg killdown station. " +
                   "Verify the chip count matches the lot record before disposal.";
        }

        public void Acknowledge(KilldownViewModel model)
        {
            var record = new KilldownRecord
            {
                RBID = model.RBID,
                LotNumber = model.LotNumber,
                Instructions = model.Instructions,
                AcknowledgedDate = DateTime.Now,
                Status = "Acknowledged"
            };

            _context.KilldownRecords.Add(record);

            var barachi = _context.Barachi.FirstOrDefault(b => b.RBID == model.RBID);
            if (barachi != null)
            {
                barachi.Status = "KilldownCompleted";
                barachi.ModifiedDate = DateTime.Now;
            }

            _context.SaveChanges();
        }
    }

    // ---------- Retaping Service ----------
    public interface IRetapingService
    {
        string GetInstructions(string rbId, string lotNumber, string type);
        void Acknowledge(RetapingViewModel model);
    }

    public class RetapingService : IRetapingService
    {
        private readonly ApplicationDbContext _context;

        public RetapingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetInstructions(string rbId, string lotNumber, string type)
        {
            // TODO: replace with real business rule / lookup table for retaping steps.
            return $"Retape Lot {lotNumber} (RBID: {rbId}). Remove the existing tape, apply new tape, " +
                   "and label with a new lot number before returning to inventory.";
        }

        public void Acknowledge(RetapingViewModel model)
        {
            var record = new RetapingRecord
            {
                RBID = model.RBID,
                LotNumber = model.LotNumber,
                Instructions = model.Instructions,
                AcknowledgedDate = DateTime.Now,
                Status = "Acknowledged"
            };

            _context.RetapingRecords.Add(record);

            var barachi = _context.Barachi.FirstOrDefault(b => b.RBID == model.RBID);
            if (barachi != null)
            {
                barachi.Status = "RetapingCompleted";
                barachi.ModifiedDate = DateTime.Now;
            }

            _context.SaveChanges();
        }
    }
}