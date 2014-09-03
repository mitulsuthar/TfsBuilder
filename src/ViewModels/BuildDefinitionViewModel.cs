using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsBuilder.ViewModels
{
    public class BuildDefinitionViewModel : ViewModelBase
    {
        private BuildDefinitionViewModel originalvalue;
        public IBuildDefinition BuildDefinition { get; private set; }
        internal BuildDefinitionViewModel(IBuildDefinition bd)
        {   
            ProcessTemplate = bd.Process.ServerPath;
            BuildController = bd.BuildController;
            BuildServer = bd.BuildServer;
            DefaultDropLocation = bd.DefaultDropLocation;
            Description = bd.Description;
            Enabled = bd.Enabled;
            FullPath = bd.FullPath;
            Id = bd.Id;
            Name = bd.Name;
            ProcessParameters = bd.ProcessParameters;
            BuildDefinition = bd;
            this.originalvalue = (BuildDefinitionViewModel)this.MemberwiseClone();
        }
        private string processtemplate;

        public string ProcessTemplate
        {
            get { return processtemplate; }
            set { processtemplate = value;
            OnPropertyChanged("ProcessTemplate");
            }
        }
        private IBuildController buildcontroller;

        public IBuildController BuildController
        {
            get { return buildcontroller; }
            set { buildcontroller = value;
            OnPropertyChanged("BuildController");
            }
        }

        private IBuildServer buildserver;

        public IBuildServer BuildServer
        {
            get { return buildserver; }
            set { buildserver = value;
            OnPropertyChanged("BuildServer");
            }
        }
        private string defaultdroplocation;

        public string DefaultDropLocation
        {
            get { return defaultdroplocation; }
            set { defaultdroplocation = value;
            OnPropertyChanged("DefaultDropLocation");
            }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value;
            OnPropertyChanged("Description");
            }
        }
        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value;
            OnPropertyChanged("Enabled");
            }
        }
        private string fullpath;

        public string FullPath
        {
            get { return fullpath; }
            set { fullpath = value;
            OnPropertyChanged("FullPath");
            }
        }
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value;
            OnPropertyChanged("Id");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
            OnPropertyChanged("Name");
            }
        }
       
        private string processparameters;

        public string ProcessParameters
        {
            get { return processparameters; }
            set { processparameters = value;
            OnPropertyChanged("ProcessParameters");
            }
        }
        
    }
}
