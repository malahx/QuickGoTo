/* 
QuickGoTo
Copyright 2015 Malah

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using UnityEngine;

namespace QuickGoTo {
	public class QToolbar {

		internal static QToolbar Instance {
			get;
			private set;
		}
	
		private bool StockToolBar {
			get {
				return QSettings.Instance.StockToolBar;
			}
		}
		private bool BlizzyToolBar {
			get {
				return QSettings.Instance.BlizzyToolBar;
			}
		}
		private ApplicationLauncher.AppScenes StockToolBar_AppScenes = ApplicationLauncher.AppScenes.ALWAYS;
		private string StockToolBar_TexturePath = Quick.MOD + "/Textures/StockToolBar";
		private string BlizzyToolBar_TexturePath = Quick.MOD + "/Textures/BlizzyToolBar";

		internal static string VAB_TexturePath = Quick.MOD + "/Textures/BlizzyVAB";
		internal static string TS_TexturePath = Quick.MOD + "/Textures/BlizzyTS";
		internal static string SPH_TexturePath = Quick.MOD + "/Textures/BlizzySPH";
		internal static string Sett_TexturePath = Quick.MOD + "/Textures/BlizzySett";
		internal static string SC_TexturePath = Quick.MOD + "/Textures/BlizzySC";
		internal static string RvSC_TexturePath = Quick.MOD + "/Textures/BlizzyRvSC";
		internal static string RvED_TexturePath = Quick.MOD + "/Textures/BlizzyRvED";
		internal static string Rv_TexturePath = Quick.MOD + "/Textures/BlizzyRv";
		internal static string RnD_TexturePath = Quick.MOD + "/Textures/BlizzyRnD";
		internal static string Rc_TexturePath = Quick.MOD + "/Textures/BlizzyRc";
		//internal static string RcVA_TexturePath = Quick.MOD + "/Textures/BlizzyRcVA";
		//internal static string RcSP_TexturePath = Quick.MOD + "/Textures/BlizzyRcSP";
		internal static string MI_TexturePath = Quick.MOD + "/Textures/BlizzyMI";
		internal static string Main_TexturePath = Quick.MOD + "/Textures/BlizzyMain";
		internal static string Lves_TexturePath = Quick.MOD + "/Textures/BlizzyLves";
		internal static string Astr_TexturePath = Quick.MOD + "/Textures/BlizzyAstr";
		internal static string Admi_TexturePath = Quick.MOD + "/Textures/BlizzyAdmi";

		private void OnClick() { 
			QGUI.Settings ();
		}




		private Texture2D StockToolBar_Texture;
		internal ApplicationLauncherButton StockToolBar_Button;
		private IButton BlizzyToolBar_Button;
		private IButton BlizzyToolBar_ButtonVAB;
		private IButton BlizzyToolBar_ButtonTS;
		private IButton BlizzyToolBar_ButtonSPH;
		private IButton BlizzyToolBar_ButtonSett;
		private IButton BlizzyToolBar_ButtonSC;
		private IButton BlizzyToolBar_ButtonRvSC;
		private IButton BlizzyToolBar_ButtonRvED;
		private IButton BlizzyToolBar_ButtonRv;
		private IButton BlizzyToolBar_ButtonRnD;
		private IButton BlizzyToolBar_ButtonRc;
		//private IButton BlizzyToolBar_ButtonRcVA;
		//private IButton BlizzyToolBar_ButtonRcSP;
		private IButton BlizzyToolBar_ButtonMI;
		private IButton BlizzyToolBar_ButtonMain;
		private IButton BlizzyToolBar_ButtonLves;
		private IButton BlizzyToolBar_ButtonAstr;
		private IButton BlizzyToolBar_ButtonAdmi;

		internal bool isBlizzyToolBar {
			get {
				return ToolbarManager.ToolbarAvailable && ToolbarManager.Instance != null;
			}
		}

		internal Rect StockToolBarPosition {
			get {
				if (StockToolBar_Button == null || ApplicationLauncher.Instance == null) {
					return new Rect ();
				}
				Camera _camera = UIManager.instance.uiCameras [0].camera;
				Vector3 _pos = StockToolBar_Button.GetAnchor ();
				Rect _rect = new Rect (0, 0, 40, 40);
				if (ApplicationLauncher.Instance.IsPositionedAtTop) {
					_rect.x = _camera.WorldToScreenPoint (_pos).x;
				} else {
					_rect.x = _camera.WorldToScreenPoint (_pos).x - 40;
					_rect.y = Screen.height - 40;
				}
				return _rect;
			}
		}

		internal static void Awake() {
			Instance = new QToolbar ();
		}

		internal void Start() {
			StockToolBar_Texture = GameDatabase.Instance.GetTexture (StockToolBar_TexturePath, false);
			GameEvents.onGUIApplicationLauncherReady.Add (OnGUIApplicationLauncherReady);
			if (BlizzyToolBar) {
				BlizzyToolBar_Init ();
			}
		}

		internal void OnDestroy() {
			GameEvents.onGUIApplicationLauncherReady.Remove (OnGUIApplicationLauncherReady);
			StockToolBar_Destroy ();
			BlizzyToolBar_Destroy ();
			Instance = null;
		}

		private void OnGUIApplicationLauncherReady() {
			if (StockToolBar) {
				StockToolBar_Init ();
			}
		}

		private void BlizzyToolBar_Init() {
			if (!isBlizzyToolBar) {
				return;
			}
			if (BlizzyToolBar_Button == null) {
				BlizzyToolBar_Button = ToolbarManager.Instance.add (Quick.MOD, Quick.MOD);
				BlizzyToolBar_Button.TexturePath = BlizzyToolBar_TexturePath;
				BlizzyToolBar_Button.ToolTip = Quick.MOD;
				BlizzyToolBar_Button.OnClick += (e) => OnClick ();
			}
			if (QSettings.Instance.EnableGoToMainMenu && BlizzyToolBar_ButtonMain == null) {
				BlizzyToolBar_ButtonMain = ToolbarManager.Instance.add (Quick.MOD + "Main", Quick.MOD + "Main");
				BlizzyToolBar_ButtonMain.TexturePath = Main_TexturePath;
				BlizzyToolBar_ButtonMain.ToolTip = QGoTo.GetText (QGoTo.GoTo.MainMenu);
				BlizzyToolBar_ButtonMain.OnClick += (e) => QGoTo.mainMenu ();
			}
			if (QSettings.Instance.EnableGoToSettings && BlizzyToolBar_ButtonSett == null) {
				BlizzyToolBar_ButtonSett = ToolbarManager.Instance.add (Quick.MOD + "Sett", Quick.MOD + "Sett");
				BlizzyToolBar_ButtonSett.TexturePath = Sett_TexturePath;
				BlizzyToolBar_ButtonSett.ToolTip = QGoTo.GetText (QGoTo.GoTo.Settings);
				BlizzyToolBar_ButtonSett.OnClick += (e) => QGoTo.settings ();
			}
			if (QSettings.Instance.EnableGoToSpaceCenter && BlizzyToolBar_ButtonSC == null) {
				BlizzyToolBar_ButtonSC = ToolbarManager.Instance.add (Quick.MOD + "SC", Quick.MOD + "SC");
				BlizzyToolBar_ButtonSC.TexturePath = SC_TexturePath;
				BlizzyToolBar_ButtonSC.ToolTip = QGoTo.GetText (QGoTo.GoTo.SpaceCenter);
				BlizzyToolBar_ButtonSC.Enabled = HighLogic.LoadedScene != GameScenes.SPACECENTER;
				BlizzyToolBar_ButtonSC.OnClick += (e) => QGoTo.spaceCenter ();
			}
			if (QSettings.Instance.EnableGoToVAB && BlizzyToolBar_ButtonVAB == null) {
				BlizzyToolBar_ButtonVAB = ToolbarManager.Instance.add (Quick.MOD + "VAB", Quick.MOD + "VAB");
				BlizzyToolBar_ButtonVAB.TexturePath = VAB_TexturePath;
				BlizzyToolBar_ButtonVAB.ToolTip = QGoTo.GetText (QGoTo.GoTo.VAB);
				BlizzyToolBar_ButtonVAB.OnClick += (e) => QGoTo.VAB ();
			}
			if (QSettings.Instance.EnableGoToSPH && BlizzyToolBar_ButtonSPH == null) {
				BlizzyToolBar_ButtonSPH = ToolbarManager.Instance.add (Quick.MOD + "SPH", Quick.MOD + "SPH");
				BlizzyToolBar_ButtonSPH.TexturePath = SPH_TexturePath;
				BlizzyToolBar_ButtonSPH.ToolTip = QGoTo.GetText (QGoTo.GoTo.SPH);
				BlizzyToolBar_ButtonSPH.OnClick += (e) => QGoTo.SPH ();
			}
			if (QSettings.Instance.EnableGoToTrackingStation && BlizzyToolBar_ButtonTS == null) {
				BlizzyToolBar_ButtonTS = ToolbarManager.Instance.add (Quick.MOD + "TS", Quick.MOD + "TS");
				BlizzyToolBar_ButtonTS.TexturePath = TS_TexturePath;
				BlizzyToolBar_ButtonTS.ToolTip = QGoTo.GetText (QGoTo.GoTo.TrackingStation);
				BlizzyToolBar_ButtonTS.Enabled = HighLogic.LoadedScene != GameScenes.TRACKSTATION;
				BlizzyToolBar_ButtonTS.OnClick += (e) => QGoTo.trackingStation ();
			}
			if (QSettings.Instance.EnableGoToRevert && BlizzyToolBar_ButtonRv == null) {
				BlizzyToolBar_ButtonRv = ToolbarManager.Instance.add (Quick.MOD + "Rv", Quick.MOD + "Rv");
				BlizzyToolBar_ButtonRv.TexturePath = Rv_TexturePath;
				BlizzyToolBar_ButtonRv.ToolTip = QGoTo.GetText (QGoTo.GoTo.Revert);
				BlizzyToolBar_ButtonRv.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				BlizzyToolBar_ButtonRv.OnClick += (e) => QGoTo.Revert ();
			}
			if (QSettings.Instance.EnableGoToRevertToEditor && BlizzyToolBar_ButtonRvED == null) {
				BlizzyToolBar_ButtonRvED = ToolbarManager.Instance.add (Quick.MOD + "RvED", Quick.MOD + "RvED");
				BlizzyToolBar_ButtonRvED.TexturePath = RvED_TexturePath;
				BlizzyToolBar_ButtonRvED.ToolTip = QGoTo.GetText (QGoTo.GoTo.RevertToEditor);
				BlizzyToolBar_ButtonRvED.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				BlizzyToolBar_ButtonRvED.OnClick += (e) => QGoTo.RevertToEditor ();
			}
			if (QSettings.Instance.EnableGoToRevertToSpaceCenter && BlizzyToolBar_ButtonRvSC == null) {
				BlizzyToolBar_ButtonRvSC = ToolbarManager.Instance.add (Quick.MOD + "RvSC", Quick.MOD + "RvSC");
				BlizzyToolBar_ButtonRvSC.TexturePath = RvSC_TexturePath;
				BlizzyToolBar_ButtonRvSC.ToolTip = QGoTo.GetText (QGoTo.GoTo.RevertToSpaceCenter);
				BlizzyToolBar_ButtonRvSC.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				BlizzyToolBar_ButtonRvSC.OnClick += (e) => QGoTo.RevertToSpaceCenter ();
			}
			if (QSettings.Instance.EnableGoToRevert && BlizzyToolBar_ButtonRc == null) {
				BlizzyToolBar_ButtonRc = ToolbarManager.Instance.add (Quick.MOD + "Rc", Quick.MOD + "Rc");
				BlizzyToolBar_ButtonRc.TexturePath = Rc_TexturePath;
				BlizzyToolBar_ButtonRc.ToolTip = QGoTo.GetText (QGoTo.GoTo.Recover);
				BlizzyToolBar_ButtonRc.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				BlizzyToolBar_ButtonRc.OnClick += (e) => QGoTo.Recover ();
			}
			/*if (QSettings.Instance.EnableGoToRecoverToVAB && BlizzyToolBar_ButtonRcVA == null) {
				BlizzyToolBar_ButtonRcVA = ToolbarManager.Instance.add (Quick.MOD + "RcVA", Quick.MOD + "RcVA");
				BlizzyToolBar_ButtonRcVA.TexturePath = RcVA_TexturePath;
				BlizzyToolBar_ButtonRcVA.ToolTip = QGoTo.GetText (QGoTo.GoTo.RecoverToVAB);
				BlizzyToolBar_ButtonRcVA.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				BlizzyToolBar_ButtonRcVA.Enabled = FlightGlobals.ready;
				BlizzyToolBar_ButtonRcVA.OnClick += (e) => QGoTo.RecoverToVAB ();
			}
			if (QSettings.Instance.EnableGoToRecoverToSPH && BlizzyToolBar_ButtonRcSP == null) {
				BlizzyToolBar_ButtonRcSP = ToolbarManager.Instance.add (Quick.MOD + "RcSP", Quick.MOD + "RcSP");
				BlizzyToolBar_ButtonRcSP.TexturePath = RcSP_TexturePath;
				BlizzyToolBar_ButtonRcSP.ToolTip = QGoTo.GetText (QGoTo.GoTo.RecoverToSPH);
				BlizzyToolBar_ButtonRcSP.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				BlizzyToolBar_ButtonRcSP.Enabled = FlightGlobals.ready;
				BlizzyToolBar_ButtonRcSP.OnClick += (e) => QGoTo.RecoverToSPH ();
			}*/
			if (QSettings.Instance.EnableGoToRnD && BlizzyToolBar_ButtonRnD == null) {
				BlizzyToolBar_ButtonRnD = ToolbarManager.Instance.add (Quick.MOD + "RnD", Quick.MOD + "RnD");
				BlizzyToolBar_ButtonRnD.TexturePath = RnD_TexturePath;
				BlizzyToolBar_ButtonRnD.ToolTip = QGoTo.GetText (QGoTo.GoTo.RnD);
				BlizzyToolBar_ButtonRnD.Visible = QGoTo.CanScienceBuilding;
				BlizzyToolBar_ButtonRnD.OnClick += (e) => QGoTo.RnD ();
			}
			if (QSettings.Instance.EnableGoToMissionControl && BlizzyToolBar_ButtonMI == null) {
				BlizzyToolBar_ButtonMI = ToolbarManager.Instance.add (Quick.MOD + "MI", Quick.MOD + "MI");
				BlizzyToolBar_ButtonMI.TexturePath = MI_TexturePath;
				BlizzyToolBar_ButtonMI.ToolTip = QGoTo.GetText (QGoTo.GoTo.MissionControl);
				BlizzyToolBar_ButtonMI.Visible = QGoTo.CanFundBuilding;
				BlizzyToolBar_ButtonMI.OnClick += (e) => QGoTo.missionControl ();
			}
			if (QSettings.Instance.EnableGoToAstronautComplex && BlizzyToolBar_ButtonAstr == null) {
				BlizzyToolBar_ButtonAstr = ToolbarManager.Instance.add (Quick.MOD + "Astr", Quick.MOD + "Astr");
				BlizzyToolBar_ButtonAstr.TexturePath = Astr_TexturePath;
				BlizzyToolBar_ButtonAstr.ToolTip = QGoTo.GetText (QGoTo.GoTo.AstronautComplex);
				BlizzyToolBar_ButtonAstr.OnClick += (e) => QGoTo.astronautComplex ();
			}
			if (QSettings.Instance.EnableGoToAdministration && BlizzyToolBar_ButtonAdmi == null) {
				BlizzyToolBar_ButtonAdmi = ToolbarManager.Instance.add (Quick.MOD + "Admi", Quick.MOD + "Admi");
				BlizzyToolBar_ButtonAdmi.TexturePath = Admi_TexturePath;
				BlizzyToolBar_ButtonAdmi.ToolTip = QGoTo.GetText (QGoTo.GoTo.Administration);
				BlizzyToolBar_ButtonAdmi.Visible = QGoTo.CanFundBuilding;
				BlizzyToolBar_ButtonAdmi.OnClick += (e) => QGoTo.administration ();
			}
			if (QSettings.Instance.EnableGoToLastVessel && BlizzyToolBar_ButtonLves == null) {
				BlizzyToolBar_ButtonLves = ToolbarManager.Instance.add (Quick.MOD + "Lves", Quick.MOD + "Lves");
				BlizzyToolBar_ButtonLves.TexturePath = Lves_TexturePath;
				BlizzyToolBar_ButtonLves.ToolTip = QGoTo.GetText (QGoTo.GoTo.LastVessel);
				BlizzyToolBar_ButtonLves.OnClick += (e) => QGoTo.LastVessel ();
			}
		}

		private void BlizzyToolBar_Destroy() {
			if (!isBlizzyToolBar) {
				return;
			}
			if (BlizzyToolBar_Button != null) {
				BlizzyToolBar_Button.Destroy ();
				BlizzyToolBar_Button = null;
			}
			if (BlizzyToolBar_ButtonMain != null) {
				BlizzyToolBar_ButtonMain.Destroy ();
				BlizzyToolBar_ButtonMain = null;
			}
			if (BlizzyToolBar_ButtonSett != null) {
				BlizzyToolBar_ButtonSett.Destroy ();
				BlizzyToolBar_ButtonSett = null;
			}
			if (BlizzyToolBar_ButtonSC != null) {
				BlizzyToolBar_ButtonSC.Destroy ();
				BlizzyToolBar_ButtonSC = null;
			}
			if (BlizzyToolBar_ButtonVAB != null) {
				BlizzyToolBar_ButtonVAB.Destroy ();
				BlizzyToolBar_ButtonVAB = null;
			}
			if (BlizzyToolBar_ButtonSPH != null) {
				BlizzyToolBar_ButtonSPH.Destroy ();
				BlizzyToolBar_ButtonSPH = null;
			}
			if (BlizzyToolBar_ButtonTS != null) {
				BlizzyToolBar_ButtonTS.Destroy ();
				BlizzyToolBar_ButtonTS = null;
			}
			if (BlizzyToolBar_ButtonMI != null) {
				BlizzyToolBar_ButtonMI.Destroy ();
				BlizzyToolBar_ButtonMI = null;
			}
			if (BlizzyToolBar_ButtonAstr != null) {
				BlizzyToolBar_ButtonAstr.Destroy ();
				BlizzyToolBar_ButtonAstr = null;
			}
			if (BlizzyToolBar_ButtonAdmi != null) {
				BlizzyToolBar_ButtonAdmi.Destroy ();
				BlizzyToolBar_ButtonAdmi = null;
			}
			if (BlizzyToolBar_ButtonRnD != null) {
				BlizzyToolBar_ButtonRnD.Destroy ();
				BlizzyToolBar_ButtonRnD = null;
			}
			if (BlizzyToolBar_ButtonRv != null) {
				BlizzyToolBar_ButtonRv.Destroy ();
				BlizzyToolBar_ButtonRv = null;
			}
			if (BlizzyToolBar_ButtonRvED != null) {
				BlizzyToolBar_ButtonRvED.Destroy ();
				BlizzyToolBar_ButtonRvED = null;
			}
			if (BlizzyToolBar_ButtonRvSC != null) {
				BlizzyToolBar_ButtonRvSC.Destroy ();
				BlizzyToolBar_ButtonRvSC = null;
			}
			if (BlizzyToolBar_ButtonRc != null) {
				BlizzyToolBar_ButtonRc.Destroy ();
				BlizzyToolBar_ButtonRc = null;
			}
			/*if (BlizzyToolBar_ButtonRcVA != null) {
				BlizzyToolBar_ButtonRcVA.Destroy ();
				BlizzyToolBar_ButtonRcVA = null;
			}
			if (BlizzyToolBar_ButtonRcSP != null) {
				BlizzyToolBar_ButtonRcSP.Destroy ();
				BlizzyToolBar_ButtonRcSP = null;
			}*/
			if (BlizzyToolBar_ButtonLves != null) {
				BlizzyToolBar_ButtonLves.Destroy ();
				BlizzyToolBar_ButtonLves = null;
			}
		}

		private void StockToolBar_Init() {
			if (ApplicationLauncher.Ready && StockToolBar_Button == null) {
				StockToolBar_Button = ApplicationLauncher.Instance.AddModApplication (OnClick, OnClick, null, null, null, null, StockToolBar_AppScenes, StockToolBar_Texture);
			}
		}

		private void StockToolBar_Destroy() {
			if (StockToolBar_Button != null) {
				ApplicationLauncher.Instance.RemoveModApplication (StockToolBar_Button);
				StockToolBar_Button = null;
			}
		}

		internal void Reset() {
			if (!QGUI.WindowSettings) {
				if (StockToolBar_Button != null) {
					if (StockToolBar_Button.State == RUIToggleButton.ButtonState.TRUE) {
						StockToolBar_Button.SetFalse (false);
					}
				}
				if (QSettings.Instance.StockToolBar) {
					if (StockToolBar_Button == null) {
						StockToolBar_Init ();
					}
				} else if (StockToolBar_Button != null) {
					StockToolBar_Destroy ();
				}
				if (QSettings.Instance.BlizzyToolBar) {
					BlizzyToolBar_Init ();
				} else {
					BlizzyToolBar_Destroy ();
				}
			} else {
				if (StockToolBar_Button != null) {
					if (StockToolBar_Button.State == RUIToggleButton.ButtonState.FALSE) {
						StockToolBar_Button.SetTrue (false);
					}
				}
			}
		}
	}
}