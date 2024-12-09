using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    
    public GameObject spawnPoint;
    public GameObject Player;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.LogWarning(spawnPoint.transform.position);
            Player.transform.position = spawnPoint.transform.position;
        }else{
            
        }
    }
}
