﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Elastic.Object
{
    public interface IUpload
    {
        Task<string> Upload(StorageFile file);
    }
}
