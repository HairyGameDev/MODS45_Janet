using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{

    public CapsuleCollider2D capsuleCollider2D;


    public float speed;
    public float jump;
    float moveVelocity;
    public Sprite jeff_iddle;
    public Sprite jeff_walking;

    float timeSinceSwitch = 0;
    public float annimSpeed;
    bool iddle = true;
    bool frontFacing = true;

    public GameObject back;
    public GameObject back1;
    public GameObject back2;

    public GameObject zap;

    void Start()
    {
        capsuleCollider2D = transform.GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        //gameover
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("Game Over Scene");
        }
       
        moveVelocity = 0;


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            zap.SetActive(true);
	    GetComponent<AudioSource>().Play();
            StartCoroutine(killZap());
        }
        //Left Right Movement
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
        {
            //moveVelocity = -speed;
            Vector3 move = new Vector3(-1, 0, 0);
            transform.position += move * speed * Time.deltaTime;
            if (frontFacing)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                frontFacing = false;
            }
            if (timeSinceSwitch > annimSpeed)
            {
                if (iddle)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = jeff_walking;
                    iddle = false;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = jeff_iddle;
                    iddle = true;

                }
                timeSinceSwitch = 0;
            }
            else
            {
                timeSinceSwitch = timeSinceSwitch + Time.deltaTime;
            }
            // background
            if (Vector3.Distance(back2.transform.position, gameObject.transform.position) > 25)
            {
                back2.transform.position = back1.transform.position;
                back1.transform.position = back2.transform.position + new Vector3(-26.5f, 0, 0);
            }
        }
            if (Input.GetKey(KeyCode.D))
            {
                if (!frontFacing)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    frontFacing = true;
                }
              //moveVelocity = speed;

               Vector3 move = new Vector3(1, 0, 0);
               transform.position += move * speed * Time.deltaTime;
            if (timeSinceSwitch > annimSpeed)
                {
                    if (iddle)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = jeff_walking;
                        iddle = false;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = jeff_iddle;
                        iddle = true;
                    }
                    timeSinceSwitch = 0;
                }
                else
                {
                    timeSinceSwitch = timeSinceSwitch + Time.deltaTime;
                }
                if (Vector3.Distance(back1.transform.position, gameObject.transform.position) > 25)
                {
                    back1.transform.position = back2.transform.position;
                    back2.transform.position = back1.transform.position + new Vector3(26.5f, 0, 0);
                }
            }
        if ((!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = jeff_iddle;
            iddle = true;
        }

        //Jumping
        if (GetComponent<CapsuleCollider2D>().GetContacts(new ContactPoint2D[1]) >= 1 &&
            (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            gameObject.GetComponent<SpriteRenderer>().sprite = jeff_iddle;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);


    }


    IEnumerator killZap()
    {
        yield return new WaitForSeconds(0.35f);
        zap.SetActive(false);
    }

}
