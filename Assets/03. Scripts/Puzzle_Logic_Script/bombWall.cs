using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

// ���� �μӵ��� ����� �Լ�, event�� ���� �� ���� ���� ����
public class bombWall : MonoBehaviour
{
    Rigidbody2D rb;
    Color color;
    Renderer wallRenderer;
    public UnityEvent onBombed;
    public float power;

    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        color = GetComponent<bombWall>().color;
        wallRenderer = GetComponent<Renderer>();

        onBombed.AddListener(wallBreakDown);
    }

    // OnCollisionEnter2D ���� �Լ��� �̿��Ͽ� ���� ��ź �������� ������ wallBreakDown�Լ� �ߵ��ϴ� ���� �߰� ����

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

    public void wallBreakDown()
    {
        int tmp = Random.Range(-90, 90);
        Quaternion targetRotation = Quaternion.Euler(0,0,tmp);
        StartCoroutine(BreakObject(targetRotation));
    }

    private IEnumerator BreakObject(Quaternion targetRotation)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(direction * power);

        for (int i = 0; i < 100; i++)
        {
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, 0.02f);
            yield return null;
        }

        StartCoroutine(wallFadeOut());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            direction = transform.position - collision.transform.position;
            direction.Normalize();
            wallBreakDown();
        }
    }
}
