using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// ���� �μӵ��� ����� �Լ�, event�� ���� �� ���� ���� ����
public class bombWall : MonoBehaviour
{
    Rigidbody2D rb;
    CompositeCollider2D col;
    Renderer wallRenderer;
    public UnityEvent onBombed;
    public float power;

    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CompositeCollider2D>();
        wallRenderer = GetComponent<Renderer>();

        onBombed.AddListener(wallBreakDown);
    }

    // bombWall.cs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ź ������ �浹�� ���
        if (collision.gameObject.CompareTag("Bomb"))
        {
            // ���ư� ���� ���� -> ��ź�� ���ƿ� �ݴ� ����
            direction = transform.position - collision.transform.position;
            direction.Normalize();
            wallBreakDown();
        }
    }

    public void wallBreakDown()
    {
        int tmp = Random.Range(-90, 90);
        Quaternion targetRotation = Quaternion.Euler(0, 0, tmp);
        StartCoroutine(BreakObject(targetRotation));
    }

    private IEnumerator BreakObject(Quaternion targetRotation)
    {
        col.isTrigger = true;   // ������ �� �÷��̾�� ������ �÷��̾ ���ư�.
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(direction * power);

        for (int i = 0; i < 100; i++)
        {
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, 0.02f);
            yield return null;
        }

        StartCoroutine(wallFadeOut());
    }

    private IEnumerator wallFadeOut()
    {
        Color color = wallRenderer.material.color;

        while (color.a > 0)
        {
            color.a -= 0.5f * Time.deltaTime;
            wallRenderer.material.color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}
