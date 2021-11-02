using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGeneration : MonoBehaviour
{

  public GameObject player;
  public GameObject platform;
  int prevHeight = 1;
  GameObject lastGenerated;

  public GameObject rabbit;
  public GameObject bush;
  public GameObject rabbits;
  public GameObject tree;

    void Start()
    {
      Physics.IgnoreLayerCollision(3, 6);
      Physics.IgnoreLayerCollision(6, 3);      
      for(int i = -20; i < 30; i+=2){
	Generate(i);
      }
    }

    void Generate(int x){
      int height = 1;
      if (prevHeight == 1) height = (int)Random.Range(1.0f,3.0f);
      else height = (int)Random.Range(1.0f,4.0f);
      prevHeight=height;
      Vector3 position = new Vector3(x, -6+height, -1);
      GameObject pl = GameObject.Instantiate(platform, position, Quaternion.identity);
      pl.name = "platform_"+x;
      pl.transform.SetParent(transform);
      lastGenerated = pl;
      if (Random.Range(0.0f, 1.0f)<0.5){
	        GenerateEnemy(x);
      }
    }

    void GenerateEnemy(int x){
        float r = Random.Range(0.0f, 1.0f);
        Vector3 position = new Vector3(x, 0, -2);
        GameObject en;
        if (r < 0.33f)
        {
            en = GameObject.Instantiate(rabbit, position, Quaternion.identity);
        }
        else if(r < 0.66f) en = GameObject.Instantiate(bush, position, Quaternion.identity);
        else en = GameObject.Instantiate(tree, position, Quaternion.identity);
        en.name = "rabbit_" + x;
        en.transform.SetParent(rabbits.transform);
    en.GetComponent<EnemyBehaviour>().player = player;
    }
    
    void Update()
    {
      if(Vector3.Distance(player.gameObject.transform.position, lastGenerated.gameObject.transform.position) < 40) {
	Generate((int)lastGenerated.transform.position.x+2);
      }
    }
}
