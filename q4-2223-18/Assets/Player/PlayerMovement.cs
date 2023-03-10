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
    private dir direction; 
    private Ray2D debugRay;
    private void Start()
    {
        debugRay = new Ray2D(transform.position, transform.up);
        direction = dir.none; 
    }
    private void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            Debug.Log("Up"); 
            up = true;
            down = false;
            direction = dir.up;
            transform.rotation = Quaternion.Euler(0f, 0f, 90); 
        }
        else if (Input.GetKeyDown(downKey))
        {
            Debug.Log("down"); 
            down = true;
            up = false;
            direction = dir.down;
            transform.rotation = Quaternion.Euler(0f, 0f, -90);
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
            direction = dir.left;
            transform.rotation = Quaternion.Euler(0f, 0f, 180);
        }
        else if (Input.GetKeyDown(rightKey)) 
        {
            right = true;
            left = false;
            direction = dir.right;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            left = false;
            right = false;
        }
        if(!up && !down && !left && !right)
        {
            direction = dir.none; 
        }
        debugRay.origin = transform.position; 
        Debug.DrawRay(debugRay.origin, debugRay.direction * moveIncrement, Color.red);
        if (checkForValidPath(direction))
        {
            movePlayer(up, down, left, right);
        }
        

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
                debugRay.direction = transform.right; 
                return !Physics2D.Raycast(transform.position,debugRay.direction, moveIncrement,LayerMask.GetMask("Collideable")) || !Physics2D.Raycast(transform.position, debugRay.direction, moveIncrement, LayerMask.GetMask("Enemy"));
            case dir.down:
                debugRay.direction = transform.right;
                return !Physics2D.Raycast(transform.position, debugRay.direction, moveIncrement, LayerMask.GetMask("Collideable")) || !Physics2D.Raycast(transform.position, debugRay.direction, moveIncrement, LayerMask.GetMask("Enemy"));                //debugRay.direction = new Vector2(transform.position.x, transform.position.y - moveIncrement);
            case dir.left:
                debugRay.direction = transform.right;
                return !Physics2D.Raycast(transform.position, debugRay.direction, moveIncrement, LayerMask.GetMask("Collideable")) || !Physics2D.Raycast(transform.position, debugRay.direction, moveIncrement, LayerMask.GetMask("Enemy"));
            case dir.right:
                debugRay.direction = transform.right;
                return !Physics2D.Raycast(transform.position, debugRay.direction, moveIncrement, LayerMask.GetMask("Collideable")) || !Physics2D.Raycast(transform.position, debugRay.direction, moveIncrement, LayerMask.GetMask("Enemy"));

        }
        return true; 
    }
    enum dir
    {
        up,
        down,
        left,
        right,
        none 
    }
}
