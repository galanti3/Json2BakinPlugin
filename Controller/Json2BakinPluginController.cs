using Json2BakinPlugin.Models;
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
		MvMapDataLoadService loadService = new MvMapDataLoadService();
		BakinDataExportService exportService = new BakinDataExportService();

		public string JsonFolder { get; set; }
		public string BakinFolder { get; set; }

        internal void Convert()
        {
			List<MvMap> mvMaps = new List<MvMap>();
			List<string> files = Directory.GetFiles(JsonFolder, "Map*.json").ToList();
			foreach (string file in files)
			{
				MvMap map = loadService.DeserializeMapData(file);
				map.IdString = file.Split('\\').Last().Replace("Map", "").Replace(".json", "");
				exportService.Preprocess(map);
				exportService.RegisterBakinCodes(map);
				exportService.Postprocess(map);
				exportService.ExportMap(map, BakinFolder);
				mvMaps.Add(map);
			}
		}
	}
}
