
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody rigid;

    public WheelCollider wheel1, wheel2, wheel3, wheel4;

   
    public float drivespeed = 1500f;
    public float steerspeed = 30f;

   
    public float brakepower = 3000f;

    
    public Vector3 centerOfMass = new Vector3(0f, -1.5f, 0f);

    float horizontalInput;
    float verticalInput;
    bool braking;

    void Start()
    {
        rigid.centerOfMass = centerOfMass;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        braking = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        float motor = verticalInput * drivespeed;

        float currentBrake = braking ? brakepower : 0f;

        ApplyDrive(motor, currentBrake);
        ApplySteering();

        ApplyAntiRoll(wheel1, wheel2);
        ApplyAntiRoll(wheel3, wheel4);
    }

    void ApplyAntiRoll(WheelCollider leftWheel, WheelCollider rightWheel)
    {
        WheelHit hit;

        float travelLeft = 1f;
        float travelRight = 1f;

        bool groundedLeft = leftWheel.GetGroundHit(out hit);

        if (groundedLeft)
        {
            travelLeft = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;

        }

        bool groundedRight = rightWheel.GetGroundHit(out hit);

        if (groundedRight)
        {
            travelRight = (-rightWheel.transform.InverseTransformPoint(hit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;

        }
        float antiRollForce = (travelLeft - travelRight) * 8000f;

        if (groundedLeft)  
        {
            rigid.AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
        }

        

        if (groundedRight)
        {
            rigid.AddForceAtPosition(rightWheel.transform.up * antiRollForce, rightWheel.transform.position);
        }
           
    }

    void ApplyDrive(float motor, float brake)
    {
        wheel1.motorTorque = motor;
        wheel2.motorTorque = motor;
        wheel3.motorTorque = motor;
        wheel4.motorTorque = motor;

        wheel1.brakeTorque = brake;
        wheel2.brakeTorque = brake;
        wheel3.brakeTorque = brake;
        wheel4.brakeTorque = brake;
    }

    void ApplySteering()
    {
        float speed = rigid.linearVelocity.magnitude;

        float steeringReduction = Mathf.Lerp(1f, 0.25f, Mathf.Clamp01(speed / 30f));

        float steer = horizontalInput * steerspeed * steeringReduction;

        wheel1.steerAngle = steer;
        wheel2.steerAngle = steer;
    }
}
