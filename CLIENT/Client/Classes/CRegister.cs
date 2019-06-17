using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    class CRegister
    {
        public CRegister(string userid, string status, string subjectid, string subjectname, string credit, string teachername, string termindex, string termyear, string faculty, string day, string room, string startime, string finishtime, string facultyname, DateTime begindate, DateTime enddate)
        {
            this.userid = userid;
            this.status = status;
            this.subjectid = subjectid;
            this.subjectname = subjectname;
            this.credit = credit;
            this.teachername = teachername;
            this.termindex = termindex;
            this.termyear = termyear;
            this.faculty = faculty;
            this.day = day;
            this.room = room;
            this.startime = startime;
            this.finishtime = finishtime;
            this.facultyname = facultyname;
            this.begindate = begindate;
            this.enddate = enddate;
        }

        public string userid { get; set; }
        public string status { get; set; }
        public string subjectid { get; set; }
        public string subjectname { get; set; }
        public string credit { get; set; }
        public string teachername { get; set; }
        public string termindex { get; set; }
        public string termyear { get; set; }
        public string faculty { get; set; }
        public string day { get; set; }
        public string room { get; set; }
        public string startime { get; set; }
        public string finishtime { get; set; }
        public string facultyname { get; set; }
        public DateTime begindate { get; set; }
        public DateTime enddate { get; set; }

        public CRegister()
        {
        }
    }
}
