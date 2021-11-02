using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{


  public GameObject player;

  void Update()
  {
    gameObject.transform.position = new Vector3(player.transform.position.x+5, gameObject.transform.position.y, gameObject.transform.position.z);
  }
}
