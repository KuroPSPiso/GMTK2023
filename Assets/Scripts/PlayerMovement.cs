using UnityEngine;

#pragma warning disable CS0414
public class PlayerMovement : MonoBehaviour
{
    public float fireRate = 1f;
    public int powerUpStage = 1;
    public int score = 0;
    public float movementSpeed = 5f;
    public float rangeMovement = 2f;

    public bool IsDebug = false;

    int threatLimit = 10;
    float rayDist = 40f;
    int threats = 0;

    void Move()
    {
        if (this.IsDebug)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.transform.position.x > this.rangeMovement * -1f)
                    this.transform.position -= new Vector3(movementSpeed * Time.fixedDeltaTime, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (this.transform.position.x < this.rangeMovement)
                    this.transform.position += new Vector3(movementSpeed * Time.fixedDeltaTime, 0f);
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(new Ray(this.transform.position, this.transform.position + this.transform.up), out hitInfo, this.rayDist))
        {

        }

        Move();
    }

    private void OnDrawGizmos()
    {
        Color c = Gizmos.color;

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.up * this.rayDist);
        Gizmos.DrawLine(Vector3.zero, this.transform.right * this.rangeMovement);
        Gizmos.DrawLine(Vector3.zero, -this.transform.right * this.rangeMovement);

        Gizmos.color = c;
    }
}
