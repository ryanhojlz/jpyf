using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public List<GameObject> card_reference;
    public List<GameObject> player_hand;


	// Use this for initialization
	void Start ()
    {

        SpawnCard("Tank_Card");
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        // Card Holder Update Loop
        for (int i = 0; i < player_hand.Count; i++)
        {
            var pos = player_hand[i].transform.localPosition;
            pos.z = (0.15f) * i;
            player_hand[i].transform.localPosition = pos;

            player_hand[i].transform.rotation = Quaternion.Euler(0, 70, 0);


            // When a card is picked up and thrown
            if (player_hand[i].GetComponent<Card_ZiJun>().pickedup)
            {
                player_hand[i].transform.parent = null;
                player_hand.Remove(card_reference[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnCard("Tank_Card");
        }
    }

    public void SpawnCard(string name)
    {
        for (int i = 0; i < card_reference.Count; i++)
        {
            if (card_reference[i].name == name)
            {
                //Debug.Log("I ran here");
                GameObject FoundCard = Instantiate(card_reference[i].gameObject) as GameObject;
                FoundCard.transform.parent = this.gameObject.transform;
                FoundCard.transform.localPosition = Vector3.zero;
                player_hand.Add(FoundCard);
            }
        }
        
    }
}
