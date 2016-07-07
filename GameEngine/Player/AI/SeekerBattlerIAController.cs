using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Player.AI
{
    public class SeekerBattlerIAController : BattlerAIController
    {
        public float DangerDistance = 150;

        protected override void _UpdateState()
        {
            if (Character != null)
            {
                UpdateTarget();

                if (Target != null && !Target.IsDead)
                {
                    Dir = (Target.ColisionRectangle.Center.ToVector2() - Character.ColisionRectangle.Center.ToVector2()) * new Vector2(1, -1);
                    float distance = Dir.Length();
                    Dir = Dir * (1 / distance);
                    if (distance <= DangerDistance)
                        Dir = Dir * 0.1f;

                    if (distance <= DangerDistance || //Se estiver próximo, atire a vontade
                        Character.Mp == Character.MaxMp) //Se estiver com o mana cheio pode atirar
                        this.Actions[EControllerAction.Release1] = true;
                    else this.Actions[EControllerAction.Release1] = false;
                }
            }
        }

        private Character Target;

        private void UpdateTarget()
        {
            if (Target == null || Target.IsDead)
            {
                //NearTarget();
                RandomTarget();
            }
        }

        private void NearTarget()
        {
            float distance = float.MaxValue;
            foreach (var item in Character.Map.Characters.Where(c => c != Character && !c.IsDead))
            {
                float d = Vector2.Distance(item.ColisionRectangle.Center.ToVector2(), Character.ColisionRectangle.Center.ToVector2());
                if (d < distance)
                {
                    d = distance;
                    Target = item;
                }
            }
        }

        static Random Random = new Random();
        private void RandomTarget()
        {
            Character[] chars = Character.Map.Characters.Where(c => c != Character && !c.IsDead).ToArray();
            if (chars.Count() > 0)
                Target = chars[Random.Next(chars.Count())];
        }
    }
}
