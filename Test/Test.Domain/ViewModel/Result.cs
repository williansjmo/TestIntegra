using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain.ViewModel
{
    public class Result
    {
        public object result { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public bool _return { get; set; }
    }
    public class DataTableResponse
    {
        public int Draw { get; set; }
        public long RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public object[] Data { get; set; }
        public string Error { get; set; }
    }
}
