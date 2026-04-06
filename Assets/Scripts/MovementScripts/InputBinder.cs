using UnityEngine;

public enum ControlScheme { WASD, IJKL }

public class InputBinder : MonoBehaviour
{
    [SerializeField] private ControlScheme scheme = ControlScheme.WASD;
    [SerializeField] private PlatformerMovement movement;

    void Awake()
    {
        if (movement == null) movement = GetComponent<PlatformerMovement>();
        var s = movement.GetStats();
        KeyCode left, right, jump, crouch;
        if (scheme == ControlScheme.WASD)
        {
            left = s.MoveLeftKey; right = s.MoveRightKey; jump = s.JumpKey; crouch = s.CrouchKey;
        }
        else
        {
            //IJKL
            left = KeyCode.J; right = KeyCode.L; jump = KeyCode.I; crouch = KeyCode.K;
        }
        movement.SetInputSource(new KeyboardInputSource(jump, crouch, left, right));
    }
}
