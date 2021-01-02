using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;
using HatManager = OFCPCFDHIEF;


namespace Curaci2
{
    public class MyHats
    {
        static bool hatadded = false;
        [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetUnlockedHats))]
        public static class HatManagerHatsPatch
        {
            internal delegate bool d_LoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
            internal static d_LoadImage iCall_LoadImage;

            public static bool LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
            {
                if (iCall_LoadImage == null)
                    iCall_LoadImage = IL2CPP.ResolveICall<d_LoadImage>("UnityEngine.ImageConversion::LoadImage");

                var il2cppArray = (Il2CppStructArray<byte>)data;

                return iCall_LoadImage.Invoke(tex.Pointer, il2cppArray.Pointer, markNonReadable);
            }

            public static bool Prefix(HatManager __instance)
            {
                float pixelsPerUnit = 270f;
                HatBehaviour newhat = new HatBehaviour();
                Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream myStream = assembly.GetManifestResourceStream("simplehat.png");
                byte[] buttonTexture = new byte[myStream.Length];
                myStream.Read(buttonTexture, 0, (int)myStream.Length);
                LoadImage(tex, buttonTexture, false);
                newhat.MainImage = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
                newhat.ProductId = "hat_apple";
                newhat.InFront = true;
                newhat.NoBounce = true;
                if (!hatadded)
                {
                    __instance.AllHats.Add(newhat);
                    hatadded = true;
                }
                return true;
            }
        }
    }
}
