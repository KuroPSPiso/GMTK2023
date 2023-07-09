using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public int IncomePerMS;
    public int UnitLimit;
    public float BossTimer;
    public AIAsset[] AIAssets;
    public int SelectedAIAsset = -1;

    public int BossCountDownTimer { get { return (int)(this.bossTimer + 0.5f); } }
    private float bossTimer;

    public SortedDictionary<string, GameObject> SpawnedUnits;

    private void Start()
    {
        SpawnedUnits = new SortedDictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        bossTimer -= Time.deltaTime;

        if (this.SelectedAIAsset > -1 && Input.GetMouseButtonDown(0))
        {
            //raycast and check for "SPAWN" area
            //draw ray or something to draw route or path

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if(hit.collider.tag.ToUpper() == "SPAWN")
                {
                    BuyUnit(this.AIAssets[this.SelectedAIAsset], new Vector2(hit.point.x, hit.point.y));
                }
            }
        }
    }

    public void BuyUnit(AIAsset aiAsset, Vector2 position)
    {
        GameObject spawnedUnit = (GameObject) GameObject.Instantiate(aiAsset.Prefab, new Vector3(position.x, position.y, 0), Quaternion.Euler(-Vector3.up));
        string identifier = Time.time.ToString();
        spawnedUnit.GetComponent<AIHealth>().Identifier = identifier;
        this.SpawnedUnits.Add(identifier, spawnedUnit);
    }
}
