using UnityEngine;



public class FirstPersonCameraController : MonoBehaviour
{
    public float mouseSensitivity = 2.0f;
    public float moveSpeed = 5.0f;
    public float grivateSpeed = 1.0f;
    public Camera playerCamera;
    private float verticalRotation = 0.0f;
    private CharacterController characterController;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isGround;
    void Start()
    {


        characterController = GetComponent<CharacterController>();
        // 记录初始位置和旋转角度
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        //characterController.stepOffset = 0.3f;
        Debug.Log(initialPosition + " " + initialRotation);
    }


    private void OnDisable()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
    void LateUpdate()
    {
        RaycastHit hit;
        float raylingth = characterController.height * 0.5f-0.2f;
        isGround = Physics.Raycast(transform.position, Vector3.down, out hit, raylingth);
    
     /*   Debug.Log(isGround);*/
        // 检测是否按住右键
        bool isRightMouseButtonDown = Input.GetMouseButton(1);

        if (isRightMouseButtonDown)
        {
            // 视角旋转
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX);
            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        // 移动
        float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 speed =  transform.rotation * new Vector3(sideSpeed, 0, forwardSpeed);
        
        characterController.Move(speed * Time.deltaTime);
        if (!isGround)
        {
            characterController.Move(new Vector3(0, -grivateSpeed*Time.deltaTime, 0));
        }
        


    }
}