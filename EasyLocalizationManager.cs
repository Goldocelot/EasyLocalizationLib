using BepInEx;
using HutongGames.PlayMaker;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace EasyLocalizationLib
{
    internal class EasyLocalizationManager
    {
        private Dictionary<BaseUnityPlugin, LANG> _pluginLocalizations;

        private static EasyLocalizationManager _instance;

        private EasyLocalizationManager()
        {
            _pluginLocalizations = new Dictionary<BaseUnityPlugin, LANG>();
        }

        public void AddPluginLocalization(BaseUnityPlugin plugin, LANG defaultLang)
        {
            Plugin.Logger.LogInfo("Adding the locazition");

            if (plugin == null) throw new NullReferenceException("The BaseUnityPlugin used to register your mod localization should be your Plugin instance, not null.");
            if (defaultLang == null) throw new NullReferenceException($"The default lang used to register the mod {plugin.name} should be the lang the mod support by default, not null.");
            if (_pluginLocalizations.ContainsKey(plugin)) throw new Exception($"{plugin.name} as already been registered by EasyLocalization.");
            if (!IsDefaultLangExisting(plugin, defaultLang)) throw new Exception($"{plugin.name} as no {defaultLang} file inside his EasyLocalization folder.");

            _pluginLocalizations.Add(plugin, defaultLang);

            Plugin.Logger.LogInfo("Locazition added");
        }

        public void ApplyLocalization()
        {

            Plugin.Logger.LogInfo("Applying the locazition");
            foreach (KeyValuePair<BaseUnityPlugin, LANG> pluginLocalization in _pluginLocalizations)
            {
                string langPath = GetPluginLocalizationPath(pluginLocalization.Key)+GetLang()+ ".json";

                if (File.Exists(langPath)) LoadFile(langPath);
                else LoadFile(GetPluginLocalizationPath(pluginLocalization.Key) + pluginLocalization.Value + ".json");
            }
            Plugin.Logger.LogInfo("Localization applied");
        }

        private void LoadFile(string path)
        {
            Plugin.Logger.LogInfo($"Loaded {path}");
        }

        private string GetLang()
        {
            try
            {
                string value = FsmVariables.GlobalVariables.GetFsmString("OptionsGlobalPath").Value;
                string filePath = Application.persistentDataPath + "/" + value;

                return ES3.Load<string>("GameLanguage", filePath).Split('_')[1].ToUpper();
            }
            catch (Exception ex)
            {
                return "DEFAULT";
            }
        }

        private string GetPluginLocalizationPath(BaseUnityPlugin plugin)
        {
            string[] splittedPath = Path.GetDirectoryName(plugin.Info.Location).Split(Path.DirectorySeparatorChar);
            string dllName = splittedPath[splittedPath.Length - 1];
            return plugin.Info.Location.Substring(0, plugin.Info.Location.Length - dllName.Length) + "EasyLocalization/";
        }
        private bool IsDefaultLangExisting(BaseUnityPlugin plugin, LANG defaultLang)
        {
            string defaultLocalizationpath = GetPluginLocalizationPath(plugin) + defaultLang + ".xml";
            return Directory.Exists(Path.GetDirectoryName(defaultLocalizationpath)) && File.Exists(defaultLocalizationpath);
        }

        public static EasyLocalizationManager GetInstance()
        {
            Plugin.Logger.LogInfo("Check instance");
            if (_instance == null)
            {
                Plugin.Logger.LogInfo("instance is null");
                _instance = new EasyLocalizationManager();
            }

            Plugin.Logger.LogInfo("returning instance");
            return _instance;
        }
    }
}
