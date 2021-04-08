using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    /* 플레이어와 부딪혀 트리거되었을 때 동작하는 모든 오브젝트 관리 */

    public GameObject player;
    public GameObject Space;
    public GameObject Right;
    public GameObject Left;
    public GameObject wall;
    public GameObject stairTrigger;
    public GameObject Platform1;
    public GameObject Platform2;
    public GameObject ElevatorTrigger;
    
    Rigidbody2D playerRigid;
    SpriteRenderer objRenderer;
    SpriteRenderer objRenderer2;
    GameObject obj = null;

    Coroutine myCoroutine;
    Coroutine myCoroutine2;

    private void Awake() {
        playerRigid = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        // Keyboard Guide - Space
        if (collision.name.Equals("SpaceTrigger")) {
            objRenderer = Space.GetComponent<SpriteRenderer>();
            Space.SetActive(true);
            myCoroutine = StartCoroutine(Blink(objRenderer));
        }

        // Keyboard Guide - Horizontal
        if (collision.name.Equals("HorizontalTrigger")) {
            objRenderer = Right.GetComponent<SpriteRenderer>();
            Right.SetActive(true);
            myCoroutine = StartCoroutine(Blink(objRenderer));
            objRenderer2 = Left.GetComponent<SpriteRenderer>();
            Left.SetActive(true);
            myCoroutine2 = StartCoroutine(LateBlink(objRenderer2));
        }

        // Turn the lights on (CutScene1)
        if (collision.name.Contains("Light")) {
            string lightName = "Light" + Regex.Replace(collision.name, @"\D", "");
            obj = GameObject.Find("Lamps/Light/" + lightName);
            obj.SetActive(true);
        }

        // Avoid Bugs - Edge
        if (collision.name.Equals("edge")) {
            Destroy(wall);
            Time.timeScale = 1f;
            playerRigid.mass = 1f;
        }

        // Change Scenes
        if (collision.tag.Equals("SceneTrigger")) {
            string subScene = "Sub" + Regex.Replace(SceneManager.GetActiveScene().name, @"\D", "");
            if (collision.name.Contains("Main")) {
                Destroy(collision.gameObject);
                stairTrigger.SetActive(true);
                SceneManager.LoadScene(subScene, LoadSceneMode.Additive);
                player.transform.position = new Vector2(-19.8f, -145.3f);
            } else {
                SceneManager.UnloadSceneAsync(subScene);
                player.transform.position = new Vector2(5.96f, -13.46f);
            }
        }

        if (collision.name.Equals("StairTrigger")) {
            StartCoroutine(TwoSec());
            Platform1.SetActive(true);
        }

        if (collision.name.Equals("PlatformTrigger")) {
            Platform2.SetActive(true);
            ElevatorTrigger.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // Keyboard Guide - Space
        if (collision.name.Equals("SpaceTrigger")) {
            StartCoroutine(StopCor(myCoroutine));
            Space.SetActive(false);
        }

        // Keyboard Guide - Horizontal
        if (collision.name.Equals("HorizontalTrigger")) {
            StartCoroutine(StopCor(myCoroutine));
            Right.SetActive(false);
            StartCoroutine(StopCor(myCoroutine2));
            Left.SetActive(false);
        }
    }

    IEnumerator TwoSec() {
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator Blink(SpriteRenderer renderer) {
        while (true) {
            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            renderer.material.color = new Color(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator LateBlink(SpriteRenderer renderer) {
        yield return new WaitForSeconds(0.3f);
        while (true) {
            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            renderer.material.color = new Color(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator StopCor(Coroutine cor) {
        yield return new WaitForSeconds(5f);
        StopCoroutine(cor);
    }
}
