using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private BattleBehaviourComponent battleBehaviour;
    [SerializeField] private InteractionBehaviourComponent interactionBehaviour;
    [SerializeField] private QuestBehaviourComponent questBehaviour;
    
    public BattleBehaviourComponent BattleBehaviour => battleBehaviour;
    public InteractionBehaviourComponent InteractionBehaviour => interactionBehaviour;
    public QuestBehaviourComponent QuestBehaviour => questBehaviour;
}