/// <summary>
/// When level up , the point will be manually added to the character
/// </summary>
public interface ISelectPoint
{
    void SelectPoint(string name, int point);
    void ResetPoint(string name);
}