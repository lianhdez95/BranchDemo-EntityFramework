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
    [DefaultClassOptions]
    [ImageName("BO_Category")]
    [DefaultProperty(nameof(Name))]
    public class Company : BaseObject
    {
        public Company()
        {
        }

        [RuleRequiredField]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<Branch> Branches { get; set; } = new ObservableCollection<Branch>();

    }
}