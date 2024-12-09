using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    public GameObject ending;
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            ending.SetActive(true);
            Destroy(gameObject);
        }else{
            Debug.LogWarning("oh no");
        }

        
    }
}
