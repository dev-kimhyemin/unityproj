using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    Vector3 triggerPosition;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Player")) {
            triggerPosition = this.transform.position;
            string nextScene = "Sub" + Regex.Replace(SceneManager.GetActiveScene().name, @"\D", "");
            // Debug.Log(nextScene);
            SceneManager.LoadScene(nextScene);
        }
    }
}
