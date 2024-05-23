using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //Sound that plays when the player reaches a checkpoint
    private Transform currentCheckpoint; //Stores the current checkpoint
    private Damageable playerHealth; //Reference to the PlayerHealth script

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has tag 'Player'");
        }

        playerHealth = player.GetComponent<Damageable>();
    }

    public void RespawnPlayer()
    {
        transform.position = currentCheckpoint.position; //Move the player to the current checkpoint
        playerHealth.Respawn(); //Respawn the player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == ("Checkpoint"))
        {
            currentCheckpoint = collision.transform; //Set the current checkpoint to the checkpoint the player collided with
            AudioSource.PlayClipAtPoint(checkpointSound, transform.position); //Play the checkpoint sound
            collision.GetComponent<Collider2D>().enabled = false; //Disable the collider of the checkpoint so it can't be triggered again
        }
    }
}
