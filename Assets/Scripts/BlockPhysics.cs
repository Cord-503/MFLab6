using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockPhysics : MonoBehaviour
{
    bool simulate = false;
    [SerializeField] Rigidbody2D rb;

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
            gravitationalForce = new Vector2(mass * gravity * Mathf.Sin(angle), 0);
            normalForce = new Vector2(0, mass * -gravity * Mathf.Cos(angle));
            frictionForce = -gravitationalForce.normalized * friction * normalForce.magnitude;
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
    }
}
