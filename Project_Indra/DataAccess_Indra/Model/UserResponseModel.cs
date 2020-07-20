using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Indra.Model
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public Nullable<int> IdUser { get; set; } 
        public string UserName { get; set; }
        public int Response { get; set; }
        public System.DateTime DateResponse { get; set; }
    }
}
