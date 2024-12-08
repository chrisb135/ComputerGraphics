using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{

    public int pointValue = 100;
   
    public PointBehaviour pointSystem;


    private void OnTriggerEnter(Collider other){

        if(other.CompareTag("Player")){
            if(pointSystem != null){
                pointSystem.AddPoints(pointValue);
            }else{
                Debug.LogWarning("oh no");
            }
            Destroy(gameObject);
        }

    }
 
}
