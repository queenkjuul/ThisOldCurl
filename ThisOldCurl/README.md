# ThisOldCurl

![ThisOldCurl logo, like This Old House from PBS](/Images/ThisOldCurl.png)

An easy to use libCurl wrapper for .NET Framework 2.0 - in other words, the absolute easiest way to make HTTPS requests in your Windows 98 applications.

**BONUS:** [Windows 98 curl executables are available](./Executables)*

Seriously, one line:

`string response = SuperEasyCurl.Get("https://example.com");`

TLS 1.2, so the modern internet won't kick us off for a good few years yet.

# ***This project uses outdated libraries and should not be considered secure†***

***This library is only useful if you are targeting Windows 98/Me/2K. If you are targeting XP or later, use a more modern library. If you are using this to transmit important private data, rethink your life choices.***

I am an idiot, and I basically learned C# this week; this code is probably very bad, but it does what I want, which is provide a nice, simple, easy way to reach HTTPS servers from Windows 98. **Do not use it for anything important.**

Essentially the entire libcurl API is exposed in one way or another, but not everything is tested. For all basic use cases and even most advanced ones, this should do the job for you. There aren't full test suites, but there is a sample application that confirms most of the behavior.

Oh yeah, this is hard-coded as 32-bit, as you might expect for something targeting Windows 98.

*the noSSH executable is more reliable on 98 - the SSH build should work on 2k/XP

