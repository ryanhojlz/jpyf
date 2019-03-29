using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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

    // Use this for initialization
    private void Awake()
    {
        player = GameObject.Find("Player_object");
        timeToPosses = timeToPossesReference;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ObjectCount = objList.Count;
        // Update Objects in the list
        UpdateListObj();
        UpdatePosses();
        PossesInteraction();
        UpdatePossesion();


        //Debug.Log("Can posses " + canPosses);
        //Debug.Log("Possesing? " + isPossesing);
        //Debug.Log("Can possesion Progression " + possesProgression);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log("Pressed");
            PossesUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Assign to list
        AssignToList(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        // Exit to List
        ExitList(other.gameObject);
    }

    

    // List Update Function of Selected Object
    void UpdateListObj()
    {
        // Return if there is nothing in the list
        if (objList.Count <= 0)
        {
            // No unit to posses
            unit2Posses = null;
            return;
        }
        for (int i = 0; i < objList.Count; i++)
        {
            // If null || not active remove
            if (objList[i].activeSelf == false || objList[i] == null)
            {
                objList.Remove(objList[i]);
            }
            if (i == targetIndex)
            {
                unit2Posses = objList[i].gameObject;
                ChangeShader(unit2Posses, 0.1f, new Vector4(255, 0, 255, 0));
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

    // Interaction to start possesion
    public void PossesUpdate()
    {
        //// Can only run when not possesing 
        //if (isPossesing)
        //    return;
        //Debug.Log("Ran here2");

        if (canPosses)
            isPossesing = true;
    }

    // Actual Possesion Interaction
    void PossesInteraction()
    {
        if (isPossesing)
        {
            //timeToPosses -= 1 * Time.deltaTime;
            possesProgression -= 1 * Time.deltaTime;
            ChangeShader(unit2Posses, 0.1f, new Vector4(255, 255, 0, 255));
            if (possesProgression >= possesProgressionCap)
            {
                // Update unit
                SuccessPosession();
                ReInitPossesInteraction();
                isPossesing = false;
            }
            if (possesProgression < 0 || timeToPosses < 0)
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
        }
    }

    // ReInit
    void ReInitPossesInteraction()
    {
        canPosses = false;
        timeToPosses = timeToPossesReference;
        possesProgression = 2.0f;
    }

    // For selecting Target
    public void TargetIndex(bool updown)
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
        else if (targetIndex > objList.Count)
        {
            // Because Array start from 0
            targetIndex = objList.Count - 1;
        }
    }

    // Change Shader value func
    void ChangeShader(GameObject targetObj,float width,Vector4 color)
    {
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

        // If possesion is in play stop possesion
        if (isPossesing)
        {
            // If its the unit that player is trying to access
            if (go == unit2Posses)
            {
                isPossesing = false;
                ReInitPossesInteraction();
            }
        }
        else
        {
            for (int i = 0; i < objList.Count; i++)
            {
                if (go == objList[i])
                {
                    // Deactive outline
                    ChangeShader(objList[i], 0, new Vector4(0, 0, 0, 0));
                    objList.Remove(objList[i]);
                }
            }
        }
    }
    
    // Succes Possesion
    void SuccessPosession()
    {
        // If alr possesing unit
        if (nowPossesing)
        {
            var existingObj = player.GetComponent<ControllerPlayer>().CurrentUnit;
            existingObj.GetComponent<NavMeshAgent>().enabled = true;
            existingObj.GetComponent<BasicGameOBJ>().isPossessed = false;
            existingObj.GetComponent<BasicGameOBJ>().SetStateMachine(
                new AttackState(existingObj.GetComponent<Attack_Unit>(), 
                existingObj.GetComponent<BasicGameOBJ>().minionWithinRange,
                existingObj.GetComponent<BasicGameOBJ>().Enemy_Tag));
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

            nowPossesing = false;
        }
        // Update position
        this.transform.position = player.GetComponent<ControllerPlayer>().CurrentUnit.transform.position;
    }
    

}
