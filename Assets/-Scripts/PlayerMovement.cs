using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    [Header("Movement")] 
    [SerializeField] private float speed;
    public float sprintTime;
    [SerializeField] private float sprintRechargeTime;
    [Header("Mouse Settings")]
    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private float cameraClampAngle = 80f;
    [Header("Physics")] 
    [SerializeField] private float gravitationalStrength;
    [SerializeField] private float sprintMultiplier;
    [Header("References")] 
    private Rigidbody rb;
    [SerializeField] private Transform camHolder;
    [SerializeField] private Transform camPoint;
    
    private Vector2 input;
    private Vector2 currentRotation;
    private bool isSprinting;
    [HideInInspector] public float sprintCounter;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        camHolder = Camera.main!.gameObject.transform;
        UIManager.pm = this;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb.useGravity = false;
    }
    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            isSprinting = false;
        }
        ApplyGravity();

        if (isSprinting){
            Sprint();
            sprintCounter -= Time.deltaTime;
            if (sprintCounter <= 0){
                isSprinting = false;
            }
        }
        else{
            if (sprintCounter < sprintTime)
                sprintCounter += Time.deltaTime * 1/sprintRechargeTime * sprintTime;
            Move();
        }
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -cameraClampAngle, cameraClampAngle);

        camHolder.position = camPoint.position;
        camHolder.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        transform.rotation = Quaternion.Euler(0, currentRotation.x, 0);
    }
    
    private void Move()
    {
        var forceY = transform.forward * input.y;
        var forceX = transform.right * input.x;
        var vel  = (forceX + forceY).normalized * (speed * 100 * Time.deltaTime);
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
    }
    private void Sprint()
    {
        var forceY = transform.forward * input.y;
        var forceX = transform.right * input.x;
        var vel  = (forceX + forceY).normalized * (speed * 100 * Time.deltaTime * sprintMultiplier);
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
    }

    private void ApplyGravity()
    {
        if (rb.velocity.y >= 0){
            rb.AddForce(Vector3.down * gravitationalStrength);
        }
        else{
            rb.AddForce(Vector3.down * (gravitationalStrength * 2));
        }
    }
}
