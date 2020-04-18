package Collector.Dimension;

import Collector.Dimension.Block;

public class ItemStack extends Block {
    int value;
    int quantity;

    public ItemStack(String name, int value, int quantity) {
        super(name);
        this.value = value;
        this.quantity = quantity;
    }
}
