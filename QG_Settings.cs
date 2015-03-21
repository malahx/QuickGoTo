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
using System.IO;
using UnityEngine;

namespace QuickGoTo {
	public class QSettings : MonoBehaviour {

		public readonly static QSettings Instance = new QSettings();

		internal static string FileConfig = KSPUtil.ApplicationRootPath + "GameData/" + Quick.MOD + "/Config.txt";

		[Persistent] public bool EnableGoToTrackingStation = true;
		[Persistent] public bool EnableGoToSpaceCenter = true;
		[Persistent] public bool EnableGoToMissionControl = true;
		[Persistent] public bool EnableGoToAdministration = true;
		[Persistent] public bool EnableGoToRnD = true;
		[Persistent] public bool EnableGoToAstronautComplex = true;
		[Persistent] public bool EnableGoToVAB = true;
		[Persistent] public bool EnableGoToSPH = true;
		[Persistent] public bool EnableGoToLastVessel = true;
		[Persistent] public bool EnableGoToRecover = true;
		//[Persistent] public bool EnableGoToRecoverToVAB = true;
		//[Persistent] public bool EnableGoToRecoverToSPH = true;
		[Persistent] public bool EnableGoToRevert = true;
		[Persistent] public bool EnableGoToRevertToEditor = true;
		[Persistent] public bool EnableGoToRevertToSpaceCenter = true;
		[Persistent] public bool EnableGoToMainMenu = false;
		[Persistent] public bool EnableGoToSettings = false;

		[Persistent] public bool StockToolBar = true;
		[Persistent] public bool BlizzyToolBar = true;

		[Persistent] public string CurrentGUISkin = HighLogic.Skin.name;
		[Persistent] public bool ImageOnly = true;

		public void Save() {
			ConfigNode _temp = ConfigNode.CreateConfigFromObject(this, new ConfigNode());
			_temp.Save(FileConfig);
			Quick.Log ("Settings Saved");
		}
		public void Load() {
			if (File.Exists (FileConfig)) {
				try {
					ConfigNode _temp = ConfigNode.Load (FileConfig);
					ConfigNode.LoadObjectFromConfig (this, _temp);
				} catch {
					Save ();
				}
				Quick.Log ("Settings Loaded");
			} else {
				Save ();
			}
		}
	}
}