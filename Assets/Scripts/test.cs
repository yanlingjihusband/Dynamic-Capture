using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float speed = 1f;
        //Vector3 a = new Quaternion(0.75749999f, 0.04080000f, -0.06790000f, 0.64789999f).eulerAngles;
        //transform.Rotate(a * speed * Time.deltaTime);
        transform.rotation = new Quaternion(0.75749999f, 0.04080000f, -0.06790000f, 0.64789999f);
    }
}
