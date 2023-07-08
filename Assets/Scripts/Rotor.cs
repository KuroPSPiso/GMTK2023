using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        float rotSpeed = Time.deltaTime * speed;
        this.transform.Rotate(new Vector3(rotSpeed, rotSpeed, rotSpeed));
    }
}
