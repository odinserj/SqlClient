// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Security.Authentication;

namespace Microsoft.Data.SqlClient.SNI
{
    /// <summary>
    /// SNI connection handle
    /// </summary>
    internal abstract class SNIHandle
    {
        /// <summary>
        /// Exclude TLS 1.3 (not fully supported).
        /// </summary>
        protected readonly SslProtocols SupportedProtocols = LocalAppContextSwitches.UseSystemDefaultSecureProtocols ? SslProtocols.None : SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
        //protected readonly SslProtocols SupportedProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
#pragma warning disable CS0618 // Type or member is obsolete
            | SslProtocols.Ssl2 | SslProtocols.Ssl3
#pragma warning restore CS0618 // Type or member is obsolete
            ;

        /// <summary>
        /// Dispose class
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Set async callbacks
        /// </summary>
        /// <param name="receiveCallback">Receive callback</param>
        /// <param name="sendCallback">Send callback</param>
        public abstract void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback);

        /// <summary>
        /// Set buffer size
        /// </summary>
        /// <param name="bufferSize">Buffer size</param>
        public abstract void SetBufferSize(int bufferSize);

        /// <summary>
        /// Send a packet synchronously
        /// </summary>
        /// <param name="packet">SNI packet</param>
        /// <returns>SNI error code</returns>
        public abstract uint Send(SNIPacket packet);

        /// <summary>
        /// Send a packet asynchronously
        /// </summary>
        /// <param name="packet">SNI packet</param>
        /// <returns>SNI error code</returns>
        public abstract uint SendAsync(SNIPacket packet);

        /// <summary>
        /// Receive a packet synchronously
        /// </summary>
        /// <param name="packet">SNI packet</param>
        /// <param name="timeoutInMilliseconds">Timeout in Milliseconds</param>
        /// <returns>SNI error code</returns>
        public abstract uint Receive(out SNIPacket packet, int timeoutInMilliseconds);

        /// <summary>
        /// Receive a packet asynchronously
        /// </summary>
        /// <param name="packet">SNI packet</param>
        /// <returns>SNI error code</returns>
        public abstract uint ReceiveAsync(ref SNIPacket packet);

        /// <summary>
        /// Enable SSL
        /// </summary>
        public abstract uint EnableSsl(uint options);

        /// <summary>
        /// Disable SSL
        /// </summary>
        public abstract void DisableSsl();

        /// <summary>
        /// Check connection status
        /// </summary>
        /// <returns>SNI error code</returns>
        public abstract uint CheckConnection();

        /// <summary>
        /// Last handle status
        /// </summary>
        public abstract uint Status { get; }

        /// <summary>
        /// Connection ID
        /// </summary>
        public abstract Guid ConnectionId { get; }

        public virtual int ReserveHeaderSize => 0;

        public abstract SNIPacket RentPacket(int headerSize, int dataSize);

        public abstract void ReturnPacket(SNIPacket packet);

        /// <summary>
        /// Gets a value that indicates the security protocol used to authenticate this connection.
        /// </summary>
        public virtual int ProtocolVersion { get; } = 0;
#if DEBUG
        /// <summary>
        /// Test handle for killing underlying connection
        /// </summary>
        public abstract void KillConnection();
#endif
    }
}
