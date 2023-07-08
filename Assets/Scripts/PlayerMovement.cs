using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0414
public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 5f;
    int threatLimit = 10;
    float rayDist = 40f;


    int threats = 0;


    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(new Ray(this.transform.position, this.transform.forward), out hitInfo, this.rayDist))
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.forward * this.rayDist);
    }
}
