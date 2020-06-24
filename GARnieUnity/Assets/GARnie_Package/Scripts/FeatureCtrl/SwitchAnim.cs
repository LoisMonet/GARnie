using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// To manage anim of 3d model animated by default(without Trilib)
/// </summary>
public class SwitchAnim : MonoBehaviour
{

    /// <summary>
    /// Name of anim choosen
    /// </summary>
    public static string animchoice = "";

    /// <summary>
    /// Allow to initialize animchoice
    /// </summary>
    bool boolanimchoice = false;
    /// <summary>
    /// Allow to call just one time anim play in Update 
    /// </summary>
    bool boolequalschoice = false;
    /// <summary>
    /// To save current anim state
    /// </summary>
    AnimatorClipInfo[] myAnimatorClip;
    /// <summary>
    /// get clone using threedmodelbutton.lastcallGO
    /// </summary>
    string getclonebefore;

    /// <summary>
    /// Get clone nbr -1 
    /// </summary>
    int getclonenbr;


    /// <summary>
    /// To run anim asked
    /// </summary>
    public void Tapbutton()
    {


        if ((!(transform.GetChild(0).GetComponent<Text>().text.Equals(SceneAssetCtrl.MODELANIMMAINTITLE)) && !(GameObject.FindGameObjectWithTag("3dmodelanim") == null)))
        {

            foreach (GameObject go in SceneAssetCtrl.instance.modelAssets)
            {
                if (ThreeDModelButton.lastcallGO.Contains(go.name) && GameObject.FindGameObjectWithTag("inbb") == null)
                {
                    animchoice = transform.GetChild(0).GetComponent<Text>().text;
                    AnimatorOfGameObject(GameObject.Find(ThreeDModelButton.lastcallGO)).Play(animchoice);

                }
            }

        }

    }

    /// <summary>
    /// To get animator of a game Object
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public Animator AnimatorOfGameObject(GameObject go)
    {
     
        foreach (Animator animTmp in go.GetComponentsInChildren<Animator>())
        {

            return animTmp;
            

        }
        
        return null;
    }


    /// <summary>
    /// Allow to have the same anim if you reinstanciate 3d model 
    /// </summary>
    void Update()
    {

        if (ThreeDModelButton.linkedswithAnim)
        { //button clicked

            if (!(GameObject.Find(ThreeDModelButton.lastcallGO) == null) && (GameObject.Find(ThreeDModelButton.lastcallGO).activeSelf) && !(AnimatorOfGameObject(GameObject.Find(ThreeDModelButton.lastcallGO)) == null))
            {

                if (ThreeDModelButton.lastcallGO.Contains(SceneAssetCtrl.CLONEIDENTIFIER))
                { //put value just if last call of GO is a clone


                    if (ThreeDModelButton.lastcallGO.Contains(SceneAssetCtrl.ANGLECLOSEIDENTIFIER) && !ThreeDModelButton.lastcallGO.Contains($"{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}1"))
                    { //get clone to get the same anim that clone-1
                        getclonebefore = ThreeDModelButton.lastcallGO.Substring(ThreeDModelButton.lastcallGO.IndexOf(SceneAssetCtrl.ANGLECLOSEIDENTIFIER) + 1);
                        getclonenbr = int.Parse(getclonebefore) - 1;
                        getclonebefore = ThreeDModelButton.lastcallGO.Substring(0, ThreeDModelButton.lastcallGO.IndexOf(SceneAssetCtrl.ANGLECLOSEIDENTIFIER) + 1) + getclonenbr;
                        myAnimatorClip = AnimatorOfGameObject(GameObject.Find(getclonebefore)).GetCurrentAnimatorClipInfo(0);

                       


                    }
                    else if (ThreeDModelButton.lastcallGO.Contains($"{SceneAssetCtrl.ANGLECLOSEIDENTIFIER}1"))
                    { //get value of getclonebefore like pikachu... 
                        getclonebefore = ThreeDModelButton.lastcallGO.Substring(0, ThreeDModelButton.lastcallGO.IndexOf(SceneAssetCtrl.ANGLEOPENIDENTIFIER));
                        myAnimatorClip = AnimatorOfGameObject(GameObject.Find(getclonebefore)).GetCurrentAnimatorClipInfo(0);
                    }

                    animchoice = myAnimatorClip[0].clip.name;
                    AnimatorOfGameObject(GameObject.Find(ThreeDModelButton.lastcallGO)).Play(animchoice);
                    ThreeDModelButton.linkedswithAnim = false;




                }
            }
        }

    }

}

