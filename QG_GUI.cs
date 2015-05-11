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

	public class QGUI : MonoBehaviour {

		private static string VAB_TexturePath = Quick.MOD + "/Textures/StockVAB";
		private static string TS_TexturePath = Quick.MOD + "/Textures/StockTS";
		private static string SPH_TexturePath = Quick.MOD + "/Textures/StockSPH";
		private static string Sett_TexturePath = Quick.MOD + "/Textures/StockSett";
		private static string SC_TexturePath = Quick.MOD + "/Textures/StockSC";
		private static string RvSC_TexturePath = Quick.MOD + "/Textures/StockRvSC";
		private static string RvED_TexturePath = Quick.MOD + "/Textures/StockRvED";
		private static string Rv_TexturePath = Quick.MOD + "/Textures/StockRv";
		private static string RnD_TexturePath = Quick.MOD + "/Textures/StockRnD";
		private static string Rc_TexturePath = Quick.MOD + "/Textures/StockRc";
		private static string MI_TexturePath = Quick.MOD + "/Textures/StockMI";
		private static string Main_TexturePath = Quick.MOD + "/Textures/StockMain";
		private static string Lves_TexturePath = Quick.MOD + "/Textures/StockLves";
		private static string Astr_TexturePath = Quick.MOD + "/Textures/StockAstr";
		private static string Admi_TexturePath = Quick.MOD + "/Textures/StockAdmi";
		private static string Conf_TexturePath = Quick.MOD + "/Textures/StockConf";

		private static Texture2D VAB_Texture;
		private static Texture2D TS_Texture;
		private static Texture2D SPH_Texture;
		private static Texture2D Sett_Texture;
		private static Texture2D SC_Texture;
		private static Texture2D RvSC_Texture;
		private static Texture2D RvED_Texture;
		private static Texture2D Rv_Texture;
		private static Texture2D RnD_Texture;
		private static Texture2D Rc_Texture;
		private static Texture2D MI_Texture;
		private static Texture2D Main_Texture;
		private static Texture2D Lves_Texture;
		private static Texture2D Astr_Texture;
		private static Texture2D Admi_Texture;
		private static Texture2D Conf_Texture;

		private static string[] GUIWhiteList = {
			"MainMenuSkin",
			"KSP window 1",
			"KSP window 2",
			"KSP window 3",
			"KSP window 7",
			"GameSkin"
		};

		internal static bool WindowSettings = false;
		internal static bool WindowGoTo = false;

		internal static Rect RectSettings;
		internal static Rect RectGoTo;
		internal static GUIStyle GoToButtonStyle;
		private static GUIStyle LabelStyle;
		internal static int ButtonHeight;
		private static int GUISpace = 4;

		internal static void Awake() {
			RectSettings = new Rect ();
			RectGoTo = new Rect ();
			RefreshTexture ();
		}

		internal static void RefreshTexture() {
			bool _ImageOnly = QSettings.Instance.ImageOnly;
			VAB_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? VAB_TexturePath : QBlizzyToolbar.VAB_TexturePath), false);
			TS_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? TS_TexturePath : QBlizzyToolbar.TS_TexturePath), false);
			SPH_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? SPH_TexturePath : QBlizzyToolbar.SPH_TexturePath), false);
			Sett_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Sett_TexturePath : QBlizzyToolbar.Sett_TexturePath), false);
			SC_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? SC_TexturePath : QBlizzyToolbar.SC_TexturePath), false);
			RvSC_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? RvSC_TexturePath : QBlizzyToolbar.RvSC_TexturePath), false);
			RvED_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? RvED_TexturePath : QBlizzyToolbar.RvED_TexturePath), false);
			Rv_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Rv_TexturePath : QBlizzyToolbar.Rv_TexturePath), false);
			RnD_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? RnD_TexturePath : QBlizzyToolbar.RnD_TexturePath), false);
			Rc_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Rc_TexturePath : QBlizzyToolbar.Rc_TexturePath), false);
			MI_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? MI_TexturePath : QBlizzyToolbar.MI_TexturePath), false);
			Main_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Main_TexturePath : QBlizzyToolbar.Main_TexturePath), false);
			Lves_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Lves_TexturePath : QBlizzyToolbar.Lves_TexturePath), false);
			Astr_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Astr_TexturePath : QBlizzyToolbar.Astr_TexturePath), false);
			Admi_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Admi_TexturePath : QBlizzyToolbar.Admi_TexturePath), false);
			Conf_Texture = GameDatabase.Instance.GetTexture((_ImageOnly ? Conf_TexturePath : QBlizzyToolbar.Conf_TexturePath), false);
		}

		internal static void RefreshStyle(bool force = false) {
			if (GoToButtonStyle == null || force) {
				GoToButtonStyle = new GUIStyle (GUI.skin.button);
				GoToButtonStyle.alignment = (QSettings.Instance.ImageOnly ? TextAnchor.MiddleCenter : TextAnchor.MiddleLeft);
				GoToButtonStyle.padding = new RectOffset ((QSettings.Instance.ImageOnly ? 0 : 10), 0, 0, 0);
				GoToButtonStyle.imagePosition = (QSettings.Instance.ImageOnly ? ImagePosition.ImageOnly : ImagePosition.ImageLeft);
			}
			if (LabelStyle == null || force) {
				LabelStyle = new GUIStyle (GUI.skin.label);
				LabelStyle.stretchWidth = true;
				LabelStyle.stretchHeight = true;
				LabelStyle.alignment = TextAnchor.MiddleCenter;
				LabelStyle.fontStyle = FontStyle.Bold;
				LabelStyle.normal.textColor = Color.white;
			}
		}

		internal static void RefreshRect() {
			if (WindowSettings) {
				RectSettings.width = 915;
				RectSettings.x = (Screen.width - RectSettings.width) / 2;
				RectSettings.y = (Screen.height - RectSettings.height) / 2;
			}
			if (WindowGoTo) {
				if (!QSettings.Instance.ImageOnly) {
					RectGoTo.width = 250;
					ButtonHeight = 30;
				} else {
					RectGoTo.width = 40;
					ButtonHeight = 40;
				}
				RectGoTo.x = (Screen.width - RectGoTo.width) / 2;
				RectGoTo.y = (Screen.height - RectGoTo.height) / 2;
				if (QStockToolbar.Enabled) {
					Rect _Spos = new Rect ();
					if (QStockToolbar.Instance.isActive) {
						_Spos = QStockToolbar.Instance.Position;
					}
					int _width = (QSettings.Instance.ImageOnly ? (HighLogic.LoadedSceneIsEditor ? 75 : 5) : (HighLogic.LoadedSceneIsEditor ? 325 : 255));
					if (_Spos == new Rect () || (!QSettings.Instance.ImageOnly && Screen.width - _Spos.x < _width)) {
						RectGoTo.x = Screen.width - _width;
					} else {
						RectGoTo.x = _Spos.x;
					}
					if (ApplicationLauncher.Instance.IsPositionedAtTop) {
						RectGoTo.y = _Spos.y + _Spos.height -2;
					} else {
						RectGoTo.y = Screen.height - _Spos.height - RectGoTo.height +2;
					}
				}
			}
			if (WindowSettings && WindowGoTo) {
				if (RectSettings.x + RectSettings.width > RectGoTo.x) {
					RectSettings.x = 10;
				}
				if (QSettings.Instance.ImageOnly && RectSettings.x + RectSettings.width > RectGoTo.x) {
					RectGoTo.x = 5;
				}
			}
		}

		internal static void Refresh() {
			GUI.skin = AssetBase.GetGUISkin (QSettings.Instance.CurrentGUISkin);
			RefreshRect ();
			RefreshStyle ();
		}

		private static void Lock(bool activate, ControlTypes Ctrl) {
			if (HighLogic.LoadedSceneIsEditor) {
				if (activate) {
					EditorLogic.fetch.Lock(true, true, true, "EditorLock" + Quick.MOD);
				} else {
					EditorLogic.fetch.Unlock ("EditorLock" + Quick.MOD);
				}
			}
			if (activate) {
				InputLockManager.SetControlLock (Ctrl, "Lock" + Quick.MOD);
			} else {
				InputLockManager.RemoveControlLock ("Lock" + Quick.MOD);
			}
			if (InputLockManager.GetControlLock ("Lock" + Quick.MOD) != ControlTypes.None) {
				InputLockManager.RemoveControlLock ("Lock" + Quick.MOD);
			}
			if (InputLockManager.GetControlLock ("EditorLock" + Quick.MOD) != ControlTypes.None) {
				InputLockManager.RemoveControlLock ("EditorLock" + Quick.MOD);
			}
		}

		public static void Settings() {
			SettingsSwitch ();
			if (!WindowSettings) {
				QStockToolbar.Instance.Reset ();
				QuickGoTo.BlizzyToolbar.Reset ();
				QSettings.Instance.Save ();
			}
		}

		internal static void SettingsSwitch() {
			WindowSettings = !WindowSettings;
			QStockToolbar.Instance.Set (WindowSettings);
			Lock (WindowSettings, ControlTypes.KSC_ALL | ControlTypes.TRACKINGSTATION_UI | ControlTypes.CAMERACONTROLS | ControlTypes.MAP);
			WindowGoTo = false;
			RectGoTo.height = 0;
		}

		internal static void ToggleGoTo() {
			WindowGoTo = !WindowGoTo;
			QStockToolbar.Instance.Set (WindowGoTo);
			Lock (WindowGoTo, ControlTypes.KSC_ALL | ControlTypes.TRACKINGSTATION_UI | ControlTypes.CAMERACONTROLS | ControlTypes.MAP);
		}

		internal static void HideGoTo() {
			WindowGoTo = false;
			Lock (WindowGoTo, ControlTypes.KSC_ALL | ControlTypes.TRACKINGSTATION_UI | ControlTypes.CAMERACONTROLS | ControlTypes.MAP);
		}

		internal static void ShowGoTo() {
			WindowGoTo = true;
			Lock (WindowGoTo, ControlTypes.KSC_ALL | ControlTypes.TRACKINGSTATION_UI | ControlTypes.CAMERACONTROLS | ControlTypes.MAP);
		}

		internal static void OnGUI() {
			if (WindowSettings) {
				Refresh ();
				RectSettings = GUILayout.Window (1584654, RectSettings, DrawSettings, Quick.MOD + " " + Quick.VERSION, GUILayout.Width (RectSettings.width), GUILayout.ExpandHeight (true));
			}
			if (WindowGoTo) {
				Refresh ();
				if (QStockToolbar.Instance.isActive) {
					if (!QStockToolbar.Instance.isTrue && !QStockToolbar.Instance.isHovering) {
						HideGoTo ();
						return;
					}
				}
				DrawGoTo (RectGoTo);
			}
		}

		private static void DrawSettings(int id) {
			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();
			GUILayout.Box("Options",GUILayout.Height(30));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			GUILayout.BeginHorizontal ();
			QSettings.Instance.StockToolBar = GUILayout.Toggle (QSettings.Instance.StockToolBar, "Use the Stock ToolBar", GUILayout.Width (300));
			if (QSettings.Instance.StockToolBar) {
				QSettings.Instance.StockToolBar_ModApp = !GUILayout.Toggle (!QSettings.Instance.StockToolBar_ModApp, "Put QuickGoTo in Stock", GUILayout.Width (300));
			} else {
				GUILayout.Space (300);
			}
			if (QBlizzyToolbar.isAvailable) {
				QSettings.Instance.BlizzyToolBar = GUILayout.Toggle (QSettings.Instance.BlizzyToolBar, "Use the Blizzy ToolBar", GUILayout.Width (300));
			}
			GUILayout.EndHorizontal ();
			GUILayout.Space (5);

			GUILayout.BeginHorizontal ();
			bool _bool = GUILayout.Toggle (QSettings.Instance.ImageOnly, "Panel with only the icons", GUILayout.Width (300));
			if (_bool != QSettings.Instance.ImageOnly) {
				QSettings.Instance.ImageOnly = _bool;
				RefreshStyle (true);
				RefreshTexture ();
			}
			if (QSettings.Instance.StockToolBar) {
				QSettings.Instance.LockHover = GUILayout.Toggle (QSettings.Instance.LockHover, "Lock buttons on hover", GUILayout.Width (300));
			} else {
				GUILayout.Space (300);
			}
			GetSkin ();
			GUILayout.EndHorizontal ();
			GUILayout.Space (5);

			GUILayout.BeginHorizontal();
			GUILayout.Box("Enable/Disable",GUILayout.Height(30));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			QSettings.Instance.EnableGoToMainMenu = GUILayout.Toggle (QSettings.Instance.EnableGoToMainMenu, QGoTo.GetText(QGoTo.GoTo.MainMenu), GUILayout.Width (300));
			QSettings.Instance.EnableGoToSettings = GUILayout.Toggle (QSettings.Instance.EnableGoToSettings, QGoTo.GetText(QGoTo.GoTo.Settings), GUILayout.Width (300));
			QSettings.Instance.EnableGoToSpaceCenter = GUILayout.Toggle (QSettings.Instance.EnableGoToSpaceCenter, QGoTo.GetText(QGoTo.GoTo.SpaceCenter), GUILayout.Width (300));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			QSettings.Instance.EnableGoToVAB = GUILayout.Toggle (QSettings.Instance.EnableGoToVAB, QGoTo.GetText(QGoTo.GoTo.VAB), GUILayout.Width (300));
			QSettings.Instance.EnableGoToSPH = GUILayout.Toggle (QSettings.Instance.EnableGoToSPH, QGoTo.GetText(QGoTo.GoTo.SPH), GUILayout.Width (300));
			QSettings.Instance.EnableGoToTrackingStation = GUILayout.Toggle (QSettings.Instance.EnableGoToTrackingStation, QGoTo.GetText(QGoTo.GoTo.TrackingStation), GUILayout.Width (300));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			QSettings.Instance.EnableGoToMissionControl = GUILayout.Toggle (QSettings.Instance.EnableGoToMissionControl, QGoTo.GetText(QGoTo.GoTo.MissionControl), GUILayout.Width (300));
			QSettings.Instance.EnableGoToAdministration = GUILayout.Toggle (QSettings.Instance.EnableGoToAdministration, QGoTo.GetText(QGoTo.GoTo.Administration), GUILayout.Width (300));
			QSettings.Instance.EnableGoToRnD = GUILayout.Toggle (QSettings.Instance.EnableGoToRnD, QGoTo.GetText(QGoTo.GoTo.RnD), GUILayout.Width (300));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);


			GUILayout.BeginHorizontal();
			QSettings.Instance.EnableGoToAstronautComplex = GUILayout.Toggle (QSettings.Instance.EnableGoToAstronautComplex, QGoTo.GetText(QGoTo.GoTo.AstronautComplex), GUILayout.Width (300));
			QSettings.Instance.EnableGoToLastVessel = GUILayout.Toggle (QSettings.Instance.EnableGoToLastVessel, QGoTo.GetText (QGoTo.GoTo.LastVessel, true), GUILayout.Width (300));
			QSettings.Instance.EnableGoToRecover = GUILayout.Toggle (QSettings.Instance.EnableGoToRecover, QGoTo.GetText(QGoTo.GoTo.Recover), GUILayout.Width (300));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			QSettings.Instance.EnableGoToRevert = GUILayout.Toggle (QSettings.Instance.EnableGoToRevert, QGoTo.GetText(QGoTo.GoTo.Revert), GUILayout.Width (300));
			QSettings.Instance.EnableGoToRevertToEditor = GUILayout.Toggle (QSettings.Instance.EnableGoToRevertToEditor, QGoTo.GetText(QGoTo.GoTo.RevertToEditor), GUILayout.Width (300));
			QSettings.Instance.EnableGoToRevertToSpaceCenter = GUILayout.Toggle (QSettings.Instance.EnableGoToRevertToSpaceCenter, QGoTo.GetText(QGoTo.GoTo.RevertToSpaceCenter), GUILayout.Width (300));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			GUILayout.BeginHorizontal();
			QSettings.Instance.EnableSettings = GUILayout.Toggle (QSettings.Instance.EnableSettings, QGoTo.GetText(QGoTo.GoTo.Configurations) + " (if disabled, it will be displayed only on the Space Center)", GUILayout.Width (900));
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Close", GUILayout.Height(30))) {
				Settings ();
			}
			GUILayout.EndHorizontal ();
			GUILayout.Space (5);
			GUILayout.EndVertical();
		}

		private static void DrawGoTo(Rect rectGoTo) {
			int _rect = 3;
			bool _lockHover = QSettings.Instance.LockHover && QSettings.Instance.StockToolBar && QStockToolbar.Instance.isHovering && !QStockToolbar.Instance.isTrue;
			GUILayout.BeginArea (rectGoTo);
			GUILayout.BeginVertical ();
			GUILayout.Space (3);

			GUI.enabled = !_lockHover;

			if (!QGoTo.CanMainMenu) {
				GUI.enabled = false;
			}
			if (QSettings.Instance.EnableGoToMainMenu) {
				_rect += ButtonHeight + GUISpace;
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent (QGoTo.GetText (QGoTo.GoTo.MainMenu), Main_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
					ToggleGoTo ();
					QGoTo.mainMenu ();
				}
				GUILayout.EndHorizontal ();
			}
			if (QSettings.Instance.EnableGoToSettings) {
				_rect += ButtonHeight + GUISpace;				
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.Settings), Sett_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
					ToggleGoTo ();
					QGoTo.settings ();
				}
				GUILayout.EndHorizontal ();
			}
			GUI.enabled = !_lockHover;

			if (QSettings.Instance.EnableGoToSpaceCenter) {
				_rect += ButtonHeight + GUISpace;				
				if (!QGoTo.CanSpaceCenter) {
					GUI.enabled = false;
				}
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.SpaceCenter), SC_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
					ToggleGoTo ();
					QGoTo.spaceCenter ();
				}
				GUILayout.EndHorizontal ();
				GUI.enabled = !_lockHover;
			}

			if (HighLogic.CurrentGame.Parameters.Flight.CanLeaveToEditor || !HighLogic.LoadedSceneIsFlight) {
				if (QSettings.Instance.EnableGoToVAB) {
					_rect += ButtonHeight + GUISpace;					
					if (!QGoTo.CanEditor (EditorFacility.VAB)) {
						GUI.enabled = false;
					}
					GUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.VAB), VAB_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
						ToggleGoTo ();
						QGoTo.VAB ();
					}
					GUILayout.EndHorizontal ();
					GUI.enabled = !_lockHover;
				}
				if (QSettings.Instance.EnableGoToSPH) {
					_rect += ButtonHeight + GUISpace;					
					if (!QGoTo.CanEditor (EditorFacility.SPH)) {
						GUI.enabled = false;
					}
					GUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.SPH), SPH_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
						ToggleGoTo ();
						QGoTo.SPH ();
					}
					GUILayout.EndHorizontal ();
					GUI.enabled = !_lockHover;
				}
			}

			if (QSettings.Instance.EnableGoToTrackingStation && (HighLogic.CurrentGame.Parameters.Flight.CanLeaveToTrackingStation || !HighLogic.LoadedSceneIsFlight)) {
				_rect += ButtonHeight + GUISpace;				
				if (!QGoTo.CanTrackingStation) {
					GUI.enabled = false;
				}
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.TrackingStation), TS_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
					ToggleGoTo ();
					QGoTo.trackingStation ();
				}
				GUILayout.EndHorizontal ();
				GUI.enabled = !_lockHover;
			}

			if (!QGoTo.CanSpaceCenter && HighLogic.LoadedScene != GameScenes.SPACECENTER) {
				GUI.enabled = false;
			}
			if (QGoTo.CanFundBuilding) {
				if (QSettings.Instance.EnableGoToMissionControl) {
					_rect += ButtonHeight + GUISpace;					
					GUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.MissionControl), MI_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
						ToggleGoTo ();
						QGoTo.missionControl ();
					}
					GUILayout.EndHorizontal ();
				}

				if (QSettings.Instance.EnableGoToAdministration) {
					_rect += ButtonHeight + GUISpace;					
					GUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.Administration), Admi_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
						ToggleGoTo ();
						QGoTo.administration ();
					}
					GUILayout.EndHorizontal ();
				}
			}

			if (QGoTo.CanScienceBuilding) {
				if (QSettings.Instance.EnableGoToRnD) {
					_rect += ButtonHeight + GUISpace;					
					GUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.RnD), RnD_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
						ToggleGoTo ();
						QGoTo.RnD ();
					}
					GUILayout.EndHorizontal ();
				}
			}

			if (QSettings.Instance.EnableGoToAstronautComplex) {
				_rect += ButtonHeight + GUISpace;				
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.AstronautComplex), Astr_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
					ToggleGoTo ();
					QGoTo.astronautComplex ();
				}
				GUILayout.EndHorizontal ();
			}
			GUI.enabled = !_lockHover;
		
			if (QSettings.Instance.EnableGoToLastVessel) {
				_rect += ButtonHeight + GUISpace;				
				if (!QGoTo.CanLastVessel) {
					GUI.enabled = false;
				}
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.LastVessel), Lves_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
					ToggleGoTo ();
					QGoTo.LastVessel ();
				}
				GUILayout.EndHorizontal ();
				GUI.enabled = !_lockHover;
			}

			if (HighLogic.LoadedSceneIsFlight) {
				if (QSettings.Instance.EnableGoToRecover && QGoTo.CanRecover) {
					_rect += ButtonHeight + GUISpace;					
					GUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.Recover), Rc_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
						ToggleGoTo ();
						QGoTo.Recover ();
					}
					GUILayout.EndHorizontal ();
				}

				if (HighLogic.CurrentGame.Parameters.Flight.CanRestart && FlightDriver.CanRevertToPostInit) {
					if (QSettings.Instance.EnableGoToRevert) {
						_rect += ButtonHeight + GUISpace;						
						if (!QGoTo.CanRevert) {
							GUI.enabled = false;
						}
						GUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.Revert), Rv_Texture), GoToButtonStyle, GUILayout.Width (rectGoTo.width), GUILayout.Height (ButtonHeight))) {
							ToggleGoTo ();
							QGoTo.Revert ();
						}
						GUILayout.EndHorizontal ();
						GUI.enabled = !_lockHover;
					}
				}
				
				if (HighLogic.CurrentGame.Parameters.Flight.CanLeaveToEditor && FlightDriver.CanRevertToPrelaunch) {
					if (QSettings.Instance.EnableGoToRevertToEditor) {
						_rect += ButtonHeight + GUISpace;	
						if (!QGoTo.CanRevertToEditor) {
							GUI.enabled = false;
						}
						GUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.RevertToEditor), RvED_Texture), GoToButtonStyle, GUILayout.Width (rectGoTo.width), GUILayout.Height (ButtonHeight))) {
							ToggleGoTo ();
							QGoTo.RevertToEditor ();
						}
						GUILayout.EndHorizontal ();
						GUI.enabled = !_lockHover;
					}
				}

				if (HighLogic.CurrentGame.Parameters.Flight.CanLeaveToSpaceCenter && FlightDriver.CanRevertToPrelaunch) {
					if (QSettings.Instance.EnableGoToRevertToSpaceCenter) {
						_rect += ButtonHeight + GUISpace;
						if (!QGoTo.CanRevertToSpaceCenter) {
							GUI.enabled = false;
						}
						GUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.RevertToSpaceCenter), RvSC_Texture), GoToButtonStyle, GUILayout.Width(rectGoTo.width), GUILayout.Height(ButtonHeight))) {
							ToggleGoTo ();
							QGoTo.RevertToSpaceCenter ();
						}
						GUILayout.EndHorizontal ();
						GUI.enabled = !_lockHover;
					}
				}
			}
			if (QSettings.Instance.EnableSettings || HighLogic.LoadedScene == GameScenes.SPACECENTER) {
				_rect += ButtonHeight + GUISpace;				
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent (" " + QGoTo.GetText (QGoTo.GoTo.Configurations), Conf_Texture), GoToButtonStyle, GUILayout.Width (rectGoTo.width), GUILayout.Height (ButtonHeight))) {
					Settings ();
				}
				GUILayout.EndHorizontal ();
			}
				
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
			RectGoTo.height = _rect;
		}

		private static void GetSkin() {
			if (GUILayout.Button ("◄", GUILayout.Width(20), GUILayout.Height(25))) {
				int _i = Array.FindIndex (GUIWhiteList, item => item == QSettings.Instance.CurrentGUISkin);
				_i--;
				if (_i < 0) {
					_i = GUIWhiteList.Length - 1;
				}
				QSettings.Instance.CurrentGUISkin = GUIWhiteList[_i];
				GUI.skin = AssetBase.GetGUISkin(QSettings.Instance.CurrentGUISkin);
				RefreshStyle (true);
				RectSettings.height = 0;
			}
			GUILayout.Label ("Skin: " + QSettings.Instance.CurrentGUISkin, LabelStyle, GUILayout.Width (200), GUILayout.Height(30));
			if (GUILayout.Button ("►", GUILayout.Width (20), GUILayout.Height(25))) {
				int _i = Array.FindIndex (GUIWhiteList, item => item == QSettings.Instance.CurrentGUISkin);
				_i++;
				if (_i >= GUIWhiteList.Length) {
					_i = 0;
				}
				QSettings.Instance.CurrentGUISkin = GUIWhiteList[_i];
				GUI.skin = AssetBase.GetGUISkin(QSettings.Instance.CurrentGUISkin);
				RefreshStyle (true);
				RectSettings.height = 0;
			}
		}
	}
}