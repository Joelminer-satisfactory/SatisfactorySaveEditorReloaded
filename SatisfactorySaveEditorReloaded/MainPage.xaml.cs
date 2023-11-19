using System.Diagnostics;

namespace SatisfactorySaveEditorReloaded
{
    public partial class MainPage : ContentPage
    {
        Grid treeGrid;
        public MainPage()
        {
            InitializeComponent();
            treeGrid = TreeView.CreateTreeView(MainGrid);
            MainGrid.Add(treeGrid,1);
            
            for (int x = 1; x < MainGrid.ColumnDefinitions.Count-1; x++)
            {
                for (int y = 0; y < MainGrid.RowDefinitions.Count; y++)
                {
                    Border border = new Border { Stroke = Color.FromArgb("#ffffff"), StrokeThickness = 0.5};
                    MainGrid.Add(border, x, y);
                }
            }

        }
    }

}
