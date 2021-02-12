using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    float speed = 100;
    float bullet_lifetime = 2;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, bullet_lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }
}
