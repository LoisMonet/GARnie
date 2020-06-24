using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Info to debug in app
/// When click on Battery
/// </summary>
public class ExtraInfo : MonoBehaviour
{
    /// <summary>
    /// Used in extrainfo
    /// </summary>
    int j;

    /// <summary>
    /// Text with debug info
    /// </summary>
    public Text text;

    // Use this for initialization
    void Start()
    {
        text.gameObject.SetActive(false);
    }

    /// <summary>
    /// show or not extra info
    /// </summary>
    public void ExtraInfoM()
    {
        j++;
        if (j % 2 == 1)
            text.gameObject.SetActive(true);
        else
            text.gameObject.SetActive(false);

    }


}

