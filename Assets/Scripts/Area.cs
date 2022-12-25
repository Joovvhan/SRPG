using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    private Transform character;
    private Player player;
    private PolygonCollider2D polyCollider;

    // Start is called before the first frame update
    void Start()
    {
        character = transform.parent;
        player = character.GetComponent<Player>();
        polyCollider = GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        //Debug.Log("Mouse Stay on Area");
    }

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(worldPosition);

        if (player.selected & !player.moved)
        {
            player.StartMoving(worldPosition);
            //StartCoroutine(player.Move(worldPosition));
            //Debug.Log("Coroutine Started");
        }
    }

    public void PolyMesh(List<Vector3> verticiesList)
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.color = new Color(0.0f, 1.0f, 1.0f, 0.1f);

        Vector3[] verticies = verticiesList.ToArray();
        int n = verticies.Length;

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (n - 2); i++)
        {
            trianglesList.Add(0);
            //trianglesList.Add(i);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }

        trianglesList.Add(0);
        trianglesList.Add(1);
        trianglesList.Add(n - 1);

        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < verticies.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.normals = normals;

        //polyCollider
        polyCollider.pathCount = 1;

        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < n; i++)
        {
            pathList.Add(new Vector2(verticies[i].x, verticies[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);
    }
}
