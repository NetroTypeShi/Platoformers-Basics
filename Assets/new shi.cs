using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newshi : MonoBehaviour
{
    [SerializeField] float maxDistance = 0f;
    [SerializeField] float acceleration = 0f;
    Vector2 direction;
    [SerializeField]Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = direction; 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += direction * acceleration * Time.deltaTime;
        transform.up = direction;
        if(gameObject.transform.position.x >= maxDistance || gameObject.transform.position.y >= maxDistance||gameObject.transform.position.x<= -maxDistance|| gameObject.transform.position.y<= -maxDistance)
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}

