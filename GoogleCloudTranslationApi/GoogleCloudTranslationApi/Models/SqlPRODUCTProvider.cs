using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GoogleCloudTranslationApi.Models
{
    public  class  SqlPRODUCTProvider : SqlPRODUCTProviderBase
    {

        public SqlPRODUCTProvider (string _ConnStr)
            : base (_ConnStr)
        {}



    }
}
