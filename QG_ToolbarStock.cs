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
using UnityEngine;

namespace QuickGoTo {
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	public class QStockToolbar : MonoBehaviour {

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
		
		private ApplicationLauncher.AppScenes AppScenes = ApplicationLauncher.AppScenes.ALWAYS;
		private static string TexturePath = QuickGoTo.MOD + "/Textures/StockToolBar";

		private void OnTrue () {
			if (QGUI.WindowSettings) {
				return;
			}
			QGUI.ShowGoTo ();
		}

		private void OnFalse () {
			if (QGUI.WindowSettings) {
				QGUI.Settings ();
				return;
			}
			QGUI.HideGoTo ();
		}

		private void OnHover () {
			if (!QSettings.Instance.StockToolBar_OnHover || QGUI.WindowSettings) {
				return;
			}
			QGUI.ShowGoTo ();
		}

		private void OnHoverOut () {
			if (!QSettings.Instance.StockToolBar_OnHover || QGUI.WindowSettings || !QGUI.WindowGoTo) {
				return;
			}
			if (QGUI.RectGoTo == new Rect ()) {
				QGUI.HideGoTo ();
				return;
			}
			if (!isTrue && !isHovering) {
				QGUI.HideGoTo ();
			}
		}

		private void OnEnable () {
		}

		private void OnDisable () {
			QGUI.HideGoTo ();
		}
			
		internal static Texture2D GetTexture {
			get {
				return GameDatabase.Instance.GetTexture(TexturePath, false);
			}
		}

		private ApplicationLauncherButton appLauncherButton;

		internal static bool isAvailable {
			get {
				return ApplicationLauncher.Ready && ApplicationLauncher.Instance != null;
			}
		}

		internal static bool isModApp(ApplicationLauncherButton button) {
			bool _hidden;
			return ApplicationLauncher.Instance.Contains (button, out _hidden);
		}

		internal bool isActive {
			get {
				return appLauncherButton != null && isAvailable;
			}
		}

		internal bool isHovering {
			get {
				if (!QSettings.Instance.StockToolBar_OnHover || appLauncherButton == null || QGUI.RectGoTo == new Rect()) {
					return false;
				}
				return appLauncherButton.toggleButton.IsHovering || QGUI.RectGoTo.Contains (Mouse.screenPos);
			}
		}

		internal bool isTrue {
			get {
				if (appLauncherButton == null || QGUI.RectGoTo == new Rect()) {
					return false;
				}
				return appLauncherButton.State == RUIToggleButton.ButtonState.TRUE;
			}
		}

		internal bool isFalse {
			get {
				if (appLauncherButton == null || QGUI.RectGoTo == new Rect()) {
					return false;
				}
				return appLauncherButton.State == RUIToggleButton.ButtonState.FALSE;
			}
		}

		internal Rect Position {
			get {
				if (appLauncherButton == null || !isAvailable) {
					return new Rect ();
				}
				Camera _camera = UIManager.instance.uiCameras [0].camera;
				Vector3 _pos = appLauncherButton.GetAnchor ();
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

		internal static QStockToolbar Instance {
			get;
			private set;
		}

		private void Awake() {
			if (Instance != null) {
				Destroy (this);
				return;
			}
			Instance = this;
			DontDestroyOnLoad (Instance);
			GameEvents.onGUIApplicationLauncherReady.Add (AppLauncherReady);
			GameEvents.onGUIApplicationLauncherDestroyed.Add (AppLauncherDestroyed);
			GameEvents.onLevelWasLoadedGUIReady.Add (AppLauncherDestroyed);
		}
			
		private void AppLauncherReady() {
			QSettings.Instance.Load ();
			if (!Enabled) {
				return;
			}
			Init ();
		}

		private void AppLauncherDestroyed(GameScenes gameScene) {
			if (CanUseIt) {
				return;
			}
			Destroy ();
		}
		
		private void AppLauncherDestroyed() {
			Destroy ();
		}

		private void OnDestroy() {
			GameEvents.onGUIApplicationLauncherReady.Remove (AppLauncherReady);
			GameEvents.onGUIApplicationLauncherDestroyed.Remove (AppLauncherDestroyed);
			GameEvents.onLevelWasLoadedGUIReady.Remove (AppLauncherDestroyed);
		}

		private void Init() {
			if (!isAvailable || !CanUseIt) {
				return;
			}
			if (appLauncherButton == null) {
				if (ModApp) {
					appLauncherButton = ApplicationLauncher.Instance.AddModApplication (new RUIToggleButton.OnTrue (this.OnTrue), new RUIToggleButton.OnFalse (this.OnFalse), new RUIToggleButton.OnHover (this.OnHover), new RUIToggleButton.OnHoverOut (this.OnHoverOut), new RUIToggleButton.OnEnable (this.OnEnable), new RUIToggleButton.OnDisable (this.OnDisable), AppScenes, GetTexture);
					ApplicationLauncher.Instance.EnableMutuallyExclusive (appLauncherButton);
				} else {
					appLauncherButton = ApplicationLauncher.Instance.AddApplication (new RUIToggleButton.OnTrue (this.OnTrue), new RUIToggleButton.OnFalse (this.OnFalse), new RUIToggleButton.OnHover (this.OnHover), new RUIToggleButton.OnHoverOut (this.OnHoverOut), new RUIToggleButton.OnEnable (this.OnEnable), new RUIToggleButton.OnDisable (this.OnDisable), GetTexture);
					appLauncherButton.VisibleInScenes = ApplicationLauncher.AppScenes.ALWAYS;
				}
				ApplicationLauncher.Instance.AddOnHideCallback (OnHide);
			}
		}

		private void OnShow() {
			QGUI.ShowGoTo ();
			ApplicationLauncher.Instance.RemoveOnShowCallback (OnShow);
		}

		private void OnHide() {
			if (!isAvailable) {
				return;
			}
			if (QGUI.WindowSettings || QGUI.WindowGoTo) {
				ApplicationLauncher.Instance.AddOnShowCallback (OnShow);
			}
			if (QGUI.WindowSettings) {
				QGUI.SettingsSwitch ();
			}
			QGUI.HideGoTo ();
		}

		private void Destroy() {
			if (appLauncherButton != null) {
				ApplicationLauncher.Instance.RemoveModApplication (appLauncherButton);
				ApplicationLauncher.Instance.RemoveApplication (appLauncherButton);
				appLauncherButton = null;
			}
		}

		internal void Set(bool SetTrue, bool force = false) {
			if (!isAvailable) {
				return;
			}
			if (appLauncherButton != null) {
				if (SetTrue) {
					if (appLauncherButton.State == RUIToggleButton.ButtonState.FALSE) {
						appLauncherButton.SetTrue (force);
					}
				} else {
					if (appLauncherButton.State == RUIToggleButton.ButtonState.TRUE) {
						appLauncherButton.SetFalse (force);
					}
				}
			}
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
		}
	}
}