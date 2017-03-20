using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Elastic.Object
{
    public interface IPicker
    {
        //List<BitmapImage> imagelist { get; set; }
        Task<List<StorageFile>> openBtn_Click();
        //Task<byte[]> ReadFile(StorageFile file);




    }
}
