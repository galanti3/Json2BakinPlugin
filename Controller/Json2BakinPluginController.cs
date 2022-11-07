﻿using Json2BakinPlugin.Models;
using Json2BakinPlugin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2bakinPlugin.Controller
{
    class Json2BakinPluginController
    {
		MvMapDataLoadService _loadService;
		Json2BakinConvertService _convertService;
		BakinDataExportService _exportService;

		public string JsonFolder { get; set; }
		public string BakinFolder { get; set; }

        internal void Convert()
        {
			List<MvMap> mvMaps = new List<MvMap>();
			List<string> files = Directory.GetFiles(JsonFolder, "Map*.json").ToList();
			_loadService.LoadDatabase(JsonFolder);
			foreach (string file in files)
			{
				_loadService.DeserializeMapData(file, file.Split('\\').Last().Replace("Map", "").Replace(".json", ""));
				_exportService.Preprocess();
				_exportService.RegisterBakinCodes();
				_exportService.Postprocess();
				_exportService.ExportMap(BakinFolder);
				mvMaps.Add(_loadService.GetMap());
			}
		}

        #region Initialize
		public Json2BakinPluginController()
        {
			_loadService = new MvMapDataLoadService();
			_convertService = new Json2BakinConvertService();
			_exportService = new BakinDataExportService(_loadService, _convertService);
        }
		#endregion
	}
}
