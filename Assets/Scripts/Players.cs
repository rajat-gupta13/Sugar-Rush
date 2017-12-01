using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class Players : MonoBehaviour {
    [System.Serializable]
    public class Done_Boundary
    {
        public float xMin, xMax;
    }
    public GameObject bodySourceManager;
    public float speed = 0.2f;
    public JointType trackedJoint;
    private BodySourceManager bodyManager;
    private Body[] bodies;
    private float bodyStartHipPositionX;
    private float bodyStartHipPositionY;
    private bool collectedData = false;
    public float xMin, xMax;
    public GameObject playerHips;
    public bool jumping = false;
    public bool sliding = false;
    public bool punching = false;
    public bool tripping = false;
    private CapsuleCollider playerCollider;
    private Animator playerAnimator;
    public float multiplier;
    private Vector3 shoulderL, elbowL, wristL, shoulderR, elbowR, wristR, shoulderS;
    public GameObject scoreManager;
    private float angleElbowLeft, angleElbowRight, angleShoulderLeft, angleShoulderRight;
    //---------INSERT-------------------
    public static Transform instance;
    public float distane_punch;
    void Awake()
    {
        if (instance == null)
        {
            instance = transform;
        }
    }
    //----------end---------------------

	// Use this for initializattion
    void Start() {
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        playerAnimator = gameObject.GetComponent<Animator>();
        if (bodySourceManager == null)
        {
            Debug.Log("Assign Game Object with Body Source Manager");
        }
        else
        {
            bodyManager = bodySourceManager.GetComponent<BodySourceManager>();
        }
    }

    private ulong currTrackingId = 0;
    private Body GetActiveBody()
    {
        if (currTrackingId <= 0)
        {
            foreach (Body body in this.bodies)
            {
                if (body.IsTracked)
                {
                    currTrackingId = body.TrackingId;
                    return body;
                }
            }

            return null;
        }
        else
        {
            foreach (Body body in this.bodies)
            {
                if (body.IsTracked && body.TrackingId == currTrackingId)
                {
                    return body;
                }
            }
        }

        currTrackingId = 0;
        return GetActiveBody();
    }


    // Update is called once per frame
    void Update() {
        float debugSideways = Input.GetAxis("Horizontal");
        Vector3 movement1 = new Vector3(debugSideways, 0.0f, 1.0f);
        GetComponent<Rigidbody>().velocity = movement1 * speed;
        if (tripping)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
        GetComponent<Rigidbody>().position = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().position.x, xMin, xMax), gameObject.transform.position.y, gameObject.transform.position.z);
        playerCollider.center = new Vector3(0.0f, playerHips.transform.position.y + 0.4f, 0.0f);


        if (Input.GetKeyDown(KeyCode.Space)) {
            playerAnimator.SetBool("Jump", true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAnimator.SetBool("Slide", true);
        }
        //-------------INSET-----------------
        if (!punching && Input.GetKeyDown(KeyCode.A))
        {
            playerAnimator.SetBool("Punch",true);
            punching = true;
            MajorSound.instance.punch_audio();
            Ray ray=new Ray(transform.position+new Vector3(0,2,0),transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, distane_punch))
            {
                if (hit.transform.tag == "Ghost")
                {
                    hit.transform.GetComponent<Ghost>().flag_stop = true;
                    LifeSystem.instance.eat();
                }
            }
            
         
        }
     

        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            LifeSystem.instance.eat();
        }*/
        //------------------end-----------------
        if (bodyManager == null)
        {
            return;
        }
        bodies = bodyManager.GetData();

        if (bodies == null)
        {
            return;
        }
        Body body = GetActiveBody();
      
        if (body == null)
        {
                return;
        }
        if (body.IsTracked)
        {
            if (!collectedData)
            {
                collectedData = true;
                bodyStartHipPositionX = body.Joints[trackedJoint].Position.X;
                bodyStartHipPositionY = body.Joints[trackedJoint].Position.Y;
                
            }
            shoulderL = new Vector3 (body.Joints[JointType.ShoulderLeft].Position.X, body.Joints[JointType.ShoulderLeft].Position.Y, body.Joints[JointType.ShoulderLeft].Position.Z);
            elbowL = new Vector3(body.Joints[JointType.ElbowLeft].Position.X, body.Joints[JointType.ElbowLeft].Position.Y, body.Joints[JointType.ElbowLeft].Position.Z);
            wristL = new Vector3(body.Joints[JointType.WristLeft].Position.X, body.Joints[JointType.WristLeft].Position.Y, body.Joints[JointType.WristLeft].Position.Z);
            shoulderR = new Vector3(body.Joints[JointType.ShoulderRight].Position.X, body.Joints[JointType.ShoulderRight].Position.Y, body.Joints[JointType.ShoulderRight].Position.Z);
            elbowR = new Vector3(body.Joints[JointType.ElbowRight].Position.X, body.Joints[JointType.ElbowRight].Position.Y, body.Joints[JointType.ElbowRight].Position.Z);
            wristR = new Vector3(body.Joints[JointType.WristRight].Position.X, body.Joints[JointType.WristRight].Position.Y, body.Joints[JointType.WristRight].Position.Z);
            shoulderS = new Vector3(body.Joints[JointType.SpineShoulder].Position.X, body.Joints[JointType.SpineShoulder].Position.Y, body.Joints[JointType.SpineShoulder].Position.Z);
            float leanForward = body.Lean.Y;
            //float leanSideWays = body.Lean.X;
            float moveSideways = body.Joints[trackedJoint].Position.X * multiplier;
            float moveVertical = body.Joints[trackedJoint].Position.Y - bodyStartHipPositionY;
            angleElbowLeft = Vector3.Angle((shoulderL - elbowL), (wristL - elbowL));
            angleElbowRight = Vector3.Angle((shoulderR - elbowR), (wristR - elbowR));
            angleShoulderLeft = Vector3.Angle((shoulderS - shoulderL), (elbowL - shoulderL));
            angleShoulderRight = Vector3.Angle((shoulderS - shoulderR), (elbowR - shoulderR));
            //Vector3 movement = new Vector3(moveSideways, 0.0f, 1.0f);
            //GetComponent<Rigidbody>().velocity = movement * speed;
            gameObject.transform.position = new Vector3(moveSideways, gameObject.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, xMin, xMax), gameObject.transform.position.y, gameObject.transform.position.z);
            if (moveVertical < -0.2 && !sliding)
            {
                playerAnimator.SetBool("Slide", true);
            }
            else if (moveVertical > 0.165 && !jumping)
            {
                playerAnimator.SetBool("Jump", true);
            }

            if (((body.HandRightConfidence == TrackingConfidence.High && body.HandRightState == HandState.Closed) && angleElbowRight >= 165.0f && angleShoulderRight <= 175.0f && body.Joints[JointType.WristRight].Position.Y > body.Joints[JointType.ShoulderRight].Position.Y)
                || ((body.HandLeftConfidence == TrackingConfidence.High && body.HandLeftState == HandState.Closed) && angleElbowLeft >= 165.0f && angleShoulderLeft <= 175.0f && body.Joints[JointType.WristLeft].Position.Y > body.Joints[JointType.ShoulderLeft].Position.Y))
            //if (!punching && (angleElbowRight >= 165.0f && angleShoulderRight <= 175.0f) || (angleElbowLeft >= 165.0f && angleShoulderLeft <= 175.0f))
            {
                playerAnimator.SetBool("Punch",true);
                punching = true;
                //---------INSERT---------------
                MajorSound.instance.punch_audio();

                Ray ray=new Ray(transform.position+new Vector3(0,2,0),transform.forward);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, distane_punch))
                {
                    if (hit.transform.tag == "Ghost")
                    {
                        hit.transform.GetComponent<Ghost>().flag_stop = true;
                        LifeSystem.instance.eat();
                    }
                }
                //------------end-------------------
            }

        }

    }

    public void PunchDone()
    {
        playerAnimator.SetBool("Punch", false);
        punching = false;
    }

    public void JumpDone()
    {
        playerAnimator.SetBool("Jump", false);
        jumping = false;
        speed /= 1.5f;
    }

    public void SlideDone()
    {
        playerAnimator.SetBool("Slide", false);
        playerCollider.direction = 1;
        sliding = false;
    }

    public void SlideStart()
    {
        //-----------INSERT-----------
        MajorSound.instance.slide_audio();

        //-----------end------------
        playerCollider.direction = 2;
        sliding = true;
    }

    public void JumpStart() 
    {
        jumping = true;
        speed *= 1.5f;
        //-----------INSERT-----------
        MajorSound.instance.jump_audio();

        //-----------end------------
    }
    public void TrippingDone()
    {
        playerAnimator.SetBool("Tripping", false);
        playerCollider.direction = 1;
        tripping = false;
        scoreManager.GetComponent<ScoreManager>().scoreMultiplier = 1.0f;
    }

    public void TrippingStart()
    {
        //-----------INSERT-----------
        MajorSound.instance.fall_audio();

        //-----------end------------
        playerCollider.direction = 2;
        tripping = true;
        scoreManager.GetComponent<ScoreManager>().scoreMultiplier = 0.0f;
    }
}
