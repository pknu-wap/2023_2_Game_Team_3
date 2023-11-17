using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �μӵ��� ����� �Լ�, event�� ���� �� ���� ���� ����
public class bombWall : MonoBehaviour
{
    Rigidbody2D rb;
    Color color;
    Renderer wallRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        color = GetComponent<bombWall>().color;
        wallRenderer = GetComponent<Renderer>();
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

    public void wallBreakDown()
    {
        // ���� ���� ���������� �������� ���� �߰� ����
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(wallFadeOut());
    }
}
