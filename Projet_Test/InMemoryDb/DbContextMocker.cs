﻿using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Projet_Test.InMemoryDb
{
    public static class DbContextMocker
    {
        /// <summary>
        /// Initialise un context Afpana en mémoire
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static AFPANADbContext GetAFPANADbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AFPANADbContext>()
                            .UseInMemoryDatabase(databaseName: dbName).Options;
            var dbContext = new AFPANADbContext(options);

            return dbContext;
        }

    }
}
