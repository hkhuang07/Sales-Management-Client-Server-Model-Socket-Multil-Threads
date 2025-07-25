using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElectronicsStore.DataAccess
{
    public class Categories
    {
        public int ID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public virtual ObservableCollectionListSource<Products> Products { get; } = new();
    }
}
