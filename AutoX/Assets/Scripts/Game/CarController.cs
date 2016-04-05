using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    public static CarController instance;

    //rigidBody
    private Rigidbody2D rigidBody;

    //engine
    private bool engine;

    //Gear type;
    public float gearCurrent;
    public float gearNuetral;
    public float gear1;
    public float gear2;
    public float gear3;
    public float gear4;
    public float gearReverse;
    public Vector2 initialPosition;

    public Detectors colliderLeft;
    public Detectors colliderRight;
    public Detectors colliderForward;

    //Pedals
    private int gasPedal;
    private int breakPedal;
    private int clutchPedal;

    private float turnDelay = 3.0f;
    private float addedSpeed = 0.0f;

    private int currentAngle;
    private bool isPathsExist;
    private bool canTurn;
    private bool running;

    private bool hasBuild;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this);
        }
    }


    // Use this for initialization
    void Start()
    {
        engine = false;
        rigidBody = GetComponent<Rigidbody2D>();
        currentAngle = 0;

        isPathsExist = false;
        canTurn = true;
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            useLanguage();

            if (canTurn == false)
            {
                turnDelay -= Time.deltaTime;
                if (turnDelay < 0.0f)
                {
                    turnDelay = 3.0f;
                    canTurn = true;
                }
            }
        }
    }

    public void setInitial()
    {
        engine = false;
        rigidBody = GetComponent<Rigidbody2D>();
        currentAngle = 0;
        gearCurrent = 0;
        addedSpeed = 0.0f;
        this.transform.position = initialPosition;

        isPathsExist = false;
        canTurn = true;
        running = false;

        hasBuild = false;
        rigidBody.velocity = new Vector2(0.0f, 0.0f);

        WastedPanelController.instance.Open(false);
    }

    public void buildCar()
    {
        if (Grammar.compile())
        {
            ErrorPanelController.instance.SetMessage("No Errors Found.\nSuccessful build.");
            ErrorPanelController.instance.Open(true);
            hasBuild = true;
        }
        else
        {
            ErrorPanelController.instance.SetMessage("Errors have been found.\nCheck console.");
            ErrorPanelController.instance.Open(true);
            hasBuild = false;
        }
    }

    public void runCar(ButtonRibbon btn)
    {
        if (hasBuild == true)
        {
            if (running == true)
            {
                running = false;
                setInitial();
                btn.deselect();
            }
            else
            {
                btn.select();
                running = true;
            }

        }
        else
        {
            btn.deselect();
            ErrorPanelController.instance.SetMessage("Error.\nBuild first");
            ErrorPanelController.instance.Open(true);
            running = false;
        }

    }

    public void useLanguage()
    {
        Grammar.run();
    }


    public void turn(int num)
    {
        if (canTurn)
        {
            num = (num == 1) ? 1 : -1;
            currentAngle = (360 + (int)(rigidBody.rotation) + (90 * num)) % 360;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, (float)currentAngle);
            updateVelocity();

            canTurn = false;
        }
        
    }

    public void updateVelocity()
    {
        if (engine)
        {
            int x = (int)Mathf.Cos(Mathf.Deg2Rad * currentAngle);
            int y = (int)Mathf.Sin(Mathf.Deg2Rad * currentAngle);
            float speed = gearCurrent + addedSpeed;
            rigidBody.velocity = new Vector2(speed * x, speed * y);
        }

    }

    public void zeroVelocity()
    {
        rigidBody.velocity = new Vector2(0.0f, 0.0f);
        Wait(2);
    }

    public int getCurrentAngle()
    {
        return currentAngle;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {

        if(GetComponent<BoxCollider2D>().IsTouching(coll)){
            isPathsExist = (coll.gameObject.tag == "paths") ? true : false;

            if(coll.gameObject.tag == "block")
            {
                WastedPanelController.instance.Open(true);
            }
        }

    }
    
    void OnTriggerExit2D(Collider2D coll)
    {
        if (GetComponent<BoxCollider2D>().IsTouching(coll))
        {
            isPathsExist = (coll.gameObject.tag == "paths") ? false : true;
        }
    }

    void autoTurn()
    {
        if (isPathsExist){
            turn(-1);
            isPathsExist = false;
        }
    }

    public void setEngine(bool engine)
    {
        this.engine = engine;
    }

    public bool isEngineOn()
    {
        Debug.Log("I've been called");
        return engine;
    }

   public void askDriver()
    {
        pauseCar(true);
        AskDriverController.instance.Open(true);
    }

    public void askDriver(int forward, int left, int right)
    {
        pauseCar(true);

        if((forward + left + right) == 1)
        {
            if(forward != 0)
            {
                turnForward();
            }
            else if(left != 0)
            {
                turnLeft();
            }
            else if(right != 0)
            {
                turnRight();
            }

        }
        else
        {
            AskDriverController.instance.setButtonLeft(left);
            AskDriverController.instance.setButtonRight(right);
            AskDriverController.instance.setButtonUp(forward);
            AskDriverController.instance.Open(true);
        }

        
    }

   
    public void turnLeft()
    {
        turn(1);
        isPathsExist = false;
        AskDriverController.instance.Open(false);
        pauseCar(false);
    }

    public void turnRight()
    {
        turn(-1);
        isPathsExist = false;
        AskDriverController.instance.Open(false);
        pauseCar(false);
    }

    public void turnForward()
    {
        isPathsExist = false;
        AskDriverController.instance.Open(false);
        pauseCar(false);
    }

   public bool isOtherRoadExist()
   {
        
        if(isPathsExist == true && canTurn == true)
        {
            return true;
        }
        else
        {
            return false;
        }
   }

    public int isLeftRoadAvailable()
    {
        int ret = 1;
        if (colliderLeft.collide)
        {
            ret = 0;
        }
        return ret;
    }

    public int isRightRoadAvailable()
    {
        int ret = 1;
        if (colliderRight.collide)
        {
            ret = 0;
        }
        return ret;
    }

    public int isForwardRoadAvailable()
    {
        int ret = 1;
        if (colliderForward.collide)
        {
            ret = 0;
        }
        return ret;
    }

    public void stepGas(int num)
    {
        if(num > 0)
        {
            if(gearCurrent > 0.0f)
            {
                addedSpeed = 0.3f;
            }
            else
            {
                addedSpeed = -0.3f;
            }
        }
        else
        {
            addedSpeed = 0;
        }
        updateVelocity();
    }

    public void stepClutch(int clutch)
    {
        if(clutch < 0)
        {
            clutch = 0;
        }
        else if(clutch >= 100)
        {
            clutch = 100;
        }

        clutchPedal = clutch;
    }

   public void changeGear(int gear)
    {
        if(clutchPedal >= 100 || this.engine == false)
        {
            if(gear >= 0 && gear <= 4)
            {
                float x = gear - gearCurrent;

                if(x <= -0.9f)
                {
                    zeroVelocity();
                    engine = false;
                }
            }


            if(gear == 0){
                gearCurrent = gearNuetral; 
            }
            else if(gear == 1)
            {
                gearCurrent = gear1;
            }
            else if(gear == 2)
            {
                gearCurrent = gear2;
            }
            else if(gear == 3)
            {
                gearCurrent = gear3;
            }
            else if(gear == 4)
            {
                gearCurrent = gear4;
            }
            else if(gear == 5)
            {
                gearCurrent = gearReverse;

                if(rigidBody.velocity.x != 0.0f || rigidBody.velocity.y != 0.0f)
                {
                    zeroVelocity();
                    engine = false;
                }
            }
            else
            {

            }

                

            updateVelocity();
        }
    }
   
   public void pauseCar(bool isPause)
   {
        if (isPause)
        {
            rigidBody.velocity = new Vector2(0.0f, 0.0f);
        }
        else
        {
            updateVelocity();
        }

        running = !isPause;
   }

    public int getCurrentGear()
    {
        int retval = 0;

        if (gearCurrent == gearNuetral)
        {
            retval = 0;
        }
        else if (gearCurrent == gear1)
        {
            retval = 1;
        }
        else if (gearCurrent == gear2)
        {
            retval = 2;
        }
        else if (gearCurrent == gear3)
        {
            retval = 3;
        }
        else if (gearCurrent == gear4)
        {
            retval = 4;
        }
        else if (gearCurrent == gearReverse)
        {
            retval = 5;
        }

        return retval;
    }

    public void Wait(int n)
    {
        StartCoroutine(waitForSeconds(n));
    }



    IEnumerator waitForSeconds(int n)
    {
        yield return new WaitForSeconds(n);
    }

   
}
