using Core.Scripts.Gravitation;

namespace Core.Scripts.Player.Gravitation
{
    public class PlayerGravitation : BaseGravitation
    {
        public PlayerGravitation(float inspectGravityValue, float additionalInspectGravityValue, float passiveStress)
            : base(inspectGravityValue, additionalInspectGravityValue, passiveStress) {  }
    }
}