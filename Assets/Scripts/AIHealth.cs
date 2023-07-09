using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    [Header("Ability info")]
    public bool CanDropPower;
    public bool CanDropBomb;
    public MeshRenderer ModifierMaterialRef;
    [Tooltip("Drop rate out of 100")]
    [Range(0f, 100f)]
    public float DropRate = 20f;
    private bool hasDropFunctionality;

    [Header("Base")]
    public float Health;
    public int Score; //determines cost and points for the player
    public AudioClip clip;
    public string Identifier;

    private bool isDestroying = false;

    private void Start()
    {
        this.hasDropFunctionality = Random.Range(0f, 100f) < this.DropRate;

        if(this.hasDropFunctionality && this.CanDropPower && this.ModifierMaterialRef != null)
        {
            for (int i = 0; i < this.ModifierMaterialRef.materials.Length; i++)
            {
                if(this.ModifierMaterialRef.materials[i].HasProperty("_HasPowerUp")) this.ModifierMaterialRef.materials[i].SetInt("_HasPowerUp", 1);
            }
        }
    }

    private void Destroy()
    {
        if (!this.isDestroying)
        {
            if (clip != null) Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);

            foreach (Transform child in this.gameObject.transform)
            {
                this.isDestroying = true;
                GameObject.Destroy(child.gameObject);
            }
            GameObject.Destroy(this.gameObject);
        }
    }

    public bool DealDamage(float damage, out int score)
    {
        score = this.Score;

        if (this.isDestroying) return false;
        this.Health -= damage;

        if (Health < 0)
        {
            this.Destroy();
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        AIManager aiManager;
        if (Managers.TryGetAIManager(out aiManager)) aiManager.SpawnedUnits.Remove(this.Identifier);
    }
}
