using System;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool spiceTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player reached the finish point!");
            SceneController.instance.NextLevel(spiceTrigger);
        }
    }
}
