
namespace Collector.Dimension
{
    public class ItemStack {
        int value;
        int quantity;

        public ItemStack(Blocks id, int value, int quantity)
        {
            this.value = value;
            this.quantity = quantity;
        }
    }
}
