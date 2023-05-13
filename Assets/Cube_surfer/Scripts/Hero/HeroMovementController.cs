using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMovementController : MonoBehaviour
{

    private float forwardMovementSpeed = 0;
    [SerializeField] private float horizontalMovementSpeed;
    [SerializeField] private float horizontalLimitValue;

    private float horizontalValue;

    public GameObject endedSound;
    private float newPositionX;
    private float newPositionZ;

    private float smoothing = 0.15f;
    private float sensitivity = 45f;
    private float temp = 0f;

    private Vector2 currentTouchPosition;
    private Vector2 previousTouchPosition;

    public Animator _hero;

    private Quaternion currentRotation;
    private Quaternion targetRotation;

    private bool direction = true;
    private bool turnChck = false;
    private GameManager _gameManager;
    private Vector3 previousPosition;
    private float turnConst = 1;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _hero = this.GetComponentInChildren<Animator>();
        previousPosition = transform.position;
    }
    void FixedUpdate()
    {
        HandleHeroHorizontalInput();
        SetHeroForwardMovement();
        rotationMove();
        if (direction)
        {
            SetHeroHorizontalMovement();
        }
        if (!direction)
        {
            SetHeroVerticalMovement();
        }
        Vector3 currentPosition = transform.position;
        Vector3 difference = currentPosition - previousPosition; // Farký hesapla
        previousPosition = currentPosition; // Geçerli pozisyonu kaydet

        if ((difference.z >-0.3f&& difference.z < -0.1f) || (difference.x > 0.1f && difference.x < 0.3f))
        {
            turnConst = -1;
        }
        if((difference.z < 0.3f && difference.z > 0.1f) || (difference.x > -0.1f && difference.x < -0.3f))
        {
            turnConst = 1;
        }
        Debug.Log(turnConst);    
        Debug.Log(direction);    
        Debug.Log(difference); // Farký konsolda göster
    }
    /*private void HandleHeroHorizontalInput()
    {
        if (Input.GetMouseButton(0))
        {
            horizontalValue = Input.GetAxis("Mouse X");
        }
        else
        {
            horizontalValue = 0;
        }
    }*/
    private void HandleHeroHorizontalInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _gameManager.startGame();
            forwardMovementSpeed = 10f;
            currentTouchPosition = Input.GetTouch(0).position;
            float touchDelta = (currentTouchPosition - previousTouchPosition).x * sensitivity / Screen.width;
            horizontalValue = Mathf.Lerp(horizontalValue, touchDelta, smoothing);
            previousTouchPosition = currentTouchPosition;
        }
        else
        {
            horizontalValue = 0;
        }
    }

    private void SetHeroForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.fixedDeltaTime);
    }

    private void SetHeroHorizontalMovement()
    {
        newPositionX = transform.position.x + horizontalValue *(turnConst) * horizontalMovementSpeed * Time.fixedDeltaTime;
        newPositionX = Mathf.Clamp(newPositionX, horizontalLimitValue-5, horizontalLimitValue+5);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }
    private void SetHeroVerticalMovement()
    {
        newPositionZ = transform.position.z + horizontalValue* (turnConst) * horizontalMovementSpeed * Time.fixedDeltaTime;
        newPositionZ = Mathf.Clamp(newPositionZ, horizontalLimitValue-5, horizontalLimitValue+5);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPositionZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            endedSound.SetActive(true);
            forwardMovementSpeed = 0;
            _hero.SetTrigger("death");
            _gameManager.rePlay();
        }
        if (other.gameObject.tag == "right")
        {
            //FindObjectOfType<CameraFollowController>().finishCameraMovement();
            turnRight();
        }
        if (other.gameObject.tag == "left")
        {
            //FindObjectOfType<CameraFollowController>().finishCameraMovement();
            turnLeft();
        }
        if (other.gameObject.tag == "road")
        {
            roadFunc(other.gameObject);
        }
        if(other.gameObject.tag == "finish")
        {
            _gameManager.nextLevel();
        }
    }
    void rotationMove()
    {
        currentRotation = transform.rotation;
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, 2f * Time.fixedDeltaTime);
        //Debug.Log(targetRotation);
    }

    void ResetTurnChck()
    {
        turnChck = false;
    }

    public void turnRight()
    {
        if (!turnChck)
        {
            turnAnyWhere(90f);
        }
    }

    public void turnLeft()
    {
        if (!turnChck)
        {
            turnAnyWhere(-90f);
        }
    }

    public void roadFunc(GameObject obj)
    {
        if (!direction)
        {
            horizontalLimitValue = obj.gameObject.transform.position.z;
        }
        if (direction)
        {
            horizontalLimitValue = obj.gameObject.transform.position.x;
        }
    }

    public void resetSpeed()
    {
        forwardMovementSpeed = 0;
    }

    void turnAnyWhere(float angle)
    {
        temp += angle;
        if (direction)
        {
            horizontalLimitValue = transform.position.z + 5f;
        }
        if (!direction)
        {
            horizontalLimitValue = transform.position.x- 5f;
        }
        targetRotation = Quaternion.Euler(0f, temp, 0f);
        Debug.Log(targetRotation);
        direction = !direction;
        turnChck = true;
        Invoke("ResetTurnChck", 1f);
    }
}
