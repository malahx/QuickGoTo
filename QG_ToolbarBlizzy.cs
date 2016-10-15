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

using System.Collections;
using UnityEngine;

namespace QuickGoTo {
	public class QBlizzyToolbar {
	
		internal bool Enabled {
			get {
				return QSettings.Instance.BlizzyToolBar;
			}
		}
		internal string TexturePath = QuickGoTo.MOD + "/Textures/BlizzyToolBar";
		internal static string VAB_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyVAB";
		internal static string TS_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyTS";
		internal static string SPH_TexturePath = QuickGoTo.MOD + "/Textures/BlizzySPH";
		internal static string Sett_TexturePath = QuickGoTo.MOD + "/Textures/BlizzySett";
		internal static string SC_TexturePath = QuickGoTo.MOD + "/Textures/BlizzySC";
		internal static string RvSC_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyRvSC";
		internal static string RvED_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyRvED";
		internal static string Rv_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyRv";
		internal static string RnD_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyRnD";
		internal static string Rc_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyRc";
		internal static string MI_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyMI";
		internal static string Main_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyMain";
		internal static string Lves_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyLves";
		internal static string Astr_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyAstr";
		internal static string Admi_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyAdmi";
		internal static string Conf_TexturePath = QuickGoTo.MOD + "/Textures/BlizzyConf";

		private Coroutine CoroutineEach;

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


		private void OnClick () {
			QGoTo.Instance.ToggleGoTo ();
		}


		private void StartEach() {
			if (CoroutineEach != null) {
				return;
			}
			CoroutineEach = QGoTo.Instance.StartCoroutine (UpdateEach ());
			QuickGoTo.Log ("StartEach", "QBlizzyToolbar");
		}

		private void StopEach() {
			if (CoroutineEach == null) {
				return;
			}
			QGoTo.Instance.StopCoroutine (UpdateEach ());
			CoroutineEach = null;
			QuickGoTo.Log ("StopEach", "QBlizzyToolbar");
		}

		private void RestartEach() {
			if (CoroutineEach == null) {
				return;
			}
			StopEach ();
			StartEach ();
			QuickGoTo.Log ("RestartEach", "QBlizzyToolbar");
		}

		private IEnumerator UpdateEach () {
			yield return new WaitForSeconds (1);
			Coroutine _coroutine = CoroutineEach;
			QuickGoTo.Log ("StartUpdateEach " + _coroutine.GetHashCode(), "QBlizzyToolbar");
			while (_coroutine == CoroutineEach) {
				Update ();
				yield return new WaitForSeconds (1);
			}
			QuickGoTo.Log ("EndUpdateEach " + _coroutine.GetHashCode(), "QBlizzyToolbar");
		}

