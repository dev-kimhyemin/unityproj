using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public GameObject escPanel;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && escPanel.activeSelf) {
            Time.timeScale = 1f;
            escPanel.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !escPanel.activeSelf) {
            Time.timeScale = 0f;
            escPanel.SetActive(true);
        }
    }

    public void Resume() {
        Time.timeScale = 1f;
        escPanel.SetActive(false);
    }

    public void Retry() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main01");
    }
}
