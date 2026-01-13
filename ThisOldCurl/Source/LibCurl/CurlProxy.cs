using System;
using System.Collections.Generic;
using System.Text;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// this enum was added in 7.10
    /// </summary>
    public enum curl_proxytype
    {
        /// <summary>
        /// added in 7.10, new in 7.19.4 default is to use CONNECT HTTP/1.1
        /// </summary>
        CURLPROXY_HTTP = 0,
        /// <summary>
        /// added in 7.19.4, force to use CONNECT HTTP/1.0
        /// </summary>
        CURLPROXY_HTTP_1_0 = 1,
        /// <summary>
        /// support added in 7.15.2, enum existed already in 7.10
        /// </summary>
        CURLPROXY_SOCKS4 = 4,
        /// <summary>
        /// added in 7.10
        /// </summary>
        CURLPROXY_SOCKS5 = 5,
        /// <summary>
        /// added in 7.18.0
        /// </summary>
        CURLPROXY_SOCKS4A = 6,
        /// <summary>
        /// Use the SOCKS5 protocol but pass along the
        /// host name rather than the IP address. added
        /// in 7.18.0
        /// </summary>
        CURLPROXY_SOCKS5_HOSTNAME = 7
    }


    [Flags]
    public enum CurlAuthFlags
    {
        /// <summary>
        /// No HTTP authentication.
        /// </summary>
        CURLAUTH_NONE = 0,
        /// <summary>
        /// HTTP Basic authentication (default).
        /// </summary>
        CURLAUTH_BASIC = 1 << 0,
        /// <summary>
        /// HTTP Digest authentication.
        /// </summary>
        CURLAUTH_DIGEST = 1 << 1,
        /// <summary>
        /// HTTP Negotiate (SPNEGO) authentication.
        /// </summary>
        CURLAUTH_NEGOTIATE = 1 << 2,
        /// <summary>
        /// Alias for CURLAUTH_NEGOTIATE.
        /// Deprecated since the advent of CURLAUTH_NEGOTIATE.
        /// </summary>
        CURLAUTH_GSSNEGOTIATE = CURLAUTH_NEGOTIATE,
        /// <summary>
        /// HTTP NTLM authentication.
        /// </summary>
        CURLAUTH_NTLM = 1 << 3,
        /// <summary>
        /// HTTP Digest authentication with IE flavour.
        /// </summary>
        CURLAUTH_DIGEST_IE = 1 << 4,
        /// <summary>
        /// HTTP NTLM authentication delegated to winbind helper.
        /// </summary>
        CURLAUTH_NTLM_WB = 1 << 5,
        /// <summary>
        /// Use together with a single other type to force no
        /// authentication or just that single type.
        /// </summary>
        CURLAUTH_ONLY = 1 << 31,
        /// <summary>
        /// All fine types set.
        /// </summary>
        CURLAUTH_ANY = ~CURLAUTH_DIGEST_IE,
        /// <summary>
        /// All fine types except Basic.
        /// </summary>
        CURLAUTH_ANYSAFE = ~(CURLAUTH_BASIC | CURLAUTH_DIGEST_IE)
    }

    [Flags]
    public enum CurlSshAuthFlags
    {
        /// <summary>
        /// All authentication types supported by the server.
        /// </summary>
        CURLSSH_AUTH_ANY = ~0,
        /// <summary>
        /// No authentication allowed.
        /// </summary>
        CURLSSH_AUTH_NONE = 0,
        /// <summary>
        /// Public/private key authentication using key files.
        /// </summary>
        CURLSSH_AUTH_PUBLICKEY = 1 << 0,
        /// <summary>
        /// Password authentication.
        /// </summary>
        CURLSSH_AUTH_PASSWORD = 1 << 1,
        /// <summary>
        /// Host-based authentication using host key files.
        /// </summary>
        CURLSSH_AUTH_HOST = 1 << 2,
        /// <summary>
        /// Keyboard-interactive authentication.
        /// </summary>
        CURLSSH_AUTH_KEYBOARD = 1 << 3,
        /// <summary>
        /// Agent-based authentication (ssh-agent, Pageant, etc.).
        /// </summary>
        CURLSSH_AUTH_AGENT = 1 << 4,
        /// <summary>
        /// Default SSH authentication methods.
        /// Alias for CURLSSH_AUTH_ANY.
        /// </summary>
        CURLSSH_AUTH_DEFAULT = CURLSSH_AUTH_ANY
    }

    [Flags]
    public enum CurlGssApiDelegationFlags
    {
        /// <summary>
        /// No delegation (default).
        /// </summary>
        CURLGSSAPI_DELEGATION_NONE = 0,

        /// <summary>
        /// Delegate credentials if permitted by policy.
        /// </summary>
        CURLGSSAPI_DELEGATION_POLICY_FLAG = 1 << 0,

        /// <summary>
        /// Always delegate credentials.
        /// </summary>
        CURLGSSAPI_DELEGATION_FLAG = 1 << 1
    }

    public enum CurlErrorSize
    {
        CURL_ERROR_SIZE = 256
    }
}
