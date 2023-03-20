public interface IAreaTeamEffect : IAreaEffect, ITeamEffect
{
    bool IsPassive { get; set; }
}