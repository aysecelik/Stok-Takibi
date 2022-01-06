using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İşbirSünger
{
    class  baglanti
    {
        public SqlConnection baglan()
        {
            
                SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=ISBIRSUNGER;Integrated Security=True");
                baglan.Open();
                return baglan;
           
        }
    }
}
