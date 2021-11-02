using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowScroll : MonoBehaviour
{

  public GameObject player;
  public float speed;
  float lastPlayerX = 0;

    void start()
    {
     lastPlayerX = player.transform.position.x;
    }
    
  void Update()
  {
   float diff = player.transform.position.x - lastPlayerX;
   if (diff != 0.0f){
     transform.position += new Vector3(speed*diff, 0);
   }
   lastPlayerX = player.transform.position.x;
  }
}
