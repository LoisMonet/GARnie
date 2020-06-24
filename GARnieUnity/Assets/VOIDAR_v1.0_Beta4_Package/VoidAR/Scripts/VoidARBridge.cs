using System.Collections.Generic;
using UnityEngine.Experimental;
using UnityEngine;
public static class VoidARBridge
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Register()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Debug.Log("VoidARBridge");
#if UNITY_ANDROID
        VoidAR.GetInstance().helper = new VoidARAndroidHelper();
#elif UNITY_IOS
        VoidAR.GetInstance().helper = new VoidARiOSHelper();
#else
        VoidAR.GetInstance().helper = new VoidARDefaultHelper();
#endif
    }
}