using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Take_Damage : MonoBehaviour
{
    public float countdownTimer = 0.1f;
    Material material = null;
    // Use this for initialization
    void Start()
    {
        Material[] materials;

        materials = this.GetComponent<Renderer>().materials;

        if (materials.Length > 0)
        {
            material = materials[0];
            materials[0].color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer -= Time.deltaTime;
        if (countdownTimer < 0f)
        {
            Material[] materials;

            materials = this.GetComponent<Renderer>().materials;

            if (materials.Length > 0)
            {
                materials[0].color = Color.white;
                Destroy(this);
            }
        }
    }
}
