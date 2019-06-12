using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    public class CSubject
    {
        public string subjectid { get; set; }
        public string subjectname { get; set; }
        public int credit { get; set; }
        public string teachername { get; set; }
        public string termindex { get; set; }
        public string termyear { get; set; }
        public string faculty { get; set; }

        public CSubject(string subjectid, string subjectname, int credit, string teachername, string termindex, string termyear, string faculty)
        {
            this.subjectid = subjectid;
            this.subjectname = subjectname;
            this.credit = credit;
            this.teachername = teachername;
            this.termindex = termindex;
            this.termyear = termyear;
            this.faculty = faculty;
        }

        public CSubject()
        {
        }
    }
}
