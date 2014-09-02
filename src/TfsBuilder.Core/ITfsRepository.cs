using System;
namespace TfsBuilder.Lib
{
    public interface ITfsRepository
    {
        System.Collections.Generic.List<Microsoft.TeamFoundation.Framework.Client.TeamProjectCollection> GetTeamProjectCollections(Uri tfsServer);
        System.Collections.Generic.List<string> GetTfsServerList();
        Microsoft.TeamFoundation.Client.TfsTeamProjectCollection GetTfsTeamProjectCollection(Guid collectionId, Uri tfsServer);
    }
}
