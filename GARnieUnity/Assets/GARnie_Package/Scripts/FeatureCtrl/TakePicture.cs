using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// To take a picture of the scene
/// </summary>
public class TakePicture : MonoBehaviour
{

    /// <summary>
    /// Without raw image
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="fileName"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public static IEnumerator TakeScreenshot(Rect rect, string fileName, float ratio)
    {
        // Wait for the end of the frame to avoid any rendering artifacts
        yield return new WaitForEndOfFrame();

        // Get the camera from which the screenshot will be grabbed
        Camera camera = Camera.main;

        // Apply the ratio
        rect.height *= ratio;
        rect.width *= ratio;

        //Set the target texture render  
        camera.Render();

        // Create a a new Texture2D that is the same size as the camera view
        Texture2D texture = new Texture2D((int)(camera.pixelWidth), (int)(camera.pixelHeight));

        // Read the pixels of the screen and apply them to the texture
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        // Encode To PNG
        byte[] bytes = texture.EncodeToPNG();

        //Save to 
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/Pictures/" + fileName, bytes);

    }
}