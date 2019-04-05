using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public GameObject TargetObject;//The object that is using this prompt
    public float despawnTimer = 4;
    float timer;
    Vector3 _pos;

    private void Start()
    {
        timer = despawnTimer;
    }

    public void SetPromptTarget(GameObject character)
    {
        TargetObject = character;
    }

    private void Awake()
    {

    }

    private void Update()
    {
        if (TargetObject)
        {
            this.gameObject.transform.position = TargetObject.gameObject.transform.position + TargetObject.gameObject.transform.up * (this.gameObject.transform.localScale.y + TargetObject.gameObject.transform.localScale.y);
        }
        else
        {
            Destroy(gameObject);
        }
        
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
