public class PlayerGravitation : BaseGravitation
{
    public PlayerGravitation(float inspectGravityValue, float passiveStress)
        : base(inspectGravityValue, passiveStress) {  }

    public override float BaseGravitate(float velocity, bool isGrounded)
    {
        return base.BaseGravitate(velocity, isGrounded);
    }
}