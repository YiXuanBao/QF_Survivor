using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using TMPro;
using System.Collections.Generic;
using QF;


public static class UtilityBuiltin
{
    public static class ResPath
    {
        public static string GetCombinePath(params string[] args)
        {
            return Utility.Path.GetRegularPath(System.IO.Path.Combine(args));
        }
        public static string GetDataTablePath(string name)
        {
            return Utility.Text.Format("Assets/AAAGame/DataTable/{0}.txt", name);
        }

        public static string GetSoundPath(string name)
        {
            return Utility.Text.Format("Assets/AAAGame/Audio/{0}", name);
        }

        public static string GetScenePath(string name)
        {
            return Utility.Text.Format("Assets/AAAGame/Scene/{0}.unity", name);
        }
        public static string GetEntityPath(string name)
        {
            return Utility.Text.Format("Assets/AAAGame/Prefabs/Entity/{0}.prefab", name);
        }

        public static string GetUIFormPath(string v)
        {
            return Utility.Text.Format("Assets/AAAGame/Prefabs/UI/{0}.prefab", v);
        }

        public static string GetTexturePath(string fileName)
        {
            return Utility.Text.Format("Assets/AAAGame/Textures/{0}", fileName);
        }
        public static string GetSpritesPath(string fileName)
        {
            return Utility.Text.Format("Assets/AAAGame/Sprites/{0}", fileName);
        }
        public static string GetConfigPath(string v)
        {
            return Utility.Text.Format("Assets/AAAGame/Config/{0}.txt", v);
        }
        public static string GetScriptableConfigPath(string v)
        {
            return Utility.Text.Format("Assets/AAAGame/ScriptableAssets/{0}.asset", v);
        }

        public static string GetPrefab(string v)
        {
            return Utility.Text.Format("Assets/AAAGame/Prefabs/{0}.prefab", v);
        }

        public static string GetLanguagePath(string v)
        {
            return Utility.Text.Format("Assets/AAAGame/Language/{0}.json", v);
        }
        public static string GetMaterialPath(string v)
        {
            return Utility.Text.Format("Assets/AAAGame/Material/{0}.mat", v);
        }

        public static string GetHotfixDll(string dllName)
        {
            return Utility.Text.Format("Assets/AAAGame/HotfixDlls/{0}.bytes", dllName);
        }

        public static string GetScriptableAsset(string v)
        {
            return Utility.Text.Format("Assets/AAAGame/ScriptableAssets/{0}.asset", v);
        }
    }
    public class Json
    {
        public static string ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        
        public static T ToObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static object ToObject(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        }
    }
    public class CurvePath
    {
        public static float GetPathLength(Vector3[] pathPoints, bool isLoop = false)
        {
            float result = 0;
            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                result += Vector3.Distance(pathPoints[i], pathPoints[i + 1]);
            }

            if (isLoop)
            {
                result += Vector3.Distance(pathPoints[0], pathPoints[pathPoints.Length - 1]);
            }
            return result;
        }
    }
    public class ColorExt
    {
        /// <summary>
        /// 获取两个颜色的相似度
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static float Difference(Color c1, Color c2)
        {
            c1 *= 255; c2 *= 255;
            var averageR = (c1.r + c2.r) * 0.5f;
            return Mathf.Sqrt((2 + averageR / 255f) * Mathf.Pow(c1.r - c2.r, 2) + 4 * Mathf.Pow(c1.g - c2.g, 2) + (2 + (255 - averageR) / 255f) * Mathf.Pow(c1.b - c2.b, 2)) / (3 * 255f);
        }
    }
    public class ArrayExt
    {
        public static void Disorder<T>(ref List<T> list)
        {
            int halfLen = list.Count / 2;
            for (int i = 0; i < halfLen; i++)
            {
                int idxA = UnityEngine.Random.Range(0, halfLen + 1);
                int idxB = list.Count - idxA - 1;
                T tmpValue = list[idxA];
                list[idxA] = list[idxB];
                list[idxB] = tmpValue;
            }
        }
    }

    public class Valuer
    {
        public static string GetByteLengthString(long byteLength)
        {
            if (byteLength < 1024L) // 2 ^ 10
            {
                return Utility.Text.Format("{0} Bytes", byteLength);
            }

            if (byteLength < 1048576L) // 2 ^ 20
            {
                return Utility.Text.Format("{0:F2} KB", byteLength / 1024f);
            }

            if (byteLength < 1073741824L) // 2 ^ 30
            {
                return Utility.Text.Format("{0:F2} MB", byteLength / 1048576f);
            }

            if (byteLength < 1099511627776L) // 2 ^ 40
            {
                return Utility.Text.Format("{0:F2} GB", byteLength / 1073741824f);
            }

            if (byteLength < 1125899906842624L) // 2 ^ 50
            {
                return Utility.Text.Format("{0:F2} TB", byteLength / 1099511627776f);
            }

            if (byteLength < 1152921504606846976L) // 2 ^ 60
            {
                return Utility.Text.Format("{0:F2} PB", byteLength / 1125899906842624f);
            }

            return Utility.Text.Format("{0:F2} EB", byteLength / 1152921504606846976f);
        }

        public static string HideChars(string str)
        {
            var emailChars = str.ToCharArray();
            if (emailChars.Length > 5)
            {
                for (int i = 3; i < emailChars.Length - 2; i++)
                {
                    emailChars[i] = '*';
                }
            }
            else
            {
                for (int i = 1; i < emailChars.Length - 1; i++)
                {
                    emailChars[i] = '*';
                }
            }
            return emailChars.ArrayToString();
        }
        /// <summary>
        /// 转换成1K 1M
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToCoins(int num)
        {
            if (num >= 100000000)
                return (num / 1000000D).ToString("0.#M");
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##M");
            if (num >= 100000)
                return (num / 1000D).ToString("0K");
            if (num >= 100000)
                return (num / 1000D).ToString("0.#K");
            if (num >= 1000)
                return (num / 1000D).ToString("0.##K");
            return num.ToString("#,0");
        }
        public static string ToCoins(float num)
        {
            return ToCoins(Valuer.RoundToInt(num));
        }
        /// <summary>
        /// 转换成美元格式
        /// </summary>
        /// <param name="usd"></param>
        /// <returns></returns>
        public static string ToUsd(float usd)
        {
            return Utility.Text.Format("{0:N2}", usd);
        }
        public static string ToUsd(long usdCent)
        {
            return ToUsd(usdCent * 0.01f);
        }
        public static string Float2String(float v, int dotNum)
        {
            if (v - (int)v != 0)
            {
                return v.ToString(Utility.Text.Format("N{0}", dotNum));
            }
            else
            {
                return v.ToString();
            }
        }
        /// <summary>
        /// 格式化秒为 00:00:00
        /// </summary>
        /// <param name="seconds">单位秒</param>
        /// <returns></returns>
        public static string ToTime(float seconds)
        {
            return System.TimeSpan.FromSeconds(seconds).ToString("hh\\:mm\\:ss");
        }

        public static int RoundToInt(float value)
        {
            return (int)Math.Round(value, MidpointRounding.AwayFromZero);
        }
    }

    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
}