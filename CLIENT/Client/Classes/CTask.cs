using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    public class CTask
    {
        public string taskid { get; set; }
        public string taskname { get; set; }
        public string userid { get; set; }
        public string subjectid { get; set; }
        public string subjectname { get; set; }
        
        public int progress { get; set; }
        public DateTime deadline { get; set; }
        public string description { get; set; }

    }
}
