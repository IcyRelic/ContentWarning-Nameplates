using BepInEx;
using Steamworks;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nameplates.Settings;
using TMPro;
using UnityEngine;


namespace Nameplates
{
    [ContentWarningPlugin(ModGuid, ModVersion, true)]
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    [BepInDependency(ContentSettings.MyPluginInfo.PLUGIN_GUID)]
    public class NameplateMod : BaseUnityPlugin
    {
        public const string ModGuid = "com.icyrelic.nameplates";
        public const string ModName = "Nameplates";
        public const string ModVersion = "1.0.1";
        
        private List<Nameplate> nameplates = new List<Nameplate>();

        void Update()
        {
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
            var maxHealthField = data.player.data.GetType().GetField("maxHealth", BindingFlags.Static | BindingFlags.Public);
            
            if(maxHealthField == null)
                return;
            data.pmaxHealth = (float)maxHealthField.GetValue(null);

            nameplates.Add(go.AddComponent<Nameplate>());
        }

    }

    internal class NameplateData : MonoBehaviour
    {
        public Player player;
        public ulong steamId;
        public float pmaxHealth;
    }

    internal class Nameplate : MonoBehaviour
    {
        public TextMeshPro tmp;
        public NameplateData data;
        public bool isDestroyed = false;

        void Awake()
        {
            data = gameObject.GetComponent<NameplateData>();
            
            tmp = this.gameObject.AddComponent<TextMeshPro>();
            tmp.fontSize = 1.5f;
            tmp.color = Color.green;
            tmp.alignment = TextAlignmentOptions.Center;
            
            gameObject.transform.SetParent(data.player.refs.cameraPos);
        }

        void Update()
        {
            tmp.text = data.player.Photon().NickName;

            if (NameplateConfig.ColourBasedOnHealth)
            {
                tmp.color = Color.Lerp(Color.red, Color.green, data.player.data.health / data.pmaxHealth);
            }
            
            Vector3 pos = data.player.refs.cameraPos.position;
            pos.y += 0.5f;

            tmp.transform.position = pos;
            tmp.transform.LookAt(MainCamera.instance.Camera().transform);
            tmp.transform.Rotate(Vector3.up, 180f);
        }

        void OnDestroy()
        {
            isDestroyed = true;
        }
    }

    internal static class Extensions
    {
        internal static Photon.Realtime.Player Photon(this Player player) => player.refs.view.Owner;
        internal static Camera Camera(this MainCamera camera) => camera.GetComponent<Camera>();

        internal static ulong SteamID(this Photon.Realtime.Player player)
        {
            SteamAvatarHandler.TryGetSteamIDForPlayer(player, out CSteamID steamID);

            return steamID.m_SteamID;
        }
    }
}
