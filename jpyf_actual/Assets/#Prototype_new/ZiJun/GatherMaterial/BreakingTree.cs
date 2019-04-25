using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingTree : MonoBehaviour
{
    bool Player = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            Debug.Log("I AM INSIDE");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CollectMaterial();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2")
        {
            Player = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            Player = false;
        }
    }

    public void CollectMaterial()
    {
        if (Player)
        {
            //GameObject.Find("QTE").GetComponent<Slider>().QTEStart();
            ItemSpawnHandlerScript tempObj = GameObject.Find("ItemSpawnPoint").GetComponent<ItemSpawnHandlerScript>();
            tempObj.SpawnItem(this.gameObject.transform.position);
            Destroy(this.gameObject);
        }
    }
}
