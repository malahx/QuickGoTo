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

	[KSPAddon(KSPAddon.Startup.EveryScene, false)]
	public class QuickGoTo : Quick {

		private void Awake() {
			if (HighLogic.LoadedSceneIsGame) {
				QToolbar.Awake ();
				QGUI.Awake ();
			}
			GameEvents.onGameSceneLoadRequested.Add (OnGameSceneLoadRequested);
			//GameEvents.onGUIEditorToolbarReady.Add (OnGUIEditorToolbarReady);
		}

		private void Start() {
			if (HighLogic.LoadedSceneIsGame) {
				QSettings.Instance.Load ();
				QToolbar.Instance.Start ();
				if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
					StartCoroutine (this.PostInitSC ());
				}
				if (HighLogic.LoadedScene == GameScenes.MAINMENU) {
					QGoTo.LastVessels = new List<QData>();
				}
			}
		}

		internal void OnGameSceneLoadRequested(GameScenes gameScenes) {
			if (HighLogic.LoadedSceneIsFlight) {
				Vessel _vessel = FlightGlobals.ActiveVessel;
				if (_vessel != null) {
					QGoTo.AddLastVessel(_vessel.protoVessel);
				}
			}
		}

		private IEnumerator PostInitSC () {
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

		/*internal void OnGUIEditorToolbarReady() {
			QGoTo.PostInitED ();
		}*/

		private void OnDestroy() {
			if (QToolbar.Instance != null) {
				QToolbar.Instance.OnDestroy ();
			}
			GameEvents.onGameSceneLoadRequested.Remove (OnGameSceneLoadRequested);
			//GameEvents.onGUIEditorToolbarReady.Remove (OnGUIEditorToolbarReady);
		}

		private void OnGUI() {
			if (HighLogic.LoadedSceneIsGame) {
				GUI.skin = AssetBase.GetGUISkin (QSettings.Instance.CurrentGUISkin);
				QGUI.OnGUI ();
			}
		}
	}
}