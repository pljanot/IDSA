﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using LumenWorks.Framework.IO.Csv;
using CsvReaderModule;
using DBModule;
using CsvReaderModule.Controllers;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public interface ICsvView
    {
        //void SetControler(CsvViewController ctr);
        //void LoadCsvFile();
        void BoxMsg(string s);
        string OpenDialog();
        void RefreshView();
        //OpenFileDialog dialog { get; set; }
    }

    public partial class CsvView : UserControl, ICsvView
    {
        private CsvViewController presenter;
        
        public CsvView()
        {
            InitializeComponent();

            ServiceLocator.Instance.Register(new CsvViewController(this)); 
            presenter = ServiceLocator.Instance.Resolve<CsvViewController>();
        }


        private void prepareGridHeaders<T>()
        {
            var collection = presenter.getHeaders<T>(csvDataGrid.Columns.Count);
            foreach (var element in collection)
            {
                csvDataGrid.Columns[collection.IndexOf(element)].HeaderText = element.ToString();
            }
        }

        // I dont know if passing type throw few function's and making it dependent on each other is good...
        private void loadCsvData<T>()
        {
            var csv = presenter.LoadCsvFile(OpenDialog());
            if (csv != null)
            {
                csvDataGrid.DataSource = csv;
                //Task.Factory.StartNew(() => presenter.AddCompany(csv.ToList())); //dispose crash task, task runs 
                                                                                   //infinite when app close some dispose problem occurs
                prepareGridHeaders<T>();
                csv.Dispose();
            }
        }

        private void loadCsv_Click(object sender, EventArgs e)
        {
            loadCsvData<CsvEnums._company>();
        }

        private void loadFinData_Click(object sender, EventArgs e)
        {
            loadCsvData<CsvEnums._financialData>();
        }

        //public void SetControler(CsvViewController ctr)
        //{
        //    presenter = ctr;
        //}

        public void BoxMsg(string s)
        {
            MessageBox.Show(s);
        }

        public string OpenDialog()
        {
            using (var tmpDialog = new OpenFileDialog())
            {
                if (tmpDialog.ShowDialog() == DialogResult.OK)
                {
                    return tmpDialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }

        private void baseView_Click(object sender, EventArgs e)
        {
            //presenter.selectColumns();
        }

        public void RefreshView()
        {
            //presenter.
        }

        public void Dispose()       //TODO: Dispose() hides dispose() from ComponentModel.Component ???
        {
            base.Dispose();
            presenter.Dispose();
        }

        private void saveDb_Click(object sender, EventArgs e)
        {
            //presenter.AddCompany(csv.ToList());
            //presenter.saveDb<datatype>();
        }
    }
}
