using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlatform : MonoBehaviour
{
    public Text uiText;
    float rotation = 0;

    private void Start()
    {
        rotation = transform.rotation.z;
        UpdateUIText();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rotation--;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotation++;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotation);
        UpdateUIText(); 
    }

    void UpdateUIText()
    {
        uiText.text = "Press W key to move the platform upward\nPress the S key to move the platform downward";
    }
}
