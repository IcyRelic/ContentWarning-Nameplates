using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Nameplates
{
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
