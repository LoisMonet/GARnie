using UnityEngine;

/// <summary>
/// Use java class to get battery level on Android
/// Use eu.modelolito.test3.AndroidPlugin android package
/// </summary>
public class BatteryLevelPlugin

{

    public static float GetBatteryLevel()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {

                    using (var androidPlugin = new AndroidJavaObject("eu.modelolito.test3.AndroidPlugin", currentActivity))
                    {

                        return androidPlugin.Call<float>("GetBatteryPct");
                    }
                }
            }
        }

        return 1f;
    }
}
