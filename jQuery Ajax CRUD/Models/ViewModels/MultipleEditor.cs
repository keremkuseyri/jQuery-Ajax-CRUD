using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace jQuery_Ajax_CRUD.Models.ViewModels
{
    public class ViewModelTT
    {
        public IEnumerable<TransactionModel> Transactions { get;private set; }
        public IEnumerable<TeachersModel> Teachers { get;private set; }
        public IEnumerable<SyncModel> Syncs { get; private set; }
        public IEnumerable<SyncModel1> Syncs1 { get; private set; }
        public IEnumerable<SyncModel2> Syncs2 { get; private set; }
        public IEnumerable<ToSyncModel> ToSyncs { get; private set; }
        public IEnumerable<ToSyncModel1> ToSyncs1 { get; private set; }
        public IEnumerable<ToSyncModel2> ToSyncs2 { get; private set; }
        
    }
}
