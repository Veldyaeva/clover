using System.Drawing;
using DevExpress.XtraBars;

namespace SewingProduction
{
    partial class SpMainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpMainForm));
            popupMenu1 = new PopupMenu(components);
            barManager1 = new SewingProduction.Core.Class.CustomControls.CustomBarManager(components);
            bar1 = new Bar();
            barSubMenu = new BarSubItem();
            barBtnProfile = new BarButtonItem();
            barBtnSettings = new BarButtonItem();
            barBtnAbout = new BarButtonItem();
            barBtnHelp = new BarButtonItem();
            barSubSpr = new BarSubItem();
            barSubEquipment = new BarSubItem();
            barBtnEqInTeams = new BarButtonItem();
            barBtnEqDirectory = new BarButtonItem();
            barBtnEqTypes = new BarButtonItem();
            barBtnClassMatrix = new BarButtonItem();
            barBtnOperationTypes = new BarButtonItem();
            barSubTeamsShop = new BarSubItem();
            barBtnTeams = new BarButtonItem();
            barBtnShops = new BarButtonItem();
            barBtnProdTypes = new BarButtonItem();
            barButtonItemBrigObject = new BarButtonItem();
            barBtnCalcCard1 = new BarButtonItem();
            barBtnWorkers = new BarButtonItem();
            barBtnTariffs = new BarButtonItem();
            barBtnNacenki = new BarButtonItem();
            barBtnModelsMark = new BarButtonItem();
            barSubDefects = new BarSubItem();
            barBtnSockDefects = new BarButtonItem();
            barSubProduction = new BarSubItem();
            barSubKnitting = new BarSubItem();
            barBtnOperPlan = new BarButtonItem();
            barBtnMasterDesk1 = new BarButtonItem();
            barButtonItemSteamMasterWorkTable = new BarButtonItem();
            barButtonItemCutMasterWorkTable = new BarButtonItem();
            barBtnKnitterDesk = new BarButtonItem();
            barBtnAnalytics = new BarButtonItem();
            barSubSewing = new BarSubItem();
            barBtnMasterDesk = new BarButtonItem();
            barBtnCutShop = new BarButtonItem();
            barBtnTeamWork = new BarButtonItem();
            barBtnArticle = new BarButtonItem();
            barBtnCalcCard = new BarButtonItem();
            barBtnTimesheet = new BarButtonItem();
            barButtonItemScreen = new BarButtonItem();
            barBtnAt = new BarButtonItem();
            barBtnQuestion = new BarButtonItem();
            skinBarSubItem2 = new SkinBarSubItem();
            skinDropDownButtonItem2 = new SkinDropDownButtonItem();
            skinPaletteDropDownButtonItem2 = new SkinPaletteDropDownButtonItem();
            barDockControlTop = new BarDockControl();
            barDockControlBottom = new BarDockControl();
            barDockControlLeft = new BarDockControl();
            barDockControlRight = new BarDockControl();
            skinBarSubItem1 = new SkinBarSubItem();
            skinDropDownButtonItem1 = new SkinDropDownButtonItem();
            skinPaletteDropDownButtonItem1 = new SkinPaletteDropDownButtonItem();
            xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(components);
            miniToolStrip = new System.Windows.Forms.MenuStrip();
            barButtonItemExchangeApp = new BarButtonItem();
            barSubReports = new BarSubItem();
            barBtnPublicArticul = new BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)popupMenu1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).BeginInit();
            SuspendLayout();
            // 
            // popupMenu1
            // 
            popupMenu1.Manager = barManager1;
            popupMenu1.Name = "popupMenu1";
            // 
            // barManager1
            // 
            barManager1.Bars.AddRange(new Bar[] { bar1 });
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.HideIfNoRight = true;
            barManager1.Items.AddRange(new BarItem[] { skinBarSubItem1, skinDropDownButtonItem1, skinPaletteDropDownButtonItem1, skinBarSubItem2, skinDropDownButtonItem2, skinPaletteDropDownButtonItem2, barSubMenu, barBtnProfile, barBtnSettings, barBtnAbout, barBtnHelp, barSubSpr, barSubEquipment, barBtnEqInTeams, barBtnEqDirectory, barBtnEqTypes, barBtnClassMatrix, barBtnOperationTypes, barSubTeamsShop, barBtnTeams, barBtnShops, barBtnProdTypes, barBtnCalcCard1, barBtnWorkers, barBtnTariffs, barBtnNacenki, barBtnModelsMark, barSubDefects, barBtnSockDefects, barSubProduction, barSubKnitting, barBtnOperPlan, barBtnMasterDesk1, barBtnKnitterDesk, barBtnAnalytics, barSubSewing, barBtnMasterDesk, barBtnCutShop, barBtnTeamWork, barBtnArticle, barBtnCalcCard, barBtnTimesheet, barBtnQuestion, barBtnAt, barButtonItemSteamMasterWorkTable, barButtonItemCutMasterWorkTable, barButtonItemScreen, barSubReports, barBtnPublicArticul, barButtonItemBrigObject, barButtonItemExchangeApp });
            barManager1.MaxItemId = 51;
            barManager1.SkipDevExpressSkinItems = true;
            // 
            // bar1
            // 
            bar1.BarName = "Main menu";
            bar1.DockCol = 0;
            bar1.DockRow = 0;
            bar1.DockStyle = BarDockStyle.Top;
            bar1.FloatLocation = new Point(834, 136);
            bar1.FloatSize = new Size(46, 100);
            bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barSubMenu), new LinkPersistInfo(barSubSpr), new LinkPersistInfo(barSubProduction), new LinkPersistInfo(barBtnTeamWork), new LinkPersistInfo(barBtnArticle), new LinkPersistInfo(barBtnCalcCard), new LinkPersistInfo(barBtnTimesheet), new LinkPersistInfo(barButtonItemScreen), new LinkPersistInfo(barBtnAt), new LinkPersistInfo(barBtnQuestion), new LinkPersistInfo(skinBarSubItem2), new LinkPersistInfo(skinDropDownButtonItem2), new LinkPersistInfo(skinPaletteDropDownButtonItem2), new LinkPersistInfo(barSubReports), new LinkPersistInfo(barButtonItemExchangeApp) });
            bar1.OptionsBar.AllowCollapse = true;
            bar1.OptionsBar.AllowQuickCustomization = false;
            bar1.OptionsBar.DisableClose = true;
            bar1.OptionsBar.DistanceBetweenItems = 3;
            bar1.OptionsBar.ExpandAnimationDuration = 1;
            bar1.OptionsBar.MultiLine = true;
            bar1.OptionsBar.UseWholeRow = true;
            bar1.Text = "Main menu";
            //// 
            //// barManager1_1
            //// 
            //barManager1.Bars.AddRange(new Bar[] { bar1 });
            //barManager1.DockControls.Add(barDockControlTop);
            //barManager1.DockControls.Add(barDockControlBottom);
            //barManager1.DockControls.Add(barDockControlLeft);
            //barManager1.DockControls.Add(barDockControlRight);
            //barManager1.Form = this;
            //barManager1.HideIfNoRight = true;
            //barManager1.Items.AddRange(new BarItem[] { skinBarSubItem1, skinDropDownButtonItem1, skinPaletteDropDownButtonItem1, skinBarSubItem2, skinDropDownButtonItem2, skinPaletteDropDownButtonItem2, barSubMenu, barBtnProfile, barBtnSettings, barBtnAbout, barBtnHelp, barSubSpr, barSubEquipment, barBtnEqInTeams, barBtnEqDirectory, barBtnEqTypes, barBtnClassMatrix, barBtnOperationTypes, barSubTeamsShop, barBtnTeams, barBtnShops, barBtnProdTypes, barBtnCalcCard1, barBtnWorkers, barBtnTariffs, barBtnNacenki, barBtnModelsMark, barSubDefects, barBtnSockDefects, barSubProduction, barSubKnitting, barBtnOperPlan, barBtnMasterDesk1, barBtnKnitterDesk, barBtnAnalytics, barSubSewing, barBtnMasterDesk, barBtnCutShop, barBtnTeamWork, barBtnArticle, barBtnCalcCard, barBtnTimesheet, barBtnQuestion, barBtnAt, barButtonItemSteamMasterWorkTable, barButtonItemCutMasterWorkTable, barButtonItemScreen, barSubReports, barBtnPublicArticul });
            //barManager1.MaxItemId = 51;
            //barManager1.SkipDevExpressSkinItems = true;
			//// 
   //         // bar1_1
   //         // 
   //         bar1.BarName = "Main menu";
   //         bar1.DockCol = 0;
   //         bar1.DockRow = 0;
   //         bar1.DockStyle = BarDockStyle.Top;
   //         bar1.FloatLocation = new Point(834, 136);
   //         bar1.FloatSize = new Size(46, 100);
   //         bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barSubMenu), new LinkPersistInfo(barSubSpr), new LinkPersistInfo(barSubProduction), new LinkPersistInfo(barBtnTeamWork), new LinkPersistInfo(barBtnArticle), new LinkPersistInfo(barBtnCalcCard), new LinkPersistInfo(barBtnTimesheet), new LinkPersistInfo(barButtonItemScreen), new LinkPersistInfo(barBtnAt), new LinkPersistInfo(barBtnQuestion), new LinkPersistInfo(skinBarSubItem2), new LinkPersistInfo(skinDropDownButtonItem2), new LinkPersistInfo(skinPaletteDropDownButtonItem2), new LinkPersistInfo(barSubReports) });
   //         bar1.OptionsBar.AllowCollapse = true;
   //         bar1.OptionsBar.AllowQuickCustomization = false;
   //         bar1.OptionsBar.DisableClose = true;
   //         bar1.OptionsBar.DistanceBetweenItems = 3;
   //         bar1.OptionsBar.ExpandAnimationDuration = 1;
   //         bar1.OptionsBar.MultiLine = true;
   //         bar1.OptionsBar.UseWholeRow = true;
   //         bar1.Text = "Main menu";
            // 
            // barSubMenu
            // 
            barSubMenu.Caption = "Меню";
            barSubMenu.Id = 6;
            barSubMenu.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnProfile), new LinkPersistInfo(barBtnSettings), new LinkPersistInfo(barBtnAbout), new LinkPersistInfo(barBtnHelp) });
            barSubMenu.Name = "barSubMenu";
            barSubMenu.Tag = "МенюToolStripMenuItem";
            // 
            // barBtnProfile
            // 
            barBtnProfile.Caption = "Профиль";
            barBtnProfile.Id = 7;
            barBtnProfile.Name = "barBtnProfile";
            barBtnProfile.Tag = "профильToolStripMenuItem";
            barBtnProfile.ItemClick += профильToolStripMenuItem_Click;
            // 
            // barBtnSettings
            // 
            barBtnSettings.Caption = "Настройки";
            barBtnSettings.Id = 8;
            barBtnSettings.Name = "barBtnSettings";
            barBtnSettings.Tag = "настройкиToolStripMenuItem";
            barBtnSettings.ItemClick += настройкиToolStripMenuItem_Click;
            // 
            // barBtnAbout
            // 
            barBtnAbout.Caption = "О программе";
            barBtnAbout.Id = 9;
            barBtnAbout.Name = "barBtnAbout";
            barBtnAbout.Tag = "оПрограммеToolStripMenuItem";
            barBtnAbout.ItemClick += оПрограммеToolStripMenuItem_Click;
            // 
            // barBtnHelp
            // 
            barBtnHelp.Caption = "Помощь";
            barBtnHelp.Id = 10;
            barBtnHelp.Name = "barBtnHelp";
            barBtnHelp.Tag = "помощьToolStripMenuItem";
            barBtnHelp.ItemClick += помощьToolStripMenuItem_Click;
            // 
            // barSubSpr
            // 
            barSubSpr.Caption = "Справочники";
            barSubSpr.Id = 11;
            barSubSpr.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barSubEquipment), new LinkPersistInfo(barSubTeamsShop), new LinkPersistInfo(barBtnCalcCard1), new LinkPersistInfo(barBtnWorkers), new LinkPersistInfo(barBtnTariffs), new LinkPersistInfo(barBtnNacenki), new LinkPersistInfo(barBtnModelsMark), new LinkPersistInfo(barSubDefects) });
            barSubSpr.Name = "barSubSpr";
            barSubSpr.Tag = "справочникиToolStripMenuItem";
            // 
            // barSubEquipment
            // 
            barSubEquipment.Caption = "Оборудование";
            barSubEquipment.Id = 12;
            barSubEquipment.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnEqInTeams), new LinkPersistInfo(barBtnEqDirectory), new LinkPersistInfo(barBtnEqTypes), new LinkPersistInfo(barBtnClassMatrix), new LinkPersistInfo(barBtnOperationTypes) });
            barSubEquipment.Name = "barSubEquipment";
            barSubEquipment.Tag = "оборудованиеToolStripMenuItem";
            // 
            // barBtnEqInTeams
            // 
            barBtnEqInTeams.Caption = "Оборудование в бригадах";
            barBtnEqInTeams.Id = 13;
            barBtnEqInTeams.Name = "barBtnEqInTeams";
            barBtnEqInTeams.Tag = "оборудованиеВБригадахToolStripMenuItem";
            barBtnEqInTeams.ItemClick += оборудованиеВБригадахToolStripMenuItem_Click;
            // 
            // barBtnEqDirectory
            // 
            barBtnEqDirectory.Caption = "Справочник Оборудования";
            barBtnEqDirectory.Id = 14;
            barBtnEqDirectory.Name = "barBtnEqDirectory";
            barBtnEqDirectory.Tag = "оборудованиеToolStripMenuItem1";
            barBtnEqDirectory.ItemClick += оборудованиеToolStripMenuItem_Click;
            // 
            // barBtnEqTypes
            // 
            barBtnEqTypes.Caption = "Виды оборудования";
            barBtnEqTypes.Id = 15;
            barBtnEqTypes.Name = "barBtnEqTypes";
            barBtnEqTypes.Tag = "видыОборудованияToolStripMenuItem";
            barBtnEqTypes.ItemClick += видыОборудованияToolStripMenuItem_Click;
            // 
            // barBtnClassMatrix
            // 
            barBtnClassMatrix.Caption = "Матрицы классов";
            barBtnClassMatrix.Id = 16;
            barBtnClassMatrix.Name = "barBtnClassMatrix";
            barBtnClassMatrix.Tag = "матрицыКлассовToolStripMenuItem";
            barBtnClassMatrix.ItemClick += матрицаКлассовToolStripMenuItem_Click;
            // 
            // barBtnOperationTypes
            // 
            barBtnOperationTypes.Caption = "Виды операций";
            barBtnOperationTypes.Id = 17;
            barBtnOperationTypes.Name = "barBtnOperationTypes";
            barBtnOperationTypes.Tag = "видыОперацийToolStripMenuItem";
            barBtnOperationTypes.ItemClick += видОперацToolStripMenuItem_Click;
            // 
            // barSubTeamsShop
            // 
            barSubTeamsShop.Caption = "Бригады/Цеха";
            barSubTeamsShop.Id = 18;
            barSubTeamsShop.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnTeams), new LinkPersistInfo(barBtnShops), new LinkPersistInfo(barBtnProdTypes), new LinkPersistInfo(barButtonItemBrigObject) });
            barSubTeamsShop.Name = "barSubTeamsShop";
            barSubTeamsShop.Tag = "бригадыЦехаToolStripMenuItem";
            // 
            // barBtnTeams
            // 
            barBtnTeams.Caption = "Бригады";
            barBtnTeams.Id = 19;
            barBtnTeams.Name = "barBtnTeams";
            barBtnTeams.Tag = "бригадыToolStripMenuItem";
            barBtnTeams.ItemClick += бригадыToolStripMenuItem_Click;
            // 
            // barBtnShops
            // 
            barBtnShops.Caption = "Цеха";
            barBtnShops.Id = 20;
            barBtnShops.Name = "barBtnShops";
            barBtnShops.Tag = "цехаToolStripMenuItem";
            barBtnShops.ItemClick += цехаToolStripMenuItem1_Click;
            // 
            // barBtnProdTypes
            // 
            barBtnProdTypes.Caption = "Виды производств";
            barBtnProdTypes.Id = 21;
            barBtnProdTypes.Name = "barBtnProdTypes";
            barBtnProdTypes.Tag = "видыПроизводствToolStripMenuItem";
            barBtnProdTypes.ItemClick += видыПроизводстваToolStripMenuItem_Click;
            // 
            // barButtonItemBrigObject
            // 
            barButtonItemBrigObject.Caption = "Подразделения бригад";
            barButtonItemBrigObject.Id = 48;
            barButtonItemBrigObject.Name = "barButtonItemBrigObject";
            barButtonItemBrigObject.ItemClick += barButtonItemBrigObject_ItemClick;
            // 
            // barBtnCalcCard1
            // 
            barBtnCalcCard1.Caption = "Карточка расчета";
            barBtnCalcCard1.Id = 22;
            barBtnCalcCard1.Name = "barBtnCalcCard1";
            barBtnCalcCard1.Tag = "карточкаРасчетаToolStripMenuItem1";
            barBtnCalcCard1.ItemClick += карточкаРасчетаToolStripMenuItem1_Click;
            // 
            // barBtnWorkers
            // 
            barBtnWorkers.Caption = "Работники";
            barBtnWorkers.Id = 23;
            barBtnWorkers.Name = "barBtnWorkers";
            barBtnWorkers.Tag = "работникиToolStripMenuItem";
            barBtnWorkers.ItemClick += работникиToolStripMenuItem_Click;
            // 
            // barBtnTariffs
            // 
            barBtnTariffs.Caption = "Тарифы/Константы";
            barBtnTariffs.Id = 24;
            barBtnTariffs.Name = "barBtnTariffs";
            barBtnTariffs.Tag = "тарифыToolStripMenuItem";
            barBtnTariffs.ItemClick += тарифыToolStripMenuItem_Click;
            // 
            // barBtnNacenki
            // 
            barBtnNacenki.Caption = "Коэф. наценки";
            barBtnNacenki.Id = 25;
            barBtnNacenki.Name = "barBtnNacenki";
            barBtnNacenki.Tag = "наценкиToolStripMenuItem";
            barBtnNacenki.ItemClick += наценкиToolStripMenuItem_Click;
            // 
            // barBtnModelsMark
            // 
            barBtnModelsMark.Caption = "Модели с признаком маркировки";
            barBtnModelsMark.Id = 26;
            barBtnModelsMark.Name = "barBtnModelsMark";
            barBtnModelsMark.Tag = "моделиСПризнакомМаркировкToolStripMenuItem";
            barBtnModelsMark.ItemClick += моделиСПризнакомМаркировкToolStripMenuItem_Click;
            // 
            // barSubDefects
            // 
            barSubDefects.Caption = "Виды браков";
            barSubDefects.Id = 27;
            barSubDefects.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnSockDefects) });
            barSubDefects.Name = "barSubDefects";
            barSubDefects.Tag = "видыБраковПряжиToolStripMenuItem";
            // 
            // barBtnSockDefects
            // 
            barBtnSockDefects.Caption = "Виды браков носков";
            barBtnSockDefects.Id = 28;
            barBtnSockDefects.Name = "barBtnSockDefects";
            barBtnSockDefects.Tag = "видыБраковНосковToolStripMenuItem";
            barBtnSockDefects.ItemClick += видыБраковНосковToolStripMenuItem_Click;
            // 
            // barSubProduction
            // 
            barSubProduction.Caption = "Производство";
            barSubProduction.Id = 29;
            barSubProduction.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barSubKnitting), new LinkPersistInfo(barSubSewing) });
            barSubProduction.Name = "barSubProduction";
            barSubProduction.Tag = "производствоToolStripMenuItem";
            // 
            // barSubKnitting
            // 
            barSubKnitting.Caption = "Вязальное производство";
            barSubKnitting.Id = 30;
            barSubKnitting.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnOperPlan), new LinkPersistInfo(barBtnMasterDesk1), new LinkPersistInfo(barButtonItemSteamMasterWorkTable), new LinkPersistInfo(barButtonItemCutMasterWorkTable), new LinkPersistInfo(barBtnKnitterDesk), new LinkPersistInfo(barBtnAnalytics) });
            barSubKnitting.Name = "barSubKnitting";
            barSubKnitting.Tag = "вязальноеПроизводствоToolStripMenuItem";
            // 
            // barBtnOperPlan
            // 
            barBtnOperPlan.Caption = "Оперативное планирование";
            barBtnOperPlan.Id = 31;
            barBtnOperPlan.Name = "barBtnOperPlan";
            barBtnOperPlan.Tag = "оперативноеПланированиеToolStripMenuItem";
            barBtnOperPlan.ItemClick += оперативноеПланированиеToolStripMenuItem_Click;
            // 
            // barBtnMasterDesk1
            // 
            barBtnMasterDesk1.Caption = "Рабочий стол мастера Вяз. цеха";
            barBtnMasterDesk1.Id = 32;
            barBtnMasterDesk1.Name = "barBtnMasterDesk1";
            barBtnMasterDesk1.Tag = "рабочийСтолМастераВязЦехаToolStripMenuItem1";
            barBtnMasterDesk1.ItemClick += рабочийСтолМастераВязЦехаToolStripMenuItem1_Click;
            // 
            // barButtonItemSteamMasterWorkTable
            // 
            barButtonItemSteamMasterWorkTable.Caption = "Рабочий стол мастера Отпарки";
            barButtonItemSteamMasterWorkTable.Id = 45;
            barButtonItemSteamMasterWorkTable.Name = "barButtonItemSteamMasterWorkTable";
            barButtonItemSteamMasterWorkTable.Tag = "рабочийСтолМастераОтпаркиToolStripMenuItem1";
            barButtonItemSteamMasterWorkTable.ItemClick += barButtonItemSteamMasterWorkTable_ItemClick;
            // 
            // barButtonItemCutMasterWorkTable
            // 
            barButtonItemCutMasterWorkTable.Caption = "Рабочий стол мастера Раскр. цеха";
            barButtonItemCutMasterWorkTable.Id = 46;
            barButtonItemCutMasterWorkTable.Name = "barButtonItemCutMasterWorkTable";
            barButtonItemCutMasterWorkTable.Tag = "рабочийСтолМастераРаскрЦехаToolStripMenuItem1";
            barButtonItemCutMasterWorkTable.ItemClick += barButtonItemCutMasterWorkTable_ItemClick;
            // 
            // barBtnKnitterDesk
            // 
            barBtnKnitterDesk.Caption = "Рабочий стол вязальщицы";
            barBtnKnitterDesk.Id = 33;
            barBtnKnitterDesk.Name = "barBtnKnitterDesk";
            barBtnKnitterDesk.Tag = "рабочийСтолВязальщицыToolStripMenuItem";
            barBtnKnitterDesk.ItemClick += рабочийСтолВязальщицыToolStripMenuItem_Click;
            // 
            // barBtnAnalytics
            // 
            barBtnAnalytics.Caption = "Аналитика";
            barBtnAnalytics.Id = 34;
            barBtnAnalytics.Name = "barBtnAnalytics";
            barBtnAnalytics.Tag = "аналитикаToolStripMenuItem";
            barBtnAnalytics.ItemClick += аналитикаToolStripMenuItem_Click;
            // 
            // barSubSewing
            // 
            barSubSewing.Caption = "Швейное производство";
            barSubSewing.Id = 35;
            barSubSewing.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnMasterDesk), new LinkPersistInfo(barBtnCutShop) });
            barSubSewing.Name = "barSubSewing";
            barSubSewing.Tag = "швейноеПроизводствоToolStripMenuItem";
            // 
            // barBtnMasterDesk
            // 
            barBtnMasterDesk.Caption = "Рабочий стол мастера";
            barBtnMasterDesk.Id = 36;
            barBtnMasterDesk.Name = "barBtnMasterDesk";
            barBtnMasterDesk.Tag = "рабочийСтолМастераToolStripMenuItem";
            barBtnMasterDesk.ItemClick += рабочийСтолМастераToolStripMenuItem_Click;
            // 
            // barBtnCutShop
            // 
            barBtnCutShop.Caption = "Раскройный цех";
            barBtnCutShop.Id = 37;
            barBtnCutShop.Name = "barBtnCutShop";
            barBtnCutShop.Tag = "раскройныйЦехToolStripMenuItem";
            barBtnCutShop.ItemClick += раскройныйЦехToolStripMenuItem_Click;
            // 
            // barBtnTeamWork
            // 
            barBtnTeamWork.Caption = "Технологическая схема";
            barBtnTeamWork.Id = 38;
            barBtnTeamWork.Name = "barBtnTeamWork";
            barBtnTeamWork.Tag = "TeamWorktoolStripMenuItem";
            barBtnTeamWork.ItemClick += TeamWorktoolStripMenuItem_Click;
            // 
            // barBtnArticle
            // 
            barBtnArticle.Caption = "Артикул";
            barBtnArticle.Id = 39;
            barBtnArticle.Name = "barBtnArticle";
            barBtnArticle.Tag = "артикулToolStripMenuItem";
            barBtnArticle.ItemClick += артикулToolStripMenuItem_Click;
            // 
            // barBtnCalcCard
            // 
            barBtnCalcCard.Caption = "Карточка расчета";
            barBtnCalcCard.Id = 40;
            barBtnCalcCard.Name = "barBtnCalcCard";
            barBtnCalcCard.Tag = "карточкаРасчетаToolStripMenuItem";
            barBtnCalcCard.ItemClick += карточкаРасчетаToolStripMenuItem_Click;
            // 
            // barBtnTimesheet
            // 
            barBtnTimesheet.Caption = "Табель";
            barBtnTimesheet.Id = 41;
            barBtnTimesheet.Name = "barBtnTimesheet";
            barBtnTimesheet.Tag = "табельToolStripMenuItem";
            barBtnTimesheet.ItemClick += табельToolStripMenuItem_Click;
            // 
            // barButtonItemScreen
            // 
            barButtonItemScreen.Alignment = BarItemLinkAlignment.Right;
            barButtonItemScreen.Caption = "Скриншот окна";
            barButtonItemScreen.Id = 47;
            barButtonItemScreen.ImageOptions.Image = (Image)resources.GetObject("barButtonItemScreen.ImageOptions.Image");
            barButtonItemScreen.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItemScreen.ImageOptions.LargeImage");
            barButtonItemScreen.Name = "barButtonItemScreen";
            barButtonItemScreen.ItemClick += barButtonItemScreen_ItemClick;
            // 
            // barBtnAt
            // 
            barBtnAt.Alignment = BarItemLinkAlignment.Right;
            barBtnAt.Caption = "Редактор справки";
            barBtnAt.Id = 43;
            barBtnAt.ImageOptions.Image = (Image)resources.GetObject("barBtnAt.ImageOptions.Image");
            barBtnAt.ImageOptions.LargeImage = (Image)resources.GetObject("barBtnAt.ImageOptions.LargeImage");
            barBtnAt.Name = "barBtnAt";
            barBtnAt.Tag = "справкаtoolStripMenuItem";
            barBtnAt.ItemClick += справкаtoolStripMenuItem_Click;
            // 
            // barBtnQuestion
            // 
            barBtnQuestion.Alignment = BarItemLinkAlignment.Right;
            barBtnQuestion.Caption = "Справка";
            barBtnQuestion.Id = 42;
            barBtnQuestion.ImageOptions.Image = (Image)resources.GetObject("barBtnQuestion.ImageOptions.Image");
            barBtnQuestion.ImageOptions.LargeImage = (Image)resources.GetObject("barBtnQuestion.ImageOptions.LargeImage");
            barBtnQuestion.Name = "barBtnQuestion";
            barBtnQuestion.Tag = "кнопкаToolStripMenuItem";
            barBtnQuestion.ItemClick += кнопкаToolStripMenuItem_Click;
            // 
            // skinBarSubItem2
            // 
            skinBarSubItem2.Alignment = BarItemLinkAlignment.Right;
            skinBarSubItem2.Id = 3;
            skinBarSubItem2.ImageOptions.Image = (Image)resources.GetObject("skinBarSubItem2.ImageOptions.Image");
            skinBarSubItem2.ImageOptions.LargeImage = (Image)resources.GetObject("skinBarSubItem2.ImageOptions.LargeImage");
            skinBarSubItem2.Name = "skinBarSubItem2";
            skinBarSubItem2.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            // 
            // skinDropDownButtonItem2
            // 
            skinDropDownButtonItem2.Alignment = BarItemLinkAlignment.Right;
            skinDropDownButtonItem2.Id = 4;
            skinDropDownButtonItem2.Name = "skinDropDownButtonItem2";
            // 
            // skinPaletteDropDownButtonItem2
            // 
            skinPaletteDropDownButtonItem2.ActAsDropDown = true;
            skinPaletteDropDownButtonItem2.Alignment = BarItemLinkAlignment.Right;
            skinPaletteDropDownButtonItem2.ButtonStyle = BarButtonStyle.DropDown;
            skinPaletteDropDownButtonItem2.Enabled = false;
            skinPaletteDropDownButtonItem2.Id = 5;
            skinPaletteDropDownButtonItem2.Name = "skinPaletteDropDownButtonItem2";
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlTop.Size = new Size(1184, 25);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 513);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlBottom.Size = new Size(1184, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 25);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlLeft.Size = new Size(0, 488);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            barDockControlRight.Location = new Point(1184, 25);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            barDockControlRight.Size = new Size(0, 488);
            // 
            // skinBarSubItem1
            // 
            skinBarSubItem1.Caption = "Тема оформления";
            skinBarSubItem1.Id = 0;
            skinBarSubItem1.Name = "skinBarSubItem1";
            // 
            // skinDropDownButtonItem1
            // 
            skinDropDownButtonItem1.Id = 1;
            skinDropDownButtonItem1.Name = "skinDropDownButtonItem1";
            // 
            // skinPaletteDropDownButtonItem1
            // 
            skinPaletteDropDownButtonItem1.ActAsDropDown = true;
            skinPaletteDropDownButtonItem1.ButtonStyle = BarButtonStyle.DropDown;
            skinPaletteDropDownButtonItem1.Enabled = false;
            skinPaletteDropDownButtonItem1.Id = 2;
            skinPaletteDropDownButtonItem1.Name = "skinPaletteDropDownButtonItem1";
            // 
            // xtraTabbedMdiManager1
            // 
            xtraTabbedMdiManager1.AppearancePage.Header.Font = new Font("Segoe UI", 10F);
            xtraTabbedMdiManager1.AppearancePage.Header.Options.UseFont = true;
            xtraTabbedMdiManager1.AppearancePage.Header.Options.UseTextOptions = true;
            xtraTabbedMdiManager1.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            xtraTabbedMdiManager1.AppearancePage.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            xtraTabbedMdiManager1.AppearancePage.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeaderAndOnMouseHover;
            xtraTabbedMdiManager1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            xtraTabbedMdiManager1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            xtraTabbedMdiManager1.MdiParent = this;
            xtraTabbedMdiManager1.ShowToolTips = DevExpress.Utils.DefaultBoolean.True;
            xtraTabbedMdiManager1.UseFormIconAsPageImage = DevExpress.Utils.DefaultBoolean.True;
            xtraTabbedMdiManager1.PageAdded += XtraTabbedMdiManager1_PageAdded;
            // 
            // miniToolStrip
            // 
            miniToolStrip.AccessibleName = "Выбор нового элемента";
            miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            miniToolStrip.AutoSize = false;
            miniToolStrip.BackColor = SystemColors.ButtonFace;
            miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            miniToolStrip.Location = new Point(639, 30);
            miniToolStrip.Name = "miniToolStrip";
            miniToolStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            miniToolStrip.Size = new Size(473, 165);
            miniToolStrip.TabIndex = 21;
            // 
            // barButtonItemExchangeApp
            // 
            barButtonItemExchangeApp.Caption = "Выгрузки в 1С";
            barButtonItemExchangeApp.Id = 49;
            barButtonItemExchangeApp.Name = "barButtonItemExchangeApp";
            barButtonItemExchangeApp.ItemClick += barButtonItemExchangeApp_ItemClick;
            // barSubReports
            // 
            barSubReports.Caption = "Отчеты";
            barSubReports.Id = 49;
            barSubReports.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnPublicArticul) });
            barSubReports.Name = "barSubReports";
            // 
            // barBtnPublicArticul
            // 
            barBtnPublicArticul.Caption = "Опубликованные артикулы";
            barBtnPublicArticul.Id = 50;
            barBtnPublicArticul.Name = "barBtnPublicArticul";
            barBtnPublicArticul.ItemClick += barBtnPublicArticul_ItemClick;
            // 
            // SpMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(1184, 513);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = miniToolStrip;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SpMainForm";
            Text = "Швейное производство";
            FormClosing += SpMainForm_FormClosing;
            Load += SpMainForm_Load;
            KeyDown += SpMainForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)popupMenu1).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        public SewingProduction.Core.Class.CustomControls.CustomBarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private System.Windows.Forms.ToolStripMenuItem разделенияТрудаToolStripMenuItem;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.SkinBarSubItem skinBarSubItem2;
        private DevExpress.XtraBars.SkinDropDownButtonItem skinDropDownButtonItem2;
        private DevExpress.XtraBars.SkinPaletteDropDownButtonItem skinPaletteDropDownButtonItem2;
        private DevExpress.XtraBars.SkinBarSubItem skinBarSubItem1;
        private DevExpress.XtraBars.SkinDropDownButtonItem skinDropDownButtonItem1;
        private DevExpress.XtraBars.SkinPaletteDropDownButtonItem skinPaletteDropDownButtonItem1;
        private System.Windows.Forms.MenuStrip miniToolStrip;
        private DevExpress.XtraBars.BarSubItem barSubMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnProfile;
        private DevExpress.XtraBars.BarButtonItem barBtnSettings;
        private DevExpress.XtraBars.BarButtonItem barBtnAbout;
        private DevExpress.XtraBars.BarButtonItem barBtnHelp;

        private DevExpress.XtraBars.BarSubItem barSubSpr;
        private DevExpress.XtraBars.BarSubItem barSubEquipment;
        private DevExpress.XtraBars.BarButtonItem barBtnEqInTeams;
        private DevExpress.XtraBars.BarButtonItem barBtnEqDirectory;
        private DevExpress.XtraBars.BarButtonItem barBtnEqTypes;
        private DevExpress.XtraBars.BarButtonItem barBtnClassMatrix;
        private DevExpress.XtraBars.BarButtonItem barBtnOperationTypes;

        private DevExpress.XtraBars.BarSubItem barSubTeamsShop;
        private DevExpress.XtraBars.BarButtonItem barBtnTeams;
        private DevExpress.XtraBars.BarButtonItem barBtnShops;
        private DevExpress.XtraBars.BarButtonItem barBtnProdTypes;

        private DevExpress.XtraBars.BarButtonItem barBtnCalcCard1;
        private DevExpress.XtraBars.BarButtonItem barBtnWorkers;
        private DevExpress.XtraBars.BarButtonItem barBtnTariffs;
        private DevExpress.XtraBars.BarButtonItem barBtnNacenki;
        private DevExpress.XtraBars.BarButtonItem barBtnModelsMark;
        private DevExpress.XtraBars.BarSubItem barSubDefects;
        private DevExpress.XtraBars.BarButtonItem barBtnSockDefects;

        private DevExpress.XtraBars.BarSubItem barSubProduction;
        private DevExpress.XtraBars.BarSubItem barSubKnitting;
        private DevExpress.XtraBars.BarButtonItem barBtnOperPlan;
        private DevExpress.XtraBars.BarButtonItem barBtnMasterDesk1;
        private DevExpress.XtraBars.BarButtonItem barBtnKnitterDesk;
        private DevExpress.XtraBars.BarButtonItem barBtnAnalytics;

        private DevExpress.XtraBars.BarSubItem barSubSewing;
        private DevExpress.XtraBars.BarButtonItem barBtnMasterDesk;
        private DevExpress.XtraBars.BarButtonItem barBtnCutShop;

        private DevExpress.XtraBars.BarButtonItem barBtnTeamWork;
        private DevExpress.XtraBars.BarButtonItem barBtnArticle;
        private DevExpress.XtraBars.BarButtonItem barBtnCalcCard;
        private DevExpress.XtraBars.BarButtonItem barBtnTimesheet;

        private DevExpress.XtraBars.BarButtonItem barBtnQuestion;
        private DevExpress.XtraBars.BarButtonItem barBtnAt;
        private BarButtonItem barButtonItemSteamMasterWorkTable;
        private BarButtonItem barButtonItemCutMasterWorkTable;
        private BarButtonItem barButtonItemScreen;

        private BarButtonItem barButtonItemBrigObject;
        private BarButtonItem barButtonItemExchangeApp;

        private BarSubItem barSubReports;
        private BarButtonItem barBtnPublicArticul;

    }
}
