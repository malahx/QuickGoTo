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
			/*RecoverToVAB,
			RecoverToSPH,*/
			Revert,
			RevertToEditor,
			RevertToSpaceCenter,
			MainMenu,
			Settings
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
				return string.Format ("Go to the {0}", (QGoTo.LastVesselLastIndex != null && !force ? QGoTo.LastVesselLastIndex.protoVessel.vesselName : "last Vessel"));
			case GoTo.Recover:
				return "Recover";
			/*case GoTo.RecoverToVAB:
				return "Recover to the VAB";
			case GoTo.RecoverToSPH:
				return "Recover to the SPH";*/
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
					if (FlightGlobals.ClearToSave() == ClearToSaveStatus.CLEAR) {
						return true;
					}
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
							if (FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR) {
								return true;
							}
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
							if (FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR) {
								return true;
							}
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

		/*public static bool CanRecoverpVessel(ProtoVessel pvessel) {
			if (HighLogic.LoadedSceneIsEditor) {
				if (pvessel != null) {
					if (pvessel.landed || pvessel.splashed) {
						Quick.Warning ("pvessel is landed or splashed");
						return true;
					}
				}
			}
			return false;
		}*/

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
					if (FlightGlobals.ready && HighLogic.CurrentGame.Parameters.Flight.CanRestart && FlightDriver.CanRevertToPrelaunch && FlightDriver.PreLaunchState != null && ShipConstruction.ShipType != EditorFacility.None) {
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
					if (FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR) {
						return true;
					}
				}
			}
			return false;
		}

		public static bool CanLastVessel {
			get {
				if (HighLogic.LoadedSceneIsGame) {
					QData _lastVessel = LastVesselLastIndex;
					if (_lastVessel != null) {
						if (pVesselExists (_lastVessel.protoVessel)) {
							if (!HighLogic.LoadedSceneIsFlight) {
								return true;
							} else {
								if (FlightGlobals.ready && FlightGlobals.ActiveVessel != null && _lastVessel != null) {
									Vessel _vessel = _lastVessel.protoVessel.vesselRef;
									if (!_vessel.isActiveVessel && (!_vessel.loaded && HighLogic.CurrentGame.Parameters.Flight.CanSwitchVesselsFar) || (_vessel.loaded && HighLogic.CurrentGame.Parameters.Flight.CanSwitchVesselsNear)) {
										if (FlightGlobals.ActiveVessel.IsClearToSave() == ClearToSaveStatus.CLEAR) {
											return true;
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

		public static bool isAstronautComplex() {
			CMAstronautComplex _CMAstronautComplex;
			return isAstronautComplex(out _CMAstronautComplex);
		}

		public static bool isAstronautComplex(out CMAstronautComplex CMAstronautComplex) {
			CMAstronautComplex = (CMAstronautComplex)CMAstronautComplex.FindObjectOfType (typeof(CMAstronautComplex));
			return CMAstronautComplex != null;
		}

		public static bool isRnD {
			get {
				RDSceneSpawner _RDSceneSpawner = (RDSceneSpawner)RDSceneSpawner.FindObjectOfType (typeof(RDSceneSpawner));
				return _RDSceneSpawner != null;
			}
		}

		public static bool pVesselExists(ProtoVessel pvessel) {
			return HighLogic.CurrentGame.flightState.protoVessels.Exists(pv => pv.vesselID == pvessel.vesselID);
		}

		internal static QData LastVesselLastIndex {
			get {
				if (LastVessels.Count < 1) {
					return null;
				}
				return LastVessels [LastVessels.Count - 1];
			}
		}

		internal static void PostInitSC() {
			if (LastVessels.Count == 0) {
				List<ProtoVessel> _pVessels = HighLogic.CurrentGame.flightState.protoVessels;
				foreach (ProtoVessel _pVessel in _pVessels) {
					AddLastVessel (_pVessel);
				}
			} else {
				List<QData> _lastVessels = LastVessels;
				foreach (QData _lastVessel in _lastVessels) {
					if (!pVesselExists (_lastVessel.protoVessel)) {
						LastVessels.Remove (_lastVessel);
						Quick.Warning ("Remove from the last Vessels: " + _lastVessel.protoVessel.vesselName);
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
			if (LastVesselLastIndex != null) {
				if (LastVesselLastIndex.protoVessel.vesselID == pVessel.vesselID) {
					Quick.Warning ("Kept the last Vessel: " + pVessel.vesselName);
					return;
				}
			}
			if (pVessel.vesselType == VesselType.Unknown || pVessel.vesselType == VesselType.SpaceObject || pVessel.vesselType == VesselType.Debris) {
				Quick.Warning (string.Format ("Can't save the last Vessel: ({0}) {1}", pVessel.vesselType.ToString (), pVessel.vesselName));
				return;
			}
			LastVessels.Add (new QData (pVessel));
			Quick.Warning ("Saved the last Vessel: " + pVessel.vesselName);
			if (LastVessels.Count > 10) {
				Quick.Warning ("Delete the first vessel from last Vessel: " + LastVessels[0].protoVessel.vesselName);
				LastVessels.RemoveAt (0);
			}
			Quick.Warning ("Last Vessel has keep " + LastVessels.Count + " vessels");
		}

		/* internal static void PostInitED() {
			Quick.Warning ("PostInitED");
			if (SavedGoTo != GoTo.None) {
				Quick.Warning ("GoTo OK");
				switch (SavedGoTo) {
				case GoTo.RecoverToSPH:
				case GoTo.RecoverToVAB:
					Quick.Warning ("Switch OK");
					if (CanRecoverpVessel (LastVessel)) {
						Quick.Warning ("LastVessel isrecoverable");
						MissionRecoveryDialog _data = MissionRecoveryDialog.CreateFullDialog(LastVessel);
						//GameEvents.onGUIRecoveryDialogSpawn.Fire();
						//GameEvents.OnVesselRecoveryRequested.Fire (LastVessel);
						//GameEvents.onVesselRecovered.Fire (LastVessel);
						//ShipConstruction.RecoverVesselFromFlight (LastVessel, HighLogic.CurrentGame.flightState);
						Quick.Log ("LastVessel recovered: " + LastVessel.vesselName);
						LastVessel = null;
					}
					break;
				}
				SavedGoTo = GoTo.None;
			}
		}*/

		public static void mainMenu() {
			SavedGoTo = GoTo.None;
			if (CanMainMenu) {
				GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
				HighLogic.LoadScene (GameScenes.MAINMENU);
				InputLockManager.ClearControlLocks ();
				Quick.Log (GetText (GoTo.MainMenu));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.MainMenu));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.MainMenu), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void settings() {
			SavedGoTo = GoTo.None;
			if (CanMainMenu) {
				GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
				HighLogic.LoadScene (GameScenes.SETTINGS);
				InputLockManager.ClearControlLocks ();
				Quick.Log (GetText (GoTo.Settings));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.Settings));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.Settings), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void trackingStation() {
			SavedGoTo = GoTo.None;
			if (CanTrackingStation) {
				GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
				HighLogic.LoadScene	(GameScenes.LOADINGBUFFER);
				HighLogic.LoadScene (GameScenes.TRACKSTATION);
				InputLockManager.ClearControlLocks ();
				Quick.Log (GetText (GoTo.TrackingStation));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.TrackingStation));
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
				Quick.Log (GetText (GoTo.SpaceCenter));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.SpaceCenter));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.SpaceCenter), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		internal static void ClearSpaceCenter() {
			if (RUIPrefabLauncher.Instance != null) {
				GameEvents.onGUILaunchScreenDespawn.Fire ();
			}
			if (isMissionControl) {
				GameEvents.onGUIMissionControlDespawn.Fire ();
			}
			if (isAdministration) {
				GameEvents.onGUIAdministrationFacilityDespawn.Fire ();
			}
			CMAstronautComplex _CMAstronautComplex;
			if (isAstronautComplex(out _CMAstronautComplex)) {
				POINTER_INFO _ptr = new POINTER_INFO ();
				_ptr.evt = POINTER_INFO.INPUT_EVENT.TAP;
				_CMAstronautComplex.ButtonExit (ref _ptr);
				GameEvents.onGUIAstronautComplexDespawn.Fire ();
			}
			if (isRnD) {
				GameEvents.onGUIRnDComplexDespawn.Fire ();
			}
			InputLockManager.ClearControlLocks ();
			Quick.Log ("Clear SpaceCenter");
		}

		public static void missionControl() {
			SavedGoTo = GoTo.None;
			if (HighLogic.LoadedSceneIsGame) {
				if (CanFundBuilding) {
					if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
						ClearSpaceCenter ();
						GameEvents.onGUIMissionControlSpawn.Fire ();
						InputLockManager.ClearControlLocks ();
						Quick.Log (GetText(GoTo.MissionControl));
						return;
					}
					if (CanSpaceCenter) {
						SavedGoTo = GoTo.MissionControl;
						gotoSpaceCenter ();
						return;
					}
				}
			}
			Quick.Warning ("You can't " + GetText(GoTo.MissionControl));
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
						Quick.Log (GetText(GoTo.Administration));
						return;
					}
					if (CanSpaceCenter) {
						SavedGoTo = GoTo.Administration;
						gotoSpaceCenter ();
						return;
					}
				}
			}
			Quick.Warning ("You can't " + GetText(GoTo.Administration));
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
						Quick.Log (GetText(GoTo.RnD));
						return;
					}
					if (CanSpaceCenter) {
						SavedGoTo = GoTo.RnD;
						gotoSpaceCenter ();
						return;
					}
				}
			}
			Quick.Warning ("You can't " + GetText(GoTo.RnD));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.RnD), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void astronautComplex() {
			SavedGoTo = GoTo.None;
			if (HighLogic.LoadedSceneIsGame) {
				if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
					ClearSpaceCenter ();
					GameEvents.onGUIAstronautComplexSpawn.Fire ();
					InputLockManager.ClearControlLocks ();
					Quick.Log (GetText(GoTo.AstronautComplex));
					return;
				}
				if (CanSpaceCenter) {
					SavedGoTo = GoTo.AstronautComplex;
					gotoSpaceCenter ();
					return;
				}
			}
			Quick.Warning ("You can't " + GetText(GoTo.AstronautComplex));
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
				Quick.Log (GetText (GoTo.VAB));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.VAB));
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
				Quick.Log (GetText (GoTo.SPH));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.SPH));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.SPH), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void Recover() {
			SavedGoTo = GoTo.None;
			if (CanRecover) {
				GameEvents.OnVesselRecoveryRequested.Fire (FlightGlobals.ActiveVessel);
				InputLockManager.ClearControlLocks ();
				Quick.Log (GetText (GoTo.Recover));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.Recover));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.Recover), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		/*public static void RecoverToVAB() {
			SavedGoTo = GoTo.None;
			if (CanRecover) {
				SavedGoTo = GoTo.RecoverToVAB;
				gotoVAB ();
				Quick.Log (GetText (GoTo.RecoverToVAB));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.Recover));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.Recover), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void RecoverToSPH() {
			SavedGoTo = GoTo.None;
			if (CanRecover) {
				SavedGoTo = GoTo.RecoverToSPH;
				gotoSPH ();
				Quick.Log (GetText (GoTo.RecoverToSPH));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.Recover));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.Recover), 10, ScreenMessageStyle.UPPER_RIGHT);
		}*/

		public static void Revert() {
			SavedGoTo = GoTo.None;
			if (CanRevert) {
				FlightDriver.RevertToLaunch ();
				InputLockManager.ClearControlLocks ();
				Quick.Log (GetText (GoTo.Revert));
				return;
			}
			Quick.Warning ("You can't " + GetText (GoTo.Revert));
			ScreenMessages.PostScreenMessage ("You can't " + GetText (GoTo.Revert), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void RevertToEditor() {
			SavedGoTo = GoTo.None;
			if (CanRevertToEditor) {
				FlightDriver.RevertToPrelaunch (ShipConstruction.ShipType);
				InputLockManager.ClearControlLocks ();
				Quick.Log (GetText (GoTo.RevertToEditor));
				return;
			}
			Quick.Warning ("You can't " + GetText (GoTo.RevertToEditor));
			ScreenMessages.PostScreenMessage ("You can't " + GetText (GoTo.RevertToEditor), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void RevertToSpaceCenter() {
			SavedGoTo = GoTo.None;
			if (CanRevertToEditor) {
				gotoSpaceCenter (FlightDriver.PreLaunchState);
				Quick.Log (GetText (GoTo.RevertToSpaceCenter));
				return;
			}
			Quick.Warning ("You can't " + GetText (GoTo.RevertToSpaceCenter));
			ScreenMessages.PostScreenMessage ("You can't " + GetText (GoTo.RevertToSpaceCenter), 10, ScreenMessageStyle.UPPER_RIGHT);
		}

		public static void LastVessel() {
			SavedGoTo = GoTo.None;
			if (CanLastVessel) {
				string _saveGame = GamePersistence.SaveGame (SaveGame, HighLogic.SaveFolder, SaveMode.OVERWRITE);
				QData _lastVessel = LastVesselLastIndex;
				FlightDriver.StartAndFocusVessel (_saveGame, _lastVessel.idx);
				LastVessels.Remove(_lastVessel);
				InputLockManager.ClearControlLocks ();
				Quick.Log (GetText (GoTo.LastVessel));
				return;
			}
			Quick.Warning ("You can't " + GetText(GoTo.LastVessel));
			ScreenMessages.PostScreenMessage ("You can't " + GetText(GoTo.LastVessel), 10, ScreenMessageStyle.UPPER_RIGHT);
		}
	}
}