using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stopwatch : MonoBehaviour
{
    float timer;
    float seconds;
    float minutes;

    public static string timeText = "00 minutes and 00 seconds";


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StopwatchCalcul();
    }

    void StopwatchCalcul()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = (int)(timer / 60);

	timeText = minutes.ToString("00") + " minutes and " + seconds.ToString("00") + " seconds!";

        GetComponent<UnityEngine.UI.Text>().text = minutes.ToString("00") + ":" + seconds.ToString("00");

    }

}
