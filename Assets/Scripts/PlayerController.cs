using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// I want the player to be ablte to move left and right with diagonals
// To be able to jump when at a certain level or upgrade.
// button to shoot machine gun or other weapons. 
public class PlayerController : MonoBehaviour
{
    public float speed = 15.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed. (Smoother Movement.)
    private bool isOnGround = true;
    public float jumpForce = 15.0f;
    public int collectableCount = 0;
    public int ammo = 0;
    public float fireRate = 0.75f;
    public float bulletSpeed = 30.0f;
    public float health = 10.0f;
    public float money = 0.0f;

    private Rigidbody rb; // Reference to player's Rigidbody.
    public GameObject projectilePrefab;
    public Camera playerCamera;

    // Start is called before the first frame update
    private void Start()

    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnGround == true && Input.GetButtonDown("Jump")) //If on ground (Has tag of ground)
            // You can jump by pressing space bar. 
        {
            //rb is the rigidbody of player, Vector3.up applys force in the y axis/direction(0,1,0)
            //This ForceMode.Impulse, means it will apply immediate force to the rigid body. 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;//(this ensures you cannot just jump forever/ jump in mid air.)
        }
        //if you get hit too often by ennemies, and health gets to zero, destroy player. 
        Shooting();
    }


    //This is called at fixed intervals
    //Is better for smoother movement and physics calculations
    //So it doesnt run with the frame rate but syncs with the physics engine. 
    private void FixedUpdate()
    {
        // Move player forward based on vertical input 1 is W or up arrow (move forward)
        // -1 is down or S which is move backward.
        float moveVertical = Input.GetAxis("Vertical");
        //transform.forward gives a forward vector of Player.
        //moveVertical: Movement SCALES by input determining if move forward or backwards
        //Time.fixedDeltaTime: This ensures movement is framerate INDEPENDENT. keeps movement smooth. 
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        //Moves the rigidbody(player), to new position smoothly. 
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input (A D)
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        //Quaternion: This creates rotation around the Y axis.
        //turnRotation: is a quaterion. It represents rotation in Unity, So how much the player should rotate that frame or time. 
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //Applys the rotation to the Rigidbody (rb.rotation)
        rb.MoveRotation(rb.rotation * turnRotation);
    }


    //when the player collids with the ground, he will be able to jump again
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isOnGround = true;
        }

        // If player collides with an enemy, lose 2 health
        if (collision.gameObject.tag == "ennemy")
        {
            health -= 2;

            // Check if health is zero or below to destroy the player
            if (health <= 0)
            {
                Die();
            }
        }
    }


    // Method to handle player death
    private void Die()
    {
        //This will destroy the player
        // This is its own method so that you can add effects or animations for death. 
        Destroy(gameObject);
    }


    //detect when player hits a collectable object
    private void OnTriggerEnter(Collider other)
    {
        //keeps count/ adds collectables you encounter.

        if (other.gameObject.CompareTag("Collectable"))
        {
            collectableCount++;
            ammo = collectableCount;
            //I want to make the player be considerate with ammo, as it is money as well.
            //Eventually be able to buy upgrades like armor, health, damage, attack speed and so forth. 
            money = collectableCount; 
        }
    }


    private void Shooting()
    {
        // Check if left mouse button is pressed and ammo is available
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            Shoot();

            // Create a ray from the camera to the mouse position
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 targetPoint;

            // If the ray hits an object, set the target point to the hit point
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(100); // Set a distant target point if no object is hit
            }

            // Calculate the direction from the player to the target point
            Vector3 direction = (targetPoint - transform.position).normalized;

            // Instantiate the bullet at the player's position and set its direction
            GameObject bullet = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.LookRotation(direction));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // Set the bullet's velocity to move towards the target point
            if (bulletRb != null)
            {
                bulletRb.velocity = direction * bulletSpeed;
            }
        }
    }


    //Decrease Ammo and Collectablewhen shooting
    private void Shoot()
    {
        ammo--;
        collectableCount--;
    }

    // Handle physics-based movement and rotation.
}