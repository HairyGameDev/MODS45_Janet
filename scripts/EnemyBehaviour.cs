using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{

  public string type;
  public Sprite niceSprite;
  public Sprite notNiceSprite;

    public GameObject player;
  
  float timeSinceSwitch = 0;
  float timeToNext = 0.5f;

  bool lookingRight = false;

    bool isEvil = false;

    public float speed;

    public float jump;
  
  void Start()
  {

  }

  void Update()
  {
    if (type=="rabbit" && !isEvil){
      timeSinceSwitch += Time.deltaTime;
      if (timeSinceSwitch > timeToNext){
	if (lookingRight) transform.rotation = Quaternion.Euler(0, 0f, 0);
	else transform.rotation = Quaternion.Euler(0, 180f, 0);
	lookingRight = !lookingRight;
	timeSinceSwitch = 0;
	timeToNext = 0.5f + Random.Range(0.0f, 1.0f);
      }
      if (!lookingRight) GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, GetComponent<Rigidbody2D>().velocity.y);
      else GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, GetComponent<Rigidbody2D>().velocity.y);
    }
        if (isEvil)
        {
            int dir = 1;
            if (player.transform.position.x < transform.position.x)
            {
                dir = -1;
                transform.rotation = Quaternion.Euler(0, 0f, 0);
            }
            else transform.rotation = Quaternion.Euler(0, 180f, 0);
            Vector3 move = new Vector3(dir, 0, 0);
            transform.position += move * speed * Time.deltaTime;
            if (GetComponent<BoxCollider2D>().GetContacts(new ContactPoint2D[2]) >= 2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }
           
          
        }
  }

    public void MakeEvil()
    {
        isEvil = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = notNiceSprite;
        gameObject.layer = 7;
    }

    public void MakeNice()
    {
        isEvil = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = niceSprite;
        gameObject.layer = 6;
    }

    //kill
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Game Over Scene");
        }
        if (col.gameObject.tag == "zap")
         {
            MakeNice();
        }
    }
}
