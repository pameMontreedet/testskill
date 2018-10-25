using System;

namespace AppLib.Core {
    public abstract class BaseStorage {
        
        protected AppDbContext Database { get; set; }
        public BaseStorage(AppDbContext dbContext){
            Database = dbContext;
        }
    }
}