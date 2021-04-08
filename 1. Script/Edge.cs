using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRigid;
    public GameObject wall;

    private void Awake() {
        playerRigid = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Player")) {
            Destroy(wall);
            Time.timeScale = 1f;
            playerRigid.mass = 1f;
        }
    }
}
