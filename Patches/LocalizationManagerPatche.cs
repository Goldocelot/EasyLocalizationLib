using HarmonyLib;

namespace EasyLocalizationLib.Patches
{

    [HarmonyPatch(typeof(LocalizationManager))]
    internal class LocalizationManagerPatche
    {
        [HarmonyPatch("Awake"), HarmonyPostfix]
        static private void Awake()
        {
            EasyLocalizationManager.GetInstance().ApplyLocalization();
        }

        [HarmonyPatch("ClearDictionary"), HarmonyPostfix]
        static public void ClearDictionary()
        {
            EasyLocalizationManager.GetInstance().ApplyLocalization();
        }
    }
}
