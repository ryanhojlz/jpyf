using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Behaviour : MonoBehaviour
{
    public TextMesh Text;
    public float speed_fade_way;
    public float speed_move_up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var temp = Text.color;
        temp.a = Text.color.a - (Time.deltaTime * speed_fade_way);
        Text.color = temp;

        transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * speed_move_up), transform.position.z);

        if (Text.color.a <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
