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

	[KSPAddon(KSPAddon.Startup.EveryScene, false)]
	public class QuickGoTo : Quick {

		internal static QuickGoTo Instance;
		internal static QBlizzyToolbar BlizzyToolbar;
		internal static QStockToolbar StockToolbar;

		private void Awake() {
			BlizzyToolbar = new QBlizzyToolbar ();
			StockToolbar = new QStockToolbar ();
			GameEvents.onGUIApplicationLauncherDestroyed.Add (StockToolbar.AppLauncherDestroyed);
			GameEvents.onGameSceneLoadRequested.Add (OnGameSceneLoadRequested);
			GameEvents.onGUIAstronautComplexSpawn.Add (AstronautComplexSpawn);
			GameEvents.onGUIAstronautComplexDespawn.Add (AstronautComplexDespawn);
			GameEvents.onGUIRnDComplexSpawn.Add (RnDComplexSpawn);
			GameEvents.onGUIRnDComplexDespawn.Add (RnDComplexDespawn);
			QGUI.Awake ();
		}

		private void Start() {
			QSettings.Instance.Load ();
			if (HighLogic.LoadedSceneIsGame) {
				BlizzyToolbar.Start ();
				StartCoroutine (StockToolbar.AppLauncherReady ());
			}
			if (HighLogic.LoadedScene == GameScenes.SPACECENTER) {
				StartCoroutine (QGoTo.PostInit ());
			}
			if (HighLogic.LoadedScene == GameScenes.MAINMENU) {
				QGoTo.LastVessels = new List<QData>();
			}
			StartEach ();
		}

		private void OnGameSceneLoadRequested(GameScenes gameScenes) {
			if (HighLogic.LoadedSceneIsFlight) {
				Vessel _vessel = FlightGlobals.ActiveVessel;
				if (_vessel != null) {
					QGoTo.AddLastVessel(_vessel.protoVessel);
				}
			}
			StockToolbar.AppLauncherDestroyed ();
		}
			
		private void AstronautComplexSpawn() {
			QGoTo.isAstronautComplex = true;
		}
		private void AstronautComplexDespawn() {
			QGoTo.isAstronautComplex = false;
		}
		private void RnDComplexSpawn() {
			QGoTo.isRnD = true;
		}
		private void RnDComplexDespawn() {
			QGoTo.isRnD = false;
		}

		private void OnDestroy() {
			StopEach ();
			BlizzyToolbar.OnDestroy ();
			GameEvents.onGUIApplicationLauncherDestroyed.Remove (StockToolbar.AppLauncherDestroyed);
			GameEvents.onGameSceneLoadRequested.Remove (OnGameSceneLoadRequested);
			GameEvents.onGUIAstronautComplexSpawn.Remove (AstronautComplexSpawn);
			GameEvents.onGUIAstronautComplexDespawn.Remove (AstronautComplexDespawn);
			GameEvents.onGUIRnDComplexSpawn.Remove (RnDComplexSpawn);
			GameEvents.onGUIRnDComplexDespawn.Remove (RnDComplexDespawn);
		}
			
		private void OnGUI() {
			if (!HighLogic.LoadedSceneIsGame) {
				return;
			}
			QGUI.OnGUI ();
		}
	}
}