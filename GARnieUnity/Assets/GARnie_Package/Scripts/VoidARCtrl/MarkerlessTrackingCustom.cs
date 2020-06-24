using UnityEngine;

/// <summary>
/// Custom class of MarkerlessTracking of VoidAR
/// </summary>
public class MarkerlessTrackingCustom : MonoBehaviour, ITricking
{
    private int lastState = -1;
    private bool isActive = false;
    private int timeDelay = 0;
    /// <summary>
    /// Get value of stateCode
    /// </summary>
    public static int state2;
    /// <summary>
    /// To get the y position of GameObject tracked 
    /// </summary>
    public static float posYtrackingallgood;

    /// <summary>
    /// 跟踪反馈（每帧）
    /// </summary>
    /// <param name="stateCode"></param>
    public void UpdateTracking(int stateCode)
    {
        if (stateCode == 1099)
        {
            Debug.LogError("server error");
        }
        else if (stateCode == 501)
        {
            Debug.LogError("key error");
        }
        else if (stateCode == 101)
        {
            Debug.LogError("use time limit error");
        }
        lastState = stateCode;

        if (lastState == 2)
        {
            timeDelay = 100;
        }
        state2 = stateCode;//add myself
        posYtrackingallgood = transform.localPosition.y; //add myself
    }

    public int GetTrackingState()
    {
        return lastState;
    }

    /// <summary>
    /// 设置跟踪活动状态
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value)
    {
        isActive = value;
    }

    public bool GetActive()
    {
        return isActive;
    }
}