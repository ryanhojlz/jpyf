using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Possesor : MonoBehaviour
{
    public GameObject playerReference;
    public List<GameObject> nearbyObjects;
    public bool canPosses = false;
    public bool startPossesing = false;
    public bool firstspawn = false;

    public float possesTime = 8.0f;
    public float possesionProgress = 2.0f;
    public float possesionProgressCap = 10;

    private Material oldmat;
    public GameObject textprefab;

   
	// Use this for initialization
	void Start ()
    {
        playerReference = GameObject.Find("Player_object");
        oldmat = this.gameObject.GetComponent<Renderer>().material;
        oldmat.color = this.gameObject.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update ()
    {

        

        if (startPossesing)
        {
            // Timer ticks down
            possesTime -= 1 * Time.deltaTime;
            possesionProgress -= 1 * Time.deltaTime;
            // Fail Condition
            if (possesTime <= 0)
            {
                possesTime = 8;
                possesionProgress = 2.0f;
                var kleur = GetComponent<Renderer>().material;
                kleur.color = Color.red;
                GetComponent<Renderer>().material = kleur;
                startPossesing = false;

            }


            // Sucess Condition
            if (possesionProgress >= possesionProgressCap)
            {
                possesTime = 8;
                possesionProgress = 2.0f;
                var kleur = GetComponent<Renderer>().material;
                kleur.color = Color.blue;
                GetComponent<Renderer>().material = kleur;
                startPossesing = false;
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject = nearbyObjects[0].gameObject;
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().isPossessed = true;
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().SetStateMachine(new PossessState());
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<NavMeshAgent>().enabled = false;
                playerReference.GetComponent<ControllerPlayer>().Spirit = this.gameObject;

                this.gameObject.SetActive(false);
                //GameObject.FindObjectOfType<ControllerPlayer>().PlayerControllerObject = nearbyObjects[0].gameObject;

            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                possesionProgress += 1;
            }
        }
        else if (!startPossesing)
        {
            if (nearbyObjects.Count > 0)
                canPosses = true;
            else
                canPosses = false;

            if (canPosses)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    startPossesing = true;
                    var kleur = GetComponent<Renderer>().material;
                    kleur.color = Color.green;
                    GetComponent<Renderer>().material = kleur;
                    Text_Instantiate();
                }
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Control Unit")
        //    nearbyObjects.Add(other.gameObject);

        if (other.gameObject.tag == "Ally_Unit")
        {
            //Building check4building = other.gameObject.GetComponent<Building>();
            if (other.gameObject.GetComponent<Building>())
                return;
            for (int i = 0; i < nearbyObjects.Count; ++i)
            {
                // Return if object is in list
                if (nearbyObjects[i].gameObject == other.gameObject)
                    return;
            }
            nearbyObjects.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.name == "Control Unit")
        //    nearbyObjects.Remove(other.gameObject);

        if (other.gameObject.tag == "Ally_Unit")
            nearbyObjects.Remove(other.gameObject);
    }

    public void Text_Instantiate()
    {
        GameObject newtext = Instantiate(textprefab) as GameObject;
        newtext.GetComponent<TextEffectControl>().possesor = this.gameObject;
        // change ltr to closest object
        newtext.GetComponent<TextEffectControl>().yokai = nearbyObjects[0];
        newtext.GetComponent<Transform>().parent = newtext.GetComponent<TextEffectControl>().yokai.transform;
        newtext.GetComponent<Transform>().localPosition = Vector3.zero;
        var localpos = newtext.GetComponent<Transform>().localPosition;
        localpos.y += 1.5f;
        newtext.GetComponent<Transform>().localPosition = localpos;
        
        //newtext.transform.parent = nearbyObjects[0].transform;
        //newtext.transform.localPosition = Vector3.zero;
        //var localpos = newtext.transform.localPosition;
        //localpos.y += 3;
        //transform.localPosition = localpos;
    }


    
    
}
