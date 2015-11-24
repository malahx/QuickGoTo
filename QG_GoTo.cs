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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickGoTo {
	public class QGoTo : MonoBehaviour {

		internal enum GoTo {
			None,
			TrackingStation,
			SpaceCenter,
			MissionControl,
			Administration,
			RnD,
			AstronautComplex,
			VAB,
			SPH,
			LastVessel,
			Recover,
			Revert,
			RevertToEditor,
			RevertToSpaceCenter,
			MainMenu,
			Settings,
			Configurations
		}	

		internal static string GetText(GoTo goTo, bool force = false) {
			switch (goTo) {
			case GoTo.TrackingStation:
				return "Go to the Tracking Station";
			case GoTo.SpaceCenter:
				return "Go to the Space Center";
			case GoTo.MissionControl:
				return "Go to the Mission Control";
			case GoTo.Administration:
				return "Go to the Administration";
			case GoTo.RnD:
				return "Go to the Research and Dev.";
			case GoTo.AstronautComplex:
				return "Go to the Astronaut Complex";
			case GoTo.VAB:
				return "Go to the Vehicle Assembly";
			case GoTo.SPH:
				return "Go to the Space Plane Hangar";
			case GoTo.LastVessel:
				QData _lastVessel = QGoTo.LastVesselLastIndex ();
				return string.Format ("Go to the {0}", (_lastVessel != null && !force ? "vessel: " + _lastVessel.protoVessel.vesselName : "last Vessel"));
			case GoTo.Recover:
				return "Recover";
			case GoTo.Revert:
				return "Revert to Launch";
			case GoTo.RevertToEditor:
				return "Revert to Editor";
			case GoTo.RevertToSpaceCenter:
				return "Revert to SpaceCenter";
			case GoTo.MainMenu:
				return "Go to The Main Menu";
			case GoTo.Settings:
				return "Go to the Settings";
			case GoTo.Configurations:
				return "QuickGoTo: Settings";
			}
			return string.Empty;
		}

		[KSPField(isPersistant = true)] internal static List<QData> LastVessels = new List<QData>();
		[KSPField(isPersistant = true)] private static GoTo SavedGoTo = GoTo.None;

		private static string SaveGame = "persistent";

		public static bool CanMainMenu {
			get {
				if (!HighLogic.LoadedSceneIsFlight) {
					return true;
				}
				if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null && HighLogic.CurrentGame.Parameters.Flight.CanLeaveToMainMenu) {
					return FlightGlobals.ClearToSave() == ClearToSaveStatus.CLEAR;
				}
				return false;
			}
		}

		public static bool CanTrackingStation {
			get {
				if (HighLogic.LoadedSceneIsGame) {
					if (HighLogic.LoadedScene != GameScenes.TRACKSTATION) {
						if (!HighLogic.LoadedSceneIsFlight) {
							return true;
						}
						if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null && HighLogic.CurrentGame.Parameters.Flight.CanLeaveToTrackingStation) {
							return FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR;
						}
					}
				}
				return false;
			}
		}

		public static bool CanSpaceCenter {
			get {
				if (HighLogic.LoadedSceneIsGame) {
					if (HighLogic.LoadedScene != GameScenes.SPACECENTER) {
						if (!HighLogic.LoadedSceneIsFlight) {
							return true;
						}
						if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null && HighLogic.CurrentGame.Parameters.Flight.CanLeaveToSpaceCenter) {
							return FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR;
						}
					}
				}
				return false;
			}
		}

		public static bool CanRecover {
			get {
				if (HighLogic.LoadedSceneIsFlight) {
					if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null) {
						if (FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR && FlightGlobals.ActiveVessel.IsRecoverable) {
							return true;
						}
					}
				}
				return false;
			}
		}

		public static bool CanRevert {
			get {
				if (HighLogic.LoadedSceneIsFlight) {
					if (FlightGlobals.ready && HighLogic.CurrentGame.Parameters.Flight.CanRestart && FlightDriver.CanRevertToPostInit && FlightDriver.PostInitState != null) {
						return true;
					}
				}
				return false;
			}
		}

		public static bool CanRevertToEditor {
			get {
				if (HighLogic.LoadedSceneIsFlight) {
					if (FlightGlobals.ready && HighLogic.CurrentGame.Parameters.Flight.CanLeaveToEditor && FlightDriver.CanRevertToPrelaunch && FlightDriver.PreLaunchState != null && ShipConstruction.ShipType != EditorFacility.None) {
						return true;
					}
				}
				return false;
			}
		}

		public static bool CanRevertToSpaceCenter {
			get {
				if (HighLogic.LoadedSceneIsFlight) {
					if (FlightGlobals.ready && HighLogic.CurrentGame.Parameters.Flight.CanLeaveToSpaceCenter && FlightDriver.CanRevertToPrelaunch && FlightDriver.PreLaunchState != null && ShipConstruction.ShipType != EditorFacility.None) {
						return true;
					}
				}
				return false;
			}
		}

		public static bool CanEditor(EditorFacility editorFacility) {
			if (HighLogic.LoadedSceneIsGame) {
				if (HighLogic.LoadedSceneIsEditor) {
					if (EditorDriver.fetch != null && EditorLogic.fetch != null) {
						if (EditorDriver.editorFacility != editorFacility) {
							return true;
						}
					}
					return false;
				}
				if (!HighLogic.LoadedSceneIsFlight) {
					return true;
				} 
				if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null && HighLogic.CurrentGame.Parameters.Flight.CanLeaveToEditor) {
					return FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR;
				}
			}
			return false;
		}

		public static bool CanLastVessel {
			get {
				if (HighLogic.LoadedSceneIsGame) {
					QData _lastVessel = LastVesselLastIndex ();
					if (_lastVessel != null) {
						ProtoVessel _pVessel = _lastVessel.protoVessel;
						if (_pVessel != null) {
							if (pVesselExists (_pVessel)) {
								if (!HighLogic.LoadedSceneIsFlight) {
									return true;
								} else {
									if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null) {
										Guid _guid = _pVessel.vesselID;
										if (_guid != Guid.Empty) {
											Vessel _vessel = FlightGlobals.Vessels.FindLast (v => v.id == _guid);
											if (_vessel != null) {
												if (!_vessel.isActiveVessel && (!_vessel.loaded && HighLogic.CurrentGame.Parameters.Flight.CanSwitchVesselsFar) || (_vessel.loaded && HighLogic.CurrentGame.Parameters.Flight.CanSwitchVesselsNear)) {
													return FlightGlobals.ActiveVessel.IsClearToSave () == ClearToSaveStatus.CLEAR;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				return false;
			}
		}

		public static bool CanFundBuilding {
			get {
				return HighLogic.CurrentGame.Mode == Game.Modes.CAREER;
			}
		}

		public static bool CanScienceBuilding {
			get {
				return HighLogic.CurrentGame.Mode == Game.Modes.CAREER || HighLogic.CurrentGame.Mode == Game.Modes.SCIENCE_SANDBOX;
			}
		}

		public static bool isMissionControl {
			get {
				return MissionControl.Instance != null;
			}
		}

		public static bool isAdministration {
			get {
				return Administration.Instance != null;
			}
		}

		public static bool isAstronautComplex {
			get;
			internal set;
		}

		public static bool isRnD {
			get;
			internal set;
		}

		public static bool isBat {
			get {
				return isMissionControl || isAdministration || isAstronautComplex || isRnD;
			}
		}

		public static bool pVesselExists(ProtoVessel pvessel) {
			FlightState _flightState = HighLogic.CurrentGame.flightState;
			if (_flightState != null) {
				return _flightState.protoVessels.Exists (pv => pv.vesselID == pvessel.vesselID);
			}
			return false;
		}

		internal static QData LastVesselLastIndex() {
			int _index;
			return LastVesselLastIndex(out _index);
		}

		internal static QData LastVesselLastIndex(out int index) {
			index = -1;
			QData _lastVessel = null;
			while (LastVessels.Count > 0) {
				index = LastVessels.Count - 1;
				_lastVessel = LastVessels [index];
				if (_lastVessel != null) {
					ProtoVessel _pVessel = _lastVessel.protoVessel;
					if (_pVessel != null) {
						if (pVesselExists (_pVessel)) {
							if (_lastVessel.idx != -1) {
								break;
							}
						}					
					}
				}
				LastVessels.RemoveAt (index);
				QuickGoTo.Warning ("Remove a vessel from the last Vessels");
			}
			return _lastVessel;
		}

		internal static IEnumerator PostInit() {
			while (ApplicationLauncher.Instance == null) {
				yield return 0;
			}
			if (HighLogic.CurrentGame.Mode == Game.Modes.CAREER) { 
				while (Funding.Instance == null) {
					yield return 0;
				}
				while (Reputation.Instance == null) {
					yield return 0;
				}
				while (Contracts.ContractSystem.Instance == null) {
					yield return 0;
				}
				while (Contracts.Agents.AgentList.Instance == null) {
					yield return 0;
				}
				while (FinePrint.ContractDefs.Instance == null) {
					yield return 0;
				}
				while (Strategies.StrategySystem.Instance == null) {
					yield return 0;
				}
				while (ScenarioUpgradeableFacilities.Instance == null) {
					yield return 0;
				}
				while (ContractsApp.Instance == null) {
					yield return 0;
				}
				while (CurrencyWidget.FindObjectOfType (typeof(CurrencyWidget)) == null) {
					yield return 0;
				}
			}
			if (HighLogic.CurrentGame.Mode == Game.Modes.CAREER || HighLogic.CurrentGame.Mode == Game.Modes.SCIENCE_SANDBOX) {
				while (ResearchAndDevelopment.Instance == null) {
					yield return 0;
				}
			}
			QGoTo.PostInitSC ();
		}

		internal static void PostInitSC() {
			if (LastVessels.Count < 1) {
				FlightState _flightState = HighLogic.CurrentGame.flightState;
				if (_flightState != null) {
					List<ProtoVessel> _pVessels = _flightState.protoVessels;
					foreach (ProtoVessel _pVessel in _pVessels) {
						if (_pVessel != null) {
							AddLastVessel (_pVessel);
						}
					}
				}
			} else {
				List<QData> _lastVessels = LastVessels;
				LastVessels = new List<QData> ();
				foreach (QData _lastVessel in _lastVessels) {
					if (pVesselExists (_lastVessel.protoVessel)) {
						LastVessels.Add (_lastVessel);
					} else {
						QuickGoTo.Warning ("Remove from the last Vessels: " + _lastVessel.protoVessel.vesselName);
					}
				}
			}
			if (SavedGoTo != GoTo.None) {
				switch (SavedGoTo) {
				case GoTo.Administration:
					administration ();
					break;
				case GoTo.AstronautComplex:
					astronautComplex ();
					break;
				case GoTo.RnD:
					RnD ();
					break;
				case GoTo.MissionControl:
					missionControl ();
					break;
				}
				SavedGoTo = GoTo.None;
			}
		}

		internal static void AddLastVessel(ProtoVessel pVessel) {
			QData _lastVessel = LastVesselLastIndex ();
			if (_lastVessel!= null) {
				if (_lastVessel.protoVessel.vesselID == pVessel.vesselID) {
					QuickGoTo.Warning ("Kept the last Vessel: " + pVessel.vesselName);
					return;
				}
			}
			if (pVessel.vesselType == VesselType.Unknown || pVessel.vesselType == VesselType.SpaceObject || pVessel.vesselType == VesselType.Debris) {
				//Quick.Warning (string.Format ("Can't save the last Vessel: ({0}) {1}", pVessel.vesselType.ToString (), pVessel.vesselName));
				return;
			}
			LastVessels.Add (new QData (pVessel));
			QuickGoTo.Warning (string.Format("Saved the last Vessel: ({0}){1}", LastVessels.Count, pVessel.vesselName));
			if (LastVessels.Count > 10) {
				QuickGoTo.Warning ("Delete the first vessel from last Vessel: " + LastVessels[0].protoVessel.vesselName);
				LastVessels.RemoveAt (0);
			}
			//Quick.Warning ("Last Vessel has keep " + LastVessels.Count + " vessels");
		}

		public static void mainMenu() {
			SavedGoTo = GoTo.None;
			if (CanMainMenu) {
				GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
				HighLogic.LoadScene (GameScenes.MAINMENU);
				InputLockManager.ClearControlLocks ();
				QuickGoTo.Log (GetText (GoTo.MainMenu));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.MainMenu));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.MainMenu), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void settings() {
			SavedGoTo = GoTo.None;
			if (CanMainMenu) {
				GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
				HighLogic.LoadScene (GameScenes.SETTINGS);
				InputLockManager.ClearControlLocks ();
				QuickGoTo.Log (GetText (GoTo.Settings));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.Settings));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.Settings), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void trackingStation() {
			SavedGoTo = GoTo.None;
			if (CanTrackingStation) {
				GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
				HighLogic.LoadScene	(GameScenes.LOADINGBUFFER);
				HighLogic.LoadScene (GameScenes.TRACKSTATION);
				InputLockManager.ClearControlLocks ();
				QuickGoTo.Log (GetText (GoTo.TrackingStation));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.TrackingStation));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.TrackingStation), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		private static void gotoSpaceCenter(GameBackup gameBackup = null) {
			if (gameBackup == null) {
				GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
			} else {
				GamePersistence.SaveGame (gameBackup, SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
			}
			HighLogic.LoadScene (GameScenes.SPACECENTER);
			InputLockManager.ClearControlLocks ();
		}

		public static void spaceCenter() {
			SavedGoTo = GoTo.None;
			if (CanSpaceCenter) {
				gotoSpaceCenter ();
				QuickGoTo.Log (GetText (GoTo.SpaceCenter));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.SpaceCenter));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.SpaceCenter), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		internal static void ClearSpaceCenter() {
			if (RUIPrefabLauncher.Instance != null) {
				GameEvents.onGUILaunchScreenDespawn.Fire ();
				QuickGoTo.Log ("Clear LaunchScreen");
			}
			if (isMissionControl) {
				GameEvents.onGUIMissionControlDespawn.Fire ();
				QuickGoTo.Log ("Clear MissionControl");
			}
			if (isAdministration) {
				GameEvents.onGUIAdministrationFacilityDespawn.Fire ();
				QuickGoTo.Log ("Clear Administration");
			}
			if (isAstronautComplex) {
				CMAstronautComplex _CMAstronautComplex = (CMAstronautComplex)CMAstronautComplex.FindObjectOfType (typeof(CMAstronautComplex));
				POINTER_INFO _ptr = new POINTER_INFO ();
				_ptr.evt = POINTER_INFO.INPUT_EVENT.TAP;
				_CMAstronautComplex.ButtonExit (ref _ptr);
				GameEvents.onGUIAstronautComplexDespawn.Fire ();
				QuickGoTo.Log ("Clear AstronautComplex");
			}
			if (isRnD) {
				GameEvents.onGUIRnDComplexDespawn.Fire ();
				QuickGoTo.Log ("Clear Research And Development");
			}
			InputLockManager.ClearControlLocks ();
			QuickGoTo.Log ("Clear SpaceCenter");
		}

		public static void missionControl() {
			SavedGoTo = GoTo.None;
			if (HighLogic.LoadedSceneIsGame) {
				if (CanFundBuilding) {
					if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
						ClearSpaceCenter ();
						GameEvents.onGUIMissionControlSpawn.Fire ();
						InputLockManager.ClearControlLocks ();
						QuickGoTo.Log (GetText(GoTo.MissionControl));
						return;
					}
					if (CanSpaceCenter) {
						SavedGoTo = GoTo.MissionControl;
						gotoSpaceCenter ();
						return;
					}
				}
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.MissionControl));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.MissionControl), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void administration() {
			SavedGoTo = GoTo.None;
			if (HighLogic.LoadedSceneIsGame) {
				if (CanFundBuilding) {
					if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
						ClearSpaceCenter ();
						GameEvents.onGUIAdministrationFacilitySpawn.Fire ();
						InputLockManager.ClearControlLocks ();
						QuickGoTo.Log (GetText(GoTo.Administration));
						return;
					}
					if (CanSpaceCenter) {
						SavedGoTo = GoTo.Administration;
						gotoSpaceCenter ();
						return;
					}
				}
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.Administration));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.Administration), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void RnD() {
			SavedGoTo = GoTo.None;
			if (HighLogic.LoadedSceneIsGame) {
				if (CanScienceBuilding) {
					if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
						ClearSpaceCenter ();
						GameEvents.onGUIRnDComplexSpawn.Fire ();
						InputLockManager.ClearControlLocks ();
						QuickGoTo.Log (GetText(GoTo.RnD));
						return;
					}
					if (CanSpaceCenter) {
						SavedGoTo = GoTo.RnD;
						gotoSpaceCenter ();
						return;
					}
				}
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.RnD));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.RnD), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void astronautComplex() {
			SavedGoTo = GoTo.None;
			if (HighLogic.LoadedSceneIsGame) {
				if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
					ClearSpaceCenter ();
					GameEvents.onGUIAstronautComplexSpawn.Fire ();
					InputLockManager.ClearControlLocks ();
					QuickGoTo.Log (GetText(GoTo.AstronautComplex));
					return;
				}
				if (CanSpaceCenter) {
					SavedGoTo = GoTo.AstronautComplex;
					gotoSpaceCenter ();
					return;
				}
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.AstronautComplex));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.AstronautComplex), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		private static void gotoVAB() {
			GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
			EditorFacility editorFacility = EditorFacility.None;
			if (ShipConstruction.ShipConfig != null) {
				editorFacility = ShipConstruction.ShipType;
			}
			EditorDriver.StartupBehaviour = (editorFacility == EditorFacility.VAB ? EditorDriver.StartupBehaviours.LOAD_FROM_CACHE : EditorDriver.StartupBehaviours.START_CLEAN);
			EditorDriver.StartEditor (EditorFacility.VAB);
			InputLockManager.ClearControlLocks ();
		}

		public static void VAB() {
			SavedGoTo = GoTo.None;
			if (CanEditor(EditorFacility.VAB)) {
				gotoVAB ();
				QuickGoTo.Log (GetText (GoTo.VAB));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.VAB));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.VAB), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		private static void gotoSPH() {
			GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
			EditorFacility editorFacility = EditorFacility.None;
			if (ShipConstruction.ShipConfig != null) {
				editorFacility = ShipConstruction.ShipType;
			}
			EditorDriver.StartupBehaviour = (editorFacility == EditorFacility.SPH ? EditorDriver.StartupBehaviours.LOAD_FROM_CACHE : EditorDriver.StartupBehaviours.START_CLEAN);
			EditorDriver.StartEditor (EditorFacility.SPH);
			InputLockManager.ClearControlLocks ();
		}

		public static void SPH() {
			SavedGoTo = GoTo.None;
			if (CanEditor (EditorFacility.SPH)) {
				gotoSPH ();
				QuickGoTo.Log (GetText (GoTo.SPH));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.SPH));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.SPH), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void Recover() {
			SavedGoTo = GoTo.None;
			if (CanRecover) {
				GameEvents.OnVesselRecoveryRequested.Fire (FlightGlobals.ActiveVessel);
				InputLockManager.ClearControlLocks ();
				QuickGoTo.Log (GetText (GoTo.Recover));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.Recover));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.Recover), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void Revert() {
			SavedGoTo = GoTo.None;
			if (CanRevert) {
				FlightDriver.RevertToLaunch ();
				InputLockManager.ClearControlLocks ();
				QuickGoTo.Log (GetText (GoTo.Revert));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText (GoTo.Revert));
			ScreenMessages.PostScreenMessage ("You can't " + GetText (GoTo.Revert), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void RevertToEditor() {
			SavedGoTo = GoTo.None;
			if (CanRevertToEditor) {
				FlightDriver.RevertToPrelaunch (ShipConstruction.ShipType);
				InputLockManager.ClearControlLocks ();
				QuickGoTo.Log (GetText (GoTo.RevertToEditor));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText (GoTo.RevertToEditor));
			ScreenMessages.PostScreenMessage ("You can't " + GetText (GoTo.RevertToEditor), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void RevertToSpaceCenter() {
			SavedGoTo = GoTo.None;
			if (CanRevertToSpaceCenter) {
				gotoSpaceCenter (FlightDriver.PreLaunchState);
				QuickGoTo.Log (GetText (GoTo.RevertToSpaceCenter));
				return;
			}
			QuickGoTo.Warning ("You can't " + GetText (GoTo.RevertToSpaceCenter));
			ScreenMessages.PostScreenMessage ("You can't " + GetText (GoTo.RevertToSpaceCenter), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void LastVessel() {
			SavedGoTo = GoTo.None;
			if (CanLastVessel) {
				int _index = -1;
				QData _lastVessel = LastVesselLastIndex (out _index);
				if (_lastVessel != null) {
					int _idx = _lastVessel.idx;
					if (_idx != -1) {
						string _saveGame = GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
						QuickGoTo.Log (GetText (GoTo.LastVessel));
						FlightDriver.StartAndFocusVessel (_saveGame, _idx);
						InputLockManager.ClearControlLocks ();
						LastVessels.RemoveAt (_index);
						QuickGoTo.Warning ("Remove from the last Vessels: " + _lastVessel.protoVessel.vesselName);
						return;
					}
				}
			}
			QuickGoTo.Warning ("You can't " + GetText(GoTo.LastVessel));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.LastVessel), 10, ScreenMessageStyle.UPPER_RIGHT);
		}
	}
}