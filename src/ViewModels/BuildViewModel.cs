using System.Globalization;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TfsBuilder.ViewModels;
using TfsBuilder.Lib;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
namespace TfsBuilder.ViewModels
{
    public class BuildViewModel : ViewModelBase
    {
        public TfsTeamProjectCollection _tpc;
        public IBuildServer _buildserver { get; private set; }
        private ITfsRepository _tfsRepository;

        public BuildViewModel()
        {

        }
 
        public BuildViewModel(ITfsRepository tfsRepository)
        {
            _tfsRepository = tfsRepository;
            this.QueueBuildCommand = new DelegateCommand(QueueBuild);
            this.QueueBuildCommand.RaiseCanExecuteChanged();
            this.OnPropertyChanged("QueueBuildCommand");
        }
        public DelegateCommand QueueBuildCommand { get; private set; }       
       
        public void QueueBuild()
        {
            IBuildDefinition buildDefinition = SelectedBuildDefinition.BuildDefinition;
            IBuildRequest bdrequest;
            bdrequest = buildDefinition.CreateBuildRequest();
            _buildserver.QueueBuild(bdrequest);                
            
        }
        #region TfsServers
        private ObservableCollection<string> tfsServerList = null;
        public ObservableCollection<string> TfsServerList
        {
            get
            {
                if (tfsServerList == null)
                {
                    GetTfsServerList();
                }
                return tfsServerList;
            }
            set
            {
                tfsServerList = value;
                OnPropertyChanged("TfsServerList");
            }

        }
        public void GetTfsServerList()
        {
            List<string> serverList = _tfsRepository.GetTfsServerList();
            var slist = new ObservableCollection<string>();
            foreach (var server in serverList)
            {
                slist.Add(server);
            }
            TfsServerList = slist;
        }
        private string selectedTfsServer;

        public string SelectedTfsServer
        {
            get { return selectedTfsServer; }
            set
            {
                selectedTfsServer = value;
                OnPropertyChanged("SelectedTfsServer");
                GetTfsTeamProjectCollection();
            }
        }

        #endregion
        #region TfsTeamProjectCollections
        private ObservableCollection<TeamProjectCollection> tfsTeamProjectCollectionList = null;

        public ObservableCollection<TeamProjectCollection> TfsTeamProjectCollectionList
        {
            get
            {
                if (SelectedTfsServer != null && tfsTeamProjectCollectionList == null)
                {
                    GetTfsTeamProjectCollection();
                }
                return tfsTeamProjectCollectionList;
            }
            set
            {
                tfsTeamProjectCollectionList = value;
                OnPropertyChanged("TfsTeamProjectCollectionList");
            }
        }
        public void GetTfsTeamProjectCollection()
        {
            Uri uri = new Uri(selectedTfsServer);
            var collectionList = _tfsRepository.GetTeamProjectCollections(uri);
            var tfscollist = new ObservableCollection<TeamProjectCollection>();
            foreach (var col in collectionList)
            {
                tfscollist.Add(col);
            }
            TfsTeamProjectCollectionList = tfscollist;
        }
        private TeamProjectCollection selectedTeamProjectCollection;

        public TeamProjectCollection SelectedTeamProjectCollection
        {
            get { return selectedTeamProjectCollection; }
            set
            {
                if (value != null)
                {
                    selectedTeamProjectCollection = value;
                    OnPropertyChanged("SelectedTeamProjectCollection");
                    Uri uri = new Uri(SelectedTfsServer);
                    _tpc = _tfsRepository.GetTfsTeamProjectCollection(selectedTeamProjectCollection.Id, uri);
                    _buildserver = _tpc.GetService<IBuildServer>();                    
                    this.GetProjects();
                }
            }
        }

        #endregion
        #region Projects
        private ObservableCollection<ProjectViewModel> projectlist;
        public ObservableCollection<ProjectViewModel> ProjectList
        {
            get
            {
                if (selectedTeamProjectCollection != null && projectlist == null)
                {
                    this.GetProjects();
                }
                return projectlist;
            }
            set
            {
                projectlist = value;
                OnPropertyChanged("ProjectList");
            }
        }
        internal void GetProjects()
        {
            if (projectlist == null)
                projectlist = new ObservableCollection<ProjectViewModel>();
            projectlist.Clear();
            var pl = new ObservableCollection<ProjectViewModel>();
            var wiStore = _tpc.GetService<WorkItemStore>();
            foreach (Project project in wiStore.Projects)
            {
                ProjectViewModel item = new ProjectViewModel(project);
                pl.Add(item);
            }
            ProjectList = pl;
        }
        private ProjectViewModel selectedproject;

        public ProjectViewModel SelectedProject
        {
            get
            {
                return selectedproject;
            }
            set
            {
                if (value != null)
                {
                    selectedproject = value;
                    OnPropertyChanged("SelectedProject");
                    this.GetBuildDefinitions();
                }
            }
        }
        #endregion        
        #region BuilDefinitions
        private ObservableCollection<BuildDefinitionViewModel> builddefinitionlist = null;

