using Shapkin_Task_10.Models;
using System.ComponentModel;
using System.Linq;

namespace Shapkin_Task_10.AppData
{
    public interface IItemsService
    {
        BindingList<ItemType> GetItemTypes();
        IQueryable<Item> GetItems();
    }
}
