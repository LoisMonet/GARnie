using UnityEngine;
using System.Collections;

/// <summary>
/// Make Toast Message short or long
/// </summary>
public class ToastExample2 : MonoBehaviour
{
    private AndroidJavaObject toastExample = null;
    private AndroidJavaObject activityContext = null;

    /// <summary>
    /// true long else short time
    /// </summary>
    /// <param name="b">true long else short time;</param>
    /// <param name="message"></param>
    public void MethodToastExample(bool b, string message)
    {

#if UNITY_EDITOR

#elif UNITY_ANDROID
				
				using(AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
				using(AndroidJavaClass pluginClass = new AndroidJavaClass("eu.modelolito.test3.ToastExample")) {
					if(pluginClass != null) {
						toastExample = pluginClass.CallStatic<AndroidJavaObject>("instance");
						toastExample.Call("setContext", activityContext);
						activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							toastExample.Call("showMessage", message,b); //true long else short time;
						}));
					}
				}
			
#endif
    }

}
