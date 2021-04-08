using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    /* Moving object smoothly */  

    [SerializeField] [Range(0f, 4f)] float lerpTime = 0;
    [SerializeField] Vector3[] myPositions = null;

    int posIndex = 0;
    int length;
    float t = 0f;

    void Start()
    {
        length = myPositions.Length;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myPositions[posIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t > .9f) {
            t = 0f;
            posIndex++;
            posIndex = (posIndex >= length) ? 0 : posIndex;
        }
    }
}
