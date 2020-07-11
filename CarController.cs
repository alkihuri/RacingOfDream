using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider front_LeftWheel;
    public WheelCollider front_RightWheel;
    public WheelCollider back_LeftWheel;
    public WheelCollider back_RightWheel;
    private Rigidbody carRigidBody;
    [SerializeField, Range(1,45)] float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        carRigidBody = GetComponent<Rigidbody>();
        maxSpeed = 27;
    }

    // Update is called once per frame
    void Update()
    {
        

        float brakes = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            brakes = 3000;
        }
        front_LeftWheel.brakeTorque = brakes;
        front_RightWheel.brakeTorque = brakes;
        back_LeftWheel.brakeTorque = brakes;
        back_RightWheel.brakeTorque = brakes;

        float horizontal = Input.GetAxis("Horizontal");
        float maxAngleSteer = 35;
        float angle = maxAngleSteer * horizontal;
        front_LeftWheel.steerAngle = angle;
        front_RightWheel.steerAngle = angle;
        float vertical = Input.GetAxis("Vertical");
        float maxTorque = 5500;
        maxTorque -= carRigidBody.velocity.magnitude * 200;
        float speed = maxTorque * vertical;

        if (carRigidBody.velocity.magnitude > maxSpeed)
        {
            maxTorque = 140;
            carRigidBody.velocity = carRigidBody.velocity.normalized * maxSpeed;
        }


        back_LeftWheel.motorTorque = speed;
        back_RightWheel.motorTorque = speed;
        ApplyTransformToWheel(front_LeftWheel);
        ApplyTransformToWheel(front_RightWheel);
        ApplyTransformToWheel(back_LeftWheel);
        ApplyTransformToWheel(back_RightWheel);
    }
    public void ApplyTransformToWheel(WheelCollider steerWheel)
    {
        Quaternion wheelAngle;
        Vector3 wheelPosition;
        steerWheel.GetWorldPose(out wheelPosition, out wheelAngle);
        steerWheel.gameObject.transform.rotation = wheelAngle;
    }
}
