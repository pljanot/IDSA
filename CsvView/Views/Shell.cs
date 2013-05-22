﻿using System.Windows.Forms;
using DBModule;

namespace IDSA
{
    public partial class Shell : Form
    {
        public Shell(IViewProvider viewProvider)
        {
            InitializeComponent();
            if (viewProvider.ProjectionType == EProjectionType.Tabbed)
            {
                //menuStrip1.Visible = false;

                foreach (var vd in viewProvider.GetViews())
                {
                    vd.View.Dock = DockStyle.Fill;
                    var tp = new TabPage(vd.Header);
                    tp.Controls.Add(vd.View);
                    tabControl1.TabPages.Add(tp);
                }
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            ServiceLocator.Instance.Resolve<EFUnitOfWork>().Dispose();
            //without ServiceLocator, but Dispose() method then must be static ;) (or find other solution)
            //DBModule.DBView.Dispose();
        }
    }
}
