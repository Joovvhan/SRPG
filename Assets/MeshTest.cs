using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    public PolygonCollider2D polyCollider;

    void Start()
    {

        float radius = 1f;
        //int n = 36;
        int n = 18;
        //verticies
        List<Vector3> verticiesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < n; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / n);
            verticiesList.Add(new Vector3(x, y, 0f));
        }

        polyCollider = GetComponent<PolygonCollider2D>();
        PolyMesh(verticiesList);
    }

    public void PolyMesh(List<Vector3> verticiesList)
    {
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
        trianglesList.Add(n-1);

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

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log("Debugging from test mesh: " + worldPosition.ToString());

    }
}



