﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDSA.Modules.DataScanner
{
    public class FilterListProvider
    {
        public IList<FilterDescriptor> FilterList { get; set; }

        public FilterListProvider()
        {
            FilterList = new List<FilterDescriptor>();
        }

        public IList<FilterDescriptor> GetFilters()
        {
            this.FilterList.Add(
               new FilterDescriptor()
               {
                   Filter = null,
                   Name = "--- Filter Select ---"
               });

            this.FilterList.Add(
               new FilterDescriptor()
               {
                   Filter = new EbitBasicFilter(),
                   Name = "EBIT Basic Filter"
               });

            this.FilterList.Add(
                new FilterDescriptor()
                {
                    Filter = new EbitBasicFilter(),
                    Name = "EBIT 2 Basic Filter"
                });


            this.FilterList.Add(
                new FilterDescriptor()
                {
                    Filter = new EbitBasicFilter(),
                    Name = "EBIT 3 Basic Filter"
                });
            return FilterList;
        }
    }
}