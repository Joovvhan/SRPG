using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject area;
    public GameObject range;
    public bool selected;
    public float speed = 3;
    public float rollbackSpeed = 9;

    public bool moved = false;
    public bool attacked = false;

    public int hp;
    private int maxHp=10;
    private int atk=3;

    private Vector3 orgPos;

    void Start()
    {
        area.SetActive(false);
        range.SetActive(false);
        selected = false;
        attacked = false;
        hp = maxHp;
    }

    private void Update()
    {
        //if (moved & !attacked & !range.activeSelf)
        //{
        //    range.SetActive(true);
        //}

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //Debug.Log("Backspace key was pressed");
            if (selected & moved)
            {
                //StartCoroutine(UndoMove());
                StartUndoMoving();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selected)
            {
                Done();
            }
        }
    }

    void DrawMovableArea()
    {
        float angle = 3;

        Vector3 startPos = this.transform.position;

        Vector2[] vertices = new Vector2[(int)(360/angle)];

        int layerMask = ~LayerMask.GetMask("Player");

        for (int i = 0; i < 360 / angle; i += 1)
        {
            Vector3 normalVector = new Vector3(Mathf.Cos(angle * i * Mathf.Deg2Rad), Mathf.Sin(angle * i * Mathf.Deg2Rad), 0);
            Vector3 targetPos = startPos + 3f * normalVector;

            RaycastHit2D hit = Physics2D.Linecast(startPos, targetPos, layerMask);
            if (hit.collider != null)
            {
                vertices[i] = new Vector2(hit.point.x, hit.point.y);
            }
            else
            {
                vertices[i] = new Vector2(targetPos.x, targetPos.y);
            }
        }

        foreach (Vector2 vertex in vertices)
        {
            //Debug.Log(vertex);
            Debug.DrawRay(this.transform.position, new Vector3(vertex.x, vertex.y, 0) - this.transform.position, Color.red, 15);
        }

    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        if (!moved) {
            area.SetActive(true);
            DrawMovableArea();
        }
        //Debug.Log("Mouse Enter");
        //rend.material.color = Color.red;
    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseOver()
    {
        //Debug.Log("Mouse Stay");
        //rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    }

    void OnMouseDown()
    {
        selected = !selected;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        if (!selected)
        {
            area.SetActive(false);
            range.SetActive(false);
        }
        //Debug.Log("Mouse Exit");
        //rend.material.color = Color.white;
    }

    public void Done()
    {
        selected = false;
        moved = false;
        area.SetActive(false);
        range.SetActive(false);
    }

    public void StartMoving(Vector3 worldPosition)
    {
        StartCoroutine(Move(worldPosition));
    }

    public void StartUndoMoving()
    {
        StartCoroutine(UndoMove());
    }

    private IEnumerator Move(Vector3 pos)
    {
        //Debug.Log("IEnumerator");
        Vector3 org = transform.position;
        Vector3 des = new Vector3(pos.x, pos.y, transform.position.z);
        orgPos = org;

        float elapsedTime = 0;

        float dis = (org - des).magnitude;
        float time = dis / speed;

        area.SetActive(false);
        moved = true;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(org, des, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        range.SetActive(true);
    }

    private IEnumerator UndoMove()
    {
        Vector3 org = transform.position;
        Vector3 des = orgPos;
        float elapsedTime = 0;

        float dis = (org - des).magnitude;
        float time = dis / rollbackSpeed;

        range.SetActive(false);

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(org, des, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        area.SetActive(true);
        moved = false;
    }

}