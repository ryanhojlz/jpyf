using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatisticDisplay : MonoBehaviour
{
    public Text numberWin;
    public Text numberLose;
    public Text totalPlayTime;
    public Text dateTime;
    public Text enemiesKilled;
    public Text itemsCollected;
    public int Hour;
    public int Min;

    private bool loadScene = false;

    public Canvas Loadingcanvas;

    private string scene;
    //private Text loadingText;
    public Image loadImage;

    // Use this for initialization
    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        numberWin.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_win;
        numberLose.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_lose;
        dateTime.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.dateTime;
        totalPlayTime.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.Hour + " hr : " + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.Min + " min";
        enemiesKilled.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_of_enemies_killed;
        itemsCollected.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_of_times_items_gathered;

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
            //Mainmenucanvas.enabled = false;
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
