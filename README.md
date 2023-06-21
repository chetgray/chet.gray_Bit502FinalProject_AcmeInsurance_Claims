Feature #212029: bit502 Final Project: ACME Claims and Waystar APIs and
Websites
========================================================================

> This is the Final Project for bit502.
>
> It will deal with creating databases, implementing APIs, calling APIs,
> creating API proxies, creating and installing Windows Services, and
> standing up MVC websites with and without the Arch MVC template.
>
> It will focus around entering claims into ACME Inc's Claim System from
> Waystar, decisioning those claims, and then getting the status of
> those claims.
>
> **\*\* SEE LINKS FOR INDIVIDUAL STORIES! \*\***
>
> - **Notes:**
>   - You should use Unity for Dependency Injection whenever possible.
> - **Helpful Hints:**
>   - While we want to use Unity for Dependency Injection, you don't
>     need to worry about using it for your Unit Test projects (for the
>     testing of your API Proxies).
>   - When using Unity, you may have some classes, such as
>     `BusinessLogic` classes, that you're changing often as you expand
>     your app. Rather than have to redo the Interface every few
>     minutes, I recommend just doing a `WhateverBL whateverBL = new
>     WhateverBL();` until you are done with your changes, THEN come
>     back and change any calls to the Business Logic methods to get it
>     from the `UnityBootstrapper`. Otherwise you'll be constantly
>     updating the Interface for your Business Logic (every time you add
>     a new method, etc.)
