using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    public Canvas Wincanvas;
    public Canvas LoseCanvas;
    public Canvas Loadingcanvas;
    private bool loadScene = false;

    private string scene;
    //private Text loadingText;
    public Image loadImage;

    public static Endgame Instance = null;

    private AudioClip GameSuccessSound;
    private AudioClip GameFailureSound;
    private AudioSource playSound;

    // Use this for initialization
    void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this.gameObject);

        loadImage.enabled = false;
        Loadingcanvas.enabled = false;
        GameSuccessSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().SuccessSound;
        GameFailureSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().FailureSound;
        playSound = GameObject.Find("MainMenu").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Wincanvas.enabled = false;
        LoseCanvas.enabled = false;

        //if ()
        //    GameSuccess();
        //if ()
        //    GameFailure();

        if (scene != null && loadScene == false)
        {
            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            //loadingText.text = "Loading...";
            loadImage.enabled = true;
            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());
        }

        // If the new scene has started loading...
        if (loadScene == true)
        {
            Loadingcanvas.enabled = true;
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            //loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            loadImage.color = new Color(loadImage.color.r, loadImage.color.g, loadImage.color.b, Mathf.PingPong(Time.time, 1));
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            scene = "MainMenu";
        }
    }

    void GameSuccess()
    {
        Wincanvas.enabled = true;
        playSound.clip = GameSuccessSound;
        playSound.Play();
    }

    void GameFailure()
    {
        LoseCanvas.enabled = true;
        playSound.clip = GameFailureSound;
        playSound.Play();
    }

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        //float loadTime = 0f;
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        //yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            //loadTime += Time.deltaTime;
            //Debug.Log("Time Taken to load : " + loadTime);
            yield return null;
            //Debug.Log("Time Taken to load : 2 . " + loadTime);
        }
    }
}
