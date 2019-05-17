using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickupHandlerScript : MonoBehaviour
{

    public static PickupHandlerScript Instance = null;

    // This handler is used to prevent picking up multiple objects
    public Object_ControlScript objHandler = null;
    public Transform nearest_pickup_object = null;
    public Transform currentObject = null;
    Vector3 offset = new Vector3(0, 1.5f, 0);
    Vector3 throwDirection = Vector3.zero;

    // If object is ai will drop
    float pickuptimer = 5;
    float pickuptimercap = 5;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        objHandler = GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If is AI
        if (currentObject && currentObject.GetComponent<AI_Movement>())
        {
            if (currentObject.transform.GetChild(0))
            {
                if (currentObject.transform.GetChild(0).GetComponent<Entity_Unit>().GetHealthStat() <= 0)
                {
                    objHandler.throw_item = true;
                    //currentObject.GetComponent<Rigidbody>().isKinematic = false;
                    //currentObject.transform.parent = null;
                    //currentObject = null;
                }
            }

            pickuptimer -= 1 * Time.deltaTime;
            if (pickuptimer <= 0)
            {
                pickuptimer = 0;
                objHandler.throw_item = true;
            }

        }
        else
        {
            pickuptimer = pickuptimercap;
        }

        if (objHandler.Gropper)
        {
            objHandler.throw_item = true;
        }

      
        


        // Throws object
        if (objHandler.throw_item)
        {
            
            if (currentObject)
            {
                // Drop the current object
                //currentObject.localPosition = Vector3.zero;
                currentObject.parent = null;

                // If object is an items
                if (currentObject.GetComponent<Pickup_Scripts>())
                {
                    //currentObject.GetComponent<Rigidbody>().isKinematic = false;
                    currentObject.GetComponent<BoxCollider>().enabled = true;
                }
                // If thrown object is enemy
                //if (currentObject.GetComponent<AI_Movement>())
                //{
                //    //currentObject.GetComponent<NavMeshAgent>().enabled = true;
                //    //currentObject.GetComponent<Rigidbody>().isKinematic = false;
                //    //currentObject.GetComponent<AI_Movement>().enabled = true;
                //}

                if (currentObject.GetComponent<bomb_script>())
                {
                    //currentObject.GetComponent<NavMeshAgent>().enabled = true;
                    //currentObject.GetComponent<Rigidbody>().isKinematic = false;
                    currentObject.GetComponent<SphereCollider>().enabled = true;
                    currentObject.GetComponent<bomb_script>().SetBombState_Active();
                    //currentObject.GetComponent<AI_Movement>().enabled = true;
                }

                currentObject.GetComponent<Rigidbody>().isKinematic = false;

                throwDirection = this.transform.forward * 22;
                currentObject.GetComponent<Rigidbody>().AddForce(throwDirection * 1700);
                currentObject = null;
                PlayerAnimScript.instance_player.SetTriggerThrow();
            }
        }

        //Update UI
        if (!nearest_pickup_object)
        {
            UI_FeedbackScript.Instance.InteractTrue[2] = false;
        }
        else if (nearest_pickup_object && !currentObject)
        {
            UI_FeedbackScript.Instance.TextUI[2].text = "Pickup Object";
            UI_FeedbackScript.Instance.InteractTrue[2] = true;
        }
        if (currentObject)
        {
            UI_FeedbackScript.Instance.TextUI[2].text = "Throw Object";
            UI_FeedbackScript.Instance.InteractTrue[2] = true;
        }


        // If picking up
        if (objHandler.pickup)
        {
            // Nearest Pickup object
            if (!nearest_pickup_object)
                return;


            //if (currentObject)
            //{
            //    // Drop the current object
            //    currentObject.localPosition = Vector3.zero;
            //    currentObject.parent = null;

            //    // if its a item that can be picked up
            //    if (currentObject.GetComponent<Pickup_Scripts>())
            //    {
            //        currentObject.GetComponent<Rigidbody>().isKinematic = false;
            //        currentObject.GetComponent<BoxCollider>().enabled = true;
            //    }
            //    // If its an AI object
            //    if (currentObject.GetComponent<AI_Movement>())
            //    {
            //        //currentObject.GetComponent<NavMeshAgent>().enabled = true;
            //        currentObject.GetComponent<Rigidbody>().isKinematic = false;
            //        //currentObject.GetComponent<AI_Movement>().enabled = true;
            //        throwDirection = this.transform.forward * 22;
            //        currentObject.GetComponent<Rigidbody>().AddForce(throwDirection * 1000);
            //    }

            //    currentObject = null;


            //}

            // Nearest object gone // ReAssign           
            currentObject = nearest_pickup_object;
            nearest_pickup_object = null;

            // If item has animation script
            if (currentObject.GetComponent<ItemAnimation>())
                currentObject.GetComponent<ItemAnimation>().enabled = false;

            // If object is an enemy unit
            if (currentObject.GetComponent<AI_Movement>())
            {
                
                    // Set nav mesh false
                    currentObject.GetComponent<AI_Movement>().enabled = false;
                    currentObject.GetComponent<Rigidbody>().isKinematic = true;
                    currentObject.GetComponent<NavMeshAgent>().enabled = false;
                
            }
            // If its a normal item
            if (currentObject.GetComponent<Pickup_Scripts>())
            {
                currentObject.GetComponent<Rigidbody>().isKinematic = true;
                currentObject.GetComponent<BoxCollider>().enabled = false;
            }
            if (currentObject.GetComponent<bomb_script>())
            {
                currentObject.GetComponent<Rigidbody>().isKinematic = true;
                currentObject.GetComponent<SphereCollider>().enabled = false;
                currentObject.GetComponent<bomb_script>().SetPickUp();
            }


            // Parent cos picking up**
            currentObject.parent = this.transform;
            currentObject.transform.localPosition = offset;

            // Reset rotations
            currentObject.localEulerAngles = Vector3.zero;

        }


        // Yabai code
        nearest_pickup_object = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentObject)
            return;
        if (other.GetComponent<Pickup_Scripts>())
        {
            nearest_pickup_object = other.transform;
        }
        
        if (other.GetComponent<bomb_script>())
        {
            nearest_pickup_object = other.transform;
        }
        if (other.GetComponent<Entity_Unit>())
        {
            if (other.GetComponent<Entity_Unit>().isSpawning())
            {
                return;
            }
            else if (other.GetComponent<Entity_Unit>().GetHealthStat() > 0)
                nearest_pickup_object = other.transform.parent;
        }
    }

    // Return nearest object
    public Transform ReturnNearestObject()
    {
        return nearest_pickup_object;
    }

    // Returns currently held object
    public Transform ReturnCurrentObject()
    {
        return currentObject;
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    // If its the only object i can pickup
    //    if (other.transform == nearest_pickup_object.transform)
    //    {
    //        nearest_pickup_object = null;
    //    }
    //}

    public void SpawnBomb()
    {
        if (!nearest_pickup_object)
            return;

        // Nearest object gone // ReAssign           
        currentObject = nearest_pickup_object;
        nearest_pickup_object = null;

        // If item has animation script
        if (currentObject.GetComponent<ItemAnimation>())
            currentObject.GetComponent<ItemAnimation>().enabled = false;


        if (currentObject.GetComponent<bomb_script>())
        {
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
            currentObject.GetComponent<SphereCollider>().enabled = false;
            currentObject.GetComponent<bomb_script>().SetPickUp();
        }


        // Parent cos picking up**
        currentObject.parent = this.transform;
        currentObject.transform.localPosition = offset;

        // Reset rotations
        currentObject.localEulerAngles = Vector3.zero;

    }

}
