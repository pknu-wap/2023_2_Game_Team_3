using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent StunEvent;
    public Transform playerTransform;
    public Rigidbody2D enemyRig2d;
    public SpriteRenderer spriteRenderer;

    public float moveSpeed = 3f;
    public float rotateSpeed = 10f;
    public float dashPower = 2f;
    public float dashTime = 1f;
    public float dashCooltime = 3f;
    public float stunTime = 3f;
    public bool isDashing = false;
    public bool isStunned = false;

    private Coroutine dashCoroutine;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        enemyRig2d = GetComponent<Rigidbody2D>();
        StunEvent.AddListener(Stun);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ���� ���� ������ ��� ��� ����
        if (collision.gameObject.CompareTag("Player"))
        {
            dashCoroutine = StartCoroutine(NormalMode());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // ���� ������ �� �ƹ� �ϵ� ���� �ʴ´�.
        if(isStunned)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // ��� ���̸� Dash ����
            if (isDashing)
            {
                Dash();
            }

            // ��� ���� �ƴ϶�� �÷��̾� ����
            else
            {
                LookAtPlayer();
                Move();
                BlockFlipedSprite();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �÷��̾ ������ ����� �ڷ�ƾ ����
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(dashCoroutine);
        }
    }

    public void Stun()
    {
        // ��� ��峪 ��� ��尡 ���� ���̶�� �����Ѵ�.
        if(dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }

        dashCoroutine = StartCoroutine(Stunned());
    }

    private IEnumerator Stunned()
    {
        isStunned = true;
        yield return new WaitForSecondsRealtime(stunTime);
        isStunned = false;
        // ������ ������ ��� ���� ����
        dashCoroutine = StartCoroutine(NormalMode());
    }

    // �÷��̾ �ٶ󺻴�.
    public void LookAtPlayer()
    {
        // https://unitybeginner.tistory.com/50 ���� ������
        Vector2 direction = new(
            transform.position.x - playerTransform.position.x,
            transform.position.y - playerTransform.position.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    private void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.down, Space.Self);
    }

    // ������Ʈ�� ���������� ������ �����Ѵ�.
    private void BlockFlipedSprite()
    {
        if (transform.rotation.z > 0) { spriteRenderer.flipY = true; }
        else { spriteRenderer.flipY = false; }
    }

    public void Dash()
    {
        transform.Translate(dashPower * moveSpeed * Time.deltaTime * Vector2.down, Space.Self);
    }

    private IEnumerator NormalMode()
    {
        isDashing = false;
        yield return new WaitForSecondsRealtime(dashCooltime);
        isDashing = true;
        // ��� ��尡 ������ ��� ���� ����Ī
        dashCoroutine = StartCoroutine(DashMode());
    }

    private IEnumerator DashMode()
    {
        isDashing = true;
        yield return new WaitForSecondsRealtime(dashTime);
        isDashing = false;
        // ��� ��尡 ������ ��� ���� ����Ī
        dashCoroutine = StartCoroutine(NormalMode());
    }
}