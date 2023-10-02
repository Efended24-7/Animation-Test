using UnityEngine;

public class Skeleton_Movement : MonoBehaviour{

    // This is a reference to the Rigidbody componenet called "rb"
    //public Rigidbody rb;

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    // Lock mouse cursor on start
    /*
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
*/
    // WASD movement and camera placement
    void Update() {

        // -1 to 1, meaning A key or left arrow and D key or right arrow
        float horizontal = Input.GetAxisRaw("Horizontal"); 

        // W and S key or Up and Down arrow
        float vertical = Input.GetAxisRaw("Vertical");

        // 0 for y axis
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;

        // check if we're moving in any direction
        if(direction.magnitude >= 0.1f){

            // provides the angle for 3rd-person view
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle,0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }

}
