using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CardScript : MonoBehaviour
{

    public List<GameObject> card_list;
    public List<GameObject> current_hand;

	// Use this for initialization
	void Start ()
    {
        SpawnCard("Tank_Card");
        SpawnCard("Healer_Card");
        SpawnCard("Tank_Card");
        SpawnCard("Healer_Card");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("addawdnakdnawkd");
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnCard("Tank_Card");
        }

        for (int i = 0; i < current_hand.Count; i++)
        {
            if (i >= 1)
            {
                var adjustPos = current_hand[i].transform.localPosition;
                float size = current_hand[i].GetComponent<MeshFilter>().mesh.bounds.size.x;
                adjustPos.z = (0.17f) * i;
                current_hand[i].transform.localPosition = adjustPos;

            }
            //current_hand[i].transform.rotation = Quaternion.identity;
            //current_hand[i].transform.rotation = Quaternion.Euler(90, 0, 90);
            current_hand[i].transform.localEulerAngles = new Vector3(90, 0, 90);

            if (current_hand[i].GetComponent<Card_ZiJun>().pickedup)
            {
                current_hand[i].transform.parent = null;
                current_hand.Remove(current_hand[i]);
            }
        }
    }

    public void SpawnCard(string cardname)
    {
        
        if (card_list.Count > 0)
        {
            for (int i = 0; i < card_list.Count; i++)
            {
                if (card_list[i].name == cardname)
                {
                    GameObject newCard = Instantiate(card_list[i].gameObject) as GameObject;
                    newCard.transform.SetParent(GameObject.Find("armthing").transform);
                    newCard.transform.localPosition = Vector3.zero;
                    current_hand.Add(newCard.gameObject);
                 //   Debug.Log("Rotation of this card isddsswdawdw " + newCard.transform.localRotation);

                }
            }
        }
    }
}
