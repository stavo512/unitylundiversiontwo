using UnityEngine;

/*
MovementIntent2D

Stores "what the controller wants to do"

Allows movement motors to stay reusable across Player/NPC/etc.
AI can replace this component
*/

public class MovementIntent2D : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool JumpPressed { get; private set; }



    void Update()
    {
        if (InputReader.Instance == null)
            return;

        MoveInput = InputReader.Instance.Move;

        JumpPressed = InputReader.Instance.JumpPressed;

        AttackPressed = InputReader.Instance.AttackPressed;
    }

    
}
