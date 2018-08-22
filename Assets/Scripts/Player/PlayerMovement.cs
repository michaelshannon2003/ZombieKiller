using UnityEngine;

namespace Complete
{
    public class PlayerMovement : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
        public float m_Speed = 3.0f;                 // How fast the tank moves forward and back.
        public float rotateSpeed = 3.0F;

        private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
        private string m_TurnAxisName;              // The name of the input axis for turning.
        private Rigidbody m_Rigidbody;              // Reference used to move the tank.      
       
        CharacterController controller;
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            // When the tank is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;
          
        }

        private void OnDisable()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;

        }

        private void Start()
        {
            // The axes names are based on player number.
            m_MovementAxisName = "Vertical" + m_PlayerNumber;
            m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        }

        private void FixedUpdate()
        {
            Turn();
            Move();
        }


        private void Move()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = m_Speed * Input.GetAxis(m_MovementAxisName);
            controller.SimpleMove(forward * curSpeed);
        }

        private void Turn()
        {
            //float turn = rotateSpeed * Input.GetAxis(m_TurnAxisName) * Time.deltaTime;
            // m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
            float turn = rotateSpeed * Input.GetAxis(m_TurnAxisName);
            transform.Rotate(0, turn, 0);
        }

    }
}

//public class ExampleClass : MonoBehaviour
//{
//    public float speed = 6.0F;
//    public float jumpSpeed = 8.0F;
//    public float gravity = 20.0F;
//    private Vector3 moveDirection = Vector3.zero;
//    void Update()
//    {
//        CharacterController controller = GetComponent<CharacterController>();
//        if (controller.isGrounded)
//        {
//            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//            moveDirection = transform.TransformDirection(moveDirection);
//            moveDirection *= speed;
//            if (Input.GetButton("Jump"))
//                moveDirection.y = jumpSpeed;

//        }
//        moveDirection.y -= gravity * Time.deltaTime;
//        controller.Move(moveDirection * Time.deltaTime);
//    }
//}