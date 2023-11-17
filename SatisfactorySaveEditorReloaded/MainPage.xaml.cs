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
            
        }
        public void OnButtonClicked(object sender, EventArgs e)
        {
            TreeView.AddTreeViewEntry(treeGrid, (int)Math.Round(slider.Value,0), node_name.Text);
        }
        public void OnValueChanged(object sender, EventArgs e)
        {
            if(sender == slider)
            {
                ToolTipProperties.SetText(slider, slider.Value.ToString());
                double val = slider.Value % 1;
                double adjustment = val < 2.5 ? -val : 5 - val;
                slider.Value += adjustment;
                Slider_Value.Text = slider.Value.ToString();
            }
        }
    }

}
