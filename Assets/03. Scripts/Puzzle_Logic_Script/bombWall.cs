using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        wallBreakDown();
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
        int tmp = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0,0,tmp);
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(wallFadeOut());
    }
}
