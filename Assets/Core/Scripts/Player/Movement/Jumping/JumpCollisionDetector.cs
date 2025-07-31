using System;

public class JumpCollisionDetector
{
    public event Action PlayerCollideWithSomethingFromAbove;

    public void DetectCollisionFromAbove(bool isGrounded, bool isThereSomethingAbove)
    {
        if (isGrounded == false && isThereSomethingAbove == true)
        {
            PlayerCollideWithSomethingFromAbove?.Invoke();
        }
    }
}