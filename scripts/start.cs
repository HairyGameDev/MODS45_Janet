using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (lore());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator lore()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Lore Scene");
    }
}
