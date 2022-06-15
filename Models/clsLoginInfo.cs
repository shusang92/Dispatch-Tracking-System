using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImportExcel.Models
{
    [Serializable]
    public class clsLoginInfo
    {
        private string _Name;
        private string _UserName;
        private string _DepartmentName;
        private string _RoleName;
        private string _Area;
        private string _Emailid;
        public string Name { get { return _Name; } set { _Name = value; } }
        public string UserName { get { return _UserName; } set { _UserName = value; } }
        public string DepartmentName { get { return _DepartmentName; } set { _DepartmentName = value; } }
        public string RoleName { get { return _RoleName; } set { _RoleName = value; } }
        public string Area { get { return _Area; } set { _Area = value; } }
    }
}