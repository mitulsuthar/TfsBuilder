using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSManager.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {

        private ProjectViewModel originalValue;
        internal ProjectViewModel(Project pj)
        {
            Id = pj.Id;
            Name = pj.Name;
            //copy the current value so in case there is cancel method we can revert original value back.
            this.originalValue = (ProjectViewModel)this.MemberwiseClone();
        }
        internal ProjectViewModel()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
