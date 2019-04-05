using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    public GameObject TargetObject;//The object that is using this prompt
    public float despawnTimer = 4;
    float timer;

    float speedtoGoUp = 3;
    float deltaTimeIncrement = 0;
    public SpriteRenderer spriteRenderer;

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
        Color tmp = spriteRenderer.color;
        tmp.a -= 1.5f * Time.deltaTime;
        spriteRenderer.color = tmp;

        if (TargetObject)
        {
            this.gameObject.transform.position = TargetObject.gameObject.transform.position + TargetObject.gameObject.transform.up * (this.gameObject.transform.localScale.y + TargetObject.gameObject.transform.localScale.y + (speedtoGoUp * deltaTimeIncrement));
            deltaTimeIncrement += Time.deltaTime;
            //this.gameObject.transform.position += new Vector3(0, 100 * Time.deltaTime, 0); //cannot do this as the position will reset
            //_alpha.a -= 3 * Time.deltaTime;
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
