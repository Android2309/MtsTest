using MtsTest_Logic.Interfaces;
using MtsTest_Logic.Models;
using MtsTest_Logic.Presenters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MtsTest
{
    //для простоты отладки взята директория C:\Program Files, 
    public partial class MainWindow : Window, IFolderView, IFileView
    {
        FilesPresenter filesPresenter;
        FoldersPresenter foldersPresenter;

        public MainWindow()
        {
            InitializeComponent();

            ((INotifyCollectionChanged)lvFiles.Items).CollectionChanged += lvFiles_CollectionChanged;
            ((INotifyCollectionChanged)lvFolders.Items).CollectionChanged += lvFolders_CollectionChanged;

            btnAscFiles.IsEnabled = btnDescFiles.IsEnabled = false;
            btnAscFolders.IsEnabled = btnDescFolders.IsEnabled = false;

            filesPresenter = new FilesPresenter(this);
            filesPresenter.SetFilesData();

            foldersPresenter = new FoldersPresenter(this);
            foldersPresenter.SetFoldersData();

            SetTreeView();
        }
        public IEnumerable<ViewElement> ViewFoldersData
        {
            set => this.lvFolders.ItemsSource = value;
        }

        public IEnumerable<ViewElement> ViewFilesData
        {
            set => this.lvFiles.ItemsSource = value; 
        }

        private void btnAscFolders_Click(object sender, RoutedEventArgs e)
        {
            foldersPresenter.OrderFoldersByASc();
        }
        private void btnDescFolders_Click(object sender, RoutedEventArgs e)
        {
            foldersPresenter.OrderFoldersByDesc();
        }

        private void btnAscFiles_Click(object sender, RoutedEventArgs e)
        {
            filesPresenter.OrderFilesByASc();
        }

        private void btnDescFiles_Click(object sender, RoutedEventArgs e)
        {
            filesPresenter.OrderFilesByDesc();
        }

        private void lvFiles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                btnAscFiles.IsEnabled = btnDescFiles.IsEnabled = true;
            }
        }

        private void lvFolders_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                btnAscFolders.IsEnabled = btnDescFolders.IsEnabled = true;
            }
        }


        //TODO вынести логику, (mvp паттерн), добавить файлы
        #region treeview
        private object dummyNode = null;

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
        #endregion


    }
}
