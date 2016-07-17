namespace GameEngine.Combat
{
    public interface IDamageManager
    {
        void DoAttack(Attack attack, IDefensesContainer defensesContainer);
    }
}