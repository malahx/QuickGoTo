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

using KSP.UI;
using KSP.UI.Screens;
using System;
using System.Collections;
using UnityEngine;

namespace QuickGoTo {

	public partial class QStockToolbar {

		private ApplicationLauncher.AppScenes AppScenes = ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW | ApplicationLauncher.AppScenes.SPACECENTER | ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.TRACKSTATION | ApplicationLauncher.AppScenes.VAB;
		internal ApplicationLauncherButton appLauncherButton;

		internal bool isActive {
			get {
				return appLauncherButton != null && isAvailable;
			}
		}

		internal bool isHovering {
			get {
				if (!QSettings.Instance.StockToolBar_OnHover || appLauncherButton == null || QGUI.Instance == null) {
					return false;
				}
				if (QGUI.Instance.RectGoTo == new Rect ()) {
					return false;
				}
				return appLauncherButton.IsHovering || QGUI.Instance.RectGoTo.Contains (Mouse.screenPos);
			}
		}

		internal bool isTrue {
			get {
				if (appLauncherButton == null || QGUI.Instance == null) {
					return false;
				}
				if (QGUI.Instance.RectGoTo == new Rect ()) {
					return false;
				}
				return appLauncherButton.toggleButton.CurrentState == KSP.UI.UIRadioButton.State.True;
			}
		}

		internal bool isFalse {
			get {
				if (appLauncherButton == null || QGUI.Instance == null) {
					return false;
				}
				if (QGUI.Instance.RectGoTo == new Rect ()) {
					return false;
				}
				return appLauncherButton.toggleButton.CurrentState == KSP.UI.UIRadioButton.State.False;
			}
		}

		internal Rect Position {
			get {
				if (appLauncherButton == null || !isAvailable) {
					return new Rect ();
				}
				Camera _camera = UIMainCamera.Camera;
				Vector3 _pos = _camera.WorldToScreenPoint (appLauncherButton.GetAnchorUL());
				return new Rect (_pos.x, Screen.height - _pos.y, 41, 41);
			}
		}


		public static QStockToolbar Instance {
			get;
			private set;
		}

		internal static bool Enabled {
			get {
				return QSettings.Instance.StockToolBar;
			}
		}
			
		private static bool ModApp {
			get {
				return QSettings.Instance.StockToolBar_ModApp;
			}
		}

		private static bool CanUseIt {
			get {
				return HighLogic.LoadedSceneIsGame;
			}
		}
		
		private static string TexturePath = MOD + "/Textures/StockToolBar";

		internal static Texture2D GetTexture {
			get {
				return GameDatabase.Instance.GetTexture(TexturePath, false);
			}
		}

		internal static bool isAvailable {
			get {
				return ApplicationLauncher.Ready && ApplicationLauncher.Instance != null;
			}
		}

		internal static bool isModApp(ApplicationLauncherButton button) {
			bool _hidden;
			return ApplicationLauncher.Instance.Contains (button, out _hidden);
		}
			
		protected override void Awake() {
			if (Instance != null) {
				Destroy (this);
				return;
			}
			Instance = this;
			DontDestroyOnLoad (Instance);
			GameEvents.onGUIApplicationLauncherReady.Add (AppLauncherReady);
			GameEvents.onGUIApplicationLauncherDestroyed.Add (AppLauncherDestroyed);
			GameEvents.onGUIApplicationLauncherUnreadifying.Add (AppLauncherUnreadifying);
			GameEvents.onLevelWasLoadedGUIReady.Add (AppLauncherDestroyed);
			Log ("Awake", "QStockToolbar");
		}

		protected override void Start() {
			Log ("Start", "QStockToolbar");
		}

		protected override void OnDestroy() {
			GameEvents.onGUIApplicationLauncherReady.Remove (AppLauncherReady);
			GameEvents.onGUIApplicationLauncherDestroyed.Remove (AppLauncherDestroyed);
			GameEvents.onGUIApplicationLauncherUnreadifying.Remove (AppLauncherUnreadifying);
			GameEvents.onLevelWasLoadedGUIReady.Remove (AppLauncherDestroyed);
			Log ("OnDestroy", "QStockToolbar");
		}

		private void OnTrue () {
			if (QGUI.Instance == null) {
				Warning ("No QGUI Instance", "QStockToolbar");
				return;
			}
			if (QGUI.Instance.WindowSettings) {
				return;
			}
			QGUI.Instance.ShowGoTo ();
			Log ("OnTrue", "QStockToolbar");
		}

		private void OnFalse () {
			if (QGUI.Instance == null) {
				Warning ("No QGUI Instance", "QStockToolbar");
				return;
			}
			if (QGUI.Instance.WindowSettings) {
				QGUI.Instance.Settings ();
				return;
			}
			QGUI.Instance.HideGoTo ();
			Log ("OnFalse", "QStockToolbar");
		}

