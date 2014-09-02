using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TfsBuilder.ViewModels
{
    public class FileViewModel : ViewModelBase
    {
        private FileViewModel originalvalue;
        public FileViewModel()
        {
            
        }
        private string filename;

        public string FileName
        {
            get { return filename; }
            set { filename = value;
            OnPropertyChanged("FileName");
            }
        }
        private string fullname;

        public string FullName
        {
            get { return fullname; }
            set { fullname = value;
            OnPropertyChanged("FullName");
            }
        }
       
    }
}
