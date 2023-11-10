using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig2d;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public UnityEvent gameOver;
    
    public bool isSwimming = true;
    public bool isWalking = false;
    public float speed = 30f; 
    public float gravityScale = 2f;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    public bool isMovingUp = false;
    public bool isMovingDown = false;

    public Item equipedItem = null;    // ���� ���� ���� ������
    public Item currentFocusedItem = null; // ���� �ָ� ���� ������ (��ó�� �ٰ��� ������)
    public Collider2D currentCollision = null;



    // Start is called before the first frame update
    void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("isWalking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isWalking", true);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // �����ۿ� ���� �� �ش� �������� ĳ���صд�.
        if (collision.gameObject.CompareTag("Item"))
        {
            if(collision == currentCollision)
            {
                // �̹� ����� �ݸ����̶�� ����
                return;
            }

            // ������ ���
            currentFocusedItem = collision.GetComponent<Item>();
            currentCollision = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isWalking", false);
        }

        // �����ۿ��� ����� �ָ� ���� �������� ����.
        if (collision.gameObject.CompareTag("Item"))
        {
            // ��ϵ� �������� ��� �� �ƴ϶��
            if (collision != currentCollision)
            {
                // ����� �ʴ´�.
                return;
            }

            currentFocusedItem = null;
        }
    }



    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rig2d.AddForce(new Vector2(horizontal * speed, vertical * speed), ForceMode2D.Force);

        /*isMovingLeft = rig2d.velocity.normalized.x < -0.15f;
        isMovingRight = rig2d.velocity.normalized.x > 0.15f;
        isMovingUp = rig2d.velocity.normalized.y > 0.15f;
        isMovingDown = rig2d.velocity.normalized.y < -0.15f;*/

        isMovingLeft = horizontal < -0.15f;
        isMovingRight = horizontal > 0.15f;
        isMovingUp = vertical > 0.15f;
        isMovingDown = vertical < -0.15f;

        if (animator.GetBool("isWalking"))
        {
            animator.SetBool("isMovingLeft", isMovingLeft);
            animator.SetBool("isMovingRight", isMovingRight);
            animator.SetBool("isMoving", isMovingLeft || isMovingRight);
            rig2d.gravityScale = gravityScale;

            if (isMovingRight) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }
        }
        else
        {
            animator.SetBool("isMovingLeft", isMovingLeft);
            animator.SetBool("isMovingRight", isMovingRight);
            animator.SetBool("isMovingUp", isMovingUp);
            animator.SetBool("isMovingDown", isMovingDown);
            animator.SetBool("isMoving", isMovingLeft || isMovingRight || isMovingUp || isMovingDown);
            rig2d.gravityScale = 0;

            if (isMovingRight) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }

            if (!isMovingLeft && !isMovingRight && isMovingDown) { spriteRenderer.flipY = true; }
            else { spriteRenderer.flipY = false; }
        }
    }

    private void Update()
    {
        // ���ͷ��� Ű �Է� (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetItem();
        }
    }

    /// <summary>
    /// currentFocusedItem�� ȹ���Ѵ�.
    /// </summary>
    private void GetItem()
    {
        if (currentFocusedItem == null)
        {
            return;
        }

        // ������ ȹ��
        currentFocusedItem.GetItem();
        Debug.Log("EŰ �Է�");
    }
}
