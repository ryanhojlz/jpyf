﻿using System.Collections;
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

    // Possesing interaction
    public float timeToPosses = 0;
    public float timeToPossesReference = 8;
    public float possesProgression = 5;
    public float possesProgressionCap = 10;

    // Object count
    public int ObjectCount;

    // A shader instance requires u to drag n drop shader
    public Shader ShaderInstance = null;

    // possesion effect
    public GameObject effectPossesion = null;

    // Possesion Stuff
    public int targetIndex = 0;
    public GameObject unit2Posses;

    // UI effect
    public Image UIEffect = null;

    // Enhance effect when the vr player enhance u
    public float EffectEnhances = 0;



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
        //Debug.Log("Object count is " + objList.Count);
        // Update Objects in the list
        UpdateListObj();
        // Update player possesion
        UpdatePossesion();
        // Update If its possible to Posses
        UpdatePosses();
        // Interactions
        PossesInteraction();
        // Check if in range
        CheckForObjectRange();
        


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


    /*
    // ryan im writing this to you
    // cause you too tired to write
    // but change this ontrigger exit
    // to a distance check instead
    // on trigger enter to put in list
    // then distance check to decide whetehr to remove from the list
    // dank
    // meme

    //private void OnTriggerExit(Collider other)
    //{
    //    // Exit to List
    //    //ExitList(other.gameObject);
    //    ExitList_V2(other.gameObject);
    //    //Debug.Log(other.gameObject.name + " Left ");
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    AssignToList(other.gameObject);
    //    if (other.tag == "Ally_Unit")
    //    {
    //        Physics.IgnoreCollision(other.GetComponent<SphereCollider>(), this.GetComponent<SphereCollider>());
    //    }
    //}
    */


    private void OnTriggerEnter(Collider other)
    {
        AssignToList(other.gameObject);
        if (other.tag == "Ally_Unit")
        {
            if (other.GetComponent<SphereCollider>())
                Physics.IgnoreCollision(other.GetComponent<SphereCollider>(), this.GetComponent<SphereCollider>());
        }

        if (other.name != "enivronment_test_1" || other.name != "Floor")
        {
            if (other.GetComponent<BoxCollider>())
                Physics.IgnoreCollision(other.GetComponent<BoxCollider>(), this.GetComponent<BoxCollider>());
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    AssignToList(other.gameObject);
    //    if (other.tag == "Ally_Unit")
    //    {
    //        Physics.IgnoreCollision(other.GetComponent<SphereCollider>(), this.GetComponent<SphereCollider>());
    //    }
    //    //Debug.Log("Enter");
    //}

    void CheckForObjectRange()
    {
        // if there is no object list inside
        if(objList.Count < 1)
        {
            return;
        }

        for(int i = 0; i < objList.Count; ++i)
        {
            if (objList[i] == null)
                continue;
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
        //Debug.Log("Target index  " + targetIndex);
        // Return if there is nothing in the list
        if (objList.Count < 1)
        {
            // No unit to posses
            unit2Posses = null;
            targetIndex = 0;
            objList.Clear();
            return;
        }
        else
        {
            for (int i = 0; i < objList.Count; ++i)
            {
                if (objList[i] == null)
                {
                    continue;
                }
                if (objList[i] != player.GetComponent<ControllerPlayer>().CurrentUnit)
                {
                    ChangeShader(objList[i], 0.08f, new Vector4(0, 255, 0, 255));
                }
                if (i == targetIndex)
                {
                    unit2Posses = objList[i].gameObject;
                    ChangeShader(objList[targetIndex], 0.1f, new Vector4(0, 128, 255, 255));
                }
                // If null || not active remove
                if (objList[i] == null)
                {
                    objList.RemoveAt(i);
                    //objList.Remove(objList[i]);
                }
            }
        }
        
    }
    

    // Changes the bool if can posses
    void UpdatePosses()
    {
        //if (objList.Count <= 0)
        //    return;
        //if (objList[targetIndex])
        //    canPosses = true;
        //else if (!objList[targetIndex])
        //    canPosses = false;

        if (objList.Count <= 0)
        {
            canPosses = false;
            ReInitPossesInteraction();
        }
        else
        {
            canPosses = true;
        }
    }

    // Actual Possesion Interaction
    void PossesInteraction()
    {
        // If object == null isposseing = false
        if (objList.Count <= 0)
        {
            canPosses = false;
            isPossesing = false;
            ReInitPossesInteraction();
            return;
        }
        if (targetIndex > objList.Count)
        {
            targetIndex = objList.Count - 1;
            if (targetIndex < 0)
            {
                targetIndex = 0;
            }
        }
        if (objList[targetIndex] == null)
        {
            ReInitPossesInteraction();
            objList.Remove(objList[targetIndex]);
            isPossesing = false;
            return;
        }
        if (isPossesing)
        {            
            timeToPosses -= 1 * Time.deltaTime;
            possesProgression -= (1 + objList[targetIndex].GetComponent<BasicGameOBJ>().possesionTier) * Time.deltaTime;

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
        if (canPosses)
        {
            isPossesing = true;
            //ChangeShader(objList[targetIndex], 0.1f, new Vector4(255, 255, 0, 255));
        }
    }



    // Interaction when trying to posses
    public void PossesUp()
    {
        if (isPossesing)
        {
            //Debug.Log("Ran here");
            possesProgression += 0.6f + EffectEnhances;
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
        EffectEnhances = 0;
        canPosses = false;
    }

    // For selecting Target
    public void ChangeTargetIndex(bool updown)
    {
        if (isPossesing)
            return;
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
        if (go.tag != "Ally_Unit")
            return;
        if (go.GetComponent<Building>())
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
            if (go == objList[targetIndex])
            {
                ReInitPossesInteraction();
                objList.Remove(objList[targetIndex]);
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
        {
            canPosses = false;
            isPossesing = false; ;
            ReInitPossesInteraction();
            return;
        }
            
        for (int obj = 0; obj < objList.Count; obj++)
        {
            if (objList[obj] == objList[targetIndex])
            {
                if (isPossesing)
                {
                    ReInitPossesInteraction();
                    canPosses = false;
                    isPossesing = false;
                }
                ChangeShader(objList[obj], 0, new Vector4(0, 0, 0, 0));
                objList.Remove(objList[obj]);
            }
            else if (objList[obj] != objList[targetIndex])
            {
                ChangeShader(objList[obj], 0, new Vector4(0, 0, 0, 0));
                objList.Remove(objList[obj]);
            }
        }
        //if (objList.Count <= 0)
        //{
        //    canPosses = false;
        //    isPossesing = false; ;
        //    ReInitPossesInteraction();
        //}
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
        GetComponent<Rigidbody>().isKinematic = true;

        // Change Unit
        player.GetComponent<ControllerPlayer>().CurrentUnit = objList[targetIndex];
        // Hide this Object
        GetComponent<MeshRenderer>().enabled = false;
        nowPossesing = true;
        // Send to another state
        objList[targetIndex].GetComponent<NavMeshAgent>().enabled = false;
        objList[targetIndex].GetComponent<BasicGameOBJ>().isPossessed = true;
        objList[targetIndex].GetComponent<BasicGameOBJ>().SetStateMachine(new PossessState());

        
        
        // Disable box collider so it wont jank around
        GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<ControllerPlayer>().SpiritUnit = this.gameObject;
        // Remove object from list
        objList.Remove(objList[targetIndex]);
        //
        targetIndex = 0;
        // Change so current unit is red
        ChangeShader(player.GetComponent<ControllerPlayer>().CurrentUnit, 0.15f, new Vector4(255, 0, 0, 255));
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
            GetComponent<Rigidbody>().isKinematic = false;

            player.GetComponent<ControllerPlayer>().CurrentUnit = player.GetComponent<ControllerPlayer>().SpiritUnit;
            player.GetComponent<ControllerPlayer>().SpiritUnit = null;

            //var offset = transform.position;
            //offset.y += 0.6f;
            //transform.position = offset;


            nowPossesing = false;
        }
        // Update position
        this.transform.position = player.GetComponent<ControllerPlayer>().CurrentUnit.transform.position;
        
    }
    
}