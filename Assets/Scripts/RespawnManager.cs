using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] Transform startPosition; 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnBlock();
        }
    }

    void RespawnBlock()
    {
        Instantiate(blockPrefab, startPosition.position, startPosition.rotation);
    }
}
