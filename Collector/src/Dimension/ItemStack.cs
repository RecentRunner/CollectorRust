
namespace Collector.Dimension
{
    public class ItemStack: Block {
        int value;
        int quantity;

        public ItemStack(string name, int value, int quantity) : base(name)
        {
            this.value = value;
            this.quantity = quantity;
        }
    }
}
