﻿/* 
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
	public class QBlizzyToolbar {
	
		internal bool Enabled {
			get {
				return QSettings.Instance.BlizzyToolBar;
			}
		}
		internal string TexturePath = Quick.MOD + "/Textures/BlizzyToolBar";
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
		internal static string MI_TexturePath = Quick.MOD + "/Textures/BlizzyMI";
		internal static string Main_TexturePath = Quick.MOD + "/Textures/BlizzyMain";
		internal static string Lves_TexturePath = Quick.MOD + "/Textures/BlizzyLves";
		internal static string Astr_TexturePath = Quick.MOD + "/Textures/BlizzyAstr";
		internal static string Admi_TexturePath = Quick.MOD + "/Textures/BlizzyAdmi";
		internal static string Conf_TexturePath = Quick.MOD + "/Textures/BlizzyConf";

		private void OnClick () {
			QGUI.ToggleGoTo ();
		}

		private IButton Button;
		private IButton ButtonVAB;
		private IButton ButtonTS;
		private IButton ButtonSPH;
		private IButton ButtonSett;
		private IButton ButtonSC;
		private IButton ButtonRvSC;
		private IButton ButtonRvED;
		private IButton ButtonRv;
		private IButton ButtonRnD;
		private IButton ButtonRc;
		private IButton ButtonMI;
		private IButton ButtonMain;
		private IButton ButtonLves;
		private IButton ButtonAstr;
		private IButton ButtonAdmi;
		private IButton ButtonConf;

		internal static bool isAvailable {
			get {
				return ToolbarManager.ToolbarAvailable && ToolbarManager.Instance != null;
			}
		}

		internal void Start() {
			if (!HighLogic.LoadedSceneIsGame || !isAvailable || !Enabled) {
				return;
			}
			if (Button != null) {
				Button = ToolbarManager.Instance.add (Quick.MOD, Quick.MOD);
				Button.TexturePath = TexturePath;
				Button.ToolTip = Quick.MOD;
				Button.OnClick += (e) => OnClick ();
			}
			if ((QSettings.Instance.EnableSettings || HighLogic.LoadedScene == GameScenes.SPACECENTER) && ButtonConf == null) {
				ButtonConf = ToolbarManager.Instance.add (Quick.MOD + "Conf", Quick.MOD + "Conf");
				ButtonConf.TexturePath = Conf_TexturePath;
				ButtonConf.ToolTip = QGoTo.GetText (QGoTo.GoTo.Configurations);
				ButtonConf.OnClick += (e) => QGUI.Settings ();
			}
			if (QSettings.Instance.EnableGoToMainMenu && ButtonMain == null) {
				ButtonMain = ToolbarManager.Instance.add (Quick.MOD + "Main", Quick.MOD + "Main");
				ButtonMain.TexturePath = Main_TexturePath;
				ButtonMain.ToolTip = QGoTo.GetText (QGoTo.GoTo.MainMenu);
				ButtonMain.OnClick += (e) => QGoTo.mainMenu ();
			}
			if (QSettings.Instance.EnableGoToSettings && ButtonSett == null) {
				ButtonSett = ToolbarManager.Instance.add (Quick.MOD + "Sett", Quick.MOD + "Sett");
				ButtonSett.TexturePath = Sett_TexturePath;
				ButtonSett.ToolTip = QGoTo.GetText (QGoTo.GoTo.Settings);
				ButtonSett.OnClick += (e) => QGoTo.settings ();
			}
			if (QSettings.Instance.EnableGoToSpaceCenter && ButtonSC == null) {
				ButtonSC = ToolbarManager.Instance.add (Quick.MOD + "SC", Quick.MOD + "SC");
				ButtonSC.TexturePath = SC_TexturePath;
				ButtonSC.ToolTip = QGoTo.GetText (QGoTo.GoTo.SpaceCenter);
				ButtonSC.Enabled = HighLogic.LoadedScene != GameScenes.SPACECENTER;
				ButtonSC.OnClick += (e) => QGoTo.spaceCenter ();
			}
			if (QSettings.Instance.EnableGoToVAB && ButtonVAB == null) {
				ButtonVAB = ToolbarManager.Instance.add (Quick.MOD + "VAB", Quick.MOD + "VAB");
				ButtonVAB.TexturePath = VAB_TexturePath;
				ButtonVAB.ToolTip = QGoTo.GetText (QGoTo.GoTo.VAB);
				ButtonVAB.OnClick += (e) => QGoTo.VAB ();
			}
			if (QSettings.Instance.EnableGoToSPH && ButtonSPH == null) {
				ButtonSPH = ToolbarManager.Instance.add (Quick.MOD + "SPH", Quick.MOD + "SPH");
				ButtonSPH.TexturePath = SPH_TexturePath;
				ButtonSPH.ToolTip = QGoTo.GetText (QGoTo.GoTo.SPH);
				ButtonSPH.OnClick += (e) => QGoTo.SPH ();
			}
			if (QSettings.Instance.EnableGoToTrackingStation && ButtonTS == null) {
				ButtonTS = ToolbarManager.Instance.add (Quick.MOD + "TS", Quick.MOD + "TS");
				ButtonTS.TexturePath = TS_TexturePath;
				ButtonTS.ToolTip = QGoTo.GetText (QGoTo.GoTo.TrackingStation);
				ButtonTS.Enabled = HighLogic.LoadedScene != GameScenes.TRACKSTATION;
				ButtonTS.OnClick += (e) => QGoTo.trackingStation ();
			}
			if (QSettings.Instance.EnableGoToRevert && ButtonRv == null) {
				ButtonRv = ToolbarManager.Instance.add (Quick.MOD + "Rv", Quick.MOD + "Rv");
				ButtonRv.TexturePath = Rv_TexturePath;
				ButtonRv.ToolTip = QGoTo.GetText (QGoTo.GoTo.Revert);
				ButtonRv.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRv.OnClick += (e) => QGoTo.Revert ();
			}
			if (QSettings.Instance.EnableGoToRevertToEditor && ButtonRvED == null) {
				ButtonRvED = ToolbarManager.Instance.add (Quick.MOD + "RvED", Quick.MOD + "RvED");
				ButtonRvED.TexturePath = RvED_TexturePath;
				ButtonRvED.ToolTip = QGoTo.GetText (QGoTo.GoTo.RevertToEditor);
				ButtonRvED.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRvED.OnClick += (e) => QGoTo.RevertToEditor ();
			}
			if (QSettings.Instance.EnableGoToRevertToSpaceCenter && ButtonRvSC == null) {
				ButtonRvSC = ToolbarManager.Instance.add (Quick.MOD + "RvSC", Quick.MOD + "RvSC");
				ButtonRvSC.TexturePath = RvSC_TexturePath;
				ButtonRvSC.ToolTip = QGoTo.GetText (QGoTo.GoTo.RevertToSpaceCenter);
				ButtonRvSC.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRvSC.OnClick += (e) => QGoTo.RevertToSpaceCenter ();
			}
			if (QSettings.Instance.EnableGoToRevert && ButtonRc == null) {
				ButtonRc = ToolbarManager.Instance.add (Quick.MOD + "Rc", Quick.MOD + "Rc");
				ButtonRc.TexturePath = Rc_TexturePath;
				ButtonRc.ToolTip = QGoTo.GetText (QGoTo.GoTo.Recover);
				ButtonRc.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRc.OnClick += (e) => QGoTo.Recover ();
			}
			if (QSettings.Instance.EnableGoToRnD && ButtonRnD == null) {
				ButtonRnD = ToolbarManager.Instance.add (Quick.MOD + "RnD", Quick.MOD + "RnD");
				ButtonRnD.TexturePath = RnD_TexturePath;
				ButtonRnD.ToolTip = QGoTo.GetText (QGoTo.GoTo.RnD);
				ButtonRnD.Visible = QGoTo.CanScienceBuilding;
				ButtonRnD.OnClick += (e) => QGoTo.RnD ();
			}
			if (QSettings.Instance.EnableGoToMissionControl && ButtonMI == null) {
				ButtonMI = ToolbarManager.Instance.add (Quick.MOD + "MI", Quick.MOD + "MI");
				ButtonMI.TexturePath = MI_TexturePath;
				ButtonMI.ToolTip = QGoTo.GetText (QGoTo.GoTo.MissionControl);
				ButtonMI.Visible = QGoTo.CanFundBuilding;
				ButtonMI.OnClick += (e) => QGoTo.missionControl ();
			}
			if (QSettings.Instance.EnableGoToAstronautComplex && ButtonAstr == null) {
				ButtonAstr = ToolbarManager.Instance.add (Quick.MOD + "Astr", Quick.MOD + "Astr");
				ButtonAstr.TexturePath = Astr_TexturePath;
				ButtonAstr.ToolTip = QGoTo.GetText (QGoTo.GoTo.AstronautComplex);
				ButtonAstr.OnClick += (e) => QGoTo.astronautComplex ();
			}
			if (QSettings.Instance.EnableGoToAdministration && ButtonAdmi == null) {
				ButtonAdmi = ToolbarManager.Instance.add (Quick.MOD + "Admi", Quick.MOD + "Admi");
				ButtonAdmi.TexturePath = Admi_TexturePath;
				ButtonAdmi.ToolTip = QGoTo.GetText (QGoTo.GoTo.Administration);
				ButtonAdmi.Visible = QGoTo.CanFundBuilding;
				ButtonAdmi.OnClick += (e) => QGoTo.administration ();
			}
			if (QSettings.Instance.EnableGoToLastVessel && ButtonLves == null) {
				ButtonLves = ToolbarManager.Instance.add (Quick.MOD + "Lves", Quick.MOD + "Lves");
				ButtonLves.TexturePath = Lves_TexturePath;
				ButtonLves.ToolTip = QGoTo.GetText (QGoTo.GoTo.LastVessel);
				ButtonLves.OnClick += (e) => QGoTo.LastVessel ();
			}
		}

		internal void Update() {
			if (!isAvailable) {
				return;
			}
			if (ButtonMain != null) {
				ButtonMain.Enabled = QGoTo.CanMainMenu;
			}
			if (ButtonSett != null) {
				ButtonSett.Enabled = QGoTo.CanMainMenu;
			}
			if (ButtonSC != null) {
				ButtonSC.Enabled = QGoTo.CanSpaceCenter;
			}
			if (ButtonVAB != null) {
				ButtonVAB.Enabled = QGoTo.CanEditor (EditorFacility.VAB);
			}
			if (ButtonSPH != null) {
				ButtonSPH.Enabled = QGoTo.CanEditor (EditorFacility.SPH);
			}
			if (ButtonTS != null) {
				ButtonTS.Enabled = QGoTo.CanTrackingStation;
			}
			if (ButtonMI != null) {
				ButtonMI.Enabled = QGoTo.CanFundBuilding && (QGoTo.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER);
			}
			if (ButtonAstr != null) {
				ButtonAstr.Enabled = QGoTo.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER;
			}
			if (ButtonAdmi != null) {
				ButtonAdmi.Enabled = QGoTo.CanFundBuilding && (QGoTo.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER);
			}
			if (ButtonRnD != null) {
				ButtonRnD.Enabled = QGoTo.CanScienceBuilding && (QGoTo.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER);
			}
			if (ButtonRv != null) {
				ButtonRv.Enabled = QGoTo.CanRevert;
			}
			if (ButtonRvED != null) {
				ButtonRvED.Enabled = QGoTo.CanRevertToEditor;
			}
			if (ButtonRvSC != null) {
				ButtonRvSC.Enabled = QGoTo.CanRevertToSpaceCenter;
			}
			if (ButtonRc != null) {
				ButtonRc.Enabled = QGoTo.CanRecover;
			}
			if (ButtonLves != null) {
				ButtonLves.Enabled = QGoTo.CanLastVessel;
			}
		}

		internal void OnDestroy() {
			if (!isAvailable) {
				return;
			}
			if (Button != null) {
				Button.Destroy ();
				Button = null;
			}
			if (ButtonConf != null) {
				ButtonConf.Destroy ();
				ButtonConf = null;
			}
			if (ButtonMain != null) {
				ButtonMain.Destroy ();
				ButtonMain = null;
			}
			if (ButtonSett != null) {
				ButtonSett.Destroy ();
				ButtonSett = null;
			}
			if (ButtonSC != null) {
				ButtonSC.Destroy ();
				ButtonSC = null;
			}
			if (ButtonVAB != null) {
				ButtonVAB.Destroy ();
				ButtonVAB = null;
			}
			if (ButtonSPH != null) {
				ButtonSPH.Destroy ();
				ButtonSPH = null;
			}
			if (ButtonTS != null) {
				ButtonTS.Destroy ();
				ButtonTS = null;
			}
			if (ButtonMI != null) {
				ButtonMI.Destroy ();
				ButtonMI = null;
			}
			if (ButtonAstr != null) {
				ButtonAstr.Destroy ();
				ButtonAstr = null;
			}
			if (ButtonAdmi != null) {
				ButtonAdmi.Destroy ();
				ButtonAdmi = null;
			}
			if (ButtonRnD != null) {
				ButtonRnD.Destroy ();
				ButtonRnD = null;
			}
			if (ButtonRv != null) {
				ButtonRv.Destroy ();
				ButtonRv = null;
			}
			if (ButtonRvED != null) {
				ButtonRvED.Destroy ();
				ButtonRvED = null;
			}
			if (ButtonRvSC != null) {
				ButtonRvSC.Destroy ();
				ButtonRvSC = null;
			}
			if (ButtonRc != null) {
				ButtonRc.Destroy ();
				ButtonRc = null;
			}
			if (ButtonLves != null) {
				ButtonLves.Destroy ();
				ButtonLves = null;
			}
		}

		internal void Reset() {
			if (Enabled) {
				Start ();
			} else {
				OnDestroy ();
			}
		}
	}
}