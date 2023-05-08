using System;
using System.Collections.Generic;
using System.Linq;
using ArtificerPro.Inventory;
using DebugConsole;
using UnityEngine;

namespace ArtificerPro.Item.DebugConsole
{
    [CreateAssetMenu(menuName = "Debug Console/Add Item Command", fileName = "Add Item Command")]
    public class AddItemCommand : DebugCommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="target">The GameObject the command should target</param>
        /// <param name="targetName">The name of the command to run</param>
        /// <param name="args">Any relevant arguments</param>
        public override void Execute(GameObject target, string targetName, params string[] args)
        {
            // grab the item from the found targets
            var found = Targets.First(i => string.Equals(i.name, targetName, StringComparison.CurrentCultureIgnoreCase)) 
                as BaseItem;

            // if we found something
            if (found != null)
            {
                int num = 0;
                // work out whether to add one or multiple items based on the number of arguments
                var hasAmount = args.Length > 0 && int.TryParse(args[0], out num);
                
                // always add one item
                target.GetComponent<EquippedItems>().AddItem(found);
                
                // if there's an amount of items passed through to the command
                if (hasAmount)
                {
                    // add that many to the player's inventory, minus the one we've already added
                    for (int i = 1; i < num; i++)
                        target.GetComponent<EquippedItems>().AddItem(found);
                } 
            }
        }
    }
}