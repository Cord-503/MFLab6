using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockPhysics : MonoBehaviour
{
    bool simulate = false;
    [SerializeField] GameObject platform;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform startPosition;

    [SerializeField] float mass = 1f;
    [SerializeField] float friction = 0.1f;
    [SerializeField] float gravity = -9.8f;
    float angle = 0;

    Vector2 gravitationalForce;
    Vector2 normalForce;
    Vector2 frictionForce;
    Vector2 netForce;

    bool onPlatform = false;

    void Start()
    {
        ResetBlock();
    }

    void Update()
    {
        if (simulate)
        {
            CalculateForces();
            rb.AddForce(netForce);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            simulate = true;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Gravitational Force: " + gravitationalForce);
            Debug.Log("Normal Force: " + normalForce);
            Debug.Log("Friction Force: " + frictionForce);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            angle = collision.transform.eulerAngles.z * Mathf.Deg2Rad;
            onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onPlatform = false;
        }
    }

    void CalculateForces()
    {
        if (onPlatform)
        {
            // Gravitational force parallel to the plane
            gravitationalForce = new Vector2(mass * gravity * Mathf.Sin(angle), 0);

            // Normal force perpendicular to the plane
            normalForce = new Vector2(0, mass * -gravity * Mathf.Cos(angle));

            // Frictional force opposing the direction of motion
            frictionForce = -gravitationalForce.normalized * friction * normalForce.magnitude;

            // Net force is the combination of gravitational, normal, and frictional forces
            netForce = gravitationalForce + frictionForce;
        }
        else
        {
            gravitationalForce = new Vector2(0, mass * gravity);
            frictionForce = Vector2.zero;
            netForce = gravitationalForce;
        }
    }

    void ResetBlock()
    {
        simulate = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
        netForce = Vector2.zero;
    }
}
