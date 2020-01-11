using UnityEngine;
using UnityEngine.UI;


public class Car : MonoBehaviour
{


    public bool frontLightsOn;
    public bool brakeEffectsOn;

    public double speed;
    public float rotationSpeed;
    public Transform SpeedNeedle;
    public Vector2 SpeedNeedleRotateRange = Vector3.zero;
    private Vector3 SpeedEulers = Vector3.zero;
    public Transform RpmNeedle;
    public Vector2 RpmNeedleRotateRange = Vector3.zero;
    private Vector3 RpmdEulers = Vector3.zero;
    public float _NeedleSmoothing = 1.0f;
    public Transform steeringWheel;
    public GameObject brakeEffects;
    public GameObject frontLightEffects;
    public GameObject reverseEffect;
    public GameObject FRWheel;
    public GameObject FLWheel;
    private float rotateNeedles = 0.0f;

    public Text txtSpeed, txtRPM, textSpeedScreen;
    private Rigidbody m_Rigidbody;



    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        speed = m_Rigidbody.velocity.magnitude * 2.23693629f;
        frontLightsOn = false;
        brakeEffectsOn = false;

        if (SpeedNeedle) SpeedEulers = SpeedNeedle.localEulerAngles;
        if (RpmNeedle) RpmdEulers = RpmNeedle.localEulerAngles;
    }
    void GestionSpeedAndEffect()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            brakeEffects.SetActive(false);
            if (speed >= 0 && speed < 150)
            {
                reverseEffect.SetActive(false);
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            brakeEffects.SetActive(true);
            if (speed < 0 && speed >= -15)
            {
                reverseEffect.SetActive(true);
            }
        }
        else
        {
            if (speed < 1 && speed > -1)
            {
                brakeEffects.SetActive(false);
                reverseEffect.SetActive(false);
            }
        }
    }

    void Update()
    {
        speed = m_Rigidbody.velocity.magnitude * 3.6f;
        textSpeedScreen.text = ((int) speed).ToString() + "km/h";
        GestionSpeedAndEffect();
        if (SpeedNeedle)
        {

            Vector3 temp = new Vector3(SpeedEulers.x, SpeedEulers.y, Mathf.Lerp(SpeedNeedleRotateRange.x, SpeedNeedleRotateRange.y, (rotateNeedles)));
            SpeedNeedle.localEulerAngles = Vector3.Lerp(SpeedNeedle.localEulerAngles, temp, Time.deltaTime * _NeedleSmoothing);

        }

        if (RpmNeedle)
        {
            Vector3 temp = new Vector3(RpmdEulers.x, RpmdEulers.y, Mathf.Lerp(RpmNeedleRotateRange.x, RpmNeedleRotateRange.y, (rotateNeedles)));
            RpmNeedle.localEulerAngles = Vector3.Lerp(RpmNeedle.localEulerAngles, temp, Time.deltaTime * _NeedleSmoothing);
        }

        if (steeringWheel != null)
        {
            Vector3 eulers = steeringWheel.localRotation.eulerAngles;
            eulers.z = rotateNeedles * 15.0f;

            steeringWheel.localRotation = Quaternion.Slerp(steeringWheel.localRotation, Quaternion.Euler(eulers), Time.deltaTime * 2.5f);

        }

        txtSpeed.text = ((int)(speed)).ToString() + " km/h";
        txtRPM.text = ((int)(speed * 1000.0f)).ToString();

    }

    public void TurnOnFrontLights()
    {
        if (frontLightsOn)
        {
            frontLightEffects.SetActive(true);
            rotateNeedles += Time.deltaTime;
        }
        else
        {
            frontLightEffects.SetActive(false);
            rotateNeedles -= Time.deltaTime;
        }
    }

    public void TurnOnBackLights()
    {
        if (brakeEffectsOn)
        {
            brakeEffects.SetActive(true);
        }
        else
        {
            brakeEffects.SetActive(false);
        }
    }
}