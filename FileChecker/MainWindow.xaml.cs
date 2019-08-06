using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Controls.Primitives;

namespace FileChecker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnOpenClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择目录";
            var result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                _pathContent.ItemsSource = LoadPathContent(folderBrowserDialog.SelectedPath);
                //var nodes = LoadPathContent(folderBrowserDialog.SelectedPath);
                //nodes.ForEach(item => _pathContent.Items.Add(new TreeViewItem()
                //{
                //    DataContext = item,
                //    Header = item.Name,
                //}));
            }
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<NodeInfo> LoadPathContent(string path)
        {
            var di = new DirectoryInfo(path);
            var subpaths = di.GetDirectories();
            var nodes = subpaths.Select(e => new NodeInfo()
            {
                IsFile = false,
                Fullpath = e.FullName,
                Name = e.Name,
                Children = LoadPathContent(e.FullName)
            }).ToList();
            var files = di.GetFiles().Select(e => new NodeInfo()
            {
                Ext = e.Extension,
                Name = e.Name,
                Fullpath = e.FullName,
                IsFile = true
            }).ToList();
            return nodes.Concat(files).ToList();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (ToggleButton)sender;
            var nodeInfo = (NodeInfo)btn.DataContext;
            nodeInfo.Children = LoadPathContent(nodeInfo.Fullpath);
            nodeInfo.Name = "aa";
        }

        private void _pathContent_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var context = (NodeInfo)e.NewValue;
            if (context.IsFile)
            {
                var fi = new FileInfo(context.Fullpath);
                System.Windows.MessageBox.Show($"文件名：{fi.Name}\n文件大小：{fi.Length}\n创建时间：{fi.CreationTime}");
            }
        }
    }
}
