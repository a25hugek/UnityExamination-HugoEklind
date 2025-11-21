using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 350f;
    [SerializeField] private float jumpForce = 700f;
    [SerializeField] private Transform lFoot, rFoot;
    [SerializeField] private LayerMask whatIsGround;
    
    private Rigidbody2D rgbd;
    private SpriteRenderer sprend;
    private Animator anim;
    
    private bool isGrounded;
    
    private float horizontalValue;
    private float verticalValue;
    private float rayDistance = 0.05f;
    
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
        
        CheckGrounded();
        
        if((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("space")) && isGrounded == true) {
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

    private void CheckGrounded()
    {
        RaycastHit2D lHit = Physics2D.Raycast(lFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rHit = Physics2D.Raycast(rFoot.position, Vector2.down, rayDistance, whatIsGround);
       
        //Debug.DrawRay(lFoot.position, Vector2.down * rayDistance, Color.blue, 0.05f);
        //Debug.DrawRay(rFoot.position, Vector2.down * rayDistance, Color.red, 0.05f);

        if (lHit.collider != null && lHit.collider.CompareTag("Ground") ||
            rHit.collider != null && rHit.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
