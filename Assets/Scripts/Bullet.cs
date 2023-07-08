using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DamageOnHit = 1f;
    public float Speed = 5f;
    public bool IsFromPlayer = false;
    public AudioClip SoundClip;
    private DestroyManager destroyManagerRef;
    private bool isDestroying = false;
    private Vector3 direction;
    private bool isInitialized = false;

    public void OnSpawn(Vector3 direction, bool isFromPlayer = false, float SpeedMultiplier = 1f)
    {
        this.direction = direction;
        if(SoundClip != null) Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundClip);
        this.Speed *= SpeedMultiplier;
        this.IsFromPlayer = isFromPlayer;
        this.isInitialized = true;
    }

    private void Update()
    {
        this.transform.position += Vector3.up * this.Speed * Time.deltaTime;

        if (!this.isDestroying)
        {
            if (this.destroyManagerRef == null)
            {
                Managers.TryGetDestroyManager(out this.destroyManagerRef);
            }
            else
            {
                if (this.transform.position.x < this.destroyManagerRef.Range.x ||
                    this.transform.position.x > this.destroyManagerRef.Range.x + this.destroyManagerRef.Range.width ||
                    this.transform.position.y < this.destroyManagerRef.Range.y ||
                    this.transform.position.y > this.destroyManagerRef.Range.y + this.destroyManagerRef.Range.height
                   )
                {
                    this.isDestroying = true;

                    foreach (Transform child in this.gameObject.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.isInitialized) return;

        string tag = collider.gameObject.tag.ToUpper();
        bool isHittingPlayer = tag.Contains("PLAYER");

        Debug.Log($"is hitting player: {isHittingPlayer} & {this.IsFromPlayer}");

        if (tag.Contains("VESSEL") || isHittingPlayer)
        {
            if (isHittingPlayer != this.IsFromPlayer)
            {
                //deal damage
                if (isHittingPlayer)
                {
                    //knock 1 hp
                }
                else
                {
                    //deal % damage
                }

                if (!this.isDestroying)
                {
                    foreach (Transform child in this.gameObject.transform)
                    {
                        this.isDestroying = true;
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }
        }
    }
}
