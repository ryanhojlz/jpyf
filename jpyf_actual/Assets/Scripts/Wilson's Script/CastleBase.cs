using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBase : Building
{
    private EndGameState temp;

    // Update is called once per frame
    public override void Unit_Self_Update()
    {
        if (healthValue <= 0 && this.gameObject.activeInHierarchy)
        {
            //Debug.Log(this.name);
            //end game here
            temp = new EndGameState();
            temp.EndGame(this.GetComponent<BasicGameOBJ>().Enemy_Tag);
            //this.gameObject.SetActive(false);
            Die();
        }
    }

    public float GetHealth()
    {
        return healthValue;
    }

    public void SetHealth(float _castleBaseHealth)
    {
        healthValue = _castleBaseHealth;
    }
}
