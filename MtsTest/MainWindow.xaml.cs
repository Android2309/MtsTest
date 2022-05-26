using MtsTest_Logic.Interfaces;
using MtsTest_Logic.Models;
using MtsTest_Logic.Presenters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MtsTest
{
    public partial class MainWindow : Window, IDataView
    {
        FoldersPresenter foldersPresenter;

        private object dummyNode = null;
        public MainWindow()
        {
            InitializeComponent();

            foldersPresenter = new FoldersPresenter(this);

            foldersPresenter.SetFoldersData();

            SetTreeView();
        }
        public IEnumerable<ViewElement> ViewData
        {
            set => this.lvFolders.ItemsSource = value;
        }


        public string SelectedImagePath { get; set; }


        private void SetTreeView()
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                treeView.Items.Add(item);
            }
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
