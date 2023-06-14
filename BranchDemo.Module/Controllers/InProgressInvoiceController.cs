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
    public partial class InProgressInvoiceController : ViewController
    {
        SimpleAction MarkInProgress;
        public InProgressInvoiceController()
        {
            TargetObjectType = typeof(Invoice);
            MarkInProgress = new SimpleAction(this, "In Progress", PredefinedCategory.Edit);
            MarkInProgress.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            MarkInProgress.Execute += MarkInProgress_Execute;
        }

        private void MarkInProgress_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var currentObject = e.CurrentObject as Invoice;

            if (currentObject != null)
            {
                if (currentObject.Status.Equals(Status.Started))
                {
                    currentObject.Status = Status.InProgress;
                }
                else
                {
                    throw new UserFriendlyException("Invoice must be Started before In progress");
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
