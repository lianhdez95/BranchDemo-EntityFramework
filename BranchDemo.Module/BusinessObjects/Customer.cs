using DevExpress.Charts.Native;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Filtering;
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
    [DefaultClassOptions]
    [ImageName("BO_Customer")]
    [DefaultProperty(nameof(FullName))]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    // You do not need to implement the INotifyPropertyChanged interface - EF Core implements it automatically.
    // (see https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies for details).
    public class Customer : BaseObject
    {
        public Customer()
        {
        }

        public override void OnSaving()
        {
            base.OnSaving();
            if(this.Address==null&&this.CustomerType == CustomerType.B)
            {
                throw new UserFriendlyException("Address field is mandatory for B Customers");
            }
            if (this.PhoneNumbers == null && this.CustomerType == CustomerType.B)
            {
                throw new UserFriendlyException("Phone Numbers field is mandatory for B Customers");
            }
        }

        [RuleRequiredField]
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        
        [RuleRequiredField]
        public virtual string LastName { get; set;}

        [RuleRequiredField]
        public virtual string Dni { get; set; }
        public virtual Address Address { get; set; }
        
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public string FullName
        {
            get { return ObjectFormatter.Format(FullNameFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string DisplayName
        {
            get { return FullName; }
        }

        public static string FullNameFormat = "{FirstName} {MiddleName} {LastName}";

        public virtual IList<PhoneNumber> PhoneNumbers { get; set; } = new ObservableCollection<PhoneNumber>();
        public virtual CustomerType CustomerType {  get; set; } 
    }

    public enum CustomerType
    {
        A, B
    }
}