using Shapkin_Task_10.Forms;
using Shapkin_Task_10.Models;
using System.ComponentModel;
using System.Linq;

namespace Shapkin_Task_10.AppData
{
    public class ItemsService : IItemsService
    {
        public BindingList<ItemType> GetItemTypes()
        {
            return new BindingList<ItemType>(Program.context.ItemTypes.ToList());
        }

        public IQueryable<Item> GetItems()
        {
            return Program.context.Items;
        }
    }
}