†specifically, [OpenSSL 1.0.2u](https://github.com/queenkjuul/openssl-1.0.2u-win32), LibSSH2 1.4.2, zlib 1.2.3, and [libcurl 7.42.1](./Source/curl-7.42.1)

## Usage

See [`./SampleApplication`](./SampleApplication/) for concrete examples.

_Choose Your Difficulty!_

### `SuperEasyCurl` _I'm too young to die!_

`string response = SuperEasyCurl.Get("https://example.com");`

That's it. You want data? get you some data.

There are also `SuperEasyCurl.Post()` and `SuperEasyCurl.Request()` methods. Everything is synchronous. Just provide an address and maybe a body and some headers, get back some response data.

Go ahead, block the main thread. It's Windows 98, nobody cares.

### `CurlWebRequest` _Hey, not too rough._

This aims to be a more-or-less complete implementation of the .NET `WebRequest` interface, backed by libcurl for executing the request and extracting response data. It supports the .NET 2.0 async APIs (`BeginGetRequest/EndGetRequest`) and uses standard Streams for accessing data, just like `HttpWebRequest` - for simple use cases, it aims to be a drop-in replacement.

There is one major difference that is worthy of note: `CurlWebRequest` will automatically copy response data directly into a `MemoryStream` by default. This is fine for a lot of use cases, but if you're downloading files, you probably don't want to be writing their entire contents to memory (especially on Windows 98, where you may be running with just a few megs of RAM). To account for this, `CurlWebRequest` exposes `ResponseStream` and `RequestStream` properties that should be set before calling `GetResponse()`. You could set them to a `FileStream` to direct output (or input) to a file, rather than memory.

`CurlWebRequest`, therefore, can't provide a 1:1 experience to `HttpWebRequest`, due to the lack of a `ConnectStream`-compatible wrapper. This could be done, passing data directly to and from the socket via libcurl `Send/Recv`, but this isn't a use case I'm interested in covering (if you do though, please contribute).

```csharp
CurlWebRequest request = CurlWebRequest.Create("https://example.com")
CurlWebResponse response = (CurlWebResponse)request.GetResponse();
StreamReader reader = new StreamReader(response.GetResponseStream());
string body = reader.ReadToEnd();
```

### `EasyCurl` _Hurt me plenty._

This is actually a pretty easy-to-use wrapper around the libcurl "easy API," which emits events whenever libcurl calls the `Write`, `Header`, `Debug`, and `Progress` (actually `XferInfo` under the hood) callbacks. It's not actually much harder to use than `CurlWebRequest` or `SuperEasyCurl`, the big difference is that it is drastically more powerful. `EasyCurl` exposes every libcurl option, and lets you replace the managed libcurl callbacks with your own. Doing so will break various parts of the C# EasyCurl API (including events and the input/output Streams), so make sure you know what you're doing.

A request body Stream can be provided before calling `Perform()`, as can a response body Stream. If no `UploadStream`/`DownloadStream` is supplied before calling `Perform()`, and additionally, no `WriteEvent` listeners are registered, your response data will just be disposed into the ether.

Request data can be set before calling `Perform()` by assigning the optional property `UploadStream`. This can be a `FileStream` or a `MemoryStream` that will be read in its entirely and uploaded.

A super simple example is not much more complicated than `SuperEasyCurl`:

```csharp
EasyCurl curl = new EasyCurl();
curl.URL = "https://example.com";
MemoryStream response = new MemoryStream();
curl.DownloadStream = response;
curl.Perform();
Console.WriteLine(Encoding.UTF8.GetString(curl.DownloadStream.ToArray());
```

but you can muck with things under the hood if you really feel like it:

```csharp
EasyCurl curl = new EasyCurl();
// doing this will prevent writes to DownloadStream as well as
// cancel all WriteEvent events! But you get raw access to the 
// data as it arrives instead, and can abort or pause by
// returning a CurlWriteCode
curl.SetOpt(
    CURLoption.CURLOPT_WRITEFUNCTION, 
    delegate(IntPtr data, UInt32 size, UInt32 nitems, IntPtr _user)
    {
        UInt32 bytes = size * nitems;
        byte[] buffer = new byte[bytes];
        Marshal.Copy(data, buffer, 0, (int)bytes);
        // now do something with the data in the buffer
        return bytes;
    });
curl.SetURL("https://example.com");
curl.Perform();
curl.Dispose();
```

### `MultiCurl` and `SharedCurl` _Ultra-Violence._

You can use `MultiCurl` to run multiple simultaneous transfers on a single thread. You can use `SharedCurl` to share resources across multiple requests. Create a `new SharedCurl()`, set its options, and assign it to `EasyCurl.ShareHandle` on several `EasyCurl` instances - they will all share the selected libcurl resources.

Only minimal testing has been done with running transfers on different threads with one `SharedCurl`. The underlying C code should handle it, though, so you can manually create and set a share handle instead of using the `SharedCurl` wrapper if you have issues (and please raise an issue here if you do).

`MultiCurl`, `SharedCurl`, and `EasyCurl` all provide a `Handle` property which provides the raw pointer to the underlying Curl handle. You can use that to work with the libcurl API directly - oh, speak of the devil...

### `Curl` _Nightmare!_

`ThisOldCurl.LibCurl.Curl` exposes a static class which contains P/Invoke function signatures for every external libcurl function. You can use these directly if you want. There's even [entirely untested] memory management callback definitions if you're doing something insane. It's up to you to handle the C/C# interop stuff (marshaling pointers, copying data in and out of unmanaged memory, working with libcurl structs, etc).

Functions outside of the `Curl.curl_easy_*` and `Curl.curl_multi_*` groups have mostly not been tested. You generally shouldn't need them, but they are provided just in case. If you find incorrect signatures/structs/etc, please open issues or PRs.

Rather than follow C# naming conventions, I used the original C names for everything I could. I also copied all of the comments from the original libcurl source code into the relevant portions of `LibCurl.*`. The libcurl documentation should align pretty well with this library.

## Purpose

Sometimes, you're not working with sensitive private data. For example, maybe you're just trying to check the weather [(on Windows 98)](https://github.com/queenkjuul/WindowsWeather), or listen to music [(on Windows 98)](https://github.com/queenkjuul/spotify97), or read some social media [(on Windows 98)](https://github.com/OmegaAOL/cerulean). In those cases, using an outdated version of OpenSSL from 2019 (1.0.2u) paired with an outdated version of Curl (7.42.1) is an acceptable way to fetch some data from the web.

And sometimes, you're a super smart C developer that everybody thinks is super cool, and you just use libcurl and the Win32 API directly, but I'm not a super smart C developer that everybody thinks is super cool; I'm a Java and TypeScript developer that learned Visual Basic in high school. So for people like me, I made this; this should make it super easy for anybody to build HTTPS-capable .NET applications targeting Windows 98, at least until TLS 1.2 gets turned off too.

At the end of the day, LibCURL.NET 1.3 is too old and CurlSharp is too new. Unlike those, ThisOldCurl doesn't map every option of the libcurl API directly to a named C# native property. Instead, we use the original C enums unmodified, and generally expose as much of the libcurl C API as possible. This lets experienced libcurl developers utilize the entire libcurl API from a C# application if they really want. The C# wrappers are primarily aimed at ease of use, and so only cover the most common use cases.

The upside is that absolute beginners can hit the ground running, and there are few limits on what types of applications you can build with the library. The downside here is that writing complex applications in idiomatic C# is not easy - you have to follow libcurl C patterns, and deal with some GC headaches, but the worst of it is handled for you by the handle classes, even if you decide to override much of their default behavior.

## Root CA certs

The library ships with a currently-valid (as of 1/3/2026) `cacert.pem` root certificates file. At some point in the future this will likely be out of date, so you can provide your own via `CurlHttpRequest.CaCertPath` or `EasyCurl.CaCertPath` (or `Curl.curl_easy_setopt(curl, CURLoption.CURLOPT_CAINFO, path)`). The included `cacert.pem` must be placed next to the `ThisOldCurl.dll` file in your built output. It's recommended to add it to your VS project and have it copied to the build folder automatically via the file properties (if you're not setting the path directly).

## Development

I did all the work in Visual Studio 2005 on Windows XP, but the project should open and likely even build just fine on modern Visual Studio. I do believe that in theory modern VS should build 98-compatible binaries, but I haven't tested this. In the curl source folder, you can find VS projects for various versions of Visual Studio - I used these to build the included `libcurl.dll` in VS2005 as well. [OpenSSL](https://github.com/queenkjuul/openssl-1.0.2u-win32) was built with Visual C++ 6.0 (`libeay32.dll` and `ssleay32.dll`). The zlib used to build those dependencies was prebuilt by the zlib project (v1.2.3). I unfortunately don't remember anything about how I got/built LibSSH2 (1.4.2) so it must have been easy.

The path to libcurl.dll can only be changed by rebuilding the library - so it's best to just put `libcurl.dll` right next to `ThisOldCurl.dll` (and `libeay32.dll` and `ssleay32.dll` need to be right next to `libcurl.dll` no matter what). You could do the Windows 98 thing and install `libcurl.dll, libeay32.dll, ssleay32.dll` into `System32` or whatever if you wanted I guess. I don't actually know how library resolution works to be honest.

## Windows 95

This should work with Windows 95, as long as it has [MattKC's dotnet9x](https://github.com/itsmattkc/dotnet9x) and WinSock2 (available on the dotnet9x page).

## Code of Conduct

1. be gay, do crime
2. be excellent to each other
3. no cops
