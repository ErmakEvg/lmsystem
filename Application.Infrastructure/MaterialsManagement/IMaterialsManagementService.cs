﻿using System.Collections.Generic;
using Application.Core.Data;
using LMPlatform.Models;

namespace Application.Infrastructure.MaterialsManagement
{
    using System.Net;

    public interface IMaterialsManagementService
    {
        List<Folders> GetFolders(int pid);

        Folders CreateFolder(int pid);
    }
}
