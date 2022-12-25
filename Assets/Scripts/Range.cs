using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    private Transform character;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        character = transform.parent;
        player = character.GetComponent<Player>();
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
        Debug.Log(worldPosition);
    }
}
