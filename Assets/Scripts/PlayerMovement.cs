using UnityEngine;

#pragma warning disable CS0414
public class PlayerMovement : MonoBehaviour
{
    //public float fireRate = 1f;
    public int powerUpStage = 1;
    public float movementSpeed = 5f;
    public float rangeMovement = 2f;
    public float fireRateMultiplier = 1.5f;
    public float AvoidDistance = 3f;

    public bool IsDebug = false;
    public float specialCharge = 0f;

    public bool IsBasicAttack { get { return this.basicShotTimer <= 0f; } }
    public bool IsSpecialAttack = false;

    int threatLimit = 10;
    float rayDist = 40f;
    int threats = 0;
    float basicShotTimer;

    public GameObject[] PlayerModel;
    private Attack attackType { get => this.PlayerModel[this.PlayerIndex].GetComponent<Attack>(); }
    public int PlayerIndex;

    void Move()
    {
        if (this.IsDebug)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.transform.position.x > this.rangeMovement * -1f)
                    this.transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (this.transform.position.x < this.rangeMovement)
                    this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0f);
            }
        }

        //follow pattern
        AIManager aiManager;
        if(Managers.TryGetAIManager(out aiManager))
        {
            Vector3 nearestAttackVector = new Vector3(100, 100, 100);
            float nearestAttackMagnitude = 100f;

            Vector3 nearestAvoidVector = Vector3.zero;
            float nearestAvoidMagnitude = 101f;

            foreach (GameObject go in aiManager.SpawnedUnits.Values)
            {
                float magnitude = Vector3.Distance(this.transform.position, go.transform.position);
                if (magnitude > this.AvoidDistance)
                {
                    if (Mathf.Abs(magnitude) < Mathf.Abs(nearestAttackMagnitude))
                    {
                        nearestAttackMagnitude = magnitude;
                        nearestAttackVector = go.transform.position;
                    }
                }
                else
                {
                    if(Mathf.Abs(magnitude) < Mathf.Abs(nearestAvoidMagnitude))
                    {
                        nearestAvoidMagnitude = magnitude;
                        nearestAvoidVector = go.transform.position;
                    }
                }
                if(nearestAvoidMagnitude < nearestAttackMagnitude)
                {
                    //DODGE
                    if(this.transform.position.x < nearestAvoidVector.x)
                    {
                        if (this.transform.position.x > this.rangeMovement * -1f)
                            this.transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0f);
                        //move right if too close to edge
                    }
                    else
                    {
                        if (this.transform.position.x < this.rangeMovement)
                            this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0f);
                        //move left if too close to edge
                    }
                }
                else
                {
                    //ENGAGE
                    if (this.transform.position.x < nearestAttackVector.x)
                    {
                        if (this.transform.position.x < this.rangeMovement)
                            this.transform.position += new Vector3(movementSpeed * Time.deltaTime, 0f);
                    }
                    else
                    {
                        if (this.transform.position.x > this.rangeMovement * -1f)
                            this.transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0f);
                    }
                }
            }
        }
    }

    private void Update()
    {
        PlayerManager playerManager;
        if (Managers.TryGetPlayerManager(out playerManager)) if (playerManager.Health <= 0) return;

        if(Random.Range(0, 100) < 70) this.basicShotTimer -= Time.deltaTime; //add randomness to clickrate

        if(this.IsBasicAttack)
        {
            GameObject baseAttack = (GameObject)GameObject.Instantiate(attackType.BasePrefab, this.transform.position, new Quaternion());
            baseAttack.GetComponent<Bullet>().OnSpawn(this.transform.up, true, this.fireRateMultiplier); //TODO: replace with spawnpoint and direction vector
            this.basicShotTimer = attackType.BaseCooldown;
        }

        Move();
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(new Ray(this.transform.position, this.transform.position + this.transform.up), out hitInfo, this.rayDist))
        {

        }
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
