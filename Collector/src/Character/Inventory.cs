using System.Collections.Generic;
using Collector.Dimension;
using Collector.UI;
using Myra.Graphics2D.UI;

public class Inventory {
    private LinkedList<ItemStack> inventory = new LinkedList<ItemStack>();
    
    public static Blocks GetSelectedItem()
    {
        return (Blocks) Gui._combo.SelectedIndex.Value;
    }

    public LinkedList<ItemStack> GetInventory() {
        return inventory;
    }

    public void SetInventory(LinkedList<ItemStack> inventory) {
        this.inventory = inventory;
    }
    
    public void RemoveItem(ItemStack itemStack){
        inventory.Remove(itemStack);
    }
}