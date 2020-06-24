using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// To manage buttons to change of 3D model
/// </summary>
public class ThreeDModelButton : MonoBehaviour
{
    /// <summary>
    /// Button to start an import of 3D model
    /// </summary>
    public GameObject Objbut;

    /// <summary>
    /// panel with anims of 3d model animated
    /// </summary>
    public GameObject Manageanimfilebut;
    /// <summary>
    /// COntains 5 buttons for 3 default models in the app + 1 import button +1 with silkke button
    /// </summary>
    public GameObject butForTrack1;


    /// <summary>
    /// The name of the last model created or selected
    /// </summary>
    public static string lastcallGO = " ";
    /// <summary>
    /// Don't Understand
    /// Before lastcallgo with silkke clone
    /// </summary>
    string lastLastcallGo = " ";
    /// <summary>
    /// clones number
    /// </summary>
    public static int j = 0;
    /// <summary>
    /// to go just one time in update per click on one of four buttons
    /// </summary>
    public static bool linkedswithAnim;
    /// <summary>
    /// new material for each clone
    /// </summary>
    Material mat;
    /// <summary>
    /// /to create clone
    /// </summary>
    Transform cloneGO;
    /// <summary>
    /// Virtual Joystick
    /// </summary>
    public GameObject virtualJoystick;

    static private ThreeDModelButton instance;
    /// <summary>
    /// to get name of avatar
    /// </summary>
    public static string getAvatarName;

    /// <summary>
    /// Don't Understand
    /// </summary>
    public Vector3 goodpose;

    public static int countpika = 0;

    /// <summary>
    ///  Wait 0.1f to check if Trilib is available
    /// </summary>
    public void Start()
    {
        Invoke("InitModelButton", 0.1f);
    }

    public void InitModelButton()
    {
        for (int i = 0; i < SceneAssetCtrl.instance.modelButtons.Count; i++)
        {
            int j = i;
            SceneAssetCtrl.instance.modelButtons[i].GetComponent<Button>().onClick.AddListener(() => ModelButton(j));
        }

        if (SceneAssetCtrl.instance.modelImportAsset != null)
        {
            SceneAssetCtrl.instance.modelImportButton.GetComponent<Button>().onClick.AddListener(() => ModelImportButton());

        }

        if (SceneAssetCtrl.instance.modelSilkkeAsset != null)
        {
            SceneAssetCtrl.instance.modelSilkkeButton.GetComponent<Button>().onClick.AddListener(() => ModelSilkkeButton());

        }
    }

    /// <summary>
    /// show joystick just with silkke
    /// </summary>
    public void JoyOrNot()
    {
        //print(lastcallGO);
        if ((SceneAssetCtrl.instance.modelSilkkeAsset != null) &&  lastcallGO.Contains(SceneAssetCtrl.instance.modelSilkkeAsset.name))
        {
            virtualJoystick.gameObject.SetActive(true);
        }
        else
        {
            virtualJoystick.gameObject.SetActive(false);
        }

    }

    /// <summary>
    ///  Don't Understand
    /// </summary>
    /// <param name="i"></param>
    public void GetClone(int i)
    {

        j = 0;
        foreach (Transform go in transform)
        { //loop to get clone number
            if (go.gameObject.activeSelf && go.name.Contains(transform.GetChild(i).name))
            {
                j++;

            }
        }



        if (j > 0)
        {


            if (j == 1)
            { //to get a clone of model by default 
                cloneGO = Instantiate(this.transform.Find(transform.GetChild(i).name));
            }
            else
            { //to get a clone of the clone before
                cloneGO = Instantiate(this.transform.Find(transform.GetChild(i).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + (j - 1)));
            }

            cloneGO.name = transform.GetChild(i).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + j;

            cloneGO.parent = this.transform;

            goodpose = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1f));
            goodpose = new Vector3(goodpose.x, MarkerlessTrackingCustom.posYtrackingallgood, goodpose.z); //allow to get a good tracking for all 3d models

