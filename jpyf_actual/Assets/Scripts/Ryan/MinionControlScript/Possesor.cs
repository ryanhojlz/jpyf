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

    public Shader ShaderInstance;
    private Material oldmat;
    public GameObject textprefab;
    public int targetIndex = 0;
   
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
        // Remove Null or inactive objects
        // Moved Below
        //for (int i = 0; i < nearbyObjects.Count; i++)
        //{
        //    if (nearbyObjects[i] == null || nearbyObjects[i].activeSelf == false)
        //    {
        //        nearbyObjects.Remove(nearbyObjects[i]);
        //    }
        //}

        // Selection function e.g moving from left 2 right unit
        Selection();

        // When possesing unit
        if (playerReference.GetComponent<ControllerPlayer>().Spirit)
        {
            // Idk why i wrote this is the same as the one above but sure
            //if (GetComponent<MeshRenderer>().enabled == false)
            //{
            //    this.transform.position = playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.transform.position;
            //}

            // Follows the possesed unit 
            this.transform.position = playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.transform.position;
            // Change the shader for the currently possesed unit
            this.playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(255, 255, 0, 1));
        }

        if (startPossesing)
        {
            // Timer ticks down
            possesTime -= 1 * Time.deltaTime;
            possesionProgress -= 1 * Time.deltaTime;

            FailCondition();
            SucessCondition();

            // Debug
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
            // Debug 
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


        // Remove Null or inactive objects
        for (int i = 0; i < nearbyObjects.Count; i++)
        {
            if (nearbyObjects[i] == null || nearbyObjects[i].activeSelf == false)
            {
                nearbyObjects.Remove(nearbyObjects[i]);
            }
        }

    }

    // Buggy method
    public void Text_Instantiate()
    {
        GameObject newtext = Instantiate(textprefab) as GameObject;
        newtext.GetComponent<TextEffectControl>().possesor = this.gameObject;
        
        // change ltr to closest object
        if (playerReference.GetComponent<ControllerPlayer>().Spirit)
        {
            newtext.GetComponent<TextEffectControl>().yokai = nearbyObjects[targetIndex];
        }
        else
        {
            newtext.GetComponent<TextEffectControl>().yokai = nearbyObjects[targetIndex];
        }
    }

    // Override method
    public void Text_Instantiate(GameObject follow, GameObject owner)
    {

    }

    void SucessCondition()
    {
        // Sucess Condition
        if (possesionProgress >= possesionProgressCap)
        {
            possesTime = 8;
            possesionProgress = 2.0f;
            var kleur = GetComponent<Renderer>().material;
            kleur.color = Color.blue;
            GetComponent<Renderer>().material = kleur;

            startPossesing = false;

            // If player Controlling Spirit
            if (playerReference.GetComponent<ControllerPlayer>().Spirit)
            {
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().isPossessed = false;
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<NavMeshAgent>().enabled = true;
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().SetStateMachine(
                  new AttackState(playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<Attack_Unit>(),
                  playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().minionWithinRange
                  , playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().Enemy_Tag));
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject = nearbyObjects[targetIndex].gameObject;
                nearbyObjects.Remove(nearbyObjects[targetIndex]);
            }
            else
            {
              
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject = nearbyObjects[targetIndex].gameObject;
                // Remove possesed objects
                nearbyObjects.Remove(nearbyObjects[targetIndex]);
            }


            playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().isPossessed = true;
            // Prevent other zijun state from running
            playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<BasicGameOBJ>().SetStateMachine(new PossessState());
            // Turn of the nav mesh shit
            playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<NavMeshAgent>().enabled = false;
            // Keep a copy reference of object
            playerReference.GetComponent<ControllerPlayer>().Spirit = this.gameObject;
            
            GetComponent<Rigidbody>().isKinematic = true;
            //GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;

        }
    }

    void FailCondition()
    {
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
    }

    void Selection()
    {
        //if (gameObject.GetComponent<MeshRenderer>().enabled)
        //    return;
        if (nearbyObjects.Count <= 0)
        {
            targetIndex = 0;
            return;
        }

        // Outlines 
        for (int select = 0; select < nearbyObjects.Count; select++)
        {
            // Do not apply to controlled Object
            if (nearbyObjects[select] == playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject)
                continue;

            // Selected Object
            if (targetIndex == select)
            {
                nearbyObjects[targetIndex].GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.35f);
                nearbyObjects[targetIndex].GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(255, 0, 0, 255));
            }
            else
            {
                // Non Select Objects but can be selected
                nearbyObjects[select].GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(0, 255, 0, 255));
            }
        }


        // Debug Keys
        if (Input.GetKeyDown(KeyCode.N))
        {
            --targetIndex;
            if (targetIndex < 0)
                targetIndex = 0;
            else if (targetIndex > nearbyObjects.Count)
            {
                targetIndex = nearbyObjects.Count - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ++targetIndex;
            if (targetIndex < 0)
                targetIndex = 0;
            else if (targetIndex > nearbyObjects.Count)
            {
                targetIndex = nearbyObjects.Count - 1;
            }
        }
    }

    public void UpDownIndex(bool bol)
    {

        if (bol)
        {
            ++targetIndex;
            if (targetIndex < 0)
                targetIndex = 0;
            else if (targetIndex > nearbyObjects.Count)
            {
                targetIndex = nearbyObjects.Count - 1;
            }
        }
        else if (!bol)
        {
            --targetIndex;
            if (targetIndex < 0)
                targetIndex = 0;
            else if (targetIndex > nearbyObjects.Count)
            {
                targetIndex = nearbyObjects.Count - 1;
            }
        }

        
    }

    public void StartToPosses()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Control Unit")
        //    nearbyObjects.Add(other.gameObject);

        //Building check4building = other.gameObject.GetComponent<Building>();
        if (other.gameObject.GetComponent<Building>())
            return;
        if (other.gameObject.tag == "Ally_Unit")
        {
            for (int i = 0; i < nearbyObjects.Count; ++i)
            {
                // Return if object is in list
                if (nearbyObjects[i].gameObject == other.gameObject)
                    return;
            }
            other.GetComponent<Renderer>().material.shader = ShaderInstance;
            other.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);
            //other.GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(255, 0, 0, 255));
            nearbyObjects.Add(other.gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Ally_Unit")
            return;
        if (other.gameObject.GetComponent<Building>())
            return;

        foreach (GameObject go in nearbyObjects)
        {
            if (go == other)
                continue;
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.name == "Control Unit")
        //    nearbyObjects.Remove(other.gameObject);
        if (other.GetComponent<Renderer>().material.shader == ShaderInstance)
        {
            other.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.0f);
            other.GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(0, 0, 0, 0));
        }
        if (startPossesing)
        {
            if (other.gameObject == nearbyObjects[0])
            {
                possesTime = 8;
                startPossesing = false;
                possesionProgress = 2;
                nearbyObjects.Remove(nearbyObjects[0]);
            }
        }
        else
        {
            if (other.gameObject.tag == "Ally_Unit")
                nearbyObjects.Remove(other.gameObject);
        }

    }

}
