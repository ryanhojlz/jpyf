using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{

    private bool loadScene = false;

    private string scene;
    public Canvas Loadingcanvas;
    public Text loadingText;
    public Image loadImage;

    Vector2 loadScreenMove;

    bool loadScreenMoveUpMoveDown;

    private void Start()
    {
        loadImage.enabled = false;
        Loadingcanvas.enabled = false;
    }

    // Updates once per frame
    void Update()
    {

        // If the player has pressed the space bar and a new scene is not loading yet...
        if (scene != null && loadScene == false) 
        {

            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;
            loadImage.enabled = true;
            // ...change the instruction text to read "Loading..."
            loadingText.text = "Loading...";

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

        }

        // If the new scene has started loading...
        if (loadScene == true)
        {
            Loadingcanvas.enabled = true;
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            loadScreenMove = loadImage.rectTransform.anchoredPosition;

            if (!CheckDown())
            {
                //Debug.Log("mOVE DOWN");
                loadScreenMove.y += 30 * Time.deltaTime;
            }

            //if (CheckUp())
            //{
            //    loadScreenMoveUp = !loadScreenMoveUp;
            //}
            //}
            loadImage.rectTransform.anchoredPosition = loadScreenMove;
        }

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    GoBackToMainMenu();
        //}

    }

    bool CheckUp()
    {

        //float canvasTop = Mainmenucanvas.GetComponent<RectTransform>().anchoredPosition.y + (Mainmenucanvas.GetComponent<RectTransform>().rect.height * 0.5f) * Mainmenucanvas.transform.localScale.y;
        //float loadImageTop = loadImage.rectTransform.position.y + (loadImage.rectTransform.rect.height * 0.5f) * loadImage.transform.localScale.y;
        ////Debug.Log(canvasTop + " : " + loadImageTop);
        //if (canvasTop <= loadImageTop)
        //    return true;

        //return false;

        //Debug.Log("Height : " + Mainmenucanvas.GetComponent<RectTransform>().rect.height);
        //Debug.Log("Canvas height : " + Mainmenucanvas.GetComponent<RectTransform>().anchoredPosition.y);
        //Debug.Log("Image height : " + loadImage.rectTransform.position.y);

        //Debug.Log("Length : " + loadImage.rectTransform.rect.width * 0.5f);
        //Debug.Log("canvasTop : " + canvasTop);
        //Debug.Log("loadImageTop : " + loadImageTop);

        //return (Loadingcanvas.GetComponent<RectTransform>().anchoredPosition.y
        //        + (Loadingcanvas.GetComponent<RectTransform>().rect.width * 0.5)
        //        * Loadingcanvas.transform.lossyScale.y
        //        >
        //loadImage.rectTransform.anchoredPosition.y
        //+ (loadImage.rectTransform.rect.width * 0.5)
        //* loadImage.transform.lossyScale.y) && !loadScreenMoveUp;

        //float canvasUp = Mainmenucanvas.transform.position.y + (Mainmenucanvas.GetComponent<RectTransform>().rect.height * 0.5f * Mainmenucanvas.transform.localScale.y);
        float loadImageUp = loadImage.transform.position.y + (loadImage.GetComponent<RectTransform>().rect.height * 0.5f * loadImage.transform.localScale.y);

        //Debug.Log(canvasUp + " : " + loadImageUp);

        //if (canvasUp <= loadImageUp)
            //return true;

        return false;
    }

    bool CheckDown()
    {
        //float canvasBottom = Mainmenucanvas.GetComponent<RectTransform>().anchoredPosition.y - (Mainmenucanvas.GetComponent<RectTransform>().rect.height * 0.5f) * Mainmenucanvas.transform.localScale.y;
        //float loadImageBottom = loadImage.rectTransform.position.y - (loadImage.rectTransform.rect.height * 0.5f) * loadImage.transform.localScale.y;

        //Debug.Log((Mainmenucanvas.GetComponent<RectTransform>().rect.height * 0.5f) * Mainmenucanvas.transform.localScale.y + " : " + loadImageBottom);

        //if (canvasBottom >= loadImageBottom)
        //    return true;

        //return false;
        //return (Loadingcanvas.GetComponent<RectTransform>().anchoredPosition.y
        //        - (Loadingcanvas.GetComponent<RectTransform>().rect.width * 0.5)
        //        * Loadingcanvas.transform.lossyScale.y
        //        <
        //        loadImage.rectTransform.anchoredPosition.y
        //        - (loadImage.rectTransform.rect.width * 0.5)
        //        * loadImage.transform.lossyScale.y) && loadScreenMoveUp;

        //return false;

        //float canvasBottom = Mainmenucanvas.transform.position.y - (Mainmenucanvas.GetComponent<RectTransform>().rect.height * 0.5f);// * 0.5f * Mainmenucanvas.transform.localScale.y);
        float loadImageBottom = loadImage.transform.position.y - ((loadImage.GetComponent<RectTransform>().rect.height * 0.5f));

        //Debug.Log(canvasBottom + " : " + loadImageBottom);

        //if (canvasBottom <= loadImageBottom + Mainmenucanvas.GetComponent<RectTransform>().rect.height * 0.1f)
            //return true;

        return false;
    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        //yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }

    public void GoBackToMainMenu()
    {
        scene = "MainMenu";
    }

}