using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
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
        //Debug.Log(worldPosition);

        if (player.selected & !player.moved)
        {
            player.StartMoving(worldPosition);
            //StartCoroutine(player.Move(worldPosition));
            //Debug.Log("Coroutine Started");
        }
    }
}
