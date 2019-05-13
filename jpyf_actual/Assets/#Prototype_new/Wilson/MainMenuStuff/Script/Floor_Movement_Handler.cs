using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Movement_Handler : MonoBehaviour
{
    public GameObject Camera = null;

    public GameObject Cart = null;

    public List<GameObject> floor;

    public GameObject furtherestFloor = null;
    float furtherest = 0f;

    Vector3 newPosition = Vector3.zero;

    List<GameObject> Movefloor = new List<GameObject>();

    Vector3 PositionChanger = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        Vector3 PositionOfCart = Cart.transform.position;
        Vector3 CartScale = Cart.GetComponent<Collider>().bounds.size;
        Vector3 CartCenter = Cart.GetComponent<Collider>().bounds.center;
        PositionOfCart += CartCenter;
        PositionOfCart.y -= CartScale.y * 0.5f;

        PositionChanger = Vector3.zero;
        for (int i = 0; i < floor.Count; ++i)
        {
            if (i == 0)
            {
                Vector3 firstfloorScale = floor[i].GetComponent<Collider>().bounds.size * 0.5f;

                PositionOfCart.y -= firstfloorScale.y;

                floor[i].transform.position = PositionOfCart;
            }
            else
            {
                PositionChanger = floor[i - 1].transform.position;
                PositionChanger.z += (floor[i].GetComponent<Collider>().bounds.size.z * 0.5f + floor[i - 1].GetComponent<Collider>().bounds.size.z * 0.5f);
                PositionChanger.y -= (floor[i].GetComponent<Collider>().bounds.size.y * 0.5f);
                PositionChanger.y += (floor[i - 1].GetComponent<Collider>().bounds.size.y * 0.5f);
                floor[i].transform.position = PositionChanger;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        furtherestFloor = null;
        furtherest = float.MinValue;
        for (int i = 0; i < floor.Count; ++i)
        {
            if (floor[i])
            {
                newPosition = floor[i].transform.position;
                newPosition.z -= Time.deltaTime * 10;
                floor[i].transform.position = newPosition;

                if (floor[i].transform.position.z + floor[i].GetComponent<Collider>().bounds.size.z < Camera.transform.position.z)
                {
                    Movefloor.Add(floor[i]);
                }

            }
        }

        //Debug.Log("New start");
        for (int i = 0; i < floor.Count; ++i)
        {
            if (furtherest < floor[i].transform.position.z)
            {
                if (furtherestFloor)
                {
                    //Debug.Log(furtherestFloor.transform.position + " , " + floor[i].transform.position);
                }

                furtherest = floor[i].transform.position.z;
                furtherestFloor = floor[i];
            }
        }
        //Debug.Log("New End");

        UpdateFloor();
    }

    void UpdateFloor()
    {
        if (!furtherestFloor)
            return;

        for (int i = 0; i < Movefloor.Count; ++i)
        {
            for (int j = 0; j < floor.Count; ++j)
            {
                if (furtherest < floor[i].transform.position.z)
                {
                    if (furtherestFloor)
                    {
                        //Debug.Log(furtherestFloor.transform.position + " , " + floor[i].transform.position);
                    }

                    furtherest = floor[i].transform.position.z;
                    furtherestFloor = floor[i];
                }
            }
            //PositionChanger = furtherestFloor.transform.position;
            //Movefloor[i].transform.position = PositionChanger;
            if (i == 0)
            {
                PositionChanger = furtherestFloor.transform.position;

                PositionChanger.z += (furtherestFloor.GetComponent<Collider>().bounds.size.z * 0.5f + Movefloor[i].GetComponent<Collider>().bounds.size.z * 0.5f);

                PositionChanger.y -= (floor[i].GetComponent<Collider>().bounds.size.y * 0.5f);
                PositionChanger.y += (furtherestFloor.GetComponent<Collider>().bounds.size.y * 0.5f);

                Movefloor[i].transform.position = PositionChanger;
            }
            else
            {
                //most likely won't come here
                //Debug.Log("HIIII");
                PositionChanger = Movefloor[i - 1].transform.position;

                PositionChanger.z += (Movefloor[i].GetComponent<Collider>().bounds.size.z + Movefloor[i - 1].GetComponent<Collider>().bounds.size.z * 0.5f);

                //PositionChanger.y -= (Movefloor[i].GetComponent<Collider>().bounds.size.y * 0.5f);
                //PositionChanger.y += (Movefloor[i - 1].GetComponent<Collider>().bounds.size.y * 0.5f);

                Movefloor[i].transform.position = PositionChanger;
            }
        }

        Movefloor.Clear();
    }
}
