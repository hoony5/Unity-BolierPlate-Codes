/// <summary>
/// if character is on the Area, the effect will be applied. 
/// </summary>
public interface IAreaEffect : IEffect
{
    void UpdateEffect(Character character, Character other,int areaMask);
}