using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Build.Workflow.Activities;
using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow;
using System.Collections;
namespace TfsBuilder.ViewModels
{
    public class ProcessParametersViewModel : ViewModelBase
    {
        public ProcessParametersViewModel(string _processparameters)
        {
            var processparameters = WorkflowHelpers.DeserializeProcessParameters(_processparameters);
            var myparams = new ObservableCollection<KeyPair>();
            foreach (var element in processparameters)
            {
                var cfglist = new ObservableCollection<string>();
                var pjtbdlist = new ObservableCollection<string>();
                if (element.Key.ToString() == "ProjectsToBuild")
                {   
                    var projList = ((IEnumerable)element.Value).Cast<object>()
                                   .Select(x => x.ToString())
                                   .ToArray();                    
                    foreach (var item in projList)
                    {
                        pjtbdlist.Add(item);
                    }
                    this.ProjectsToBuild = pjtbdlist;                    
                }
                if (element.Key.ToString() == "ConfigurationsToBuild")
                {
                    
                    var configList = ((IEnumerable)element.Value).Cast<object>()
                                   .Select(x => x.ToString())
                                   .ToArray();
                    foreach (var item in configList)
                    {
                        cfglist.Add(item);
                    }
                    this.PlatformConfigurations = cfglist;                   
                }
                if (element.Key.ToString() == "AdvancedBuildSettings")
                {
                    var kvp = new KeyPair(element.Key,element.Value.ToString());
                    myparams.Add(kvp);
                }
                if (element.Value.GetType().Name == "BuildSettings")
                {
                    var bds = (BuildSettings)element.Value;
                    PlatformConfigurationList pclist = new PlatformConfigurationList();
                    StringList pjb;
                    
                }
                if(element.Value.GetType().Name == "TestSpecList")
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

        private ObservableCollection<string> platformconfigurations;

        public ObservableCollection<string> PlatformConfigurations
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
