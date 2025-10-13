using System;
using System.Collections.Generic;
using System.ComponentModel;
using SewingProduction.Models;
using SewingProduction.Interfaces;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Operations
{
    public sealed class SecondsAggregator
    {
        private readonly BindingList<NormRasz> _normRaszList;
        private readonly BindingSource _annBindingSource;
        private readonly ILogger _logger;
        private readonly Action<Action> _uiDispatch;

        public SecondsAggregator(BindingList<NormRasz> normRaszList,
                                 BindingSource annBindingSource,
                                 ILogger logger,
                                 Action<Action> uiDispatch)
        {
            _normRaszList = normRaszList;
            _annBindingSource = annBindingSource;
            _logger = logger;
            _uiDispatch = uiDispatch;
        }

        public void RecalculateAndBind()
        {
            try
            {
                var ann = _annBindingSource?.DataSource as ArtNormN;
                if (ann == null) return;

                int sekVyazo = 0, sekVyaz5 = 0, sekVyaz12 = 0, sekVyaz7 = 0, sekVyaz10 = 0, sekVyaz6 = 0, sekVyaz3 = 0;
                int sekVyaz14 = 0, sekVyaz70 = 0, sekVyaz71 = 0, sekVyaz72 = 0, sekVyaz62 = 0, sekVyaz57 = 0, sekVyaz18 = 0;
                int sekVyazAll = 0, sekShv = 0, sekKr = 0, sekTotal = 0, sebTotal = 0;

                if (_normRaszList != null)
                {
                    foreach (var r in _normRaszList)
                    {
                        if (r.N1 >= 100) continue;
                        sekTotal += r.Sek;
                        sebTotal += (int)r.Seb;

                        if (r.KodPodr == 7) sekKr += r.Sek;
                        if (r.KodPodr == 1 || r.KodPodr == 6) sekVyazAll += r.Sek; else sekShv += r.Sek;

                        switch (r.KodOb)
                        {
                            case 28: sekVyazo += r.Sek; break;
                            case 25: sekVyaz5 += r.Sek; break;
                            case 35: sekVyaz12 += r.Sek; break;
                            case 26: sekVyaz7 += r.Sek; break;
                            case 37: sekVyaz10 += r.Sek; break;
                            case 38: sekVyaz6 += r.Sek; break;
                            case 29: sekVyaz3 += r.Sek; break;
                            case 62: sekVyaz14 += r.Sek; break;
                            case 55: sekVyaz70 += r.Sek; break;
                            case 59: sekVyaz71 += r.Sek; break;
                            case 73: sekVyaz72 += r.Sek; break;
                            case 60: sekVyaz62 += r.Sek; break;
                            case 114: sekVyaz57 += r.Sek; break;
                            case 115: sekVyaz18 += r.Sek; break;
                        }
                    }
                }

                bool changed = false;

                changed |= SetIfChanged(() => ann.SekVyazo, v => ann.SekVyazo = v, sekVyazo);
                changed |= SetIfChanged(() => ann.SekVyaz5, v => ann.SekVyaz5 = v, sekVyaz5);
                changed |= SetIfChanged(() => ann.SekVyaz12, v => ann.SekVyaz12 = v, sekVyaz12);
                changed |= SetIfChanged(() => ann.SekVyaz7, v => ann.SekVyaz7 = v, sekVyaz7);
                changed |= SetIfChanged(() => ann.SekVyaz10, v => ann.SekVyaz10 = v, sekVyaz10);
                changed |= SetIfChanged(() => ann.SekVyaz6, v => ann.SekVyaz6 = v, sekVyaz6);
                changed |= SetIfChanged(() => ann.SekVyaz3, v => ann.SekVyaz3 = v, sekVyaz3);
                changed |= SetIfChanged(() => ann.SekKr, v => ann.SekKr = v, sekKr);
                changed |= SetIfChanged(() => ann.Sek, v => ann.Sek = v, sekTotal);
                changed |= SetIfChanged(() => (int)ann.Seb, v => ann.Seb = v, sebTotal);
                changed |= SetIfChanged(() => ann.SekVyaz14, v => ann.SekVyaz14 = v, sekVyaz14);
                changed |= SetIfChanged(() => ann.SekVyaz70, v => ann.SekVyaz70 = v, sekVyaz70);
                changed |= SetIfChanged(() => ann.SekVyaz71, v => ann.SekVyaz71 = v, sekVyaz71);
                changed |= SetIfChanged(() => ann.SekVyaz72, v => ann.SekVyaz72 = v, sekVyaz72);
                changed |= SetIfChanged(() => ann.SekVyaz62, v => ann.SekVyaz62 = v, sekVyaz62);
                changed |= SetIfChanged(() => ann.SekVyaz57, v => ann.SekVyaz57 = v, sekVyaz57);
                changed |= SetIfChanged(() => ann.SekVyaz18, v => ann.SekVyaz18 = v, sekVyaz18);
                changed |= SetIfChanged(() => ann.SekVyaz, v => ann.SekVyaz = v, sekVyazAll);

                // SekShv depends on SekVyaz
                int newSekShv = ann.SekVyaz == 0 ? ann.Sek : sekShv;
                changed |= SetIfChanged(() => ann.SekShv, v => ann.SekShv = v, newSekShv);

                if (changed)
                {
                    _uiDispatch?.Invoke(() => _annBindingSource.ResetBindings(false));
                }
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, nameof(SecondsAggregator));
            }
        }

        private static bool SetIfChanged(Func<int> getter, Action<int> setter, int newValue)
        {
            int current = getter();
            if (current != newValue)
            {
                setter(newValue);
                return true;
            }
            return false;
        }
    }
}



