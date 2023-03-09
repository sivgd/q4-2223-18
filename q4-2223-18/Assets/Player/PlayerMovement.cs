using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Preferences")]
    public KeyCode upKey;
    public KeyCode downKey; 
    public KeyCode leftKey;
    public KeyCode rightKey; 
    [Header("Movement Variables")]
    public int moveIncrement = 1;

    [Header("Debug")]
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool left;
    [SerializeField] bool right;
    ///Input
    private void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            Debug.Log("Up"); 
            up = true;
            down = false; 
        }
        else if (Input.GetKeyDown(downKey))
        {
            Debug.Log("down"); 
            down = true;
            up = false; 
        }
        else
        {
            up = false;
            down = false;
            Debug.Log("No y axis Movement"); 
        }
        if (Input.GetKeyDown(leftKey))
        {
            left = true;
            right = false; 
        }
        else if (Input.GetKeyDown(rightKey)) 
        {
            right = true;
            left = false; 
        }
        else
        {
            left = false;
            right = false; 
        }
        movePlayer(up, down, left, right);
    }
    private void movePlayer(bool up, bool down, bool left, bool right)
    {
        if (up)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveIncrement);
        }
        if (down)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - moveIncrement);
        }
        if (left)
        {
            transform.position = new Vector2(transform.position.x - moveIncrement, transform.position.y);
        }
        if (right)
        {
            transform.position = new Vector2(transform.position.x + moveIncrement, transform.position.y);
        }
    }
    bool checkForValidPath(dir direction)
    {
        switch (direction)
        {
            case dir.up:
                //RaycastHit2D[] results
                //TODO: raycast collision detection
                Physics2D.Raycast(transform.position, new Vector2(transform.position.x, transform.position.y + moveIncrement),moveIncrement); 
                break; 
        }
        return true; 
    }
    enum dir
    {
        up,
        down,
        left,
        right
    }
}
