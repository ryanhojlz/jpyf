using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE_Manager : MonoBehaviour
{
    // I made this script bcos i didnt want to touch
    // much of wilson code

    // QTE objects
    public GameObject qte_obj = null;
    // QTE reward after completing QTE
    public int qte_rewardTier = 0;
    // Boolean to start QTE
    public bool QTEStart;

    public GameObject playerObj = null;
    public bool spAtk = false;

    // Use this for initialization
    void Start()
    {
        // Find child in transform
        qte_obj = transform.Find("QTE").gameObject;
        if (!qte_obj)
            Debug.Log("Blo ur stuff missing");
        else
            qte_obj.SetActive(false);

        // Spirit Unit
        playerObj = GameObject.Find("Player_object").GetComponent<ControllerPlayer>().CurrentUnit;
    }

    // Update is called once per frame
    void Update()
    {
        // Is currently possesing a unit 
        if (playerObj.GetComponent<NewPossesionScript>().nowPossesing)
        {
            if (QTEStart)
            {
                qte_obj.SetActive(true);
            }
        }
        else
        {
            // if not possesing unit or unit has somewhat died
            QTEStart = false;
            // Reset QTE
            qte_obj.GetComponent<Special>().ResetOutside();
            qte_obj.SetActive(false);
        }

        // For special attack once qte is done
        if (spAtk)
        {
            // Excecute special attack
            // If object is not a spirit and an actual minion unit
            if (!GameObject.Find("Player_object").
                GetComponent<ControllerPlayer>().CurrentUnit.GetComponent<NewPossesionScript>())
            {
                GameObject.Find("Player_object").
                    GetComponent<ControllerPlayer>().CurrentUnit.GetComponent<Attack_Unit>().SpecialAttack();
            }
            //
            spAtk = false;
        }


        // Debug
        if (Input.GetKeyDown(KeyCode.U))
        {
            //if (qte_obj.activeSelf)
            //    qte_obj.GetComponent<Special>().QTE_Press();
            StartQTE();
        }
    }

    // Start QTE Function
    public void StartQTE()
    {
        if (qte_obj.activeSelf)
            qte_obj.GetComponent<Special>().QTE_Press();
        
        if (!QTEStart)
            QTEStart = true;
    }


}
