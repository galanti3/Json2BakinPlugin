using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using Json2BakinPlugin.Models;
using Json2BakinPlugin.Services;
using static Json2BakinPlugin.Properties.Resources;

namespace Json2BakinPlugin.Controller
{
    class Json2BakinPluginController : INotifyPropertyChanged
    {
        MvMapDataLoadService _loadService;
        Json2BakinConvertService _convertService;
        BakinDataExportService _exportService;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        private string jsonFoler;
        public string JsonFolder
        {
            get
            {
                return jsonFoler;
            }
            set
            {
                jsonFoler = value;
                NotifyPropertyChanged("JsonFolder");
            }
        }
        
        private string bakinFolder;
        public string BakinFolder
        {
            get
            {
                return bakinFolder;
            }
            set
            {
                bakinFolder = value;
                NotifyPropertyChanged("BakinFolder");
            }
        }
        
        private string logText;
        public string LogText
        {
            get
            {
                return logText;
            }
            set
            {
                logText = value;
                NotifyPropertyChanged("LogText");
            }
        }
        
        public bool IsAddReviceComment { get; set; } = true;
        
        public bool IsAddNonConvertedComment { get; set; } = true;

        public bool IsMapNumber { get; set; } = true;

        public bool IsMapName { get; set; } = true;

        private bool uiEnabled;
        public bool UiEnabled
        {
            get
            {
                return uiEnabled;
            }
            set
            {
                uiEnabled = value;
                NotifyPropertyChanged("UiEnabled");
            }
        }
        #endregion

        #region Methods
        internal void Convert()
        {
            StartLogging();
            List<MvMap> mvMaps = new List<MvMap>();
            List<string> files = Directory.GetFiles(JsonFolder, "Map*.json").ToList();
            if (files.Count == 0)
            {
                ErrorLog(Log_NoMapExists);
                WriteLog();
                return;
            }

            LogText += Log_NumMaps + files.Count.ToString() + "\n" + Log_LoadDatabase + "\n";
            List<string> noExistDatabase = _loadService.LoadDatabase(JsonFolder);
            if (noExistDatabase.Count > 0)
            {
                foreach (string file in noExistDatabase)
                {
                    ErrorLog(Log_NoDatabaseExists + file);
                    WriteLog();
                    return;
                }
            }

            _convertService.SetReviceCommentMode(IsAddReviceComment);
            foreach (string file in files)
            {
                if(!file.Contains("MapInfos"))
                {
                    LogText += "----" + Log_StartMap + file.Split('\\').Last() + "----\n";
                    _loadService.DeserializeMapData(file, file.Split('\\').Last().Replace("Map", "").Replace(".json", ""));
                    _exportService.Preprocess();
                    _exportService.RegisterBakinCodes();
                    _exportService.Postprocess();
                    LogText += Log_StartEvents + "\n";
                    _exportService.ExportMap(BakinFolder, (!IsMapNumber && !IsMapName) ? true : IsMapNumber, IsMapName);
                    LogText += Log_EndMap + "\n";
                    mvMaps.Add(_loadService.GetMap());
                }
            }

            //common event
            LogText += "----" + Log_StartCommon + "----\n";
            string common = JsonFolder + "\\CommonEvents.json";
            _loadService.DeserializeCommonData(common);
            _exportService.PreprocessCommon();
            _exportService.RegisterBakinCodes();
            _exportService.PostprocessCommon();
            _exportService.ExportCommonEvents(BakinFolder);

            LogText += "----" + Log_ConvertEnd + "----\n";
            File.WriteAllText(BakinFolder + "\\" + "log.txt", LogText);
        }
        #endregion

        #region Privates
        private void ErrorLog(string error)
        {
            LogText += error + "\n";
        }

        private void WriteLog()
        {
            LogText += "----" + Log_ConvertEnd + "----\n";
            File.WriteAllText(BakinFolder + "\\" + "log.txt", LogText);
        }

        private void StartLogging()
        {
            LogText += "----" + Log_ConvertStart + "----\n" +
                Log_InputFolder + JsonFolder + "\n" +
                Log_OutputFolder + BakinFolder + "\n" +
                Log_NoConvertComment + (IsAddNonConvertedComment ? Log_Do : Log_DontDo) + "\n" +
                Log_ReviceComment + (IsAddReviceComment ? Log_Do : Log_DontDo) + "\n";
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        #region Initialize
        public Json2BakinPluginController()
        {
            _loadService = new MvMapDataLoadService();
            _convertService = new Json2BakinConvertService(_loadService);
            _exportService = new BakinDataExportService(_loadService, _convertService);
            UiEnabled = true;
        }
        #endregion
    }
}
