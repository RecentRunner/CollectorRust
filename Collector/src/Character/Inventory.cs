using System.Collections.Generic;
using Collector.Dimension;
    
public class Inventory {
    private LinkedList<ItemStack> inventory = new LinkedList<ItemStack>();

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