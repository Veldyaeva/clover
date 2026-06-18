using System;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models
{
    public class ShiftHistoryModel
    {
        public int KwsID { get; set; }
        public int TabStart { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public bool IsOpen => DateEnd == null;

        public string DisplayText =>
            DateStart.HasValue
                ? $"#{KwsID}  {DateStart:dd.MM HH:mm} – {(DateEnd.HasValue ? DateEnd.Value.ToString("HH:mm dd.MM") : "открыта")}"
                : $"#{KwsID}";
    }
}
