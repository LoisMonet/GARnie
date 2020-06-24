using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// Manage anims of models
/// </summary>
public class ManageFile : MonoBehaviour
{
    /// <summary>
    /// get always good pos of anim button
    /// </summary>
    Vector3 managebutpos = Vector3.zero;

    /// <summary>
    /// Anim of model anim selected
    /// </summary>
    Animator anim;
    /// <summary>
    /// count number anim button
    /// </summary>
    int i = 1;
    /// <summary>
    /// for anim
    /// Path to access YOURMODELSFOLDER
    /// </summary>
    public static string path;

    /// <summary>
    /// for manage file button
    /// </summary>
    public Transform managebutton;


    /// <summary>
    /// panel with anims of 3d model animated
    /// </summary>
    public Transform panel;



    /// <summary>
    /// for character anim button
    /// </summary>
    public Transform anim_button;

    /// <summary>
    /// for anim button
    /// </summary>
    public Text txtanim;

    





    // Use this for initialization
    void Start()
    {

        txtanim.text = SceneAssetCtrl.MODELANIMMAINTITLE;

    }



    /// <summary>
    /// Update anims
    /// </summary>
    void Update()
    {


        if (SceneAssetCtrl.modelSelected != null)
        {   //to get all anim 3d models

            for (int b = 0; b < anim_button.parent.childCount; ++b)
            {
                if (anim_button.parent.GetChild(b).name.Equals($"{SceneAssetCtrl.ANIMIDENTIFIER}{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}"))
                {
                    Destroy(anim_button.parent.GetChild(b).gameObject);
                }
            }

            anim = null;


            foreach (Animator animTmp in SceneAssetCtrl.modelSelected.transform.GetComponentsInChildren<Animator>())
            {

                anim = animTmp;
                break;

            }

            if (anim != null)
            {
                ButtonAnim(anim);
            }

            SceneAssetCtrl.modelSelected = null;

        }

    }




