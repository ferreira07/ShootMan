namespace GameEngine.Combat
{
    public interface IDamageManager
    {
        void DoAttack(IAttackContainer attackContainer, IDefensesContainer defensesContainer);
    }
}