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

User Story #212031: bit502 Final Project - Part 1: Databases
------------------------------------------------------------

> - Create a new "Acme Claims Database".
>   - Within the database, create a table for "Claim Criteria". It will
>       have the following columns:
>     - Id
>     - Claim Amount
>     - Claim Network Status (In / Out of Network)
>     - Preferred Provider (true / false)
>     - Pre-Approval Obtained (true / false)
>   - Within the database, create a table for "Provider Status". It will
>       have the following columns:
>     - Id
>     - Provider Code
>     - Provider Name
>     - In-Network (true / false)
>     - Preferred Provider (true / false)
>   - Seed the Provider Status table as follows:
>     - 1, ANT, Anthem, true, true
>     - 2, HUM, Humana, true, false
>     - 3, UHC, United Healthcare, false, false
>     - 4, AC1, ACA Insurance 1, true, false
>   - Within the database, create table for "Claims". It will have the
>       following columns:
>     - Id (Claim ID)
>     - Patient Name
>     - Provider Code
>     - Claim Amount
>     - Pre-Approval Obtained (true / false)
>     - Claim Status (int - will get values from ClaimStatusDecode so we
>         have proper normalization)
>   - Within the database, create a table for "ClaimStatusDecode". This
>       will hold the values for ClaimStatus. It will have the following
>       columns:
>     - Id
>     - ClaimStatus
>   - Seed the ClaimStatusDecode with the following values:
>     - 1, pending
>     - 2, approved
>     - 3, denied
