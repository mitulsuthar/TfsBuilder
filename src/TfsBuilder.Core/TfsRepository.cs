using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Client;

namespace TfsBuilder.Lib
{
    public class TfsRepository : ITfsRepository
    {
        public List<string> GetTfsServerList()
        {
            var tfsCol = new List<string>();
            //Remove hard coded version number form this project.
            using (var key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\VisualStudio\\12.0\\TeamFoundation\\Instances"))
            {
                foreach (var v in key.GetSubKeyNames())
                {
                    var instance = key.OpenSubKey(v);
                    if (instance != null)
                    {
                        var serverUri = Convert.ToString(instance.GetValue("Uri"));

                        tfsCol.Add(serverUri);
                    }
                    instance.Dispose();
                }
            }
            return tfsCol;
        }
        public  List<TeamProjectCollection> GetTeamProjectCollections(Uri tfsServer)
        {
            TfsConfigurationServer configurationServer =
                   TfsConfigurationServerFactory.GetConfigurationServer(tfsServer);

            var tpcService = configurationServer.GetService<ITeamProjectCollectionService>();

            var tfsTeamCollection = tpcService.GetCollections().ToList();

            return tfsTeamCollection;
        }
        public  TfsTeamProjectCollection GetTfsTeamProjectCollection(Guid collectionId, Uri tfsServer)
        {
            TfsConfigurationServer configurationServer =
                  TfsConfigurationServerFactory.GetConfigurationServer(tfsServer);
            var collection = configurationServer.GetTeamProjectCollection(collectionId);
            return collection;
        }
    }
}
