﻿using CitizenFX.Core;
using disc_inventoryhud_common.Inv;
using disc_inventoryhud_common.Inventory;
using disc_inventoryhud_server.ESX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disc_inventoryhud_server.Inventory
{

    public class ItemHandler : BaseScript
    {
        public Dictionary<string, List<CallbackDelegate>> ItemUsages = new Dictionary<string, List<CallbackDelegate>>();

        public static ItemHandler Instance { get; private set; }

        public ItemHandler()
        {
            Instance = this;
            EventHandlers[Events.AddItem] += new Action<int, string, int, dynamic>(AddItem);
            EventHandlers[Events.RemoveItem] += new Action<int, string, int, int, string>(RemoveItem);
            EventHandlers[Events.RegisterItemUse] += new Action<string, CallbackDelegate>(RegisterItemUse);
            EventHandlers[Events.UseItem] += new Action<Player, string>(UseItem);
        }

        public void AddItem(int source, string Id, int Count, dynamic MetaData)
        {
            Player player = Players[source];
            InventoryData data = GetInventoryData(player, "player");
            if (data.Inventory.Values.Any(value => value.Id == Id))
            {
                KeyValuePair<int, InventorySlot> slot = data.Inventory.First(value => value.Value.Id == Id);
                slot.Value.Count += Count;
                Inventory.UpdateSlot(slot.Key, data, slot.Value);
            }
            else
            {
                var slot = new InventorySlot
                {
                    Id = Id,
                    Count = Count,
                    MetaData = MetaData
                };
                int slotPos = FindFreeSlot(data.Inventory);
                data.Inventory.Add(slotPos, slot);
                Inventory.CreateSlot(slotPos, data, slot);
            }
            player.TriggerEvent(Events.UpdateInventory, data);
        }

        public void RemoveItem(int source, string Id, int Count, int Slot, string inventory)
        {
            Player player = Players[source];
            InventoryData data = GetInventoryData(player, inventory);
            if (Slot == 0)
            {
                KeyValuePair<int, InventorySlot> slot = data.Inventory.First(value => value.Value.Id == Id);
                if (slot.Value.Count - Count == 0)
                {
                    data.Inventory.Remove(slot.Key);
                    Inventory.DeleteSlot(slot.Key, data);
                }
                else if (slot.Value.Count - Count > 0)
                {
                    slot.Value.Count -= Count;
                    Inventory.UpdateSlot(slot.Key, data, slot.Value);
                }
                else
                {
                    Debug.WriteLine("Attempted to remove to much Items");
                }
            }
            else
            {
                KeyValuePair<int, InventorySlot> slot = data.Inventory.First(value => value.Key == Slot);
                if (slot.Value.Count - Count == 0)
                {
                    data.Inventory.Remove(slot.Key);
                    Inventory.DeleteSlot(slot.Key, data);
                }
                else if (slot.Value.Count - Count > 0)
                {
                    slot.Value.Count -= Count;
                    Inventory.UpdateSlot(slot.Key, data, slot.Value);
                }
                else
                {
                    Debug.WriteLine("Attempted to remove to much Items");
                }
            }
            player.TriggerEvent(Events.UpdateInventory, data);
        }

        public void RegisterItemUse(string Id, CallbackDelegate callbackDelegate)
        {
            if (ItemUsages.ContainsKey(Id))
            {
                ItemUsages[Id].Add(callbackDelegate);
            }
            else
            {
                ItemUsages.Add(Id, new List<CallbackDelegate>()
                {
                    callbackDelegate
                });
            }
        }

        public void UseItem([FromSource] Player player, dynamic data)
        {
            dynamic obj = new
            {
                Id = data.data.item.Id,
                Count = data.data.item.Count,
                Inventory = data.data.typeFrom,
                Slot = data.data.slotFrom
            };
            if (ItemUsages.ContainsKey(data.data.item.Id))
            {
                foreach (var action in ItemUsages[data.data.item.Id])
                {
                    action.Invoke(player.Handle, obj);
                }
            }
        }

        private InventoryData GetInventoryData(Player player, string inventory)
        {
            var xPlayer = ESXHandler.Instance.GetPlayerFromId(player.Handle);
            var InvKey = new KeyValuePair<string, string>(inventory, xPlayer.identifier);
            return Inventory.Instance.LoadedInventories[InvKey];
        }

        private int FindFreeSlot(Dictionary<int, InventorySlot> inv)
        {
            for (int i = 1; i <= inv.Count(); i++)
            {
                if (!inv.ContainsKey(i)) return i;
            }
            return inv.Count() + 1;
        }
    }
}
