using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 350f;
    [SerializeField] private float jumpForce = 700f;
    [SerializeField] private Transform lFoot, rFoot;
    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] private Image heart1, heart2, heart3;
    
    private Rigidbody2D rgbd;
    private SpriteRenderer sprend;
    private Animator anim;
    
    private bool isGrounded;
    private bool hasKnockback = false;
    private bool isDamaged = false;
    private bool canMove = true;
    
    private int maxHP = 3;
    private int currentHP;
    private int knockbackForce;
    
    private float horizontalValue;
    private float verticalValue;
    private float rayDistance = 0.05f;
    private float damagedTimer = 0;
    private float spriteFlash = 0;
    private float knockbackDelay = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        sprend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        currentHP = maxHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove == true) {
            horizontalValue = Input.GetAxis("Horizontal");
        }
        
        CheckGrounded();
        
        if((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("space")) && isGrounded == true && canMove == true) {
            Jump();
        }
        
        if(horizontalValue < 0) {
            FlipSprite(true);
        }
        if(horizontalValue > 0) {
            FlipSprite(false);
        }
        
        if(isDamaged == true) {
            damagedTimer += Time.deltaTime;
            spriteFlash += Time.deltaTime;
            if(spriteFlash >= 0.15 && sprend.enabled == true) {
                spriteFlash = 0;
                sprend.enabled = false;
            }
            else if(spriteFlash >= 0.15 && sprend.enabled == false) {
                spriteFlash = 0;
                sprend.enabled = true;
            }
            if(damagedTimer >= 3) {
                spriteFlash = 0;
                sprend.enabled = true;
                isDamaged = false;
                damagedTimer = 0;
            }
        }
        if(hasKnockback == true && isGrounded == false && canMove == false) {
            rgbd.AddForce(new Vector2(knockbackForce, 0));
            knockbackDelay += Time.deltaTime;
        }
        else if(hasKnockback == true && isGrounded == true && knockbackDelay >= 0.2) {
            hasKnockback = false;
            canMove = true;
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
    
    private void GameOver() {
        SceneManager.LoadScene(0);
    }
    
    public void TakeDamage(int damage, int direction) {
        if(isDamaged == false) {
            currentHP -= damage;

            if(direction == 1) {
                knockbackForce = 80;
            }
            else if(direction == -1) {
                knockbackForce = -80;
            }
            
            if(currentHP <= 0) {
                GameOver();
            }
            else if(currentHP > 0 && direction != 0) {
                rgbd.linearVelocity = Vector2.zero;
                hasKnockback = true;
                knockbackDelay = 0;
                canMove = false;
                canMove = false;
                isDamaged = true;
                rgbd.AddForce(new Vector2(0, 600));
                Invoke("CanMoveAgain", 1f);
            }
        }
    }
    
    private void CanMoveAgain() {
        canMove = true;
        hasKnockback = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
