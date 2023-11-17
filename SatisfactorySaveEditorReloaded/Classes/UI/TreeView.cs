using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactorySaveEditorReloaded.Classes.UI
{
    internal class TreeView
    {
        public static Grid CreateTreeView(Grid mainGrid)
        {
            Grid treeGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(36, GridUnitType.Absolute) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(36, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition {Width = new GridLength (7.5, GridUnitType.Star) }
                }
                
            };
            return treeGrid;
        }
        public static void AddTreeViewEntry(Grid treeView, int level, string name)
        {
            if (treeView == null)
            {
                Debug.WriteLine("treeView is null!");
                return;
            }
            int levelColCount = treeView.ColumnDefinitions.Count - 3;
            if (level > levelColCount)
            {
                while(level > levelColCount)
                {
                    treeView.ColumnDefinitions.Insert(1, new ColumnDefinition { Width = new GridLength(12, GridUnitType.Absolute) });
                    levelColCount = treeView.ColumnDefinitions.Count - 3;
                }
                foreach (IView child in treeView.Children)
                {
                    Element element = (Element)child;
                    if (treeView.GetColumn(child)-1 < levelColCount && element.StyleId == null)
                    {
                        treeView.SetColumnSpan(child, level + 2 - treeView.GetColumn(child));
                    }
                    else if (element.StyleId == "NodeName")
                    {
                        treeView.SetColumn(child, treeView.GetColumn(child) + level + 1);
                    }
                }
            }
            Button button = new Button { Text = "+", VerticalOptions=LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center ,StyleId = (level+1).ToString() };
            button.Clicked += (sender, args) => CollapseTreeNode(treeView, sender);

            Label depthLabel = new Label { Text = level.ToString(), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Border depthBorder = new Border { Stroke = Brush.White, StrokeThickness = 1 };

            Label nameLabel = new Label { Text = name, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, StyleId = "NodeName" };
            Border nameBorder = new Border { Stroke = Brush.White, StrokeThickness = 1, StyleId = "NodeName" };

            int newRowIndex = treeView.RowDefinitions.Count-1;
            int colIndex = treeView.ColumnDefinitions.Count-1;

            treeView.RowDefinitions.Add(new RowDefinition { Height = new GridLength(48, GridUnitType.Absolute) });
            treeView.Add(button, 0, newRowIndex);
            treeView.Add(depthLabel, 1 + level, newRowIndex);
            treeView.Add(depthBorder, 1 + level, newRowIndex);
            treeView.Add(nameLabel, colIndex, newRowIndex);
            treeView.Add(nameBorder, colIndex, newRowIndex);

            treeView.SetColumnSpan(depthLabel, colIndex - level - 1);
            treeView.SetColumnSpan(depthBorder, colIndex - level - 1);
        }
        public static void CollapseTreeNode(Grid treeView, object sender)
        {
            if(sender is Button)
            {
                Button button = (Button)sender;
                if(button.Text == "+")
                {
                    int senderDepth = 0;
                    if (sender is IView)
                    {
                        senderDepth = int.Parse(button.StyleId);
                    }
                    foreach (IView child in treeView.Children)
                    {
                        if(child is Button)
                        {
                            Button bChild = (Button)child;
                            if (int.Parse(bChild.StyleId) > senderDepth)
                            {
                                int row = treeView.GetRow(child);
                                bChild.Text = "-";
                                treeView.RowDefinitions[row] = new RowDefinition { Height = new GridLength(0, GridUnitType.Absolute) };
                            }
                        }
                    }
                    button.Text = "-";
                }
                else if (button.Text == "-")
                {
                    int senderDepth = 0;
                    if (sender is IView)
                    {
                        senderDepth = int.Parse(button.StyleId);
                    }
                    foreach (IView child in treeView.Children)
                    {
                        if (child is Button)
                        {
                            Button bChild = (Button)child;
                            if (int.Parse(bChild.StyleId) == senderDepth+1)
                            {
                                int row = treeView.GetRow(child);
                                bChild.Text = "-";
                                treeView.RowDefinitions[row] = new RowDefinition { Height = new GridLength(0, GridUnitType.Absolute) };
                            }
                        }

                    }
                    button.Text="+";
                }
            }   
        }
    }
}
