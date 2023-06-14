using BranchDemo.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BranchDemo.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class StartedInvoiceController : ViewController
    {
        SimpleAction MarkStarted;
        public StartedInvoiceController()
        {
            TargetObjectType = typeof(Invoice);
            MarkStarted = new SimpleAction(this, "Started", PredefinedCategory.Edit);
            MarkStarted.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            MarkStarted.Execute += MarkStarted_Execute;
        }

        private void MarkStarted_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var currentObject = e.CurrentObject as Invoice;

            if (currentObject != null)
            {
                if (currentObject.Status.Equals(Status.InProgress))
                {
                    currentObject.Status = Status.Started;
                }
                else
                {
                    throw new UserFriendlyException("Invoice must be In Progress before");
                }
            }

            if (this.ObjectSpace.IsModified)
                this.ObjectSpace.CommitChanges();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
