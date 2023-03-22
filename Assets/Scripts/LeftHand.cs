using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 1f;
        Vector3 a = (new Quaternion(0.75749999f, 0.04080000f, -0.06790000f, 0.64789999f) * new Quaternion(-0.65020001f, 0.16520000f, -0.18380000f, -0.71829998f) * new Quaternion(-0.81870002f, - 0.00850000f,  0.10660000f, - 0.56400001f)).eulerAngles;
        transform.Rotate(a * speed * Time.deltaTime);
    }
}
