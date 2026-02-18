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
            barBtnCalcCard1 = new BarButtonItem();
            barBtnWorkers = new BarButtonItem();
            barBtnTariffs = new BarButtonItem();
            barBtnProducts = new BarButtonItem();
            barBtnModelsMark = new BarButtonItem();
            barSubDefects = new BarSubItem();
            barBtnSockDefects = new BarButtonItem();
            barSubProduction = new BarSubItem();
            barSubKnitting = new BarSubItem();
            barBtnOperPlan = new BarButtonItem();
            barBtnMasterDesk1 = new BarButtonItem();
            barBtnKnitterDesk = new BarButtonItem();
            barBtnAnalytics = new BarButtonItem();
            barSubSewing = new BarSubItem();
            barBtnMasterDesk = new BarButtonItem();
            barBtnCutShop = new BarButtonItem();
            barBtnTeamWork = new BarButtonItem();
            barBtnArticle = new BarButtonItem();
            barBtnCalcCard = new BarButtonItem();
            barBtnTimesheet = new BarButtonItem();
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
            МенюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            профильToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            оборудованиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            оборудованиеВБригадахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            оборудованиеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            видыОборудованияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            матрицыКлассовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            видыОперацийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            бригадыЦехаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            бригадыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            цехаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            видыПроизводствToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            карточкаРасчетаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            работникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            тарифыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            изделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            моделиСПризнакомМаркировкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            видыБраковПряжиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            видыБраковНосковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            производствоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            вязальноеПроизводствоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            оперативноеПланированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            рабочийСтолМастераToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            рабочийСтолВязальщицыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            аналитикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            швейноеПроизводствоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            рабочийСтолМастераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            раскройныйЦехToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            TeamWorktoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            артикулToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            карточкаРасчетаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            кнопкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            табельToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            справкаtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            customLayoutControl1 = new SewingProduction.Core.Class.CustomLayoutControl();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)popupMenu1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManager1).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).BeginInit();
            customLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
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
            barManager1.Items.AddRange(new BarItem[] { skinBarSubItem1, skinDropDownButtonItem1, skinPaletteDropDownButtonItem1, skinBarSubItem2, skinDropDownButtonItem2, skinPaletteDropDownButtonItem2, barSubMenu, barBtnProfile, barBtnSettings, barBtnAbout, barBtnHelp, barSubSpr, barSubEquipment, barBtnEqInTeams, barBtnEqDirectory, barBtnEqTypes, barBtnClassMatrix, barBtnOperationTypes, barSubTeamsShop, barBtnTeams, barBtnShops, barBtnProdTypes, barBtnCalcCard1, barBtnWorkers, barBtnTariffs, barBtnProducts, barBtnModelsMark, barSubDefects, barBtnSockDefects, barSubProduction, barSubKnitting, barBtnOperPlan, barBtnMasterDesk1, barBtnKnitterDesk, barBtnAnalytics, barSubSewing, barBtnMasterDesk, barBtnCutShop, barBtnTeamWork, barBtnArticle, barBtnCalcCard, barBtnTimesheet, barBtnQuestion, barBtnAt });
            barManager1.MaxItemId = 44;
            barManager1.SkipDevExpressSkinItems = true;
            // 
            // bar1
            // 
            bar1.BarName = "Main menu";
            bar1.DockCol = 0;
            bar1.DockRow = 0;
            bar1.DockStyle = BarDockStyle.Top;
            bar1.FloatLocation = new Point(3298, 388);
            bar1.FloatSize = new Size(46, 100);
            bar1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barSubMenu), new LinkPersistInfo(barSubSpr), new LinkPersistInfo(barSubProduction), new LinkPersistInfo(barBtnTeamWork), new LinkPersistInfo(barBtnArticle), new LinkPersistInfo(barBtnCalcCard), new LinkPersistInfo(barBtnTimesheet), new LinkPersistInfo(barBtnAt), new LinkPersistInfo(barBtnQuestion), new LinkPersistInfo(skinBarSubItem2), new LinkPersistInfo(skinDropDownButtonItem2), new LinkPersistInfo(skinPaletteDropDownButtonItem2) });
            bar1.OptionsBar.AllowCollapse = true;
            bar1.OptionsBar.AllowQuickCustomization = false;
            bar1.OptionsBar.DisableClose = true;
            bar1.OptionsBar.DistanceBetweenItems = 3;
            bar1.OptionsBar.ExpandAnimationDuration = 1;
            bar1.OptionsBar.MultiLine = true;
            bar1.OptionsBar.UseWholeRow = true;
            bar1.Text = "Main menu";
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
            barSubSpr.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barSubEquipment), new LinkPersistInfo(barSubTeamsShop), new LinkPersistInfo(barBtnCalcCard1), new LinkPersistInfo(barBtnWorkers), new LinkPersistInfo(barBtnTariffs), new LinkPersistInfo(barBtnProducts), new LinkPersistInfo(barBtnModelsMark), new LinkPersistInfo(barSubDefects) });
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
            barSubTeamsShop.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnTeams), new LinkPersistInfo(barBtnShops), new LinkPersistInfo(barBtnProdTypes) });
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
            // barBtnProducts
            // 
            barBtnProducts.Caption = "Изделия";
            barBtnProducts.Id = 25;
            barBtnProducts.Name = "barBtnProducts";
            barBtnProducts.Tag = "изделияToolStripMenuItem";
            barBtnProducts.ItemClick += изделияToolStripMenuItem_Click;
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
            barSubKnitting.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(barBtnOperPlan), new LinkPersistInfo(barBtnMasterDesk1), new LinkPersistInfo(barBtnKnitterDesk), new LinkPersistInfo(barBtnAnalytics) });
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
            barBtnMasterDesk1.Caption = "Рабочий стол мастера";
            barBtnMasterDesk1.Id = 32;
            barBtnMasterDesk1.Name = "barBtnMasterDesk1";
            barBtnMasterDesk1.Tag = "рабочийСтолМастераToolStripMenuItem1";
            barBtnMasterDesk1.ItemClick += рабочийСтолМастераToolStripMenuItem1_Click;
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
            // barBtnAt
            // 
            barBtnAt.Alignment = BarItemLinkAlignment.Right;
            barBtnAt.Caption = "@";
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
            barBtnQuestion.Caption = "❓";
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
            // МенюToolStripMenuItem
            // 
            МенюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { профильToolStripMenuItem, настройкиToolStripMenuItem, оПрограммеToolStripMenuItem, помощьToolStripMenuItem });
            МенюToolStripMenuItem.Name = "МенюToolStripMenuItem";
            МенюToolStripMenuItem.Size = new Size(53, 20);
            МенюToolStripMenuItem.Text = "Меню";
            // 
            // профильToolStripMenuItem
            // 
            профильToolStripMenuItem.Name = "профильToolStripMenuItem";
            профильToolStripMenuItem.Size = new Size(149, 22);
            профильToolStripMenuItem.Text = "Профиль";
            профильToolStripMenuItem.Click += профильToolStripMenuItem_Click;
            // 
            // настройкиToolStripMenuItem
            // 
            настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            настройкиToolStripMenuItem.Size = new Size(149, 22);
            настройкиToolStripMenuItem.Text = "Настройки";
            настройкиToolStripMenuItem.Click += настройкиToolStripMenuItem_Click;
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(149, 22);
            оПрограммеToolStripMenuItem.Text = "О программе";
            оПрограммеToolStripMenuItem.Click += оПрограммеToolStripMenuItem_Click;
            // 
            // помощьToolStripMenuItem
            // 
            помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            помощьToolStripMenuItem.Size = new Size(149, 22);
            помощьToolStripMenuItem.Text = "Помощь";
            помощьToolStripMenuItem.Click += помощьToolStripMenuItem_Click;
            // 
            // справочникиToolStripMenuItem
            // 
            справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { оборудованиеToolStripMenuItem, бригадыЦехаToolStripMenuItem, карточкаРасчетаToolStripMenuItem1, работникиToolStripMenuItem, тарифыToolStripMenuItem, изделияToolStripMenuItem, моделиСПризнакомМаркировкToolStripMenuItem, видыБраковПряжиToolStripMenuItem });
            справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            справочникиToolStripMenuItem.Size = new Size(94, 20);
            справочникиToolStripMenuItem.Text = "&Справочники";
            // 
            // оборудованиеToolStripMenuItem
            // 
            оборудованиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { оборудованиеВБригадахToolStripMenuItem, оборудованиеToolStripMenuItem1, видыОборудованияToolStripMenuItem, матрицыКлассовToolStripMenuItem, видыОперацийToolStripMenuItem });
            оборудованиеToolStripMenuItem.Name = "оборудованиеToolStripMenuItem";
            оборудованиеToolStripMenuItem.Size = new Size(262, 22);
            оборудованиеToolStripMenuItem.Text = "Оборудование";
            // 
            // оборудованиеВБригадахToolStripMenuItem
            // 
            оборудованиеВБригадахToolStripMenuItem.Name = "оборудованиеВБригадахToolStripMenuItem";
            оборудованиеВБригадахToolStripMenuItem.Size = new Size(226, 22);
            оборудованиеВБригадахToolStripMenuItem.Text = "Оборудование в бригадах";
            оборудованиеВБригадахToolStripMenuItem.Click += оборудованиеВБригадахToolStripMenuItem_Click;
            // 
            // оборудованиеToolStripMenuItem1
            // 
            оборудованиеToolStripMenuItem1.Name = "оборудованиеToolStripMenuItem1";
            оборудованиеToolStripMenuItem1.Size = new Size(226, 22);
            оборудованиеToolStripMenuItem1.Text = "Справочник Оборудования";
            оборудованиеToolStripMenuItem1.Click += оборудованиеToolStripMenuItem_Click;
            // 
            // видыОборудованияToolStripMenuItem
            // 
            видыОборудованияToolStripMenuItem.Name = "видыОборудованияToolStripMenuItem";
            видыОборудованияToolStripMenuItem.Size = new Size(226, 22);
            видыОборудованияToolStripMenuItem.Text = "Виды оборудования";
            видыОборудованияToolStripMenuItem.Click += видыОборудованияToolStripMenuItem_Click;
            // 
            // матрицыКлассовToolStripMenuItem
            // 
            матрицыКлассовToolStripMenuItem.Name = "матрицыКлассовToolStripMenuItem";
            матрицыКлассовToolStripMenuItem.Size = new Size(226, 22);
            матрицыКлассовToolStripMenuItem.Text = "Матрицы классов";
            матрицыКлассовToolStripMenuItem.Click += матрицаКлассовToolStripMenuItem_Click;
            // 
            // видыОперацийToolStripMenuItem
            // 
            видыОперацийToolStripMenuItem.Name = "видыОперацийToolStripMenuItem";
            видыОперацийToolStripMenuItem.Size = new Size(226, 22);
            видыОперацийToolStripMenuItem.Text = "Виды операций";
            видыОперацийToolStripMenuItem.Click += видОперацToolStripMenuItem_Click;
            // 
            // бригадыЦехаToolStripMenuItem
            // 
            бригадыЦехаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { бригадыToolStripMenuItem, цехаToolStripMenuItem, видыПроизводствToolStripMenuItem });
            бригадыЦехаToolStripMenuItem.Name = "бригадыЦехаToolStripMenuItem";
            бригадыЦехаToolStripMenuItem.Size = new Size(262, 22);
            бригадыЦехаToolStripMenuItem.Text = "Бригады/Цеха";
            // 
            // бригадыToolStripMenuItem
            // 
            бригадыToolStripMenuItem.Name = "бригадыToolStripMenuItem";
            бригадыToolStripMenuItem.Size = new Size(175, 22);
            бригадыToolStripMenuItem.Text = "Бригады";
            бригадыToolStripMenuItem.Click += бригадыToolStripMenuItem_Click;
            // 
            // цехаToolStripMenuItem
            // 
            цехаToolStripMenuItem.Name = "цехаToolStripMenuItem";
            цехаToolStripMenuItem.Size = new Size(175, 22);
            цехаToolStripMenuItem.Text = "Цеха";
            цехаToolStripMenuItem.Click += цехаToolStripMenuItem1_Click;
            // 
            // видыПроизводствToolStripMenuItem
            // 
            видыПроизводствToolStripMenuItem.Name = "видыПроизводствToolStripMenuItem";
            видыПроизводствToolStripMenuItem.Size = new Size(175, 22);
            видыПроизводствToolStripMenuItem.Text = "Виды производств";
            видыПроизводствToolStripMenuItem.Click += видыПроизводстваToolStripMenuItem_Click;
            // 
            // карточкаРасчетаToolStripMenuItem1
            // 
            карточкаРасчетаToolStripMenuItem1.Name = "карточкаРасчетаToolStripMenuItem1";
            карточкаРасчетаToolStripMenuItem1.Size = new Size(262, 22);
            карточкаРасчетаToolStripMenuItem1.Text = "Карточка расчета";
            карточкаРасчетаToolStripMenuItem1.Click += карточкаРасчетаToolStripMenuItem1_Click;
            // 
            // работникиToolStripMenuItem
            // 
            работникиToolStripMenuItem.Name = "работникиToolStripMenuItem";
            работникиToolStripMenuItem.Size = new Size(262, 22);
            работникиToolStripMenuItem.Text = "Работники";
            работникиToolStripMenuItem.Click += работникиToolStripMenuItem_Click;
            // 
            // тарифыToolStripMenuItem
            // 
            тарифыToolStripMenuItem.Name = "тарифыToolStripMenuItem";
            тарифыToolStripMenuItem.Size = new Size(262, 22);
            тарифыToolStripMenuItem.Text = "Тарифы/Константы";
            тарифыToolStripMenuItem.Click += тарифыToolStripMenuItem_Click;
            // 
            // изделияToolStripMenuItem
            // 
            изделияToolStripMenuItem.Name = "изделияToolStripMenuItem";
            изделияToolStripMenuItem.Size = new Size(262, 22);
            изделияToolStripMenuItem.Text = "Изделия";
            изделияToolStripMenuItem.Click += изделияToolStripMenuItem_Click;
            // 
            // моделиСПризнакомМаркировкToolStripMenuItem
            // 
            моделиСПризнакомМаркировкToolStripMenuItem.Name = "моделиСПризнакомМаркировкToolStripMenuItem";
            моделиСПризнакомМаркировкToolStripMenuItem.Size = new Size(262, 22);
            моделиСПризнакомМаркировкToolStripMenuItem.Text = "Модели с признаком маркировки";
            моделиСПризнакомМаркировкToolStripMenuItem.Click += моделиСПризнакомМаркировкToolStripMenuItem_Click;
            // 
            // видыБраковПряжиToolStripMenuItem
            // 
            видыБраковПряжиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { видыБраковНосковToolStripMenuItem });
            видыБраковПряжиToolStripMenuItem.Name = "видыБраковПряжиToolStripMenuItem";
            видыБраковПряжиToolStripMenuItem.Size = new Size(262, 22);
            видыБраковПряжиToolStripMenuItem.Text = "Виды браков";
            // 
            // видыБраковНосковToolStripMenuItem
            // 
            видыБраковНосковToolStripMenuItem.Name = "видыБраковНосковToolStripMenuItem";
            видыБраковНосковToolStripMenuItem.Size = new Size(187, 22);
            видыБраковНосковToolStripMenuItem.Text = "Виды браков носков";
            видыБраковНосковToolStripMenuItem.Click += видыБраковНосковToolStripMenuItem_Click;
            // 
            // производствоToolStripMenuItem
            // 
            производствоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { вязальноеПроизводствоToolStripMenuItem, швейноеПроизводствоToolStripMenuItem });
            производствоToolStripMenuItem.Name = "производствоToolStripMenuItem";
            производствоToolStripMenuItem.Size = new Size(97, 20);
            производствоToolStripMenuItem.Text = "&Производство";
            // 
            // вязальноеПроизводствоToolStripMenuItem
            // 
            вязальноеПроизводствоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { оперативноеПланированиеToolStripMenuItem, рабочийСтолМастераToolStripMenuItem1, рабочийСтолВязальщицыToolStripMenuItem, аналитикаToolStripMenuItem });
            вязальноеПроизводствоToolStripMenuItem.Name = "вязальноеПроизводствоToolStripMenuItem";
            вязальноеПроизводствоToolStripMenuItem.Size = new Size(210, 22);
            вязальноеПроизводствоToolStripMenuItem.Text = "Вязальное производство";
            // 
            // оперативноеПланированиеToolStripMenuItem
            // 
            оперативноеПланированиеToolStripMenuItem.Name = "оперативноеПланированиеToolStripMenuItem";
            оперативноеПланированиеToolStripMenuItem.Size = new Size(230, 22);
            оперативноеПланированиеToolStripMenuItem.Text = "Оперативное планирование";
            оперативноеПланированиеToolStripMenuItem.Click += оперативноеПланированиеToolStripMenuItem_Click;
            // 
            // рабочийСтолМастераToolStripMenuItem1
            // 
            рабочийСтолМастераToolStripMenuItem1.Name = "рабочийСтолМастераToolStripMenuItem1";
            рабочийСтолМастераToolStripMenuItem1.Size = new Size(230, 22);
            рабочийСтолМастераToolStripMenuItem1.Text = "Рабочий стол мастера";
            рабочийСтолМастераToolStripMenuItem1.Click += рабочийСтолМастераToolStripMenuItem1_Click;
            // 
            // рабочийСтолВязальщицыToolStripMenuItem
            // 
            рабочийСтолВязальщицыToolStripMenuItem.Name = "рабочийСтолВязальщицыToolStripMenuItem";
            рабочийСтолВязальщицыToolStripMenuItem.Size = new Size(230, 22);
            рабочийСтолВязальщицыToolStripMenuItem.Text = "Рабочий стол вязальщицы";
            рабочийСтолВязальщицыToolStripMenuItem.Click += рабочийСтолВязальщицыToolStripMenuItem_Click;
            // 
            // аналитикаToolStripMenuItem
            // 
            аналитикаToolStripMenuItem.Name = "аналитикаToolStripMenuItem";
            аналитикаToolStripMenuItem.Size = new Size(230, 22);
            аналитикаToolStripMenuItem.Text = "Аналитика";
            аналитикаToolStripMenuItem.Click += аналитикаToolStripMenuItem_Click;
            // 
            // швейноеПроизводствоToolStripMenuItem
            // 
            швейноеПроизводствоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { рабочийСтолМастераToolStripMenuItem, раскройныйЦехToolStripMenuItem });
            швейноеПроизводствоToolStripMenuItem.Name = "швейноеПроизводствоToolStripMenuItem";
            швейноеПроизводствоToolStripMenuItem.Size = new Size(210, 22);
            швейноеПроизводствоToolStripMenuItem.Text = "Швейное производство";
            // 
            // рабочийСтолМастераToolStripMenuItem
            // 
            рабочийСтолМастераToolStripMenuItem.Name = "рабочийСтолМастераToolStripMenuItem";
            рабочийСтолМастераToolStripMenuItem.Size = new Size(198, 22);
            рабочийСтолМастераToolStripMenuItem.Text = "Рабочий стол мастера";
            рабочийСтолМастераToolStripMenuItem.Click += рабочийСтолМастераToolStripMenuItem_Click;
            // 
            // раскройныйЦехToolStripMenuItem
            // 
            раскройныйЦехToolStripMenuItem.Name = "раскройныйЦехToolStripMenuItem";
            раскройныйЦехToolStripMenuItem.Size = new Size(198, 22);
            раскройныйЦехToolStripMenuItem.Text = "Раскройный цех";
            раскройныйЦехToolStripMenuItem.Click += раскройныйЦехToolStripMenuItem_Click;
            // 
            // TeamWorktoolStripMenuItem
            // 
            TeamWorktoolStripMenuItem.Name = "TeamWorktoolStripMenuItem";
            TeamWorktoolStripMenuItem.Size = new Size(150, 20);
            TeamWorktoolStripMenuItem.Text = "&Технологическая схема";
            TeamWorktoolStripMenuItem.Click += TeamWorktoolStripMenuItem_Click;
            // 
            // артикулToolStripMenuItem
            // 
            артикулToolStripMenuItem.Name = "артикулToolStripMenuItem";
            артикулToolStripMenuItem.Size = new Size(65, 20);
            артикулToolStripMenuItem.Text = "Артикул";
            артикулToolStripMenuItem.Click += артикулToolStripMenuItem_Click;
            // 
            // карточкаРасчетаToolStripMenuItem
            // 
            карточкаРасчетаToolStripMenuItem.Name = "карточкаРасчетаToolStripMenuItem";
            карточкаРасчетаToolStripMenuItem.Size = new Size(116, 20);
            карточкаРасчетаToolStripMenuItem.Text = "Карточка расчета";
            карточкаРасчетаToolStripMenuItem.Click += карточкаРасчетаToolStripMenuItem_Click;
            // 
            // кнопкаToolStripMenuItem
            // 
            кнопкаToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            кнопкаToolStripMenuItem.BackColor = SystemColors.ButtonFace;
            кнопкаToolStripMenuItem.ForeColor = SystemColors.ActiveCaptionText;
            кнопкаToolStripMenuItem.Name = "кнопкаToolStripMenuItem";
            кнопкаToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            кнопкаToolStripMenuItem.Size = new Size(28, 20);
            кнопкаToolStripMenuItem.Text = "❓";
            кнопкаToolStripMenuItem.Click += кнопкаToolStripMenuItem_Click;
            // 
            // табельToolStripMenuItem
            // 
            табельToolStripMenuItem.Name = "табельToolStripMenuItem";
            табельToolStripMenuItem.Size = new Size(57, 20);
            табельToolStripMenuItem.Text = "Табель";
            табельToolStripMenuItem.Click += табельToolStripMenuItem_Click;
            // 
            // справкаtoolStripMenuItem
            // 
            справкаtoolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            справкаtoolStripMenuItem.Name = "справкаtoolStripMenuItem";
            справкаtoolStripMenuItem.Size = new Size(30, 20);
            справкаtoolStripMenuItem.Text = "@";
            справкаtoolStripMenuItem.Click += справкаtoolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { МенюToolStripMenuItem, справочникиToolStripMenuItem, производствоToolStripMenuItem, TeamWorktoolStripMenuItem, артикулToolStripMenuItem, карточкаРасчетаToolStripMenuItem, кнопкаToolStripMenuItem, табельToolStripMenuItem, справкаtoolStripMenuItem });
            menuStrip1.Location = new Point(2, 2);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(1180, 24);
            menuStrip1.TabIndex = 21;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.Visible = false;
            // 
            // customLayoutControl1
            // 
            customLayoutControl1.Controls.Add(menuStrip1);
            customLayoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            customLayoutControl1.Font = new Font("Arial", 10F);
            customLayoutControl1.Location = new Point(0, 25);
            customLayoutControl1.Name = "customLayoutControl1";
            customLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(2635, 381, 650, 400);
            customLayoutControl1.Root = Root;
            customLayoutControl1.Size = new Size(1184, 28);
            customLayoutControl1.TabIndex = 27;
            customLayoutControl1.Text = "customLayoutControl1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            Root.Size = new Size(1184, 28);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = menuStrip1;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.MinSize = new Size(104, 24);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(1184, 28);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.TextVisible = false;
            // 
            // SpMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(1184, 513);
            Controls.Add(customLayoutControl1);
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
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).EndInit();
            customLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.PopupMenu popupMenu1; 
        private SewingProduction.Core.Class.CustomControls.CustomBarManager barManager1;
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem МенюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem профильToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборудованиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборудованиеВБригадахToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборудованиеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem видыОборудованияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem матрицыКлассовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видыОперацийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem бригадыЦехаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem бригадыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem цехаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видыПроизводствToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem карточкаРасчетаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem работникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem тарифыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изделияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem моделиСПризнакомМаркировкToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видыБраковПряжиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видыБраковНосковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem производствоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вязальноеПроизводствоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оперативноеПланированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рабочийСтолМастераToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem рабочийСтолВязальщицыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem аналитикаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem швейноеПроизводствоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рабочийСтолМастераToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem раскройныйЦехToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TeamWorktoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem артикулToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem карточкаРасчетаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кнопкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem табельToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаtoolStripMenuItem;
        private System.Windows.Forms.MenuStrip miniToolStrip;
        private Core.Class.CustomLayoutControl customLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
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
        private DevExpress.XtraBars.BarButtonItem barBtnProducts;
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

    }
}