            cloneGO.localPosition = new Vector3(goodpose.x - transform.root.localPosition.x, goodpose.y - transform.root.localPosition.y, goodpose.z - transform.root.localPosition.z); //pose at the center of the screen
            if (j == 1)
            {
                cloneGO.localRotation = transform.GetChild(i).localRotation;
                cloneGO.localScale = transform.GetChild(i).localScale;
            }
            else
            {
                cloneGO.localRotation = transform.Find(transform.GetChild(i).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + (j - 1)).transform.localRotation; //get rot of last clone posed
                cloneGO.localScale = transform.Find(transform.GetChild(i).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + (j - 1)).transform.localScale;       //get scale of last clone posed
            }
        }


    }


    /// <summary>
    /// To go just one time in update per click on one of four buttons
    /// </summary>
    public void LinkedSwitchAnim()
    {

        linkedswithAnim = true;
    }


    /// <summary>
    /// Start tracking just if no object active
    /// </summary>
    /// <returns></returns>
    public bool NoActiveObject()
    {
        foreach (Transform tf in transform)
        {
            if (tf.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void ModelButton(int indexPos)
    {


        int indexAsset = SceneAssetCtrl.instance.modelAssets[indexPos].transform.GetSiblingIndex();
        //print(indexAsset);
        if (SceneAssetCtrl.instance.modelAssets[indexPos].gameObject.activeSelf)
        { //if pikachu active self
            GetClone(indexAsset);
            lastcallGO = transform.GetChild(indexAsset).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + j; //get the name of last 3d model posed
            LinkedSwitchAnim();

        }
        else
        {
            if (NoActiveObject())
            {


                VoidAR.GetInstance().startMarkerlessTracking();
                //MoveScript.play ();                                //for arrow raycast? //CONTINUE TOMORROW
                countpika++;
                transform.GetChild(indexAsset).gameObject.SetActive(true);



            }
            else
            {
                transform.GetChild(indexAsset).gameObject.SetActive(true);
                goodpose = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
                goodpose = new Vector3(goodpose.x, MarkerlessTrackingCustom.posYtrackingallgood, goodpose.z);

                transform.GetChild(indexAsset).localPosition = new Vector3(goodpose.x - transform.root.localPosition.x, goodpose.y - transform.root.localPosition.y, goodpose.z - transform.root.localPosition.z); //pose at the center of the screen
            }
            lastcallGO = transform.GetChild(indexAsset).name;

        }

        SceneAssetCtrl.modelSelected = transform.GetChild(indexAsset).gameObject;

        JoyOrNot();

    }


    public void ModelImportButton()
    {
        if(SceneAssetCtrl.instance.modelImportAsset != null)
        {
            int indexAsset = SceneAssetCtrl.instance.modelImportAsset.transform.GetSiblingIndex();

            //print(indexAsset);
            if (SceneAssetCtrl.instance.modelImportAsset.gameObject.activeSelf)
            {
                GetClone(indexAsset);
                lastcallGO = transform.GetChild(indexAsset).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + j;
                LinkedSwitchAnim();
                if (j == 1)
                {
                    mat = Instantiate(this.transform.Find(transform.GetChild(indexAsset).name).transform.GetChild(1).GetComponent<MeshRenderer>().material);  //get an instance of the material of the default model for first clone
                }
                else if (j > 1)
                {
                    mat = Instantiate(this.transform.Find(transform.GetChild(indexAsset).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + (j - 1)).transform.GetChild(1).GetComponent<MeshRenderer>().material); //get an instance of the material of the clone before for each clone

                }
                GameObject.Find(lastcallGO).transform.GetChild(1).GetComponent<MeshRenderer>().material = mat;
            }
            else
            {
                if (NoActiveObject())
                {
                    VoidAR.GetInstance().startMarkerlessTracking();
                    transform.GetChild(indexAsset).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(indexAsset).gameObject.SetActive(true);
                    goodpose = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
                    goodpose = new Vector3(goodpose.x, MarkerlessTrackingCustom.posYtrackingallgood, goodpose.z); //WORK ON THAT LATER

                    transform.GetChild(indexAsset).localPosition = new Vector3(goodpose.x - transform.root.localPosition.x, goodpose.y - transform.root.localPosition.y, goodpose.z - transform.root.localPosition.z); //pose at the center of the screen
                }
                lastcallGO = transform.GetChild(indexAsset).name;





                Objbut.SetActive(true);

                Manageanimfilebut.transform.localScale = Vector3.zero;

            }

            JoyOrNot();
        }
       

    }

    public void ModelSilkkeButton()
    {

        if((SceneAssetCtrl.instance.modelSilkkeAsset != null))
        {
            int indexAsset = SceneAssetCtrl.instance.modelSilkkeAsset.transform.GetSiblingIndex();
            if (SceneAssetCtrl.instance.modelSilkkeAsset.gameObject.activeSelf)
            {
                GetClone(indexAsset);
                lastcallGO = transform.GetChild(indexAsset).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + j;
                if (j > 1)
                {
                    lastLastcallGo = transform.GetChild(indexAsset).name + $"{SceneAssetCtrl.ANGLEOPENIDENTIFIER}{SceneAssetCtrl.CLONEIDENTIFIER}{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}" + (j - 1);
                    GameObject.Find(lastcallGO).transform.localPosition = GameObject.Find(lastLastcallGo).transform.localPosition; //don't get 2 avatars at the same position

                    if (GameObject.Find(lastcallGO).transform.Find(RotateScale.getAvatarName).transform.localPosition.y < 0)
                    { //waiting | don't fall or block in the ground
                      //print(GameObject.Find (lastcallGO).transform.Find(RotateScale.getAvatarName).transform.localPosition.y+"|test");
                        GameObject.Find(lastcallGO).transform.Find(RotateScale.getAvatarName).transform.localPosition = new Vector3(GameObject.Find(lastcallGO).transform.Find(RotateScale.getAvatarName).transform.localPosition.x, 0, GameObject.Find(lastcallGO).transform.Find(RotateScale.getAvatarName).transform.localPosition.z);
                    }

                    GameObject.Find(lastcallGO).transform.Translate(0, 0, 1); //don't get 2 avatars at the same position//MAYBE FOR JUST EDITOR

                }
                else
                { //don't fall
                  //GameObject.Find (lastcallGO).transform.localPosition = new Vector3 (GameObject.Find (lastcallGO).transform.localPosition.x, MarkerlessTracking.posYtrackingallgood, GameObject.Find (lastcallGO).transform.localPosition.z);
                    GameObject.Find(lastcallGO).transform.Find(RotateScale.getAvatarName).transform.localPosition = Vector3.zero; //WAITING
                                                                                                                                  //print (GameObject.Find (lastcallGO).transform.Find (RotateScale.getAvatarName).transform.localPosition + " testlCG");

                }


                LinkedSwitchAnim();
            }
            else
            {
                if (NoActiveObject())
                {
                    VoidAR.GetInstance().startMarkerlessTracking();
                    transform.GetChild(indexAsset).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(indexAsset).gameObject.SetActive(true);
                    goodpose = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1));
                    goodpose = new Vector3(goodpose.x, MarkerlessTrackingCustom.posYtrackingallgood, goodpose.z);

                    transform.GetChild(indexAsset).localPosition = new Vector3(goodpose.x - transform.root.localPosition.x, goodpose.y - transform.root.localPosition.y, goodpose.z - transform.root.localPosition.z); //pose at the center of the screen
                }
                lastcallGO = transform.GetChild(indexAsset).name;
                getAvatarName = lastcallGO;
                //print (getAvatarName + "|tessssss");




            }

            JoyOrNot();
        }
        

    }

    





}

