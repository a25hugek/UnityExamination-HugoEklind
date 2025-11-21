using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 350f;
    [SerializeField] private float jumpForce = 700f;
    
    private Rigidbody2D rgbd;
    private SpriteRenderer sprend;
    private Animator anim;
    
    private bool isGrounded;
    
    private float horizontalValue;
    private float verticalValue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        sprend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        
        if(Input.GetKeyDown(KeyCode.Z) && isGrounded == true) {
            Jump();
        }
        
        if(horizontalValue < 0) {
            FlipSprite(true);
        }
        if(horizontalValue > 0) {
            FlipSprite(false);
        }

        if (horizontalValue != 0) {
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }
    }
    
    private void FixedUpdate() {

        rgbd.linearVelocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.linearVelocityY);
        horizontalValue = 0;
        
    }
    
    private void FlipSprite(bool direction) {
        sprend.flipX = direction;
    }
    
    private void Jump() {
        rgbd.AddForce(new Vector2(0, jumpForce));
    }
    
}
