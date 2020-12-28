using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float speed = 5;
    float speedrot = 50;
    private void Update()
    {
        Vector3 v3 = new Vector3(0,Input.GetAxis("Horizontal"), 0.0f);
        transform.Rotate(v3 * speedrot * Time.deltaTime);
        v3 = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        transform.Translate(v3 * speed * Time.deltaTime);


    }
}
