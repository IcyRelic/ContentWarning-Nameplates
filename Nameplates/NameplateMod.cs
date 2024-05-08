using BepInEx;
using Nameplates.Component;
using Steamworks;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


namespace Nameplates
{
    [ContentWarningPlugin(ModGuid, ModVersion, true)]
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class NameplateMod : BaseUnityPlugin
    {
        public const string ModGuid = "com.icyrelic.nameplates";
        public const string ModName = "Nameplates";
        public const string ModVersion = "1.0.2";
        
        private List<Nameplate> nameplates = new List<Nameplate>();

        void Awake() => NameplateConfig.Bind(Config);

        void Update()
        {
            if(!NameplateConfig.ShowNameplates)
            {
                nameplates.ForEach(x => DestroyNameplate(x));
                return;
            }
                

            nameplates.ToList().FindAll(x => x.isDestroyed || !PlayerHandler.instance.playersAlive.Any(p => x.data.steamId == p.Photon().SteamID())).ForEach(x => DestroyNameplate(x));
            PlayerHandler.instance.playersAlive.FindAll(p => !HasNameplate(p) && !p.IsLocal).ForEach(p => CreateNameplate(p));
        }

        private bool HasNameplate(Player p) => nameplates.ToList().Any(x => x.data.steamId == p.Photon().SteamID());
        private void DestroyNameplate(Nameplate np)
        {
            if (!np.isDestroyed)
                Destroy(np.gameObject);

            nameplates.Remove(np);
        }
        private void CreateNameplate(Player p)
        {
            GameObject go = new GameObject($"Nameplate-{p.Photon().SteamID()}");
            NameplateData data = go.AddComponent<NameplateData>();
            data.player = p;
            data.steamId = p.Photon().SteamID();

            nameplates.Add(go.AddComponent<Nameplate>());
        }

    }    
}
