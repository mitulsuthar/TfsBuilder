using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using TfsBuilder.ViewModels;
using Microsoft.Practices.Prism.Mvvm;
namespace TfsBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView//, IMainWindow
    {
        private BuildViewModel _buildViewModel;
        public MainWindow(BuildViewModel buildViewModel)
        {
            if (buildViewModel == null)
            {
                throw new ArgumentNullException("buildViewModel");
            }
            _buildViewModel = buildViewModel;
            this.DataContext = _buildViewModel;
            InitializeComponent();
        }
        public TfsTeamProjectCollection tpc;
        public IBuildServer buildserver;
        
        //Do Something after Window Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Initialize tpc and build server in window loaded
               // tpc = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(ConfigurationManager.AppSettings["tpcUri"].ToString(CultureInfo.CurrentCulture)));
               // buildserver = tpc.GetService<IBuildServer>();         
               // var wiStore = tpc.GetService<WorkItemStore>();
             //   foreach (Project project in wiStore.Projects)
             //   {
                   // comboBox_TeamProjects.Items.Add(project.Name);
             //   }
                //IBuildDetailSpec buildDetailSpec = buildserver.CreateBuildDetailSpec("ProcessMonitor", "DebugProcMon");
                //buildDetailSpec.MaxBuildsPerDefinition = 1;
                //buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;

                //IBuildQueryResult results = buildserver.QueryBuilds(buildDetailSpec);        
                //if (results.Failures.Length == 0 && results.Builds.Length == 1)
                //{
                //    IBuildDetail buildDetail = results.Builds[0];
                //    //Console.WriteLine("Build: " + buildDetail.BuildNumber);
                //    //Console.WriteLine("Account requesting build " + "(build service user for triggered builds): " + buildDetail.RequestedBy);
                //    //Console.WriteLine("Build triggered by: " + buildDetail.RequestedFor);
                //    //label_message.Text = buildDetail.Status.ToString();
                //    //propertyGrid1.SelectedObject = buildDetail;
                //    foreach (var prop in buildDetail.GetType().GetProperties())
                //    {
                //        //label_message.Text += string.Concat(prop.Name, " : ", prop.GetValue(buildDetail, null));
                //    }
                //}
            }
            catch (TargetInvocationException)
            {

            }
            catch (NotImplementedException)
            {

            }
        }

        ////Do Something After Team Project is Selected
        //private  void comboBox_TeamProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{   
        //    //IBuildServiceHost[] buildServiceHosts = buildserver.QueryBuildServiceHosts("*");
        //    IBuildDefinition[] buildDefinitions = buildserver.QueryBuildDefinitions(comboBox_TeamProjects.SelectedItem.ToString());
        //    var query = from Bd in buildDefinitions
        //                where Bd.TeamProject == comboBox_TeamProjects.SelectedItem.ToString()
        //                select Bd.Name;

        //    listbox_builddefinitions.ItemsSource = query.ToList();
        //}
        
        //Do Something After Build Definition is Selected.
        private async void listbox_builddefinitions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (listbox_builddefinitions.SelectedIndex != -1)
            //    {
            //        var xs = listbox_builddefinitions.SelectedItem.ToString();
            //        IBuildServiceHost[] buildServiceHosts = buildserver.QueryBuildServiceHosts("*");

            //        IBuildDefinition[] buildDefinitions = buildserver.QueryBuildDefinitions(comboBox_TeamProjects.SelectedItem.ToString());
                    
            //        IBuildDetailSpec buildDetailSpec = buildserver.CreateBuildDetailSpec(comboBox_TeamProjects.SelectedItem.ToString(), xs);
            //        buildDetailSpec.MaxBuildsPerDefinition = 1;
            //        buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;

            //        IBuildQueryResult results = buildserver.QueryBuilds(buildDetailSpec);

            //        //buildserver.QueryBuilds(buildDefinitions.Single(x => x.Name == listbox_builddefinitions.SelectedItem.ToString()));
            //        if (results.Failures.Length == 0 && results.Builds.Length == 1)
            //        {
            //            IBuildDetail buildDetail = results.Builds[0];
                        
            //            IBuildDefinition buildDefinition = buildDetail.BuildDefinition;
                        
            //            ct_builddefintionname.Content = buildDefinition.Name;
            //            ct_builddropfolder.Content = buildDetail.DropLocation;
            //            //ct_builddropfolder.Content = buildDefinition.DefaultDropLocation;
            //            ct_processtemplate.Content = buildDefinition.Process.ServerPath.ToString();
            //            var process = WorkflowHelpers.DeserializeProcessParameters(buildDefinition.ProcessParameters);
            //            //textBox_MSBuildArguments.Text = process["MSBuildArguments"].ToString();
            //            //lbl_UserName.Text = process["Username"].ToString();
            //            //var pquery = from p in process.ToList()
            //            //             select new { p.Key, p.Value };

            //            //Show Build History for that particular Build Definition
            //            buildDetailSpec.MaxBuildsPerDefinition = 10;
            //            //listview_BuildDefinitionDetails.Items.Clear();
            //            //var allprops = from prop in buildDetail.GetType().GetProperties()
            //            //               select new ListBoxItem { Name = prop.Name, Content = prop.GetValue(buildDetail, null) };

            //            //foreach (var prop in allprops)
            //            //{
            //            //    listview_BuildDefinitionDetails.Items.Add(prop);
            //            //}

            //            Task<IBuildDetail[]> getAllBuildsTask = getBuildsAsync(buildDetailSpec);

            //           IBuildDetail[] builds = await getAllBuildsTask;
                        
            //            dtgrid_viewbuildqueue.ItemsSource = (from build in builds
            //                                                     select new
            //                                                     {
            //                                                         build.BuildNumber,
            //                                                         build.Quality,
            //                                                         build.Status
            //                                                     });
            //            //listview_viewbuilddetail.Items.Clear();
            //        }
            //    }
            //}
            //catch (TargetInvocationException)
            //{

            //}
            //catch (NotImplementedException)
            //{

            //}
        }
       
        public Task<IBuildDetail[]> getBuildsAsync(IBuildDetailSpec mybuildspec)
        {
            Task<IBuildDetail[]> task1 = Task<IBuildDetail[]>.Factory.StartNew(() =>
             {
                 var results = buildserver.QueryBuilds(mybuildspec).Builds;
                 return results;
             });
            return task1;
        }

        private void btn_QueueNewBuild_Click(object sender, RoutedEventArgs e)
        {
            var xs = listbox_builddefinitions.SelectedItem.ToString();
         
            IBuildServiceHost[] buildServiceHosts = buildserver.QueryBuildServiceHosts("*");

            IBuildDefinition[] buildDefinitions = buildserver.QueryBuildDefinitions(comboBox_TeamProjects.SelectedItem.ToString());
            IBuildDetailSpec buildDetailSpec = buildserver.CreateBuildDetailSpec(comboBox_TeamProjects.SelectedItem.ToString(), xs);
            buildDetailSpec.MaxBuildsPerDefinition = 1;
            buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;
         
            IBuildQueryResult results = buildserver.QueryBuilds(buildDetailSpec);
            if (results.Failures.Length == 0 && results.Builds.Length == 1)
            {
                IBuildDetail buildDetail = results.Builds[0];
                IBuildDefinition buildDefinition = buildDetail.BuildDefinition;                
                IBuildRequest bdrequest;
                bdrequest = buildDefinition.CreateBuildRequest();                
                buildserver.QueueBuild(bdrequest);
                view_buildqueue();
            }
            
        }
        private void view_buildqueue()
        {
            //Get SelectedItem from List of Build Definitions
            var xs = listbox_builddefinitions.SelectedItem.ToString();
            IBuildServiceHost[] buildServiceHosts = buildserver.QueryBuildServiceHosts("*");

            IBuildDefinition[] buildDefinitions = buildserver.QueryBuildDefinitions(comboBox_TeamProjects.SelectedItem.ToString());
            
            IBuildDetailSpec buildDetailSpec = buildserver.CreateBuildDetailSpec(comboBox_TeamProjects.SelectedItem.ToString(), xs);
            
            buildDetailSpec.MaxBuildsPerDefinition = 1;
            
            buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;

            IBuildQueryResult results = buildserver.QueryBuilds(buildDetailSpec);
            if (results.Failures.Length == 0 && results.Builds.Length == 1)
            {
                IBuildDetail buildDetail = results.Builds[0];
                IBuildDefinition buildDefinition = buildDetail.BuildDefinition;
                IQueuedBuildsView queuedBuildsView = buildserver.CreateQueuedBuildsView(comboBox_TeamProjects.SelectedItem.ToString());
                queuedBuildsView.StatusFilter = QueueStatus.Completed;
               // queuedBuildsView.QueryOptions = QueryOptions.Definitions;
                queuedBuildsView.Refresh(true);
                foreach (IQueuedBuild queuedBuild in queuedBuildsView.QueuedBuilds)
                {
                    //MessageBox.Show(queuedBuild.Status.ToString());
                }               
            }
        }

        private async void dtgrid_viewbuildqueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgrid_viewbuildqueue.SelectedIndex != -1) {

                var xs = listbox_builddefinitions.SelectedItem.ToString();
                IBuildServiceHost[] buildServiceHosts = buildserver.QueryBuildServiceHosts("*");

                IBuildDefinition[] buildDefinitions = buildserver.QueryBuildDefinitions(comboBox_TeamProjects.SelectedItem.ToString());

                IBuildDetailSpec buildDetailSpec = buildserver.CreateBuildDetailSpec(comboBox_TeamProjects.SelectedItem.ToString(), xs);
                buildDetailSpec.MaxBuildsPerDefinition = 10;
                buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;
                try
                {
                    if (dtgrid_viewbuildqueue.IsLoaded)
                    {
                        dynamic g = dtgrid_viewbuildqueue.SelectedValue;
                        
                        Task<IBuildDetail[]> getAllBuildsTask = getBuildsAsync(buildDetailSpec);

                        IBuildDetail[] builds = await getAllBuildsTask;
                        
                        var mybuild = from bd in builds
                                          where bd.BuildNumber.ToString() == g.BuildNumber.ToString()
                                          select new
                                          {
                                              bd.BuildNumber,
                                              DefinitionName = bd.BuildDefinition.Name,
                                              bd.BuildFinished,
                                              bd.CompilationStatus,
                                              bd.DropLocation,
                                              bd.LogLocation,
                                              bd.DropLocationRoot,
                                              bd.StartTime,
                                              bd.Status,
                                              bd.TeamProject,
                                              bd.TestStatus,
                                              bd.RequestedBy,
                                              bd.Quality,
                                              bd.ProcessParameters
                                          };
                      //  listview_viewbuilddetail.ItemsSource = mybuild.ToArray();
                        //dtgrid_viewBuildDetail.ItemsSource = builds.Select(x => x.BuildNumber == g.BuildNumber);                    

                    }
                }
                catch (Exception)
                {

                }
            }
        }
        //async Task<IBuildDetail[]> GetBuildsAsync(IBuildDetailSpec buildDetailSpec)
        //{
          
        //    IBuildDetail[] myBuilds = buildserver.QueryBuilds(buildDetailSpec).Builds;
        //    return m_builds;
        //}
        private void btn_droplocation_Click(object sender, RoutedEventArgs e)
        {
            var myvalue = ((Button)sender).Tag;
            System.Diagnostics.Process.Start(myvalue.ToString());
            
        }
        List<String> bdfiles = new List<String>();
        string[] fileExtensions = {".exe",".zip"};
       
        public void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        if (fileExtensions.Contains(System.IO.Path.GetExtension(f)))
                        {                            
                            bdfiles.Add(System.IO.Path.GetFileName(f));    
                        }
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                //Console.WriteLine(excpt.Message);
            }
        }

        private void btn_viewfiles_Click(object sender, RoutedEventArgs e)
        {
            var myvalue = ((Button)sender).Tag;
            DirSearch(myvalue.ToString());
            var query = from file in bdfiles
                        select new { file = file };
            listview_viewdropfiles.ItemsSource = query;
            bdfiles.Clear();
        }
        //#region TreeView Control
        //void TreeView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    /// Create main expanded node of TreeView 
        //    treeView.Items.Add(TreeView_CreateComputerItem());
        //    /// Update open directories every 5 second 
        //    DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(5),
        //        DispatcherPriority.Background, TreeView_Update, Dispatcher);
        //}
        //void TreeView_Update(object sender, EventArgs e)
        //{
        //    Stopwatch s = new Stopwatch();
        //    s.Start();
        //    /// Update drives and folders in Computer 
        //    /// create copy for detect what item was expanded 
        //    TreeView oldTreeView = CloneUsingXaml(treeView) as TreeView;
        //    /// populate items from scratch 
        //    treeView.Items.Clear();
        //    /// add computer expanded node with all drives 
        //    treeView.Items.Add(TreeView_CreateComputerItem());
        //    TreeViewItem newComputerItem = treeView.Items[0] as TreeViewItem;
        //    TreeViewItem oldComputerItem = oldTreeView.Items[0] as TreeViewItem;
        //    /// Save old state of item 
        //    newComputerItem.IsExpanded = oldComputerItem.IsExpanded;
        //    newComputerItem.IsSelected = oldComputerItem.IsSelected;
        //    /// check all drives for creating it's root folders 
        //    foreach (TreeViewItem newDrive in (treeView.Items[0] as TreeViewItem).Items)
        //        if (newDrive.Items.Contains(null))
        //            /// Find relative old item for newDrive 
        //            foreach (TreeViewItem oldDrive in oldComputerItem.Items)
        //                if (oldDrive.Tag as string == newDrive.Tag as string)
        //                {
        //                    newDrive.IsSelected = oldDrive.IsSelected;
        //                    if (oldDrive.IsExpanded)
        //                    {
        //                        newDrive.Items.Clear();
        //                        TreeView_AddDirectoryItems(oldDrive, newDrive);
        //                    }
        //                    break;
        //                }
        //    s.Stop();
        //    Debug.WriteLine(String.Format("TreeView_Update finished with {0} ms.", s.ElapsedMilliseconds));
        //}
        //void TreeView_AddDirectoryItems(TreeViewItem oldItem, TreeViewItem newItem)
        //{
        //    newItem.IsExpanded = oldItem.IsExpanded;
        //    newItem.IsSelected = oldItem.IsSelected;
        //    /// add folders in this drive 
        //    string[] directories = Directory.GetDirectories(newItem.Tag as string);
        //    /// for each folder create TreeViewItem 
        //    foreach (string directory in directories)
        //    {
        //        TreeViewItem treeViewItem = new TreeViewItem();
        //        treeViewItem.Header = new DirectoryInfo(directory).Name;
        //        treeViewItem.Tag = directory;
        //        try
        //        {
        //            if (Directory.GetDirectories(directory).Length > 0)
        //                /// find respective old folder 
        //                foreach (TreeViewItem oldDir in oldItem.Items)
        //                    if (oldDir.Tag as string == directory)
        //                    {
        //                        if (oldDir.IsExpanded)
        //                        {
        //                            TreeView_AddDirectoryItems(oldDir, treeViewItem);
        //                        }
        //                        else
        //                        {
        //                            treeViewItem.Items.Add(null);
        //                        }
        //                        break;
        //                    }
        //        }
        //        catch { }
        //        treeViewItem.Expanded += TreeViewItem_Expanded;
        //        newItem.Items.Add(treeViewItem);
        //    }
        //}
        //TreeViewItem TreeView_CreateComputerItem()
        //{
        //    TreeViewItem computer = new TreeViewItem { Header = "Computer", IsExpanded = true };
        //    foreach (var drive in DriveInfo.GetDrives())
        //    {
        //        TreeViewItem driveItem = new TreeViewItem();
        //        if (drive.IsReady)
        //        {
        //            driveItem.Header = String.Format("{0} ({1}:)", drive.VolumeLabel, drive.Name[0]);
        //            if (Directory.GetDirectories(drive.Name).Length > 0)
        //                driveItem.Items.Add(null);
        //        }
        //        else
        //        {
        //            driveItem.Header = String.Format("{0} ({1}:)", drive.DriveType, drive.Name[0]);
        //        }
        //        driveItem.Tag = drive.Name;
        //        driveItem.Expanded += TreeViewItem_Expanded;
        //        computer.Items.Add(driveItem);
        //    }
        //    return computer;
        //}
        //void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        //{
        //    TreeViewItem rootItem = (TreeViewItem)sender;

        //    if (rootItem.Items.Count == 1 && rootItem.Items[0] == null)
        //    {
        //        rootItem.Items.Clear();

        //        string[] dirs;
        //        try
        //        {
        //            dirs = Directory.GetDirectories((string)rootItem.Tag);
        //        }
        //        catch
        //        {
        //            return;
        //        }

        //        foreach (var dir in dirs)
        //        {
        //            TreeViewItem subItem = new TreeViewItem();
        //            subItem.Header = new DirectoryInfo(dir).Name;
        //            subItem.Tag = dir;
        //            try
        //            {
        //                if (Directory.GetDirectories(dir).Length > 0)
        //                    subItem.Items.Add(null);
        //            }
        //            catch { }
        //            subItem.Expanded += TreeViewItem_Expanded;
        //            rootItem.Items.Add(subItem);
        //        }
        //    }
        //}
        //object CloneUsingXaml(object obj)
        //{
        //    string xaml = System.Windows.Markup.XamlWriter.Save(obj);
        //    return System.Windows.Markup.XamlReader.Load(new XmlTextReader(new StringReader(xaml)));
        //} 
        //#endregion 
    }
}
