using UnityEngine;

public class KeyboardInputSource : IInputSource
{
    private readonly KeyCode _jump;
    private readonly KeyCode _crouch;
    private readonly KeyCode _left;
    private readonly KeyCode _right;

    public KeyboardInputSource(KeyCode jump, KeyCode crouch, KeyCode left, KeyCode right)
    {
        _jump = jump; _crouch = crouch; _left = left; _right = right;
    }

    public GameInputs Read(float now, bool snap, float hDead, float vDead)
    {
        var gi = new GameInputs
        {
            JumpDown = Input.GetKeyDown(_jump),
            JumpHeld = Input.GetKey(_jump),
            CrouchDown = Input.GetKeyDown(_crouch),
            CrouchHeld = Input.GetKey(_crouch),
            Move = new Vector2(
                (Input.GetKey(_left) ? -1 : (Input.GetKey(_right) ? 1 : 0)),
                (Input.GetKey(_jump) ? 1 : (Input.GetKey(_crouch) ? -1 : 0))
            )
        };

        if (snap)
        {
            gi.Move.x = Mathf.Abs(gi.Move.x) < hDead ? 0 : Mathf.Sign(gi.Move.x);
            gi.Move.y = Mathf.Abs(gi.Move.y) < vDead ? 0 : Mathf.Sign(gi.Move.y);
        }
        return gi;
    }
}
