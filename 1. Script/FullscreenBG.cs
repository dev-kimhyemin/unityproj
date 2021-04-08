using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenBG : MonoBehaviour
{
    public RectTransform rectTransform;

    private void Awake() {
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
