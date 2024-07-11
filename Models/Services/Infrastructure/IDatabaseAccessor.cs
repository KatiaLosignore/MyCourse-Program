using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        DataSet Query(FormattableString formattableQuery);
        
        // int QueryInsert(FormattableString formattableQuery);

         void QueryDelete(FormattableString formattableQuery);
    }
        
    
}