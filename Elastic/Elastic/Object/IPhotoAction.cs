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
        Task<int> openBtn_Click();
        Task<int> countImg();

        // upload images
        Task<List<string>> Upload();
        




    }
}