        public ObservableCollection<BuildDefinitionViewModel> BuildDefinitionList
        {
            get
            {
                if (this.SelectedProject != null && builddefinitionlist == null)
                {
                    GetBuildDefinitions();
                }
                return builddefinitionlist;
            }
            set
            {
                builddefinitionlist = value;
                OnPropertyChanged("BuildDefinitionList");
            }
        }

        internal void GetBuildDefinitions()
        {
            if (builddefinitionlist == null)
                builddefinitionlist = new ObservableCollection<BuildDefinitionViewModel>();
            else
                builddefinitionlist.Clear();
            IBuildDefinition[] builddefinitions = _buildserver.QueryBuildDefinitions(this.SelectedProject.Name);

            var bd = new ObservableCollection<BuildDefinitionViewModel>();
            foreach (IBuildDefinition item in builddefinitions)
            {
                BuildDefinitionViewModel bdvm = new BuildDefinitionViewModel(item);
                bd.Add(bdvm);
            }
            this.BuildDefinitionList = bd;
        }

        private BuildDefinitionViewModel selectedbuilddefinition;

        public BuildDefinitionViewModel SelectedBuildDefinition
        {
            get
            {
                return selectedbuilddefinition;
            }
            set
            {
                if (value != null)
                {
                    selectedbuilddefinition = value;
                    OnPropertyChanged("SelectedBuildDefinition");
                    this.GetBuildDetails();
                    this.BuildProcessParameters = new ProcessParametersViewModel(selectedbuilddefinition.ProcessParameters);
                    this.QueueBuildCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private ProcessParametersViewModel buildprocessparameters;

        public ProcessParametersViewModel BuildProcessParameters
        {
            get
            {
                return buildprocessparameters;
            }
            set
            {
                buildprocessparameters = value;
                OnPropertyChanged("BuildProcessParameters");
            }
        }

        #endregion
        #region BuildDetail
        private ObservableCollection<BuildDetailViewModel> builddetaillist;

        public ObservableCollection<BuildDetailViewModel> BuildDetailList
        {
            get
            {
                if (SelectedBuildDefinition != null && builddetaillist == null)
                {
                    this.GetBuildDetails();
                }
                return builddetaillist;
            }
            set
            {
                if (value != null)
                {
                    builddetaillist = value;
                    OnPropertyChanged("BuildDetailList");
                }
            }
        }

        private void GetBuildDetails()
        {
            if (builddetaillist == null)
            {
                builddetaillist = new ObservableCollection<BuildDetailViewModel>();
            }
            builddetaillist.Clear();
            IBuildDetailSpec buildDetailSpec = _buildserver.CreateBuildDetailSpec(this.SelectedProject.Name, this.SelectedBuildDefinition.Name);
            buildDetailSpec.MaxBuildsPerDefinition = 10;
            buildDetailSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;

            IBuildDetail[] builds = getBuilds(buildDetailSpec);
            var bdl = new ObservableCollection<BuildDetailViewModel>();
            foreach (var item in builds)
            {
                var bdetailviewmodel = new BuildDetailViewModel(item);
                bdl.Add(bdetailviewmodel);
            }
            this.BuildDetailList = bdl;
        }
        public IBuildDetail[] getBuilds(IBuildDetailSpec mybuildspec)
        {

            var results = _buildserver.QueryBuilds(mybuildspec).Builds;
            return results;
        }
        private BuildDetailViewModel selectedbuilddetail;

        public BuildDetailViewModel SelectedBuildDetail
        {
            get
            {
                return selectedbuilddetail;
            }
            set
            {
                if (value != null)
                {
                    selectedbuilddetail = value;
                    OnPropertyChanged("SelectedBuildDetail");
                    this.GetDeployedFiles();
                }
            }
        }
        #endregion
        #region FileViewModel
        private ObservableCollection<FileViewModel> deployedfileslist;

        public ObservableCollection<FileViewModel> DeployedFilesList
        {
            get
            {
                if (SelectedBuildDetail != null && deployedfileslist == null)
                {
                    this.GetDeployedFiles();
                }
                return deployedfileslist;
            }
            set
            {
                deployedfileslist = value;
                OnPropertyChanged("DeployedFilesList");
            }
        }
        internal void GetDeployedFiles()
        {
            if (deployedfileslist == null)
            {
                deployedfileslist = new ObservableCollection<FileViewModel>();
            }
            deployedfileslist.Clear();
            DirSearch(SelectedBuildDetail.DropLocation.ToString());
            if (fileList.Count > 0)
            {
                DeployedFilesList = fileList;
            }
        }
        string[] fileExtensions = { ".exe", ".zip",".cmd"};
        internal ObservableCollection<FileViewModel> fileList = new ObservableCollection<FileViewModel>();
        public void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        if (fileExtensions.Contains(Path.GetExtension(f)))
                        {
                            var deployedfile = new FileViewModel();
                            deployedfile.FileName = Path.GetFileName(f);
                            deployedfile.FullName = Path.GetFullPath(f);
                            fileList.Add(deployedfile);
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


        #endregion



    }
}
