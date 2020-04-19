package Collector.Dimension;

import java.util.LinkedList;

public class Inventory {
    private LinkedList<ItemStack> inventory = new LinkedList<ItemStack>();

    public LinkedList<ItemStack> getInventory() {
        return inventory;
    }

    public void setInventory(LinkedList<ItemStack> inventory) {
        this.inventory = inventory;
    }

    public void addItem(Block block){
        inventory.add((ItemStack) block);
    }

    public void removeItem(ItemStack itemStack){
        inventory.remove(itemStack);
    }
}