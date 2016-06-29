using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShootMan.Colision;
using ShootMan.Draw;
using ShootMan.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootMan.Map
{
    public class BattleMapBuilder
    {
        private BattleMap Map;

        private const int m = 32;
        private const int sY = 60;
        private const int MaxX = 24;
        private const int MaxY = 16;

        private static Vector2[] Positions = new Vector2[] {
            new Vector2(2*m, sY + 2*m),
            new Vector2((MaxX-1)*m, sY + 2*m),
            new Vector2(2*m, sY + (MaxY-1)*m),
            new Vector2((MaxX-1)*m, sY + (MaxY-1)*m)
        };

        public BattleMapBuilder()
        {
            Map = new BattleMap();
        }

        public void AddCharacter(ECharacterType characterType, IController controller)
        {
            Map.Add(CharacterFactory.CreateCharacter(characterType, Positions[Map.Characters.Count], controller));
        }
        
        public void AddRandomBarrier(EBarrierType barrierType, int count)
        {
            Random r = new Random();
            for (int i = 0; i < count; i++)
            {
                int x = r.Next(MaxX - 1) + 1;
                int y = r.Next(MaxY - 1) + 1;
                if (x < 3 || x > MaxX - 3 &&
                    y < 3 || y > MaxY - 3) continue;
                Map.Add(BarrierFactory.CreateBarrier(barrierType, GetRectangle(x, y)));
            }
        }

        public BattleMap BuildMap()
        {
            Sprite s = new Sprite() { Texture = ShootMan.textureRED, SourceRectangle = new Rectangle(0, 0, 1, 1) };
            Sprite s1 = new Sprite() { Texture = ShootMan.textureBLUE, SourceRectangle = new Rectangle(0, 0, 1, 1) };
            for (int i = 0; i <= MaxX; i++)
            {
                Map.Add(new Wall(s, GetRectangle(i, 0)));
                Map.Add(new Wall(s, GetRectangle(i, MaxY)));
            }
            for (int i = 0; i <= MaxY; i++)
            {
                Map.Add(new Wall(s1, GetRectangle(0, i + 1)));
                Map.Add(new Wall(s1, GetRectangle(MaxX, i + 1)));
            }
            AddRandomBarrier(EBarrierType.BasicBarrier, 10);

            Map.SetTime(TimeSpan.FromMinutes(0.2));

            return Map;
        }

        private static Rectangle GetRectangle(int x, int y)
        {
            return new Rectangle(x * m, sY + y * m, 1 * m, 1 * m);
        }
    }
}
