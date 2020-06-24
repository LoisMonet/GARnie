using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Use "BatteryLevelPlugin" to display battery level on the Menu
/// </summary>
public class BatteryMonitor : MonoBehaviour
{
    /// <summary>
    /// Unused but I keep
    /// Text with the battery level
    /// </summary>
    public Text batteryLevelText;
    /// <summary>
    /// Text widget showing the battery level
    /// We're using fonticons so even though this looks like a picture, it's actually a text widget.
    /// </summary>
    public Text batteryLevelIcon;

    // Character codes to use for the Font Awesome icons to use.
    static readonly string BATTERY_LEVEL_100 = Char.ConvertFromUtf32(0xf240);
    static readonly string BATTERY_LEVEL_75 = Char.ConvertFromUtf32(0xf241);
    static readonly string BATTERY_LEVEL_50 = Char.ConvertFromUtf32(0xf242);
    static readonly string BATTERY_LEVEL_25 = Char.ConvertFromUtf32(0xf243);
    static readonly string BATTERY_LEVEL_0 = Char.ConvertFromUtf32(0xf244);

    // Update is called once per frame
    void Update()
    {
        UpdateStatusIndicators();
    }

    /// <summary>
    /// Find the current device battery level and update indicators in the 
    /// UI accordingly.
    /// </summary>
    void UpdateStatusIndicators()
    {
        var currentBatteryLevel = BatteryLevelPlugin.GetBatteryLevel() * 100f;
        batteryLevelText.text = "Battery:" + String.Format("{0}%", currentBatteryLevel);

        // Show the icon that matches the current level most closely.
        if (currentBatteryLevel >= 88)
        {
            batteryLevelIcon.text = BATTERY_LEVEL_100;
        }
        else if (currentBatteryLevel >= 63)
        {
            batteryLevelIcon.text = BATTERY_LEVEL_75;
        }
        else if (currentBatteryLevel >= 38)
        {
            batteryLevelIcon.text = BATTERY_LEVEL_50;
        }
        else if (currentBatteryLevel >= 13)
        {
            batteryLevelIcon.text = BATTERY_LEVEL_25;
        }
        else
        {
            batteryLevelIcon.text = BATTERY_LEVEL_0;
        }
    }
}
