using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 5f * Time.deltaTime, 0f);
        transform.Translate(2f * Time.deltaTime, 0f, 0f);       
    }
}
