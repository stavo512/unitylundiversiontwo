using UnityEngine;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(MovementIntent2D))]
public class PlayerShootController : MonoBehaviour
{
    Shooter shooter;
    MovementIntent2D intent;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        intent = GetComponent<MovementIntent2D>();
    }

    void Update()
    {
        if (intent.AttackPressed)
        {
            shooter.Shoot();
        }
    }
}
