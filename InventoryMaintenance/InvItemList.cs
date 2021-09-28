using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMaintenance
{
    public class InvItemList
    {
        private List<InvItem> invItems;

        public InvItemList()
        {
            invItems = new List<InvItem>();
        }

        public int Count => invItems.Count;

        //Modified indexer
        public InvItem this[int i]
        {
            get
            {
                if (i < 0 || i >= invItems.Count)
                {
                    throw new ArgumentOutOfRangeException("Index out of range: " + i.ToString());
                }
                return invItems[i];
            }

            set => invItems[i] = value;
        }

        //Delegate and event
        public delegate void ChangeHandler(InvItemList itemList);
        public event ChangeHandler Changed;

        public void Add(InvItem invItem)
        {
            invItems.Add(invItem);
            Changed(this);
        }

        public void Add(int itemNo, string description, decimal price)
        {
            InvItem i = new InvItem(itemNo, description, price);
            invItems.Add(i);
            Changed(this);
        }

        public void Remove(InvItem invItem)
        {
            invItems.Remove(invItem);
            Changed(this);
        }

        public static InvItemList operator + (InvItemList itemList, InvItem newItem)
        {
            itemList.Add(newItem);
            return itemList;
        }

        public static InvItemList operator - (InvItemList itemList, InvItem item)
        {
            itemList.Remove(item);
            return itemList;
        }

        public void Fill() => invItems = InvItemDB.GetItems();

        public void Save() => InvItemDB.SaveItems(invItems);

    }
}
