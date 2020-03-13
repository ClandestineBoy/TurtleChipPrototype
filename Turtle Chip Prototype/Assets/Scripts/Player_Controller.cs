using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public KeyCode left, right, jump;
    public float spd, jumpForce, gravity, heldGravity, maxJumps;
    private float curSpd;
    private float moveDir, jumps;
    private Rigidbody rb;
    public bool canJump;
    public GameObject cam;
    public List<GameObject> platforms;
    public GameObject firstPlatform;
    public GameObject middlePlatform;
    public GameObject lastPlatform;
    void Start()
    {
        curSpd = spd;
        jumps = 0;
        canJump = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cam.transform.position.x < transform.position.x)
        {
            cam.transform.position += new Vector3(transform.position.x - cam.transform.position.x, 0, 0);
        }
            moveDir = 1;
        curSpd = spd;
            if (Input.GetKey(left))
            {
                curSpd = spd - 4;
            }
            if (Input.GetKey(right))
            {
                curSpd = spd + 4;
            }
            if (Input.GetKeyDown(jump) && canJump)
            {
                jumps += 1;
                if (jumps >= maxJumps)
                {
                    canJump = false;
                    jumps = 0;
                }
                rb.velocity = Vector2.up * jumpForce;
            }
            if (Input.GetKey(jump) && rb.velocity.y > 0)
            {
                rb.velocity += Vector3.down * heldGravity;
            }
            else
            {
                rb.velocity += Vector3.down * gravity;
            }
            rb.velocity = new Vector2(moveDir * curSpd , rb.velocity.y);

        if ((transform.position.x >= middlePlatform.transform.position.x))
        {
            makePlatform();
        }
    }

    public void makePlatform()
    {

        Destroy(firstPlatform);
        firstPlatform = middlePlatform;
        middlePlatform = lastPlatform;
        lastPlatform = Instantiate(platforms[Mathf.FloorToInt(Random.Range(0, 4))], new Vector3(middlePlatform.transform.position.x + 10, -4.42f, transform.position.z), Quaternion.identity);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            jumps = 0;
        }
    }
}
