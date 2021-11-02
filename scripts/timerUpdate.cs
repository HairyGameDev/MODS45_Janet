using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

	GetComponent<UnityEngine.UI.Text>().text = "And you did it in "+stopwatch.timeText;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
