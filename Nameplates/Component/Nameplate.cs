using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static Player;

namespace Nameplates.Component
{
    internal class Nameplate : MonoBehaviour
    {
        public TextMeshPro tmp;
        public NameplateData data;
        public bool isDestroyed = false;
        

        void Awake()
        {
            data = gameObject.GetComponent<NameplateData>();
            tmp = this.gameObject.AddComponent<TextMeshPro>();
            tmp.alignment = TextAlignmentOptions.Center;
            gameObject.transform.SetParent(data.player.refs.cameraPos);
        }

        void Update()
        {
            tmp.color = NameplateConfig.UseHealthColors ?
                    Color.Lerp(Color.red, Color.green, data.player.data.health / PlayerData.maxHealth) :
                    Color.white;
            tmp.fontSize = NameplateConfig.FontSize;

            tmp.text = data.player.Photon().NickName;

            if(NameplateConfig.DisplayHealth)
            {
                tmp.text += NameplateConfig.HealthNewLine ? "\n" : " ";
                tmp.text += $"({Math.Round((data.player.data.health / PlayerData.maxHealth) * 100)}%)";
            }

            Vector3 pos = data.player.refs.cameraPos.position;
            pos.y += NameplateConfig.HeightOffset;

            tmp.transform.position = pos;
            tmp.transform.LookAt(MainCamera.instance.Camera().transform);
            tmp.transform.Rotate(Vector3.up, 180f);
        }

        void OnDestroy()
        {
            isDestroyed = true;
        }
    }

    internal class NameplateData : MonoBehaviour
    {
        public Player player;
        public ulong steamId;
    }
}
