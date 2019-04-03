using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class NewPossesionScript : MonoBehaviour
{
    public GameObject player = null;
    public List<GameObject> objList = null;

    // If can posses objects
    public bool canPosses = false;

    // When is trying to attempt possesion
    public bool isPossesing = false;

    // When actively possesing unit
    public bool nowPossesing = false;

    public float timeToPosses = 0;
    public float timeToPossesReference = 8;
    public float possesProgression = 2;
    public float possesProgressionCap = 6;
    public int ObjectCount;
    public Shader ShaderInstance = null;
    public GameObject effectPossesion = null;

    public int targetIndex = 0;
    public GameObject unit2Posses;

    public Image UIEffect = null;

    // Use this for initialization
    private void Awake()
    {
        player = GameObject.Find("Player_object");
        timeToPosses = timeToPossesReference;
    }

    void Start()
    {

    }

    private void LateUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Update Objects in the list
        UpdateListObj();
        // Update player possesion
        UpdatePossesion();
        // Update Posses
        UpdatePosses();
        // Interactions
        PossesInteraction();
        // Check if in range
        //CheckForObjectRange();




        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log("Pressed");
            PossesUp();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeTargetIndex(false);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeTargetIndex(true);
        }

        // Update ObjectCount
       // ObjectCount = objList.Count;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Assign to list
    //    AssignToList(other.gameObject);
    //}

    // ryan im writing this to you
    // cause you too tired to write
    // but change this ontrigger exit
    // to a distance check instead
    // on trigger enter to put in list
    // then distance check to decide whetehr to remove from the list
    // dank
    // meme
    private void OnTriggerExit(Collider other)
    {
        // Exit to List
        //ExitList(other.gameObject);
        ExitList_V2(other.gameObject);
        //Debug.Log(other.gameObject.name + " Left ");
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    AssignToList(other.gameObject);
    //}

    private void OnTriggerStay(Collider other)
    {
        AssignToList(other.gameObject);
        if (other.tag == "Ally_Unit")
        {
            Physics.IgnoreCollision(other.GetComponent<SphereCollider>(),this.GetComponent<SphereCollider>());
        }
        // Debug.Log("Enter");
    }

    void CheckForObjectRange()
    {
        // if there is no object list inside
        if(objList.Count < 1)
        {
            return;
        }

        for(int i = 0; i < objList.Count; ++i)
        {
            // i love javascript
            var currentObject = objList[i].gameObject;

            var distance = Vector3.Distance(currentObject.transform.position, this.transform.position);

            // if the distance is more than 6
            // it'll be out of range
            if (distance > 6.0f)
            {
                ExitList_V2(currentObject);
                return;
            }
        }
    }

    // List Update Function of Selected Object
    void UpdateListObj()
    {
        Debug.Log("Target index  " + targetIndex);
        // Return if there is nothing in the list
        if (objList.Count < 1)
        {
            // No unit to posses
            unit2Posses = null;
            targetIndex = 0;
            return;
        }
        else
        {
            for (int i = 0; i < objList.Count; ++i)
            {
                
                if (objList[i] != player.GetComponent<ControllerPlayer>().CurrentUnit)
                {
                    ChangeShader(objList[i], 0.08f, new Vector4(0, 255, 0, 255));
                }
                if (i == targetIndex)
                {
                    unit2Posses = objList[i].gameObject;
                    ChangeShader(unit2Posses, 0.1f, new Vector4(0, 128, 255, 255));
                }
                // If null || not active remove
                if (objList[i] == null)
                {
                    //objList.RemoveAt(i);
                    objList.Remove(objList[i]);
                }
            }
        }
        
    }
    

    // Changes the bool if can posses
    void UpdatePosses()
    {
        if (objList.Count <= 0)
            return;
        if (unit2Posses)
            canPosses = true;
        else if (!unit2Posses)
            canPosses = false;
    }

    // Actual Possesion Interaction
    void PossesInteraction()
    {
        if (unit2Posses == null)
        {
            ReInitPossesInteraction();
            isPossesing = false;
            return;
        }
        if (isPossesing)
        {
            timeToPosses -= 1 * Time.deltaTime;
            possesProgression -= 1 * Time.deltaTime;
            if (possesProgression >= possesProgressionCap)
            {
                // Update unit
                SuccessPosession();
                ReInitPossesInteraction();
                isPossesing = false;
            }
            if (timeToPosses < 0)
            {
                // Fail
                ReInitPossesInteraction();
                isPossesing = false;
            }
            if (possesProgression <= 0)
            {
                // Fail
                ReInitPossesInteraction();
                isPossesing = false;
            }
        }
        //else if (!isPossesing)
        //{
        //    //ReInitPossesInteraction();
        //}
    }

    // Interaction to start possesion
    public void PossesUpdate()
    {
        //// Can only run when not possesing 
        //if (isPossesing)
        //    return;
        //Debug.Log("Ran here2");

        if (canPosses)
        {
            isPossesing = true;
            // init text
            //GameObject.Find("PossesUI").GetComponent<PossesionEffectScript>().SetRender(true);
            ChangeShader(unit2Posses, 0.1f, new Vector4(255, 255, 0, 255));
        }
    }



    // Interaction when trying to posses
    public void PossesUp()
    {
        if (isPossesing)
        {
            //Debug.Log("Ran here");
            possesProgression += 2;
        }
        if (!isPossesing)
        {
            PossesUpdate();
            GameObject.Find("PossesUI").SetActive(true);
        }
    }

    // ReInit
    void ReInitPossesInteraction()
    {
        
        timeToPosses = timeToPossesReference;
        possesProgression = 2.0f;
        //canPosses = false;
    }

    // For selecting Target
    public void ChangeTargetIndex(bool updown)
    {
       
        // If true +1 index
        if (updown)
        {
            ++targetIndex;
        }
        else if (!updown) // If false -1 index
        {
            --targetIndex;
        }

        if (targetIndex < 0)
        {
            targetIndex = 0;
        }
        if (targetIndex >= objList.Count)
        {
            // Because Array start from 0
            targetIndex = objList.Count - 1;
        }
        if (objList.Count <= 0)
        {
            targetIndex = 0;
        }

        Debug.Log("Index as of now " + targetIndex);
    }

    // Change Shader value func
    void ChangeShader(GameObject targetObj,float width,Vector4 color)
    {
        if (targetObj.GetComponent<Renderer>().material.shader != ShaderInstance)
            targetObj.GetComponent<Renderer>().material.shader = ShaderInstance;

        var Renderer = targetObj.GetComponent<Renderer>();
        Renderer.material.SetFloat("_OutlineWidth", width);
        Renderer.material.SetVector("_OutlineColor", color);
    }

    // Assign to List function
    void AssignToList(GameObject go)
    {
        // Not taking in buildings and enemy units
        if (go.GetComponent<Building>())
            return;
        if (go.tag != "Ally_Unit")
            return;
        
        // Check if its in the list alr
        foreach (GameObject _go in objList)
        {
            if (go == _go)
                return;
        }
        // If possesing do not put current unit inside list
        if (nowPossesing)
        {
            if (go == player.GetComponent<ControllerPlayer>().CurrentUnit)
                return;
        }
        // Gives outline shader to object
        go.GetComponent<Renderer>().material.shader = ShaderInstance;
        ChangeShader(go, 0, new Vector4(0, 0, 0, 0));
        objList.Add(go);
    }

    // Exiting radius func
    void ExitList(GameObject go)
    {
        // Ignore building n non allies
        if (go.tag != "Ally_Unit")
            return;
        if (go.GetComponent<Building>())
            return;
        // fix this shit

        // If possesion is in play stop possesion
        if (isPossesing)
        {
            // If its the unit that player is trying to access
            if (go == unit2Posses)
            {
                ReInitPossesInteraction();
                objList.Remove(unit2Posses);
                unit2Posses = null;
                ChangeShader(go, 0, new Vector4(0, 0, 0, 0));
                isPossesing = false;
            }
            else
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    if (go == unit2Posses)
                        continue;
                    if (go == objList[i])
                    {
                        Debug.Log("Will it crash here? find out in the next episode of dragonball Z");
                        // Deactive outline
                        ChangeShader(objList[i], 0, new Vector4(0, 0, 0, 0));
                        objList.Remove(objList[i]);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < objList.Count; i++)
            {
                if (go == objList[i])
                {
                    Debug.Log("Will it crash here? find out in the next episode of dragonball Z");
                    // Deactive outline
                    ChangeShader(objList[i], 0, new Vector4(0, 0, 0, 0));
                    objList.Remove(objList[i]);
                }
            }
        }



        //for (int obj = 0; obj < objList.Count; obj++)
        //{
        //    if (objList[obj] == unit2Posses)
        //    {
        //        if (isPossesing)
        //        {
        //            ReInitPossesInteraction();
        //            isPossesing = false;
        //        }
        //        objList.Remove(unit2Posses);

        //    }
        //}
    }

    void ExitList_V2(GameObject go)
    {
        // Ignore building n non allies
        if (go.tag != "Ally_Unit")
            return;
        if (go.GetComponent<Building>())
            return;
        if (objList.Count <= 0)
            return;
        for (int obj = 0; obj < objList.Count; obj++)
        {
            if (objList[obj] == unit2Posses)
            {
                if (isPossesing)
                {
                    isPossesing = false;
                    ReInitPossesInteraction();
                }
                ChangeShader(objList[obj], 0, new Vector4(0, 0, 0, 0));
                objList.Remove(objList[obj]);
            }
            else if (objList[obj] != unit2Posses)
            {
                ChangeShader(objList[obj], 0, new Vector4(0, 0, 0, 0));
                objList.Remove(objList[obj]);
            }
        }
    }
    
    // Succes Possesion
    void SuccessPosession()
    {
        // If alr possesing unit
        if (nowPossesing)
        {
            // Replaced currently possesing unit
            var existingObj = player.GetComponent<ControllerPlayer>().CurrentUnit;

            // Re enable the guy to walk again
            existingObj.GetComponent<NavMeshAgent>().enabled = true;
            existingObj.GetComponent<BasicGameOBJ>().isPossessed = false;
            
            // ZiJun State machine call
            existingObj.GetComponent<BasicGameOBJ>().SetStateMachine(
                new AttackState(existingObj.GetComponent<Attack_Unit>(), 
                existingObj.GetComponent<BasicGameOBJ>().minionWithinRange,
                existingObj.GetComponent<BasicGameOBJ>().Enemy_Tag));

            // Add back to existing list
            objList.Add(existingObj);
        }

        // Change Unit
        player.GetComponent<ControllerPlayer>().CurrentUnit = unit2Posses;
        // Hide this Object
        GetComponent<MeshRenderer>().enabled = false;
        nowPossesing = true;
        // Send to another state
        unit2Posses.GetComponent<NavMeshAgent>().enabled = false;
        unit2Posses.GetComponent<BasicGameOBJ>().isPossessed = true;
        unit2Posses.GetComponent<BasicGameOBJ>().SetStateMachine(new PossessState());

        
        
        // Disable box collider so it wont jank around
        GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<ControllerPlayer>().SpiritUnit = this.gameObject;
        // Remove object from list
        objList.Remove(unit2Posses);
        // Change so current unit is red
        ChangeShader(player.GetComponent<ControllerPlayer>().CurrentUnit, 0.35f, new Vector4(255, 0, 0, 255));
    }

    void UpdatePossesion()
    {
        if (!nowPossesing)
            return;
        // If unit dies
        if (player.GetComponent<ControllerPlayer>().CurrentUnit == null)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            player.GetComponent<ControllerPlayer>().CurrentUnit = player.GetComponent<ControllerPlayer>().SpiritUnit;
            player.GetComponent<ControllerPlayer>().SpiritUnit = null;

            var offset = transform.position;
            offset.y += 0.6f;
            transform.position = offset;


            nowPossesing = false;
        }
        // Update position
        this.transform.position = player.GetComponent<ControllerPlayer>().CurrentUnit.transform.position;
        
    }
    
}
