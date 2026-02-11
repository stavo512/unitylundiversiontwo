using UnityEngine;

public class CollectibleCounter : MonoBehaviour
{
    //to be attached to the collectibles (all of them)

    public static int Remaining { get; private set; }

void OnEnable()
    {
        Remaining++;
    }

    void OnDisable()
    {
        Remaining--;
    }

}
