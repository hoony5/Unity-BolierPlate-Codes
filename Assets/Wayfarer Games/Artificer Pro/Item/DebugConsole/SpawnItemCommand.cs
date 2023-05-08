using DebugConsole;
using UnityEngine;

namespace ArtificerPro.Item.DebugConsole
{
    [CreateAssetMenu(menuName = "Debug Console/Spawn Item Command", fileName = "Spawn Item Command")]
    public class SpawnItemCommand : DebugCommand
    {
        public override void Execute(GameObject target, string targetName, params string[] args)
        {
            Debug.Log("Spawned item");
        }
    }
}