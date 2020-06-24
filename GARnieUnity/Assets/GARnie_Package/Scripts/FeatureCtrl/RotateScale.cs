using UnityEngine;

/// <summary>
/// To indicate to GameObject if they can rotate or scale
/// Manage menu with a slide to show it
/// </summary>
public class RotateScale : MonoBehaviour
{

    /// <summary>
    /// To get menu with just slide
    /// </summary>
    public static string positiononscreen = "not init";
    /// <summary>
    /// To get name of avatar
    /// </summary>
    public static string getAvatarName;

    /// <summary>
    /// The object that moves with user input (SEE AGAIN LATER BECAUSE MAYBE USELESS NOW)
    /// </summary>
    public static GameObject interactableObject;
    public static bool openmenu = false;


    /// <summary>
    /// The speed at which swipe controls rotate the object.
    /// </summary>
    float moveSpeed;

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        moveSpeed = 1f;
    }

    /// <summary>
    /// Don't understand
    /// Update this instance.
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR || UNITY_IOS
        if (!this.gameObject.name.Equals(SceneAssetCtrl.instance.menuGO.name) && this.gameObject.activeSelf)
        {

            interactableObject = this.gameObject;
        }

        if ((SceneAssetCtrl.instance.modelSilkkeAsset!=null) && !this.gameObject.name.Equals(SceneAssetCtrl.instance.menuGO.name) && this.gameObject.activeSelf && gameObject.transform.parent.name.Contains(SceneAssetCtrl.instance.modelSilkkeAsset.transform.name))
        { //for silkke
            getAvatarName = this.name;
            interactableObject = this.gameObject;
            if (interactableObject.transform.localScale.y < 0.8)
            { //keep silkke on the ground
                interactableObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else
            {
                //processPinch(); 
                //print (this.gameObject.name);
            }
        }
#elif UNITY_IOS || UNITY_ANDROID
			if (!this.gameObject.name.Equals(SceneAssetCtrl.instance.menuGO.name) && this.gameObject.activeSelf) {
				if (!Menu.menuornot){ //just if you don't touch on menu screen
					
					interactableObject = this.gameObject;
					ProcessDrag ();
					

					
				}
			}

			if ((SceneAssetCtrl.instance.modelSilkkeAsset!=null) && !this.gameObject.name.Equals(SceneAssetCtrl.instance.menuGO.name) && this.gameObject.activeSelf  && gameObject.transform.parent.name.Contains (SceneAssetCtrl.instance.modelSilkkeAsset.transform.name) ) { //for silkke
				getAvatarName=this.name;
				if (!Menu.menuornot){ //just if you don't touch on menu screen
					interactableObject = this.gameObject;
					//processDrag ();
					if(interactableObject.transform.localScale.y<0.8){ //keep silkke on the ground
						interactableObject.transform.localScale=new Vector3(0.8f,0.8f,0.8f);
					}else{
						//processPinch(); 
					}
				}
			}

			//touch slide to open menu
			if (this.gameObject.name.Equals(SceneAssetCtrl.instance.menuGO.name) && !(this.gameObject.transform.localScale.x==1)&& !(this.gameObject.transform.localScale.y==1) && !(this.gameObject.transform.localScale.z==1)) {  


			TouchSlide ();


			}

#endif

    }

    /// <summary>
    /// to rotate an object on y axis 
    /// </summary>
    void ProcessDrag()
    {
        if (Input.touchCount == 3)
        {

            //Store input
            Touch fing = Input.GetTouch(0);

            if (fing.phase == TouchPhase.Moved) //If the finger has moved since the last frame
            {

                Vector2 fingMove = fing.deltaPosition;

                float deltaY = (fingMove.x * moveSpeed * -1);

                interactableObject.transform.Rotate(0, deltaY, 0);



            }
        }
    }




    /// <summary>
    /// to open menu with just a slide to the left where is the menu
    /// </summary>
    void TouchSlide()
    {
        if (Input.touchCount == 1)
        {
            //Store input
            Touch fing = Input.GetTouch(0);

            if (fing.phase == TouchPhase.Began)
            {
                if (fing.position.x > (Screen.width * (95f / 100f)))
                {
                    positiononscreen = "right position: " + fing.position.x;
                    openmenu = true;
                }
                else
                {
                    positiononscreen = "wrong position: " + fing.position.x;
                    openmenu = false;
                }
            }

            if (fing.phase == TouchPhase.Moved) //If the finger has moved since the last frame
            {

                if (openmenu)
                {

                    MenuButton.boolmenubutton = true; //active menu

                }

            }
        }
    }


}

