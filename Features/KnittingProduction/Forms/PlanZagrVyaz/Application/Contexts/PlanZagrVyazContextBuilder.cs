using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.KnittingProduction.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms.PZVForm.Application.Contexts
{
    public sealed class PlanZagrVyazContextBuilder
    {
        private readonly int _vyazPodrKod;
        private readonly BindingSource _pZVBindingSource;
        private readonly BindingSource _pachBindingSource;
        private readonly BindingSource _smenBindingSource;
        private readonly GridView _pzvGrid;
        private readonly GridView _pachGrid;
        private readonly GridView _smenGrid;

        public PlanZagrVyazContextBuilder(
            int vyazPodrKod,
            BindingSource pzvBindingSource,
            BindingSource pachBindingSource,
            BindingSource smenBindingSource,
            GridView pzvGrid,
            GridView pachGrid,
            GridView smenGrid)
        {
            //_vyazPodrKod = vyazPodrKod;
            //_pZVBindingSource = pzvBindingSource;
            //_pachBindingSource = pachBindingSource;
            //_smenBindingSource = smenBindingSource;
            //_pzvGrid = pzvGrid;
            //_pachGrid = pachGrid;
            //_smenGrid = smenGrid;
            _vyazPodrKod = vyazPodrKod;
            _pZVBindingSource = pzvBindingSource ?? throw new ArgumentNullException(nameof(pzvBindingSource));
            _pachBindingSource = pachBindingSource ?? throw new ArgumentNullException(nameof(pachBindingSource));
            _smenBindingSource = smenBindingSource ?? throw new ArgumentNullException(nameof(smenBindingSource));
            _pzvGrid = pzvGrid ?? throw new ArgumentNullException(nameof(pzvGrid));
            _pachGrid = pachGrid ?? throw new ArgumentNullException(nameof(pachGrid));
            _smenGrid = smenGrid ?? throw new ArgumentNullException(nameof(smenGrid));
        }
        public PzvSelectionContext BuildForPachSelection()
        {
            var selectedPachList = _pachBindingSource.List
                .OfType<RzvPachListByNom>()
                .Where(x => x.SyncSelection == 1)
                .ToList();

            return new PzvSelectionContext
            {
                VyazPodrKod = _vyazPodrKod,
                CurrentColumn = _pachGrid.FocusedColumn?.FieldName ?? string.Empty,
                SelectedPachList = selectedPachList
            };
        }

        public PzvSelectionContext BuildForPzvActions()
        {
            var current = _pZVBindingSource.Current as PZVOperList
                ?? _pZVBindingSource.List.OfType<PZVOperList>().FirstOrDefault();

            var selectedOperations = _pZVBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                .ToList();

            return new PzvSelectionContext
            {
                VyazPodrKod = _vyazPodrKod,
                CurrentPzvId = current?.olPzvID ?? 0,
                CurrentColumn = _pzvGrid.FocusedColumn?.FieldName ?? string.Empty,
                SelectedOperations = selectedOperations
            };
        }

        public PzvSelectionContext BuildForShiftActions()
        {
            //var shift = _smenBindingSource.Current as SmenZadanyVyaz
            //    ?? _smenBindingSource.List.OfType<SmenZadanyVyaz>().FirstOrDefault();

            //return new PzvSelectionContext
            //{
            //    VyazPodrKod = _vyazPodrKod,
            //    CurrentShiftAssignment = shift,
            //    CurrentColumn = _smenGrid.FocusedColumn?.FieldName ?? string.Empty
            //};
            var shift = _smenBindingSource.Current as SmenZadanyVyaz
                ?? _smenBindingSource.List.OfType<SmenZadanyVyaz>().FirstOrDefault();

            Debug.WriteLine($"BuildForShiftActions: shift is null = {shift == null}");

            return new PzvSelectionContext
            {
                VyazPodrKod = _vyazPodrKod,
                CurrentShiftAssignment = shift,
                CurrentColumn = _smenGrid.FocusedColumn?.FieldName ?? string.Empty
            };
        }

        public PzvSelectionContext BuildForPzvActionsWithShift()
        {
            //var pzvContext = BuildForPzvActions();
            //var shiftContext = BuildForShiftActions();

            //Debug.WriteLine($"BuildForPzvActionsWithShift: shiftContext.CurrentShiftAssignment is null = {shiftContext.CurrentShiftAssignment == null}");

            //return new PzvSelectionContext
            //{
            //    VyazPodrKod = _vyazPodrKod,
            //    CurrentPzvId = pzvContext.CurrentPzvId,
            //    CurrentColumn = pzvContext.CurrentColumn,
            //    SelectedOperations = pzvContext.SelectedOperations,
            //    SelectedPachList = pzvContext.SelectedPachList,
            //    CurrentShiftAssignment = shiftContext.CurrentShiftAssignment
            //};
            var current = _pZVBindingSource.Current as PZVOperList
                ?? _pZVBindingSource.List.OfType<PZVOperList>().FirstOrDefault();

            var selectedOperations = _pZVBindingSource.List
                .OfType<PZVOperList>()
                .Where(x => x.SyncSelection == 1)
                .ToList();

            var shift = _smenBindingSource.Current as SmenZadanyVyaz
                ?? _smenBindingSource.List.OfType<SmenZadanyVyaz>().FirstOrDefault();

            return new PzvSelectionContext
            {
                VyazPodrKod = _vyazPodrKod,
                CurrentPzvId = current?.olPzvID ?? 0,
                CurrentColumn = _pzvGrid.FocusedColumn?.FieldName ?? string.Empty,
                SelectedOperations = selectedOperations,
                CurrentShiftAssignment = shift
            };
        }
        //public PzvSelectionContext Build()
        //{
        //    var current = _pZVBindingSource.Current as PZVOperList;
        //    var shift = _smenBindingSource.Current as SmenZadanyVyaz;

        //    var selectedOperations = _pZVBindingSource.List
        //        .OfType<PZVOperList>()
        //        .Where(x => x.SyncSelection == 1)
        //        .ToList();

        //    var selectedPachList = _pachBindingSource.List
        //        .OfType<RzvPachListByNom>()
        //        .Where(x => x.SyncSelection == 1)
        //        .ToList();

        //    return new PzvSelectionContext
        //    {
        //        VyazPodrKod = _vyazPodrKod,
        //        CurrentPzvId = current?.olPzvID ?? 0,
        //        CurrentColumn = _pzvGrid.FocusedColumn?.FieldName ?? string.Empty,
        //        SelectedOperations = selectedOperations,
        //        SelectedPachList = selectedPachList,
        //        CurrentShiftAssignment = shift
        //    };
        //}
    }
}