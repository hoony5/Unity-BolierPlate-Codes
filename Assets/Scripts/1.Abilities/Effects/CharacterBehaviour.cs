using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private BattleBehaviour battleBehaviour;
    [SerializeField] private InteractionBehaviourComponent interactionBehaviour;
    [SerializeField] private QuestBehaviourComponent questBehaviour;
    
    public BattleBehaviour BattleBehaviour => battleBehaviour;
    public InteractionBehaviourComponent InteractionBehaviour => interactionBehaviour;
    public QuestBehaviourComponent QuestBehaviour => questBehaviour;
}