    public void ButtonAnim(Animator anim)
    {

        managebutpos = anim_button.transform.localPosition;
        i = 1;  //currently is better(without change nbr textures on real time)
        foreach (AnimationClip animchar in anim.runtimeAnimatorController.animationClips)
        {
            txtanim.text = animchar.name;  //give just name of each animation
            Transform animbuttonbis = Instantiate(anim_button);

            animbuttonbis.name = $"{SceneAssetCtrl.ANIMIDENTIFIER}{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}";
            animbuttonbis.transform.SetParent(panel.transform);                                                                                                      //modify difference between two but
            animbuttonbis.GetComponent<RectTransform>().localPosition = new Vector3(anim_button.GetComponent<RectTransform>().localPosition.x, anim_button.GetComponent<RectTransform>().localPosition.y - (120.0f * i), anim_button.GetComponent<RectTransform>().localPosition.z);
            animbuttonbis.GetComponent<RectTransform>().localScale = anim_button.GetComponent<RectTransform>().localScale;
            animbuttonbis.GetComponent<RectTransform>().sizeDelta = anim_button.GetComponent<RectTransform>().sizeDelta;

            i++;
        }


        anim_button.transform.localPosition = managebutpos;

        txtanim.text = SceneAssetCtrl.MODELANIMMAINTITLE;
    }

    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Checks to see whether the passed directory exists, returning true of false
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public bool CheckDirectory(string directory)
    {
        if (Directory.Exists(path + "/" + directory))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Create a new directory
    /// </summary>
    /// <param name="directory"></param>
    public void CreateDirectory(string directory)
    {

        if (CheckDirectory(directory) == false)
        {
            Directory.CreateDirectory(path + "/" + directory);

        }

    }

    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Unused
    /// Delete a directory
    /// </summary>
    /// <param name="directory"></param>
    private void DeleteDirectory(string directory)
    {
        if (CheckDirectory(directory) == true)
        {
            print("Deleting directory: " + directory);
            Directory.Delete(path + "/" + directory, true);
        }
        else
        {
            print("Error: You are trying to delete the directory " + directory + " but it does not exist!");
        }
    }
    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Unused
    /// Move a directory
    /// </summary>
    /// <param name="originalDestination"></param>
    /// <param name="newDestination"></param>
    private void MoveDirectory(string originalDestination, string newDestination)
    {
        if (CheckDirectory(originalDestination) == true && CheckDirectory(newDestination) == false)
        {
            print("Moving directory: " + originalDestination);
            Directory.Move(path + "/" + originalDestination, path + "/" + newDestination);
        }
        else
        {
            print("Error: You are trying to move a directory but it either does not exist or a folder of the same name already exists");
        }
    }
    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Unused
    /// Finds subdirectories of a given directory
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public string[] FindSubDirectories(string directory)
    {
        print("Checking directory " + directory + " for sub directories");

        string[] subdirectoryList = Directory.GetDirectories(path + "/" + directory);
        return subdirectoryList;
    }
    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// get an array of files given directory
    /// </summary>
    /// <param name="directory">where are files to find</param>
    /// <returns>the files within a given directory</returns>
    public string[] FindFiles(string directory)
    {

        string[] fileArrayTmp = Directory.GetFiles(path + "/" + directory);


        return fileArrayTmp;
    }
    // ---------------------------------------------------------------------------------------------------


    /// <summary>
    /// Check to see whether a file exists
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool CheckFile(string filePath)
    {
        if (File.Exists(path + "/" + filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // ---------------------------------------------------------------------------------------------------


    /// <summary>
    /// Create a new file
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="filename"></param>
    /// <param name="filetype"></param>
    /// <param name="fileData"></param>
    public void CreateFile(string directory, string filename, string filetype, string fileData)
    {
        print("Creating " + directory + "/" + filename + "." + filetype);

        if (CheckDirectory(directory) == true)
        {
            if (CheckFile(directory + "/" + filename + "." + filetype) == false)
            {
                // Create the file
                File.WriteAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
            }
            else
            {
                print("The file " + filename + " already exists in " + path + "/" + directory);
            }
        }
        else
        {
            print("Unable to create file as the directory " + directory + " does not exist");
        }
    }
    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Unused
    ///  Read a file and returns it's contents
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="filename"></param>
    /// <param name="filetype"></param>
    /// <returns></returns>
    public string ReadFile(string directory, string filename, string filetype)
    {
        print("Reading " + directory + "/" + filename + "." + filetype);

        if (CheckDirectory(directory) == true)
        {
            if (CheckFile(directory + "/" + filename + "." + filetype) == true)
            {
                // Read the file
                string fileContents = File.ReadAllText(path + "/" + directory + "/" + filename + "." + filetype);

                return fileContents;
            }
            else
            {
                print("The file " + filename + " does not exist in " + path + "/" + directory);

                return null;
            }
        }
        else
        {
            print("Unable to read the file as the directory " + directory + " does not exist");

            return null;
        }
    }

    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Unused
    /// Delete a specified file
    /// </summary>
    /// <param name="filePath"></param>
    public void DeleteFile(string filePath)
    {
        if (File.Exists(path + "/" + filePath))
        {
            File.Delete(path + "/" + filePath);
        }
        else
        {
            print("unable to delete file as it does not exist");
        }
    }
    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Unused
    /// Update a files contents
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="filename"></param>
    /// <param name="filetype"></param>
    /// <param name="fileData"></param>
    /// <param name="mode"></param>
    public void UpdateFile(string directory, string filename, string filetype, string fileData, string mode)
    {
        print("Updating " + directory + "/" + filename + "." + filetype);

        if (CheckDirectory(directory) == true)
        {
            if (CheckFile(directory + "/" + filename + "." + filetype) == true)
            {
                if (mode == "replace")
                {
                    File.WriteAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
                }

                if (mode == "append")
                {
                    File.AppendAllText(path + "/" + directory + "/" + filename + "." + filetype, fileData);
                }
            }
            else
            {
                print("The file " + filename + " does not exist in " + path + "/" + directory);
            }
        }
        else
        {
            print("Unable to create file as the directory " + directory + " does not exist");
        }
    }
    // ---------------------------------------------------------------------------------------------------

    /// <summary>
    /// Unused
    /// Process an opened file
    /// </summary>
    /// <param name="filepath"></param>
    public void ProcessFile(string filepath)
    {
        print("processing " + filepath);

        string fileContents = File.ReadAllText(filepath);

        print("Read file which contains: " + fileContents);
    }
    // ---------------------------------------------------------------------------------------------------

    public static string GetDCIMPath()
    {
        IntPtr classID = AndroidJNI.FindClass("com/intel/inde/mp/samples/unity/Capturing"); // com.intel.inde.mp.samples.unity // com/intel/penelope/Capturing

        IntPtr getDirectoryDCIMMethodID = AndroidJNI.GetStaticMethodID(classID, "getDirectoryDCIM", "()Ljava/lang/String;");
        jvalue[] args = new jvalue[0];
        string videoDir = AndroidJNI.CallStaticStringMethod(classID, getDirectoryDCIMMethodID, args);
        return videoDir;

    }
}

