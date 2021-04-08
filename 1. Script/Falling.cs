using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cutScene1_1;
    public GameObject player;
    public GameObject fallingTrigger;
    public GameObject wall;
    public GameObject cutSceneBg;
    private Rigidbody2D playerRigid;

    private void Awake() {
        playerRigid = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Player")) {
            cutScene1_1.SetActive(true);
            mainCamera.SetActive(false);
            Time.timeScale = 0.3f;
            Vector3 tmp = wall.transform.position;
            tmp.x = player.transform.position.x;
            wall.transform.position = tmp;
            wall.SetActive(true);
            playerRigid.mass = 0.0001f;
            cutSceneBg.SetActive(true);
        }
    }

    private void LateUpdate() {
        Vector3 tmp = cutScene1_1.transform.position;
        tmp.x = player.transform.position.x;
        tmp.y = player.transform.position.y;
        cutScene1_1.transform.position = tmp;
    }

    private void FixedUpdate() {
        if(player.transform.position.y <= -13.7) {
            Time.timeScale = 1f;
            mainCamera.SetActive(true);
            Destroy(cutScene1_1);
            Destroy(fallingTrigger);
            Destroy(wall);
            Destroy(cutSceneBg);
        }
        
        if(player.transform.position.y <= 13f && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
            playerRigid.mass = 1f;
        }
    }
}
