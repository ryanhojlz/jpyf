using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject PrefabType;//Put th alert image
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateAlert(GameObject OBJ)
    {
        GameObject NewPrompt = Instantiate(PrefabType);
        NewPrompt.GetComponent<Alert>().SetPromptTarget(OBJ);
    }
}
