using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardBlink : MonoBehaviour
{
    SpriteRenderer objRenderer;
    SpriteRenderer objRenderer2;

    Coroutine myCoroutine;
    Coroutine myCoroutine2;

    public GameObject Space;
    public GameObject Right;
    public GameObject Left;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Player") && this.gameObject.name.Equals("SpaceTrigger")) {
            objRenderer = Space.GetComponent<SpriteRenderer>();
            Space.SetActive(true);
            myCoroutine = StartCoroutine(Blink(objRenderer));
        }

        if(collision.gameObject.tag.Equals("Player") && this.gameObject.name.Equals("HorizontalTrigger")) {
            objRenderer = Right.GetComponent<SpriteRenderer>();
            Right.SetActive(true);
            myCoroutine = StartCoroutine(Blink(objRenderer));
            objRenderer2 = Left.GetComponent<SpriteRenderer>();
            Left.SetActive(true);
            myCoroutine2 = StartCoroutine(LateBlink(objRenderer2));
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag.Equals("Player") && this.gameObject.name.Equals("SpaceTrigger")) {
            StartCoroutine(StopBlink(myCoroutine));
            Space.SetActive(false);
        }

        if(collision.gameObject.tag.Equals("Player") && this.gameObject.name.Equals("HorizontalTrigger")) {
            StartCoroutine(StopBlink(myCoroutine));
            Right.SetActive(false);
            StartCoroutine(StopBlink(myCoroutine2));
            Left.SetActive(false);
        }
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

    private IEnumerator StopBlink(Coroutine cor) {
        yield return new WaitForSeconds(5f);
        StopCoroutine(cor);
    }
}
