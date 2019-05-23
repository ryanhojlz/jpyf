using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgamestats : MonoBehaviour
{
    public static Endgamestats Instance = null;

    Vector3 startPos;
    Vector3 endPos;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    public struct EndgamestatsData
    {
        public float distTravelled;
        public float light_up_time;
        public int light_collected;
        public int wood_collected;
        public int player2_healing;
        public int cart_healing;
    }

    public EndgamestatsData Data = new EndgamestatsData();

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetStartPos(Vector3 _startPos)
    {
        startPos = _startPos;
    }

    public void SetEndPos(Vector3 _endPos)
    {
        endPos = _endPos;
    }

    public void calDistTravelled()
    {
        Data.distTravelled = (startPos - endPos).magnitude;
    }

    public void IncrementLightUpTime()
    {
        Data.light_up_time += Time.deltaTime;
    }

    public void incrementLightcollected()
    {
        Data.light_collected++;
    }

    public void incrementWoodcollected()
    {
        Data.wood_collected++;
    }

    public void incrementPlayer2healing(int healing)
    {
        Data.player2_healing += healing;
    }

    public void CartHeal(int heal)
    {
        Data.cart_healing += heal;
    }


    // Returners
    public int ReturnCartHealingDone()
    {
        return Data.cart_healing;
    }

    public int ReturnPlayerHealingDone()
    {
        return Data.player2_healing;
    }

    public int ReturnWoodCollected()
    {
        return Data.wood_collected;
    }

    public int ReturnLightCollected()
    {
        return Data.light_collected;
    }

    public float ReturnLanterUpTime()
    {
        return Data.light_up_time;
    }

    public float ReturnDistanceTravel()
    {
        return Data.distTravelled;
    }

   

}
