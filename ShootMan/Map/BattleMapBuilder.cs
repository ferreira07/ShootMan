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
        public BattleMapBuilder()
        {
            Map = new BattleMap();
        }
        public void AddCharacter(ECharacterType characterType, IController controller)
        {
            Map.Add(CharacterFactory.CreateCharacter(characterType, new Vector2(100 + 100* Map.Characters.Count, 150), controller));
        }
        public BattleMap BuildMap()
        {
            Sprite s = new Sprite() { Texture = ShootMan.textureRED, SourceRectangle = new Rectangle(0, 0, 1, 1) };
            Sprite s1 = new Sprite() { Texture = ShootMan.textureBLUE, SourceRectangle = new Rectangle(0, 0, 1, 1) };
            int m = 32;
            for (int i = 0; i < 25; i++)
            {
                Map.Add(new Wall(s, new Rectangle(i * m, 92, 1 * m, 1 * m)));
                Map.Add(new Wall(s, new Rectangle(i * m, 92 + 15 * m, 1 * m, 1 * m)));
            }
            for (int i = 0; i < 14; i++)
            {
                Map.Add(new Wall(s1, new Rectangle(0, 124 + i * m, 1 * m, 1 * m)));
                Map.Add(new Wall(s1, new Rectangle(24 * m, 124 + i * m, 1 * m, 1 * m)));
            }
            return Map;
        }
    }
}
