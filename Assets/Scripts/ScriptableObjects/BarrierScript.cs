using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    private Collider2D barrierCollider;
    private SpriteRenderer barrierSpriteRenderer;

    void Awake()
    {
        barrierCollider = GetComponent<Collider2D>();
        barrierSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        SceneController.OnStageCleared += DisableBarrier;
    }
    void OnDisable()
    {
        SceneController.OnStageCleared -= DisableBarrier;
    }

    private void DisableBarrier()
    {
        barrierCollider.enabled = false;
        barrierSpriteRenderer.enabled = false;
        Debug.Log("Barrier disabled due to stage clear.");
    }
}
