using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmbox_main.Utils
{
    public enum TransactionStatusEnum
    {
        Processed ,
        Processing
    }

    public enum SortEnum
    {
        name_asc, name_desc,
        type_asc, type_desc, 
        price_asc, price_desc
    }
}