﻿namespace PCMS.API.BusinessLogic.Models.Interfaces
{
    /// <summary>
    /// Apply all Audit fields on a model.
    /// </summary>
    public interface IAuditable : IAuditCreator, IAuditModifier
    {

    }
}
