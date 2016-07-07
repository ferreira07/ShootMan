using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player.AI
{
    public class RandomBattlerIAController: BattlerAIController
    {

        static Random Random = new Random();
        protected override void _UpdateState()
        {
            Vector2 temp = new Vector2((float)(Random.NextDouble() * 2) - 1, (float)(Random.NextDouble() * 2) - 1);
            Dir = temp * 0.4f;
            if (Dir != Vector2.Zero) Dir.Normalize();

            if (Random.NextDouble() > 0.8)
            {
                this.Actions[EControllerAction.Release1] = true;
            }
            else
            {
                this.Actions[EControllerAction.Release1] = false;
            }
        }
    }
}
