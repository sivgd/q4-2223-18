using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("External References")]
    public PersistantPartyData partyData; 
    [Header("Preferences")]
    public KeyCode upKey;
    public KeyCode downKey; 
    public KeyCode leftKey;
    public KeyCode rightKey;
    [Header("Movement Variables")]
    public float moveSpeed = 3f; 
   // public int moveIncrement = 1;

    
    [Header("Debug")]
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool left;
    [SerializeField] bool right;

    private Rigidbody2D rb; 
  /*  private dir direction; 
    private Ray2D debugRay;*/
    private void Start()
    {
        /* debugRay = new Ray2D(transform.position, transform.up);
         direction = dir.none; */
        transform.position = partyData.PlayerPosition; 
        rb = GetComponent<Rigidbody2D>(); 
    }
    private void Update()
    {
        if (Input.GetKey(upKey))
        {
            Debug.Log("Up"); 
            up = true;
            down = false;
            //direction = dir.up;
           transform.rotation = Quaternion.Euler(0f, 0f, 90); 
        }
        else if (Input.GetKey(downKey))
        {
            Debug.Log("down"); 
            down = true;
            up = false;
            //direction = dir.down;
            transform.rotation = Quaternion.Euler(0f, 0f, -90);
        }
        else
        {
            up = false;
            down = false;
            Debug.Log("No y axis Movement"); 
        }
        if (Input.GetKey(leftKey))
        {
            left = true;
            right = false;
           //direction = dir.left;
            transform.rotation = Quaternion.Euler(0f, 0f, 180);
        }
        else if (Input.GetKey(rightKey)) 
        {
            right = true;
            left = false;
            //direction = dir.right;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            left = false;
            right = false;
        }
        if(!up && !down && !left && !right)
        {
            //direction = dir.none; 
        }
       /* debugRay.origin = transform.position; 
        Debug.DrawRay(debugRay.origin, debugRay.direction * moveIncrement, Color.red);
        if (checkForValidPath(direction))
        {
            movePlayer(up, down, left, right);
        }*/
        

    }
    public void FixedUpdate()
    {
        movePlayer(up, down, left, right);
        //partyData.PlayerPosition = transform.position; 
    }
    private void movePlayer(bool up, bool down, bool left, bool right)
    {
        float dX = 0, dY = 0;
        //rb.velocity = new Vector2(0f, 0f); 
        if (up)
        {
            dY = moveSpeed * Time.fixedDeltaTime; 
        }
        if (down)
        {
            dY = -moveSpeed * Time.fixedDeltaTime;
            //transform.position = new Vector2(transform.position.x, transform.position.y - moveIncrement);
        }
        if (left)
        {
            dX = -moveSpeed * Time.fixedDeltaTime;
            //transform.position = new Vector2(transform.position.x - moveIncrement, transform.position.y);
        }
        if (right)
        {
            dX = moveSpeed * Time.fixedDeltaTime;
            //transform.position = new Vector2(transform.position.x + moveIncrement, transform.position.y);
        }
        rb.velocity = new Vector2(dX, dY); 
       
    }
   /* bool checkForValidPath(dir direction)
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
    }*/
    enum dir
    {
        up,
        down,
        left,
        right,
        none 
    }
}
