using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BranchDemo.Module.BusinessObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<Product> Products { get; set; }" syntax.
    [DefaultClassOptions]
    [ImageName("BO_Product")]
    [DefaultProperty(nameof(Name))]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class Product : BaseObject
    {


        public Product()
        {
            
        }

        public override void OnCreated()
        {
            base.OnCreated();
            ApplicationUser currentUser = ObjectSpace.FindObject<ApplicationUser>(CriteriaOperator.Parse("ID=CurrentUserId()"));
            CreatedBy = currentUser.Branch;

        }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual Branch CreatedBy { get; set; }
        public virtual bool IsGlobal { get; set; }
    }
}