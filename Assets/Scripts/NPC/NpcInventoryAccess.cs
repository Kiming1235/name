using System.Reflection;
using UnityEngine;
using QuarterVillageSim.Objects;

namespace QuarterVillageSim.NPC
{
    public static class NpcInventoryAccess
    {
        private static readonly FieldInfo FoodField = typeof(NpcInventory).GetField("food", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo WoodField = typeof(NpcInventory).GetField("wood", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo StoneField = typeof(NpcInventory).GetField("stone", BindingFlags.Instance | BindingFlags.NonPublic);

        public static int GetAmount(NpcInventory inventory, ResourceKind kind)
        {
            if (inventory == null)
            {
                return 0;
            }

            return kind switch
            {
                ResourceKind.Food => inventory.Food,
                ResourceKind.Wood => inventory.Wood,
                ResourceKind.Stone => inventory.Stone,
                _ => 0
            };
        }

        public static int GetTotalAmount(NpcInventory inventory)
        {
            if (inventory == null)
            {
                return 0;
            }

            return inventory.Food + inventory.Wood + inventory.Stone;
        }

        public static void Remove(NpcInventory inventory, ResourceKind kind, int amount)
        {
            if (inventory == null || amount <= 0)
            {
                return;
            }

            switch (kind)
            {
                case ResourceKind.Food:
                    SetField(FoodField, inventory, Mathf.Max(0, inventory.Food - amount));
                    break;
                case ResourceKind.Wood:
                    SetField(WoodField, inventory, Mathf.Max(0, inventory.Wood - amount));
                    break;
                case ResourceKind.Stone:
                    SetField(StoneField, inventory, Mathf.Max(0, inventory.Stone - amount));
                    break;
            }
        }

        private static void SetField(FieldInfo fieldInfo, NpcInventory inventory, int value)
        {
            if (fieldInfo == null)
            {
                return;
            }

            fieldInfo.SetValue(inventory, value);
        }
    }
}