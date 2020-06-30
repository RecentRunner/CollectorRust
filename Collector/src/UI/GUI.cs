using System;
using Collector.Dimension;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace Collector.UI
{
    public class Gui : IRestrictions
    {
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
                var grid1 = InventoryGrid();
                var window = new Window {Content = grid1};
                window.ShowModal(_desktop);
            };

            grid.Widgets.Add(_button);

            _desktop = new Desktop
            {
                Root = grid
            };
        }

        private Grid InventoryGrid()
        {
            var textBox1 = new TextBox {AcceptsKeyboardFocus = true};
            var checkBox1 = new CheckBox {Text = "Item1", GridRow = 1, AcceptsKeyboardFocus = false};
            var checkBox2 = new CheckBox {Text = "Item2", GridRow = 2, AcceptsKeyboardFocus = false};
            var checkBox3 = new CheckBox {Text = "Item3", GridRow = 3, AcceptsKeyboardFocus = false};
            var checkBox4 = new CheckBox {Text = "Item4", GridRow = 4, AcceptsKeyboardFocus = false};
            var checkBox5 = new CheckBox {Text = "Item5", GridRow = 5, AcceptsKeyboardFocus = false};
            var checkBox6 = new CheckBox {Text = "Item6", GridRow = 6, AcceptsKeyboardFocus = false};
            var checkBox7 = new CheckBox {Text = "Item7", GridRow = 7, AcceptsKeyboardFocus = false};
            var checkBox8 = new CheckBox {Text = "Item8", GridRow = 8, AcceptsKeyboardFocus = false};
            var checkBox9 = new CheckBox {Text = "Item9", GridRow = 9, AcceptsKeyboardFocus = false};
            var checkBox10 = new CheckBox {Text = "Item10", GridRow = 10, AcceptsKeyboardFocus = false};
            var menuItem1 = new MenuItem {Text = "Drop"};
            var menuItem2 = new MenuItem {Text = "Use"};
            var menuItem3 = new MenuItem {Text = "Equip"};
            var horizontalMenu1 = new HorizontalMenu {AcceptsKeyboardFocus = true, GridColumn = 1};
            horizontalMenu1.Items.Add(menuItem1);
            horizontalMenu1.Items.Add(menuItem2);
            horizontalMenu1.Items.Add(menuItem3);
            var label1 = new Label {Text = "Description1", GridColumn = 1, GridRow = 1, AcceptsKeyboardFocus = false};
            var label2 = new Label {Text = "Description2", GridColumn = 1, GridRow = 2, AcceptsKeyboardFocus = false};
            var label3 = new Label {Text = "Description3", GridColumn = 1, GridRow = 3, AcceptsKeyboardFocus = false};
            var label4 = new Label {Text = "Description4", GridColumn = 1, GridRow = 4, AcceptsKeyboardFocus = false};
            var label5 = new Label {Text = "Description5", GridColumn = 1, GridRow = 5, AcceptsKeyboardFocus = false};
            var label6 = new Label {Text = "Description6", GridColumn = 1, GridRow = 6, AcceptsKeyboardFocus = false};
            var label7 = new Label {Text = "Description7", GridColumn = 1, GridRow = 7, AcceptsKeyboardFocus = false};
            var label8 = new Label {Text = "Description8", GridColumn = 1, GridRow = 8, AcceptsKeyboardFocus = false};
            var label9 = new Label {Text = "Description9", GridColumn = 1, GridRow = 9, AcceptsKeyboardFocus = false};
            var label10 = new Label {Text = "Description10", GridColumn = 1, GridRow = 10, AcceptsKeyboardFocus = false};
            var grid1 = new Grid {ShowGridLines = true, AcceptsKeyboardFocus = false};
            grid1.Widgets.Add(textBox1);
            grid1.Widgets.Add(checkBox1);
            grid1.Widgets.Add(checkBox2);
            grid1.Widgets.Add(checkBox3);
            grid1.Widgets.Add(checkBox4);
            grid1.Widgets.Add(checkBox5);
            grid1.Widgets.Add(checkBox6);
            grid1.Widgets.Add(checkBox7);
            grid1.Widgets.Add(checkBox8);
            grid1.Widgets.Add(checkBox9);
            grid1.Widgets.Add(checkBox10);
            grid1.Widgets.Add(horizontalMenu1);
            grid1.Widgets.Add(label1);
            grid1.Widgets.Add(label2);
            grid1.Widgets.Add(label3);
            grid1.Widgets.Add(label4);
            grid1.Widgets.Add(label5);
            grid1.Widgets.Add(label6);
            grid1.Widgets.Add(label7);
            grid1.Widgets.Add(label8);
            grid1.Widgets.Add(label9);
            grid1.Widgets.Add(label10);
            return grid1;
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