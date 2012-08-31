using HubrisEditor.ProjectIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubrisEditor.Core
{
    public interface IPostDeserializable
    {
        void PostDeserialize(ProjectManager sender);
    }
}
