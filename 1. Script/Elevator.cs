using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject player;
    public GameObject playerTrigger;
    public GameObject elevator;
    public GameObject blockPlayer;
    public GameObject platform3;
    public GameObject land;
    public GameObject lightbulb;
    public GameObject whiteBg;
    public GameObject escMenu;
    public Animator endCamAnim;
    public float smoothTime = 1f;
    public float elevatorTime = 3f;

    Player playerInstance;
    Renderer rend;
    Tilemap pfTilemap;
    Vector3 velocity, velocity2, velocity3 = Vector3.zero;
    Vector3 startPosition;
    Vector3 endPosition;
    bool isEntered = false;
    bool isRised = false;
    bool isFadeIn = false;
    bool isEnd = false;
    float diff, diff2;

    private void Awake() {
        rend = GetComponent<Renderer>();
        playerInstance = player.GetComponent<Player>();
        endPosition = transform.position;

        pfTilemap = platform3.GetComponent<Tilemap>();
        Color c = pfTilemap.color;
        c.a = 0f;
        pfTilemap.color = c;
    }

    private void Update() {
        if (isEntered) {
            startPosition = player.transform.position;
            player.transform.position = Vector3.SmoothDamp(startPosition, endPosition, ref velocity, smoothTime);
            diff = Mathf.Abs(endPosition.sqrMagnitude - startPosition.sqrMagnitude);
            if (diff < 0.005) {
                isRised = true;
            }
        }

        if (isRised) {
            rend.enabled = false;
            if(!isFadeIn)
                startFadeIn();
            elevator.transform.position = Vector3.SmoothDamp(elevator.transform.position, new Vector3(0f, 23.0f, 0f), ref velocity2, elevatorTime);
            player.transform.position = elevator.transform.position;
            diff = Mathf.Abs(new Vector3(0f, 23.0f, 0f).sqrMagnitude - elevator.transform.position.sqrMagnitude);
            if(diff < 10) {
                blockPlayer.SetActive(true);
                blockPlayer.transform.position = Vector3.SmoothDamp(blockPlayer.transform.position, new Vector3(0f, 22.0f, 0f), ref velocity3, smoothTime);
                player.transform.position = blockPlayer.transform.position;
                diff2 = Mathf.Abs(new Vector3(0f, 22.0f, 0f).sqrMagnitude - blockPlayer.transform.position.sqrMagnitude);
                if(diff2 < 1) {
                    endCamAnim.SetBool("isEnd", true);
                    land.SetActive(false);
                }
            }
        }

        if (endCamAnim.GetCurrentAnimatorStateInfo(0).IsName("endCam") && !isEnd) {
            isEnd = true;
            Invoke("EndScene", 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Player")) {
            playerInstance.SetSpeed(0f);
            playerInstance.SetJumpPower(0f);
            isEntered = true;
        }
    }

    IEnumerator FadeIn() {
        for (float f = 0.05f; f <= 1.2f; f += 0.05f) {
            Color c = pfTilemap.color;
            c.a = f;
            pfTilemap.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        /*for (float f = 1f; f <= 255f; f += 1f) {
            Color c = pfTilemap.color;
            c.a = f;
            pfTilemap.color = c;
            yield return new WaitForSeconds(1f);
        }*/
    }

    void startFadeIn() {
        StartCoroutine("FadeIn");
        isFadeIn = true;
    }

    void EndScene() {
        lightbulb.SetActive(true);
        Invoke("WhiteBg", 2f);
    }

    void WhiteBg() {
        lightbulb.SetActive(false);
        whiteBg.SetActive(true);
        Invoke("EscMenu", 2.5f);
    }

    void EscMenu() {
        escMenu.SetActive(true);
    }
}
