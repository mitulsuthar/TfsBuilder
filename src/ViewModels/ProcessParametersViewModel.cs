using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.Workflow.Activities;
using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow;
namespace TFSManager.ViewModels
{
    public class ProcessParametersViewModel : ViewModelBase
    {
        public ProcessParametersViewModel(string _processparameters)
        {

            var processparameters = WorkflowHelpers.DeserializeProcessParameters(_processparameters);
            var myparams = new ObservableCollection<KeyPair>();
            foreach (var element in processparameters)
            {
                if (element.Value.GetType().Name == "String")
                {
                    var kvp = new KeyPair(element.Key,element.Value.ToString());
                    myparams.Add(kvp);
                }
                else if (element.Value.GetType().Name == "BuildSettings")
                {
                    var cfglist = new ObservableCollection<PlatformConfiguration>();
                    var pjtbdlist = new ObservableCollection<String>();
                    var bds = (BuildSettings)element.Value;
                    PlatformConfigurationList pclist = new PlatformConfigurationList();
                    StringList pjb;
                    pclist = bds.PlatformConfigurations;
                    pjb = bds.ProjectsToBuild;
                    List<String> projbuilds = bds.ProjectsToBuild;
                    foreach (var item in pjb)
                    {
                        pjtbdlist.Add(item);
                    }
                    List<PlatformConfiguration> configlist = pclist;
                    foreach (var item in configlist)
                    {
                        cfglist.Add(item);
                    }
                    projectstobuild = pjtbdlist;
                    platformconfigurations = cfglist;
                }
                else if(element.Value.GetType().Name == "TestSpecList")
                {
                    var tsl = new ObservableCollection<TestSpec>();
                    var tslist = (TestSpecList)element.Value;
                    foreach (var item in tslist)
                    {
                        tsl.Add(item);
                    }
                    testspeclist = tsl;
                }
            }
            procparameters = myparams;
        }
        private ObservableCollection<TestSpec> testspeclist;

        public ObservableCollection<TestSpec> TestSpecList
        {
            get { return testspeclist; }
            set { 
                testspeclist = value;
                OnPropertyChanged("TestSpecList");
            }
        }
        
        
        private BuildSettings buildsettings;

        public BuildSettings BuildSettings
        {
            get { return buildsettings; }
            set
            {
                buildsettings = value;
                OnPropertyChanged("BuildSettings");
            }
        }
        private ObservableCollection<KeyPair> procparameters;

        public ObservableCollection<KeyPair> ProcParameters
        {
            get { return procparameters; }
            set { procparameters = value; }
        }
        
        private ObservableCollection<String> projectstobuild;

        public ObservableCollection<String> ProjectsToBuild
        {
            get
            {
                return projectstobuild;
            }
            set
            {
                projectstobuild = value;
                OnPropertyChanged("ProjectsToBuild");
            }
        }

        private ObservableCollection<PlatformConfiguration> platformconfigurations;

        public ObservableCollection<PlatformConfiguration> PlatformConfigurations
        {
            get { return platformconfigurations; }
            set
            {
                platformconfigurations = value;
                OnPropertyChanged("PlatformConfigurationList");
            }
        }



    }
    public class KeyPair : ViewModelBase
    {
        public KeyPair()
        {

        }
        public KeyPair(String mkey, String mvalue)
        {
            Key = mkey;
            Value = mvalue;
        }
        private string key = "Key";
        public string Key
        {
            get { return key; }
            set
            {
                if (this.key != value)
                {
                    key = value;
                    OnPropertyChanged("Key");
                }
            }
        }


        private string value ;
        public string Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
    }
}
