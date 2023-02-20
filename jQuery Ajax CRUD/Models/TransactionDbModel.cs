using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jQuery_Ajax_CRUD.Models
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        { }

        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<TeachersModel> Teachers { get; set; }
        public DbSet<LessonsModel> Lessons { get; set; }
        public DbSet<GradesModel> Grades { get; set; }
        public DbSet<TestModel> Test { get; set; }
        public DbSet<TestResults> TestResults { get; set; }
        public DbSet<BulkData> BulkData { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<SyncModel> Syncs { get; set; }
        public DbSet<SyncModel1> Syncs1 { get; set; }
        public DbSet<SyncModel2> Syncs2 { get; set; }
        public DbSet<ToSyncModel> ToSyncs { get; set; }
        public DbSet<ToSyncModel1> ToSyncs1 { get; set; }
        public DbSet<ToSyncModel2> ToSyncs2 { get; set; }








    }
}
