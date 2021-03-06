﻿namespace LMPlatform.Data.Repositories
{
    using Application.Core.Data;

    using LMPlatform.Data.Infrastructure;
    using LMPlatform.Data.Repositories.RepositoryContracts;
    using LMPlatform.Models;

    public class UsersRepository : RepositoryBase<LmPlatformModelsContext, User>, IUsersRepository
    {
        public UsersRepository(LmPlatformModelsContext dataContext) : base(dataContext)
        {
        }
    }
}