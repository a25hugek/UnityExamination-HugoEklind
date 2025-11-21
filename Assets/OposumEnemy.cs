using UnityEngine;

public class OposumEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500f;
    
    private Rigidbody2D rgbd;
    private SpriteRenderer sprend;
    private Animator anim;
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
        if (sprend.flipX == true)
        {
            rgbd.linearVelocity = new Vector2(moveSpeed * Time.deltaTime, rgbd.linearVelocityY);
        }
        else
        {
            rgbd.linearVelocity = new Vector2(-moveSpeed * Time.deltaTime, rgbd.linearVelocityY);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PatrolBox"))
        {
            sprend.flipX = !sprend.flipX;
        }
    }
}
