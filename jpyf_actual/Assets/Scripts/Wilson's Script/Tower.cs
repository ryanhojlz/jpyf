using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //tower is a non living thing so can't really do anything but has health value

    public float towerHealthValue;
    public float towerDefenseValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(towerHealthValue <= 0)
        {
            Destroy(gameObject);    
        }
    }

    public float GetHealth()
    {
        return towerHealthValue;
    }

    public void SetHealth(float _towerHealth)
    {
        towerHealthValue = _towerHealth;
    }


}
