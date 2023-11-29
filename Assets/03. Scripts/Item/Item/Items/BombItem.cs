using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BombItem : Item
{
    private LineRenderer lineRenderer;
    private CircleCollider2D bombRange;

    private bool isHanded = false;
    private bool canAim = true;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public float distance = 10f;

    public bool isCrashed = false;

    public float speed = 10f;

    public override void ActivateItem()
    {
        isHanded = true;
    }

    public override void DeactivateItem()
    {
        isHanded = false;
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        bombRange = GetComponent<CircleCollider2D>();
        bombRange.enabled = false;

        EraseAimLine();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.gameObject.CompareTag("Ground")
            || collision.gameObject.CompareTag("Land")
            || collision.gameObject.CompareTag("Enemy"))
        {
            isCrashed = true;
        }
    }

    private void Update()
    {
        if(isHanded == false)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            canAim = true;
        }

        if(canAim == true)
        {
            AimBomb();

            if(Input.GetMouseButtonDown(1))
            {
                EraseAimLine();
                canAim = false;
            }

            else if(Input.GetMouseButtonDown(0))
            {
                EraseAimLine();
                canAim = false;
                StartCoroutine(ThrowBomb());
            }
        }
    }

    private void AimBomb()
    {
        GetLineTransform();
        DrawAimLine();
    }

    private IEnumerator ThrowBomb()
    {
        transform.SetParent(null);

        Vector2 direction = endPosition - startPosition;
        direction = direction.normalized * distance;


        while(isCrashed == false && Vector3.Magnitude(transform.position - startPosition) < distance)
        {
            transform.Translate(speed * Time.deltaTime * direction);
            yield return null;
        }

        StartCoroutine(Bomb());
    }

    private IEnumerator Bomb()
    {
        Debug.Log("Bomb!");
        bombRange.enabled = true;

        // �浹 ������ ���� 1������ ���
        yield return null;
        // TODO: ���� ����Ʈ
        Destroy(gameObject);
    }

    public void DrawAimLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    public void EraseAimLine()
    {
        lineRenderer.positionCount = 0;
    }

    // Start: �� ��ġ, End: ���콺 Ŀ�� ��ġ
    private void GetLineTransform()
    {
        startPosition = transform.position;
        endPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        endPosition.z = 0;
    }
}
