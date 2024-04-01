using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Project.Utils
{
    public class Debug
    {

#if UNITY_EDITOR
        public static bool DebugMode = true;
#elif Product
        public static bool DebugMode = false;
#elif Stage
        public static bool DebugMode = true;
#elif Develop
        public static bool DebugMode = true;
#endif

        public static void Log(object message)
        {
            if (DebugMode)
            {
                UnityEngine.Debug.Log(message);
            }
        }
        public static void ColorLog(object message, string color = null)
        {
            if (DebugMode)
            {
                if (string.IsNullOrEmpty(color))
                    color = "yellow";

                UnityEngine.Debug.Log("<color=" + color + ">" + message + "</color>");
            }
        }

        public static void LogError(object message)
        {
            if (DebugMode)
            {
                UnityEngine.Debug.LogError(message);
            }
        }

        public static void LogWarning(object message)
        {
            if (DebugMode)
            {
                UnityEngine.Debug.LogWarning(message);
            }
        }

        public static void Log(object[] param)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#####");
            for (int i = 0; i < param.Length; i++)
            {
                sb.Append(" " + param[i] + ",");
            }

            string str = sb.ToString().TrimEnd(',');
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            if (DebugMode)
            {
                Debug.DrawLine(start, end, color);
            }
        }
    }
}