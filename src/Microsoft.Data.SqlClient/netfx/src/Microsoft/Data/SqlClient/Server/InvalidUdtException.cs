﻿//------------------------------------------------------------------------------
// <copyright file="InvalidUdtException.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <owner current="true" primary="true">Microsoft</owner>
// <owner current="true" primary="false">Microsoft</owner>
//------------------------------------------------------------------------------

using Microsoft.Data.Common;
using System;
using System.Runtime.Serialization;

namespace Microsoft.Data.SqlClient.Server
{

    [Serializable]
    public sealed class InvalidUdtException : SystemException
    {

        internal InvalidUdtException() : base()
        {
            HResult = HResults.InvalidUdt;
        }

        internal InvalidUdtException(String message) : base(message)
        {
            HResult = HResults.InvalidUdt;
        }

        internal InvalidUdtException(String message, Exception innerException) : base(message, innerException)
        {
            HResult = HResults.InvalidUdt;
        }

        private InvalidUdtException(SerializationInfo si, StreamingContext sc) : base(si, sc)
        {
        }

        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo si, StreamingContext context)
        {
            base.GetObjectData(si, context);
        }

        internal static InvalidUdtException Create(Type udtType, string resourceReason)
        {
            string reason = StringsHelper.GetString(resourceReason);
            string message = StringsHelper.GetString(Strings.SqlUdt_InvalidUdtMessage, udtType.FullName, reason);
            InvalidUdtException e = new InvalidUdtException(message);
            ADP.TraceExceptionAsReturnValue(e);
            return e;
        }
    }
}