		internal void Init	() {
			if (!HighLogic.LoadedSceneIsGame || !isAvailable || !Enabled) {
				return;
			}
			if (Button != null) {
				Button = ToolbarManager.Instance.add (QuickGoTo.MOD, QuickGoTo.MOD);
				Button.TexturePath = TexturePath;
				Button.ToolTip = QuickGoTo.MOD;
				Button.OnClick += (e) => OnClick ();
			}
			if ((QSettings.Instance.EnableSettings || HighLogic.LoadedScene == GameScenes.SPACECENTER) && ButtonConf == null) {
				ButtonConf = ToolbarManager.Instance.add (QuickGoTo.MOD + "Conf", QuickGoTo.MOD + "Conf");
				ButtonConf.TexturePath = Conf_TexturePath;
				ButtonConf.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.Configurations);
				ButtonConf.OnClick += (e) => QGoTo.Instance.Settings ();
			}
			if (QSettings.Instance.EnableGoToMainMenu && ButtonMain == null) {
				ButtonMain = ToolbarManager.Instance.add (QuickGoTo.MOD + "Main", QuickGoTo.MOD + "Main");
				ButtonMain.TexturePath = Main_TexturePath;
				ButtonMain.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.MainMenu);
				ButtonMain.OnClick += (e) => QGoTo.Instance.mainMenu ();
			}
			if (QSettings.Instance.EnableGoToSettings && ButtonSett == null) {
				ButtonSett = ToolbarManager.Instance.add (QuickGoTo.MOD + "Sett", QuickGoTo.MOD + "Sett");
				ButtonSett.TexturePath = Sett_TexturePath;
				ButtonSett.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.Settings);
				ButtonSett.OnClick += (e) => QGoTo.Instance.settings ();
			}
			if (QSettings.Instance.EnableGoToSpaceCenter && ButtonSC == null) {
				ButtonSC = ToolbarManager.Instance.add (QuickGoTo.MOD + "SC", QuickGoTo.MOD + "SC");
				ButtonSC.TexturePath = SC_TexturePath;
				ButtonSC.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.SpaceCenter);
				ButtonSC.Enabled = HighLogic.LoadedScene != GameScenes.SPACECENTER;
				ButtonSC.OnClick += (e) => QGoTo.Instance.spaceCenter ();
			}
			if (QSettings.Instance.EnableGoToVAB && ButtonVAB == null) {
				ButtonVAB = ToolbarManager.Instance.add (QuickGoTo.MOD + "VAB", QuickGoTo.MOD + "VAB");
				ButtonVAB.TexturePath = VAB_TexturePath;
				ButtonVAB.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.VAB);
				ButtonVAB.OnClick += (e) => QGoTo.Instance.VAB ();
			}
			if (QSettings.Instance.EnableGoToSPH && ButtonSPH == null) {
				ButtonSPH = ToolbarManager.Instance.add (QuickGoTo.MOD + "SPH", QuickGoTo.MOD + "SPH");
				ButtonSPH.TexturePath = SPH_TexturePath;
				ButtonSPH.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.SPH);
				ButtonSPH.OnClick += (e) => QGoTo.Instance.SPH ();
			}
			if (QSettings.Instance.EnableGoToTrackingStation && ButtonTS == null) {
				ButtonTS = ToolbarManager.Instance.add (QuickGoTo.MOD + "TS", QuickGoTo.MOD + "TS");
				ButtonTS.TexturePath = TS_TexturePath;
				ButtonTS.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.TrackingStation);
				ButtonTS.Enabled = HighLogic.LoadedScene != GameScenes.TRACKSTATION;
				ButtonTS.OnClick += (e) => QGoTo.Instance.trackingStation ();
			}
			if (QSettings.Instance.EnableGoToRevert && ButtonRv == null) {
				ButtonRv = ToolbarManager.Instance.add (QuickGoTo.MOD + "Rv", QuickGoTo.MOD + "Rv");
				ButtonRv.TexturePath = Rv_TexturePath;
				ButtonRv.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.Revert);
				ButtonRv.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRv.OnClick += (e) => QGoTo.Instance.Revert ();
			}
			if (QSettings.Instance.EnableGoToRevertToEditor && ButtonRvED == null) {
				ButtonRvED = ToolbarManager.Instance.add (QuickGoTo.MOD + "RvED", QuickGoTo.MOD + "RvED");
				ButtonRvED.TexturePath = RvED_TexturePath;
				ButtonRvED.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.RevertToEditor);
				ButtonRvED.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRvED.OnClick += (e) => QGoTo.Instance.RevertToEditor ();
			}
			if (QSettings.Instance.EnableGoToRevertToSpaceCenter && ButtonRvSC == null) {
				ButtonRvSC = ToolbarManager.Instance.add (QuickGoTo.MOD + "RvSC", QuickGoTo.MOD + "RvSC");
				ButtonRvSC.TexturePath = RvSC_TexturePath;
				ButtonRvSC.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.RevertToSpaceCenter);
				ButtonRvSC.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRvSC.OnClick += (e) => QGoTo.Instance.RevertToSpaceCenter ();
			}
			if (QSettings.Instance.EnableGoToRevert && ButtonRc == null) {
				ButtonRc = ToolbarManager.Instance.add (QuickGoTo.MOD + "Rc", QuickGoTo.MOD + "Rc");
				ButtonRc.TexturePath = Rc_TexturePath;
				ButtonRc.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.Recover);
				ButtonRc.Visibility = new GameScenesVisibility (GameScenes.FLIGHT);
				ButtonRc.OnClick += (e) => QGoTo.Instance.Recover ();
			}
			if (QSettings.Instance.EnableGoToRnD && ButtonRnD == null) {
				ButtonRnD = ToolbarManager.Instance.add (QuickGoTo.MOD + "RnD", QuickGoTo.MOD + "RnD");
				ButtonRnD.TexturePath = RnD_TexturePath;
				ButtonRnD.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.RnD);
				ButtonRnD.Visible = QGoTo.Instance.CanScienceBuilding;
				ButtonRnD.OnClick += (e) => QGoTo.Instance.RnD ();
			}
			if (QSettings.Instance.EnableGoToMissionControl && ButtonMI == null) {
				ButtonMI = ToolbarManager.Instance.add (QuickGoTo.MOD + "MI", QuickGoTo.MOD + "MI");
				ButtonMI.TexturePath = MI_TexturePath;
				ButtonMI.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.MissionControl);
				ButtonMI.Visible = QGoTo.Instance.CanFundBuilding;
				ButtonMI.OnClick += (e) => QGoTo.Instance.missionControl ();
			}
			if (QSettings.Instance.EnableGoToAstronautComplex && ButtonAstr == null) {
				ButtonAstr = ToolbarManager.Instance.add (QuickGoTo.MOD + "Astr", QuickGoTo.MOD + "Astr");
				ButtonAstr.TexturePath = Astr_TexturePath;
				ButtonAstr.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.AstronautComplex);
				ButtonAstr.OnClick += (e) => QGoTo.Instance.astronautComplex ();
			}
			if (QSettings.Instance.EnableGoToAdministration && ButtonAdmi == null) {
				ButtonAdmi = ToolbarManager.Instance.add (QuickGoTo.MOD + "Admi", QuickGoTo.MOD + "Admi");
				ButtonAdmi.TexturePath = Admi_TexturePath;
				ButtonAdmi.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.Administration);
				ButtonAdmi.Visible = QGoTo.Instance.CanFundBuilding;
				ButtonAdmi.OnClick += (e) => QGoTo.Instance.administration ();
			}
			if (QSettings.Instance.EnableGoToLastVessel && ButtonLves == null) {
				ButtonLves = ToolbarManager.Instance.add (QuickGoTo.MOD + "Lves", QuickGoTo.MOD + "Lves");
				ButtonLves.TexturePath = Lves_TexturePath;
				ButtonLves.ToolTip = QGoTo.Instance.GetText (QGoTo.GoTo.LastVessel);
				ButtonLves.OnClick += (e) => QGoTo.Instance.LastVessel ();
			}
			if (HighLogic.LoadedSceneIsFlight) {
				StartEach ();
			}
			QuickGoTo.Log ("Init", "QBlizzyToolbar");
		}

		internal void Update() {
			if (!isAvailable) {
				return;
			}
			if (ButtonMain != null) {
				ButtonMain.Enabled = QGoTo.Instance.CanMainMenu;
			}
			if (ButtonSett != null) {
				ButtonSett.Enabled = QGoTo.Instance.CanMainMenu;
			}
			if (ButtonSC != null) {
				ButtonSC.Enabled = QGoTo.Instance.CanSpaceCenter;
			}
			if (ButtonVAB != null) {
				ButtonVAB.Enabled = QGoTo.Instance.CanEditor (EditorFacility.VAB);
			}
			if (ButtonSPH != null) {
				ButtonSPH.Enabled = QGoTo.Instance.CanEditor (EditorFacility.SPH);
			}
			if (ButtonTS != null) {
				ButtonTS.Enabled = QGoTo.Instance.CanTrackingStation;
			}
			if (ButtonMI != null) {
				ButtonMI.Enabled = QGoTo.Instance.CanFundBuilding && (QGoTo.Instance.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER);
			}
			if (ButtonAstr != null) {
				ButtonAstr.Enabled = QGoTo.Instance.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER;
			}
			if (ButtonAdmi != null) {
				ButtonAdmi.Enabled = QGoTo.Instance.CanFundBuilding && (QGoTo.Instance.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER);
			}
			if (ButtonRnD != null) {
				ButtonRnD.Enabled = QGoTo.Instance.CanScienceBuilding && (QGoTo.Instance.CanSpaceCenter || HighLogic.LoadedScene == GameScenes.SPACECENTER);
			}
			if (ButtonRv != null) {
				ButtonRv.Enabled = QGoTo.Instance.CanRevert;
			}
			if (ButtonRvED != null) {
				ButtonRvED.Enabled = QGoTo.Instance.CanRevertToEditor;
			}
			if (ButtonRvSC != null) {
				ButtonRvSC.Enabled = QGoTo.Instance.CanRevertToSpaceCenter;
			}
			if (ButtonRc != null) {
				ButtonRc.Enabled = QGoTo.Instance.CanRecover;
			}
			if (ButtonLves != null) {
				ButtonLves.Enabled = QGoTo.Instance.CanLastVessel;
			}
		}

		internal void Destroy() {
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
			StopEach ();
			QuickGoTo.Log ("Destroy", "QBlizzyToolbar");
		}

		internal void Reset() {
			if (Enabled) {
				Init ();
			} else {
				Destroy ();
			}
			QuickGoTo.Log ("Reset", "QBlizzyToolbar");
		}
	}
}