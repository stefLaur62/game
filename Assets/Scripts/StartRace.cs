using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class StartRace : MonoBehaviour
{
    public float timeLeft = 3.0f;
    public Text startText;
    public Text chrono;
    public CarUserControl carUserControl;
    private float startTimer;
    public Transform carTransform;
    private bool ended;
    void Start()
    {
        carUserControl.canMove = false;
    }
    void Update()
    {
        if (!carUserControl.canMove)
        {
            timeLeft -= Time.deltaTime;
            startText.text = (timeLeft).ToString("0");
            if (timeLeft<0.5f)
            {
                carUserControl.canMove = true;
                startText.text = "";
                startTimer = Time.time;
                ended = false;
            }
        }
        else
        {
            float t = Time.time - startTimer;
            float m = t % 3600;
            string minutes = ((int)m / 60).ToString("00");
            string seconds = (m % 60).ToString("00");

            if ((carTransform.position.x > 340) && (carTransform.position.x < 370) &&
                (carTransform.position.y > 202) && (carTransform.position.y < 208) &&
                (carTransform.position.z < -226) && (carTransform.position.z < -215))
            {
                ended = true;
                startText.text = "End";

            }
            if (!ended)
            {
                chrono.text = minutes + ":" + seconds;
            }
        }

    }
}