		private void OnHover () {
			if (QGUI.Instance == null) {
				Warning ("No QGUI Instance", "QStockToolbar");
				return;
			}
			if (!QSettings.Instance.StockToolBar_OnHover || QGUI.Instance.WindowSettings) {
				return;
			}
			QGUI.Instance.ShowGoTo ();
			Log ("OnHover", "QStockToolbar");
		}

		private void OnHoverOut () {
			if (QGUI.Instance == null) {
				Warning ("No QGUI Instance", "QStockToolbar");
				return;
			}
			if (!QSettings.Instance.StockToolBar_OnHover || QGUI.Instance.WindowSettings || !QGUI.Instance.WindowGoTo) {
				return;
			}
			if (QGUI.Instance.RectGoTo == new Rect ()) {
				QGUI.Instance.HideGoTo (true);
				return;
			}
			if (!isTrue && !isHovering) {
				QGUI.Instance.HideGoTo ();
			}
			Log ("OnHoverOut", "QStockToolbar");
		}

		private void OnEnable () {
			Log ("OnEnable", "QStockToolbar");
		}

		private void OnDisable () {
			if (QGUI.Instance == null) {
				Warning ("No QGUI Instance", "QStockToolbar");
				return;
			}
			QGUI.Instance.HideGoTo ();
			Log ("OnDisable", "QStockToolbar");
		}

		private void AppLauncherReady() {
			if (!Enabled) {
				return;
			}
			Init ();
			Log ("AppLauncherReady", "QStockToolbar");
		}

		private void AppLauncherDestroyed(GameScenes gameScene) {
			if (CanUseIt) {
				return;
			}
			Destroy ();
		}
		
		private void AppLauncherDestroyed() {
			Destroy ();
			Log ("AppLauncherDestroyed", "QStockToolbar");
		}

		private void AppLauncherUnreadifying(GameScenes gameScenes) {
			Set (false, true);
			Log ("AppLauncherUnreadifying", "QStockToolbar");
		}

		private void Init() {
			if (!isAvailable || !CanUseIt) {
				return;
			}
			Destroy ();
			if (appLauncherButton == null) {
				if (ModApp) {
					appLauncherButton = ApplicationLauncher.Instance.AddModApplication (OnTrue, OnFalse, OnHover, OnHoverOut, OnEnable, OnDisable, AppScenes, GetTexture);
					ApplicationLauncher.Instance.EnableMutuallyExclusive (appLauncherButton);
				} else {
					appLauncherButton = ApplicationLauncher.Instance.AddApplication (OnTrue, OnFalse, OnHover, OnHoverOut, OnEnable, OnDisable, GetTexture);
					appLauncherButton.VisibleInScenes = AppScenes;
				}
				ApplicationLauncher.Instance.AddOnHideCallback (OnHide);
			}
			Log ("Init", "QStockToolbar");
		}

		private void OnShow() {
			QGUI.Instance.ShowGoTo ();
			ApplicationLauncher.Instance.RemoveOnShowCallback (OnShow);
			Log ("OnShow", "QStockToolbar");
		}

		private void OnHide() {
			if (!isAvailable || QGUI.Instance == null) {
				return;
			}
			if (QGUI.Instance.WindowSettings || QGUI.Instance.WindowGoTo) {
				ApplicationLauncher.Instance.AddOnShowCallback (OnShow);
			}
			if (QGUI.Instance.WindowSettings) {
				QGUI.Instance.SettingsSwitch ();
			} else if (QGUI.Instance.WindowGoTo) {
				QGUI.Instance.HideGoTo (true);
			}
			Log ("OnHide", "QStockToolbar");
		}

		private void Destroy() {
			if (appLauncherButton != null) {
				ApplicationLauncher.Instance.RemoveModApplication (appLauncherButton);
				ApplicationLauncher.Instance.RemoveApplication (appLauncherButton);
				appLauncherButton = null;
				Log ("Destroy", "QStockToolbar");
			}
		}

		internal void Set(bool SetTrue, bool force = false) {
			if (!isActive) {
				return;
			}
			if (SetTrue) {
				if (isFalse) {
					appLauncherButton.SetTrue (force);
				}
			} else {
				if (isTrue) {
					appLauncherButton.SetFalse (force);
				}
			}
			Log ("Set", "QStockToolbar");
		}

		internal void Reset() {
			if (appLauncherButton != null) {
				Set (false);
				if (!Enabled || (Enabled && (ModApp && !isModApp(appLauncherButton)) || (!ModApp && isModApp(appLauncherButton)))) {
					Destroy ();
				}
			}
			if (Enabled) {
				Init ();
			}
			Log ("Reset", "QStockToolbar");
		}
	}
}