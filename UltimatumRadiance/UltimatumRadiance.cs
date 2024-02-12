using System.Diagnostics;
using System.Reflection;
using JetBrains.Annotations;
using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace UltimatumRadiance
{
    [UsedImplicitly]
    public class UltimatumRadiance : Mod, ITogglableMod
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once NotAccessedField.Global
        public static UltimatumRadiance Instance;

        public UltimatumRadiance() : base("Nightmare Ultimatum Radiance") { }

        public override void Initialize()
        {
            Instance = this;

            Log("Initalizing...");

            Load();

            Log("Initalized!");
        }


        public override string GetVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(UltimatumRadiance)).Location).FileVersion;
        }

        private void Load()
        {
            USceneManager.activeSceneChanged += CheckForRadiance;
            ModHooks.LanguageGetHook += LangGet;
        }

        public void Unload()
        {
            USceneManager.activeSceneChanged -= CheckForRadiance;
            ModHooks.LanguageGetHook -= LangGet;
        }

        private static string LangGet(string key, string sheettitle, string orig)
        {
            switch (key)
            {
                case "ABSOLUTE_RADIANCE_SUPER": return "Nightmare Ultimatum";
                case "GG_S_RADIANCE": return "God of light, sworn to crush any rebellion";
                case "GODSEEKER_RADIANCE_STATUE":
                    return "Congratulations on beating Nightmare Ultimatum Radiance!\n\nNow try doing it in my Pantheon.\n\nAnd don't forget to subscribe to youtube.com/fireb0rn!";
                default: return orig;
            }
        }

        private static void CheckForRadiance(Scene from, Scene to)
        {
            if (to.name != "GG_Radiance")
            {
                return;
            }

            // ReSharper disable once ObjectCreationAsStatement
            new GameObject("AbsFinder", typeof(AbsFinder));
        }
    }
}