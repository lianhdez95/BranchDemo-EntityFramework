using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using DevExpress.XtraReports.Web.ClientControls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BranchDemo.Module.BusinessObjects
{
    // Register this entity in your DbContext (usually in the BusinessObjects folder of your project) with the "public DbSet<Invoice> Invoices { get; set; }" syntax.
    [DefaultClassOptions]
    [ImageName("BO_Invoice")]
    [DefaultProperty(nameof(DateTime))]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class Invoice : BaseObject
    {
        public Invoice()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new ObservableCollection<AssociatedEntityObject>();
        }

        

        public override void OnCreated()
        {
            base.OnCreated();
            ApplicationUser currentUser = ObjectSpace.FindObject<ApplicationUser>(CriteriaOperator.Parse("ID=CurrentUserId()"));
            SoldBy = currentUser.Branch;
            this.DateTime = DateTime.Now;
            this.Status = Status.Started;
        }

        public override void OnSaving()
        {
            base.OnSaving();
            
            if (this.SoldBy != this.Product.CreatedBy && !this.Product.IsGlobal)
            {
                throw new UserFriendlyException("Invoice Error: Product Must be global");
            }

        }

        public virtual int Amount { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual Product Product { get; set; }
        public virtual Branch SoldBy { get; set; }
        public virtual Status Status { get; set; }
        public virtual IList<Tax> Taxes { get; set; } = new ObservableCollection<Tax>();
        [NotMapped]
        public decimal TotalPrice 
        {
            get
            {
                decimal total = 0;
                if (Product != null && Amount > 0)
                {
                    total = UnitPrice * Amount + TotalTax;
                }
                return total;
            }
        }
        [NotMapped]
        public decimal UnitPrice 
        {
            get
            {
                decimal price = 0;
                if (Product != null)
                {
                    price = Product.UnitPrice;
                }
                return price;
            }
           
        }
        [NotMapped]
        public decimal TotalTax
        {
            get 
            {
                decimal total = 0;
                if (Taxes.Count > 0)
                {
                    foreach(Tax tax in Taxes)
                    {
                        total += tax.TaxPrice;
                    }
                }
                return total;
            }
            
        }
    }

    public enum Status
    {
        Started, InProgress, Completed
    }


}