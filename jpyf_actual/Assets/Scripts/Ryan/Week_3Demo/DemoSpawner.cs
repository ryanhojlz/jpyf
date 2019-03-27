using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
#endif
public class DemoSpawner : MonoBehaviour
{
    float demospawner = 0;
    public GameObject week3_demoprefab;
    public Vector3 pos1, pos2;

    public GameObject shaderdemo;
    public Shader _shader1;
    public Shader _shader2; // highlight

    public Material _materialshader;
    public Material _material;

    public List<GameObject> selectable_list;
    public int object_count = 0;
    public int selection = 0;

    private GameObject selectedObject;
    
    // Use this for initialization
	void Start ()
    {
        pos1 = new Vector3(248, 7, -66);
        pos2 = new Vector3(248, 7, 66);

        selectable_list.Add(GameObject.Find("Minion_1"));
        selectable_list.Add(GameObject.Find("Minion_2"));
        selectable_list.Add(GameObject.Find("Minion_3"));
        object_count = selectable_list.Count;

        for (int i = 0; i < selectable_list.Count; ++i)
        {
            if (i == selection)
            {
                //selectable_list[i].GetComponent<Renderer>().material.shader = _shader2;
                //selectable_list[i].GetComponent<Renderer>().material.shader.
                selectable_list[i].GetComponent<Renderer>().material = _materialshader;
                selectedObject = selectable_list[i];
            }
            else
            {
                selectable_list[i].GetComponent<Renderer>().material = _material;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        demospawner += 1 * Time.deltaTime;
        if (demospawner > 2)
        {
            demospawner = 0;
            GameObject spawn = Instantiate(week3_demoprefab) as GameObject;
            spawn.GetComponent<Dummy_MinionScript>()._faction = Dummy_MinionScript.faction.A;
            spawn.transform.position = pos1;

            GameObject spawn2 = Instantiate(week3_demoprefab) as GameObject;
            spawn2.GetComponent<Dummy_MinionScript>()._faction = Dummy_MinionScript.faction.B;
            spawn2.transform.position = pos2;

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log("shader 1 shader 1 shader 1");
            //shaderdemo.GetComponent<Material>().shader = _shader1;   
            shaderdemo.GetComponent<Renderer>().material.shader = _shader1;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Debug.Log("shader 2 shader 2 shader 2");
            shaderdemo.GetComponent<Renderer>().material.shader = _shader2;
            //shaderdemo.GetComponent<Material>().shader = _shader2;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            selection--;
            if (selection > object_count - 1)
            {
                selection = object_count - 1;
            }
            else if (selection < 0)
            {
                selection = 0;
            }
            for (int i = 0; i < selectable_list.Count; ++i)
            {
                if (i == selection)
                {
                    //selectable_list[i].GetComponent<Renderer>().material.shader = _shader2;
                    //selectable_list[i].GetComponent<Renderer>().material.shader.
                    selectable_list[i].GetComponent<Renderer>().material = _materialshader;
                    selectedObject = selectable_list[i];


                }
                else
                {
                    selectable_list[i].GetComponent<Renderer>().material = _material;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            selection++;
            if (selection > object_count - 1)
            {
                selection = object_count - 1;
            }
            else if (selection < 0)
            {
                selection = 0;
            }
            for (int i = 0; i < selectable_list.Count; ++i)
            {
                if (i == selection)
                {
                    //selectable_list[i].GetComponent<Renderer>().material.shader = _shader2;
                    //selectable_list[i].GetComponent<Renderer>().material.shader.
                    selectable_list[i].GetComponent<Renderer>().material = _materialshader;
                    selectedObject = selectable_list[i];

                }
                else
                {
                    selectable_list[i].GetComponent<Renderer>().material = _material;
                }
            }
        }


       
        //Debug.Log("Object " + object_count);
        //Debug.Log("     selection is " + selection);
        var _pos = selectedObject.transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _pos.x += 5 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _pos.x -= 5 * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _pos.z -= 5 * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _pos.z += 5 * Time.deltaTime;

        }
        selectedObject.transform.position = _pos;


#if UNITY_PS4

#endif
    }


    
}
