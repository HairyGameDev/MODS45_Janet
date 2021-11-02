using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography;

public class Janet_Behaviour : MonoBehaviour
{

    public static float speed = 4.5f;
    public Sprite janet;
    public Sprite janet_bright;
    float timeSinceSwitch = 0;
    public float annimSpeed;
    bool normal = true;

    public GameObject Player;
    public Text LosingText;

    public float HoleProb;
    public GameObject rabbits;
    public GameObject platform;
    bool alreadydone = false;
    bool prevdestroy = false;
    public GameObject rayon;
    public GameObject poof;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    GameObject findplatform(string name)
    {
        foreach(Transform child in platform.transform)
        {
            if (child.name == name) return child.gameObject;
        }
        return null;
    }

    GameObject findrabbit(string name)
    {
        foreach (Transform child in rabbits.transform)
        {
            if (child.name == name) return child.gameObject;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

        //hole
        if ((int)transform.position.x % 2 == 0)
        {
            if (!alreadydone)
            {
                alreadydone = true;
                float r = Random.Range(0.0f, 1.0f);

                if (r < HoleProb && !prevdestroy)
                {
                    GameObject rabbit = findrabbit("rabbit_" + ((int)transform.position.x - 2));
                    if (rabbit != null && Vector3.Distance(Player.transform.position, rabbit.transform.position) >= 5)
                    {
                        rayon.SetActive(true);
                        StartCoroutine(eviltherabbit(rabbit));
                    }
                    else
                    {
                        GameObject todestroy = findplatform("platform_" + ((int)transform.position.x - 2));
                        rayon.SetActive(true);
                        StartCoroutine(killrayon(todestroy));
                        //Debug.Log("platform_" + ((int)transform.position.x - 2));
                        //Debug.Log(todestroy.transform.name);
                        prevdestroy = true;
                    }
                }
                else prevdestroy = false;
            }

        }
        else alreadydone = false;
        

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (timeSinceSwitch > annimSpeed)
        {
            if (normal)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = janet_bright;
                normal = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = janet;
                normal = true;

            }
            timeSinceSwitch = 0;
        }
        else
        {
            timeSinceSwitch = timeSinceSwitch + Time.deltaTime;
        }

        //Janet lost system
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        if (distance > 12)
        {
            LosingText.text = "You're Losing Her";
            LosingText.gameObject.SetActive(true);
            if (distance > 17)
            {
                LosingText.text = "Speed up or You Won't be Able to Catch Her";
                if (distance > 22)
                {
                    LosingText.text = "She's Gone Now";
                    StartCoroutine(Lost());
                }
            }
        }
        else LosingText.gameObject.SetActive(false);

    }
    //Janet catch system
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
                SceneManager.LoadScene("Win Scene");
        }
    }
     //Lost
     IEnumerator Lost()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Game Over Scene");
    }

    IEnumerator killrayon(GameObject td)
    {
        		GetComponents<AudioSource>()[1].Play();
        yield return new WaitForSeconds(0.4f);
        rayon.SetActive(false);
        //pouf appear
        poof.SetActive(true);
        td.SetActive(false);
	GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        //disparait pouf et platform
        poof.SetActive(false);
 
    }

    IEnumerator eviltherabbit(GameObject td)
    {
    		GetComponents<AudioSource>()[1].Play();
        yield return new WaitForSeconds(0.4f);
        rayon.SetActive(false);
        //pouf appear
        poof.SetActive(true);
        td.GetComponent<EnemyBehaviour>().MakeEvil();
		GetComponents<AudioSource>()[0].Play();
        yield return new WaitForSeconds(0.4f);
        //disparait pouf 
        poof.SetActive(false);
    }



}