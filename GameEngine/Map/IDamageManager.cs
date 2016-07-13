namespace GameEngine.Map
{
    public interface IDamageManager
    {
        void DoDamage(IAttackContainer attackContainer, IDefensesContainer defensesContainer);
    }
}