using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiCarController : MonoBehaviour
{
    public WheelCollider front_LeftWheel;
    public WheelCollider front_RightWheel;
    public WheelCollider back_LeftWheel;
    public WheelCollider back_RightWheel;
    float goValue, turnValue;
    bool brakeCar;
    Rigidbody carRigidBody;
    public List<GameObject> trackPath;
    int currentPosition;
    [SerializeField, Range(5, 25)] float maxSpeed;
    float timerToRespawn;

    RaycastHit forwardObject, rightObject, leftObject;

    // Start is called before the first frame update
    void Start()
    {
        if(maxSpeed == null )
            maxSpeed = 20;
        goValue = 0;
        turnValue = 0;
        brakeCar = false;
        carRigidBody = GetComponent<Rigidbody>();
        currentPosition = 0;
        trackPath = GameObject.FindGameObjectsWithTag("CheckPoint").ToList<GameObject>() ;
        trackPath = trackPath.OrderBy(o => o.name).ToList<GameObject>(); 
        timerToRespawn = 0;
    }

    // Update is called once per frame
    
    public void ApplyTransformToWheel(WheelCollider steerWheel)
    {
        Quaternion wheelAngle;
        Vector3 wheelPosition;
        steerWheel.GetWorldPose(out wheelPosition, out wheelAngle);
        steerWheel.gameObject.transform.rotation = wheelAngle;
    }

    private void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out forwardObject))
        {
            if (forwardObject.transform.tag == "CheckPoint")
            {
                if (trackPath[currentPosition].Equals(forwardObject.transform.gameObject))
                {
                    Debug.DrawLine(transform.position, forwardObject.transform.position, Color.yellow);
                    StopTurn();
                    GoForward();
                }
            }
            else
            {
              

            }

        }

        if (Physics.Raycast(transform.position, transform.forward + transform.right, out rightObject))
        {
            if (rightObject.transform.tag == "CheckPoint")
            {
                if (trackPath[currentPosition].Equals(rightObject.transform.gameObject))
                {
                    Debug.DrawLine(transform.position, rightObject.transform.position, Color.red);
                    GetLowEngine();
                    TurnRight();
                }
            }
             
        }

        if (Physics.Raycast(transform.position, transform.forward - transform.right, out leftObject))
        {
            if (leftObject.transform.tag == "CheckPoint")
            {
                if (trackPath[currentPosition].Equals(leftObject.transform.gameObject))
                {
                    Debug.DrawLine(transform.position, leftObject.transform.position, Color.red);
                    GetLowEngine();
                    TurnLeft();
                }

            }
           
        }
  
        if (carRigidBody.velocity.magnitude < 1)
        {
            timerToRespawn += Time.deltaTime;
            if (timerToRespawn > 5)
            {


                if (currentPosition != 0)
                {
                    transform.rotation = trackPath[currentPosition - 1].transform.rotation;
                    transform.position = trackPath[currentPosition - 1].transform.position;
                }
                else
                {
                    transform.position = trackPath[trackPath.Count - 1].transform.position;
                    transform.rotation = trackPath[trackPath.Count - 1].transform.rotation;
                }

                timerToRespawn = 0;
            }
        }
  



        

        float horizontal = turnValue;
        float maxAngleSteer = 35;
        float angle = maxAngleSteer * horizontal;
        front_LeftWheel.steerAngle = angle;
        front_RightWheel.steerAngle = angle;
        float vertical = goValue;
        float maxTorque = 7990;
        float speed = maxTorque * vertical;

        if (carRigidBody.velocity.magnitude > maxSpeed)
        {
            maxTorque = 135;
            carRigidBody.velocity = carRigidBody.velocity.normalized * maxSpeed;
        }

        back_LeftWheel.motorTorque = speed;
        back_RightWheel.motorTorque = speed;
        ApplyTransformToWheel(front_LeftWheel);
        ApplyTransformToWheel(front_RightWheel);
        ApplyTransformToWheel(back_LeftWheel);
        ApplyTransformToWheel(back_RightWheel);

        trackPath[currentPosition].GetComponent<MeshRenderer>().enabled = true;
    }

    public void GoForward()
    {
        goValue = 1;
    }
    public void GoBack()
    {
        goValue = -1;
    }
    public void TurnLeft()
    {
        turnValue = -1;
    }
    public void TurnRight()
    {
        turnValue = 1;
    }

    public void GetLowEngine()
    {
        goValue = 0.3f;
    }
    public void StopTurn()
    {
        turnValue = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="CheckPoint")
        {
            if(trackPath[currentPosition].Equals(other.gameObject))
            {
                trackPath[currentPosition].GetComponent<MeshRenderer>().enabled = false;
                currentPosition++;
                if (currentPosition == trackPath.Count)
                    currentPosition = 0;
            }
        }
    }
}
