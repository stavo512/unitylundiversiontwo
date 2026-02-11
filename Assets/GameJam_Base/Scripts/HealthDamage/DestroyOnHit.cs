using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public LayerMask destroyOnLayers;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((destroyOnLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }
    }
}
