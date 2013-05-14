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

namespace WindowsFormsApplication1
{
    interface ICsvView
    {
        void SetControler(CsvViewController ctrl);
        void LoadCsvFile();
    }
    public partial class CsvView : UserControl
    {
        public CsvView()
        {
            InitializeComponent();
        }

        private void loadCsv_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var _csvReader = new CachedCsvReader(new StreamReader(ofd.FileName), false))
                        {
                            csvDataGrid.DataSource = _csvReader;
                            //set enums on collumns view.
                            if (Enum.GetNames(typeof(CsvEnums._company)).Length < csvDataGrid.Columns.Count)
                            {
                                MessageBox.Show("Mismatch enum.length != columns.count");
                            }
                            // rozkodowanie kolumn.csv - > ENUM (V)
                            foreach (CsvEnums._company cmp in Enum.GetValues(typeof(CsvEnums._company)))
                            {
                                csvDataGrid.Columns[(int)cmp].HeaderText = cmp.ToString();
                            }

                            //foreach (DataGridViewColumn col in csvDataGrid.Columns)
                            //{
                            //    col.HeaderText = "test";
                            //    //csvDataGrid.Columns[0].HeaderText = "testName1";
                            //}


                            //TODO: Maybe DbService should inherit after UnitOfWork??????

                            //using (var uow = new EFUnitOfWork())
                                //{
                                //    foreach (var item in _csvReader.ToList())
                                //    {
                                //        var company = new Company()
                                //        {
                                //            Symbol = item[(int)CsvEnums._company.Shortcut],
                                //            Name = item[(int)CsvEnums._company.Name],
                                //            Description = item[(int)CsvEnums._company.Description],
                                //            //Trade = item[(int)CsvEnums._company.Profile],
                                //            Url = item[(int)CsvEnums._company.Href]
                                //        };

                                //        uow.Companies.Add(company);
                                //    }
                                //    uow.SaveChanges();
                            //}

                            //using (var dbService = new DbService())
                            //{
                            //    foreach (var item in _csvReader.ToList())
                            //    {
                            //        var company = new Company()
                            //        {
                            //            Symbol = item[(int)CsvEnums._company.Shortcut],
                            //            Name = item[(int)CsvEnums._company.Name],
                            //            Description = item[(int)CsvEnums._company.Description],
                            //            //Trade = item[(int)CsvEnums._company.Profile],
                            //            Url = item[(int)CsvEnums._company.Href]
                            //        };

                            //        dbService.AddsNewCompany();
                            //    }
                            //}

                            // select columns ? - > transfer to data base.
                            // zarzadzanie wyswietlaniem kolumn.
                            // filtracja po wybranych kolumnach
                        }
                    }
                    catch (IOException ex)
                    {
                        //show error message.
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
