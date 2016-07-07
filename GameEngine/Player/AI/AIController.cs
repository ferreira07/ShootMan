using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Player
{
    public abstract class AIController : IController
    {
        public AIController()
        {
            Actions = new Dictionary<EControllerAction, bool>();
            Dir = Vector2.Zero;
        }

        public EControllerType ControllerType
        {
            get
            {
                return EControllerType.ArtificialInteligence;
            }
        }

        public Dictionary<EControllerAction, bool> Actions { get; set; }
        protected Vector2 Dir { get; set; }

        public bool Action(EControllerAction type)
        {
            bool ret = false;
            Actions.TryGetValue(type, out ret);
            return ret;
        }

        public Vector2 Direction()
        {
            return Dir;
        }

        public void UpdateState()
        {
            _UpdateState();
        }
        protected abstract void _UpdateState();
    }
}
