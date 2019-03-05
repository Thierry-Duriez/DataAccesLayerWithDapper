using System;
using System.Collections.Generic;
using System.Text;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Base class of all Data Transfer Object                                                                   --//
//-- Warning : For Insert IsNew must be True, For Update or Delete IsNew must be False (default)              --//
//--------------------------------------------------------------------------------------------------------------//

namespace DatasAccesLayer
{
    public abstract class DTOBase : CommonBase
    {
        [FieldSQL(ExcluSQL = true, NameSQL = "IsNew", TypeSQL = "bit")]
        public bool IsNew { get; set; }
    }
}
