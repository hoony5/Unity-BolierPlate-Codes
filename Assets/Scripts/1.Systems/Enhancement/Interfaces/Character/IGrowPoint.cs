/// <summary>
/// When Level up , the point will be automatically added to the character
/// </summary>
public interface IGrowPoint
{
    void UpdatePoint(string name, int point);        
    void ResetPoint(string name);
}