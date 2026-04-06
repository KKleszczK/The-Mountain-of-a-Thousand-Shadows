using UnityEngine;

public interface IInputSource
{
    GameInputs Read(float now, bool snap, float hDead, float vDead);
}

[System.Serializable]
public struct GameInputs
{
    public bool JumpDown;
    public bool JumpHeld;
    public bool CrouchDown;
    public bool CrouchHeld;
    public Vector2 Move;
}
