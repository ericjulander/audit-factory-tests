using System;
using System.Collections.Generic;
using Auditor;

namespace AirnomadAudits{
    public interface AuditObject
    {
        StatusCollection ExecuteAudit<T>(T location);
           
    }
}