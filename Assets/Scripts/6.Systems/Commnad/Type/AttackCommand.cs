public class AttackCommand : ICommand<float>
{
    private readonly Character _attacker;
    private readonly Character _defender;
    public AttackCommand(Character attacker, Character defender)
    {
        _attacker = attacker;
        _defender = defender;
    }
    public bool Excecute(float input)
    {
        ChangedValueStatus status = null;
        status.AddValue(-input);
        return true;
    }

    public bool Undo(float input)
    {
        throw new System.NotImplementedException();
    }
}
