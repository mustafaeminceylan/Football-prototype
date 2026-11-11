using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpheight = 2f;
    public float runSpeed = 15f;

    private Vector3 velocity;
    private float Speed;

    private CharacterController characterController;
    private bool isGrounded;
    public float stamina = 100f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 1f)
        {
            Speed = runSpeed;
            stamina -= 10f * Time.deltaTime;
        }
        else
        {
            Speed = moveSpeed;
        }



        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * Speed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded && stamina >= 20f)
        {
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpheight);
            stamina -= 20f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (stamina < 100f)
        {
            stamina += 0.5f * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0f, 100f);
    }
}
