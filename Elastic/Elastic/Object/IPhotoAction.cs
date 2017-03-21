using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elastic.Object
{
    public interface IPhotoAction
    {
        // pick images
        Task<List<string>> openBtn_Click();
        List<string> pathList();

        // upload images
        Task<List<string>> Upload();
        




    }
}
