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
        for (int i = 0; i < nearbyObjects.Count; i++)
        {
            if (nearbyObjects[i] == null || nearbyObjects[i].activeSelf == false)
            {
                nearbyObjects.Remove(nearbyObjects[i]);
            }
        }

        // When possesing unit
        if (!playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.GetComponent<Possesor>())
        {
            if (GetComponent<MeshRenderer>().enabled == false)
            {
                this.transform.position = playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject.transform.position;
            }
            Selection();
        }


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
            SucessCondition();

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

            other.GetComponent<Renderer>().material.shader = ShaderInstance;
            other.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 0.35f);
            other.GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(255,0,0,255));
            nearbyObjects.Add(other.gameObject);
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
            newtext.GetComponent<TextEffectControl>().yokai = nearbyObjects[0];
        }
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

            if (playerReference.GetComponent<ControllerPlayer>().Spirit)
            {
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject = nearbyObjects[targetIndex].gameObject;
                nearbyObjects.Remove(nearbyObjects[targetIndex]);
            }
            else
            {
                playerReference.GetComponent<ControllerPlayer>().PlayerControllerObject = nearbyObjects[0].gameObject;
                // Remove possesed objects
                nearbyObjects.Remove(nearbyObjects[0]);
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


            //this.gameObject.SetActive(false);
            //GameObject.FindObjectOfType<ControllerPlayer>().PlayerControllerObject = nearbyObjects[0].gameObject;

        }
    }


    public void ChangeShaderProperty(float lineSize, Vector4 vector)
    {
        
    }


    void Selection()
    {
        if (gameObject.GetComponent<MeshRenderer>().enabled)
            return;
        if (nearbyObjects.Count <= 0)
        {
            targetIndex = 0;
            return;
        }

        for (int select = 0; select < nearbyObjects.Count; select++)
        {
            if (targetIndex == select)
            {
                nearbyObjects[targetIndex].GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(0, 128, 255, 0));
            }
            else
            {
                nearbyObjects[select].GetComponent<Renderer>().material.SetVector("_OutlineColor", new Vector4(0, 255, 0, 0));
            }
        }


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
        //if (nearbyObjects.Count > 0)
        //{
        //    if (bol)
        //        ++targetIndex;
        //    else if (!bol)
        //        --targetIndex;

        //    if (targetIndex < 0)
        //        targetIndex = 0;
        //    else if (targetIndex > nearbyObjects.Count)
        //    {
        //        targetIndex = nearbyObjects.Count - 1;
        //    }
        //}
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
}
