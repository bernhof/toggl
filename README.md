*This library has just begun development, and lacks maturity in tests, API surface coverage and more. Issues & pull requests are welcome! :-)*

# Toggl .NET Client
A [.NET Standard 1.3](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) client library for v9 API for Toggl time tracking (http://toggl.com). For certain operations, v8 API is still used, since I haven't yet found a v9 equivalent.

Note that V9 of the Toggl API is currently [still in development](https://github.com/toggl/toggl_api_docs/blob/master/api_v9_repots_v3_basics.md). See [toggl/toggl_api_docs](https://github.com/toggl/toggl_api_docs) for Toggl's official API documentation. Most still only covers the v8 API currently.

This library is also planned to provide coverage for the [Reports API v2](https://github.com/toggl/toggl_api_docs/blob/master/reports.md). Currently, only [Detailed](https://github.com/toggl/toggl_api_docs/blob/master/reports/detailed.md) reports are supported, but the Weekly and Summary reports will soon follow.

# Asynchronous
This library is **built for asynchronous code** and it's recommended that you use asynchronous methods whereever available. The library  does, however, provide auto-generated synchronous methods for convenience in some cases.

# License
[MIT License](https://github.com/bernhof/toggl/blob/master/LICENSE)

# Acknowledgements
* Thanks to [Toggl](https://github.com/toggl) for keeping most of their development efforts open source. [toggl/mobileapp](https://github.com/toggl/mobileapp) has provided some code and inspiration for this project.
* A .NET client for Toggl v8 API, [sochix/Toggl.NET API](https://www.nuget.org/packages/TogglAPI.Net/) which was forked from [johnbabb/C--Toggl-Api-Client](https://github.com/johnbabb/C--Toggl-Api-Client)
