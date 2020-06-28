

using System;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace Collector.UI
{
    public class Gui: IRestrictions {
        private Desktop _desktop;
        public static ComboBox _combo;
        private TextButton _button;

        public Gui(Desktop desktop)
        {
            _desktop = desktop;
        }

        public void LoadGUI()
        {
            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };

            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
            grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

            
            //Position
            _combo = new ComboBox
            {
                GridColumn = 0,
                GridRow = 0
            };
            _button = new TextButton
            {
                GridColumn = 1,
                GridRow = 0,
                Text = "Inventory"
            };
            
            
            foreach (Blocks name in Enum.GetValues(typeof(Blocks))) 
            {
                _combo.Items.Add(new ListItem(name.ToString(), Color.White));
            }
            
            grid.Widgets.Add(_combo);
            
            
            _button.Click += (s, a) =>
            {
                var messageBox = Dialog.CreateMessageBox("Message", "Some message!");
                messageBox.ShowModal(_desktop);
            };

            grid.Widgets.Add(_button);

            _desktop = new Desktop
            {
                Root = grid
            };
        }

        public void Update()
        {
        }

        public void Render()
        {
            _desktop.Render();
        }
    }
}
