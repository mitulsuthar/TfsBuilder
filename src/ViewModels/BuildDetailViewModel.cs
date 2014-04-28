using Microsoft.TeamFoundation.Build.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSManager.ViewModels
{
    public class BuildDetailViewModel : ViewModelBase
    {
        internal BuildDetailViewModel originalValue;
        public BuildDetailViewModel(IBuildDetail bd)
        {
            BuildFinished = bd.BuildFinished;
            BuildNumber = bd.BuildNumber;
            DropLocation = bd.DropLocation;
            DropLocationRoot = bd.DropLocationRoot;
            LogLocation = bd.LogLocation;
            Quality = bd.Quality;
            RequestedBy = bd.RequestedBy;
            RequestedFor = bd.RequestedFor;
            CompilationStatus = bd.CompilationStatus;
            FinishTime = bd.FinishTime;
            Status = bd.Status;
            this.originalValue = (BuildDetailViewModel)this.MemberwiseClone();
        }
       
        private bool buildfinished;

        public bool BuildFinished
        {
            get { return buildfinished; }
            set
            {
                buildfinished = value;
                OnPropertyChanged("BuildFinished");
            }
        }

        private string buildnumber;

        public string BuildNumber
        {
            get { return buildnumber; }
            set
            {
                buildnumber = value;
                OnPropertyChanged("BuildNumber");
            }
        }

        private string droplocation;

        public string DropLocation
        {
            get { return droplocation; }
            set
            {
                droplocation = value;
                OnPropertyChanged("DropLocation");
            }
        }
        private string droplocationroot;

        public string DropLocationRoot
        {
            get { return droplocationroot; }
            set
            {
                droplocationroot = value;
                OnPropertyChanged("DropLocationRoot");
            }
        }

        private string loglocation;

        public string LogLocation
        {
            get { return loglocation; }
            set
            {
                loglocation = value;
                OnPropertyChanged("LogLocation");
            }
        }

        private string quality;

        public string Quality
        {
            get { return quality; }
            set
            {
                quality = value;
                OnPropertyChanged("Quality");
            }
        }
        private string requestedby;

        public string RequestedBy
        {
            get { return requestedby; }
            set
            {
                requestedby = value;
                OnPropertyChanged("RequestedBy");
            }
        }

        private string requestedfor;

        public string RequestedFor
        {
            get { return requestedfor; }
            set
            {
                requestedfor = value;
                OnPropertyChanged("RequestedFor");
            }
        }

        //public readonly string ShelvesetName { get; }
        //public readonly string SourceGetVersion { get; set; }        
        //public readonly DateTime StartTime { get; }
        /*  
          Uri BuildDefinitionUri { get; }
          //IBuildServer BuildServer { get; }
        
          //BuildPhaseStatus CompilationStatus { get; set; }
          DateTime FinishTime { get; }
          //IBuildInformation Information { get; }
          bool IsDeleted { get; }
          bool KeepForever { get; set; }
          string LabelName { get; set; }
          string LastChangedBy { get; }
          //DateTime LastChangedOn { get; }
          string ProcessParameters { get; }
          //BuildReason Reason { get; }
          string TeamProject { get; }
          //BuildPhaseStatus TestStatus { get; set; }
          Uri Uri { get; }
         */
        private BuildStatus status;

        public BuildStatus Status
        {
            get { return status; }
            set { status = value;
            OnPropertyChanged("Status");
            }
        }
         
        private BuildPhaseStatus compilationstatus;

        public BuildPhaseStatus CompilationStatus
        {
            get 
            {
                return compilationstatus; 
            }
            set 
            {
                compilationstatus = value;
                OnPropertyChanged("CompilationStatus");
            }
        }

       

        public TimeSpan ElapsedTimeSinceBuildFinished
        {
            get { 
                return (DateTime.Now - finishtime);
            }
            
        }
        
        private DateTime finishtime;

        public DateTime FinishTime
        {
            get { return finishtime; }
            set { finishtime = value;
            OnPropertyChanged("FinishTime");
            }
        }
        
    }
}
