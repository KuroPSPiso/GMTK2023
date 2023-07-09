using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeDiver : MonoBehaviour
{
    public float Speed = 5f;
    public AudioClip DestroyAudioClip;
    public Vector3 Direction;
    private DestroyManager destroyManagerRef;
    private bool isDestroying = false;

    private void Update()
    {
        this.transform.position += this.Direction * this.Speed * Time.deltaTime;

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
                    GameObject.Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
    }

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if (!this.isInitialized) return;

        string tag = collider.gameObject.tag.ToUpper();
        bool isHittingPlayer = tag.Contains("PLAYER");

        if (isHittingPlayer)
        {
            PlayerManager playerManager;
            if (Managers.TryGetPlayerManager(out playerManager))
            {
                //TODO: add invincibility frames
                playerManager.Health--;
            }

            if (!this.isDestroying)
            {
                foreach (Transform child in this.gameObject.transform)
                {
                    this.isDestroying = true;
                    GameObject.Destroy(child.gameObject);
                }
                GameObject.Destroy(this.gameObject);
                if (this.DestroyAudioClip != null) Camera.main.GetComponent<AudioSource>().PlayOneShot(this.DestroyAudioClip);
            }
            
        }
    }
}
