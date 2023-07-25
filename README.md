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

```structurizr
workspace {
    !identifiers hierarchical

    model {
        properties {
            "structurizr.groupSeparator" "/"
        }

        group "AcmeInsurance" {
            AcmeInsurance_Claims = softwareSystem "AcmeInsurance.Claims" {
                Database = container "Database" {
                    description "Stores claims and claim criteria"
                    technology "SQL Server Database"
                    
                    group "AcmeInsurance.Claims.Database.Tables" {
                        Provider = component "Provider" {
                            description "Id, Code, Name, IsInNetwork, IsPreferred"
                            technology "Table"
                        }

                        ClaimStatus = component "ClaimStatus" {
                            description "Id, Value"
                            technology "Table"
                        }

                        Claim = component "Claim" {
                            description "Id, PatientName, ProviderId, Amount, HasPreApproval, ClaimStatusId"
                            technology "Table"
                            -> ClaimStatus "ClaimStatusId = Id"
                            -> Provider "ProviderId = Id"
                        }

                        Criteria = component "Criteria" {
                            description "Id, DenialMinimumAmount, RequiresProviderIsInNetwork, RequiresProviderIsPreferred, RequiresClaimHasPreApproval"
                            technology "Table"
                        }
                    }

                    StoredProcedures = component "Stored Procedures" {
                        description "Provides access to the Claims database"
                        technology "Stored Procedures"
                        -> Claim "Manages claim data in"
                        -> Criteria "Manages criteria data in"
                    }
                }

                Data = container "Data" {
                    description "Provides access to the Claims Database"
                    technology ".NET Class Library"

                    group "AcmeInsurance.Claims.Data.DataAccess" {
                        Dal = component "Dal" {
                            description "Manages access to the Claims database"
                            technology "class"
                            -> Database "Accesses"
                            -> Database.StoredProcedures "Calls"
                        }
                    }

                    Dtos = component "Dtos" {
                        description "Represent data from the Claims database"
                        technology "classes"
                    }

                    ClaimRepository = component "ClaimRepository" {
                        description "Manages claims"
                        technology "class"
                        -> Dal "Accesses the database using"
                        -> Database "Manages claims data in"
                        -> Database.StoredProcedures "Manages claim data using"
                        -> Dtos "Returns"
                    }

                    CriteriaRepository = component "CriteriaRepository" {
                        description "Manages criteria"
                        technology "class"
                        -> Dal "Accesses the database using"
                        -> Database "Manages criteria data in"
                        -> Database.StoredProcedures "Manages criteria data using"
                        -> Dtos "Returns"
                    }
                }

                Models = container "Models" {
                    description "Provides models for criteria"
                    technology ".NET Class Library"

                    ClaimModel = component "ClaimModel" {
                        description "Represents a claim"
                        technology "class"
                    }

                    CriteriaModel = component "CriteriaModel" {
                        description "Represents criteria"
                        technology "class"
                    }
                }

                Business = container "Business" {
                    description "Provides business logic for claims"
                    technology ".NET Class Library"

                    ClaimBl = component "ClaimBl" {
                        description "Manages claims"
                        technology "class"
                        -> Data "Accesses claims data using"
                        -> Data.ClaimRepository "Accesses claims data using"
                        -> Data.Dtos "Converts to/from"
                        -> Models.ClaimModel "Converts to/from and returns"
                    }

                    CriteriaBl = component "CriteriaBl" {
                        description "Manages criteria"
                        technology "class"
                        -> Data "Accesses criteria data using"
                        -> Data.CriteriaRepository "Accesses criteria data using"
                        -> Data.Dtos "Converts to/from"
                        -> Models.CriteriaModel "Converts to/from and returns"
                    }
                }

                group "AcmeInsurance.Claims.CriteriaManager.Web" {
                    Web_CriteriaManager = container "Web.CriteriaManager" {
                        description "Allows Managers to enter criteria used to approve or deny a claim"
                        technology "ASP.NET MVC"

                        ViewModels = component "ViewModels" {
                            description "Used to pass criteria data between the views and controllers"
                            technology "classes"
                        }

                        Views = component "Views" {
                            description "Allows criteria to be viewed and edited"
                            technology "Razor"
                            -> ViewModels "Displays data from and stores data in"
                        }

                        group "AcmeInsurance.Claims.Web.CriteriaManager.Controllers" {
                            CriteriaController = component "CriteriaController" {
                                description "Manages criteria"
                                technology "Controller class"
                                -> Business.CriteriaBl "Manages criteria using"
                                -> Models.CriteriaModel "Converts ViewModels from/to"
                                -> ViewModels "Converts Models from/to"
                                -> Views "Displays criteria and allows criteria to be entered using"
                            }
                        }
                    }
                }

                group "AcmeInsurance.Claims.WebServices" {
                    WebServices_Api = container "WebServices.Api" {
                        description "Enter new claims and check the status of existing claims"
                        technology "ASP.NET Web API"

                        group "AcmeInsurance.Claims.WebServices.Controllers" {
                            ClaimsController = component "ClaimsController" {
                                description "Manages claims"
                                technology "ApiController class"
                                -> Business.ClaimBl "Manages claims using"
                                -> Models.ClaimModel "De/serializes JSON from/to"
                            }
                        }
                    }

                    WebServices_Proxy = container "WebServices.Proxy" {
                        description "Enter new claims and check the status of existing claims"
                        technology ".NET Class Library/NuGet Package"

                        ClaimProxy = component "ClaimProxy" {
                            description "Manages claims"
                            technology "class"
                            -> WebServices_Api.ClaimsController "Calls" "JSON/HTTPS"
                            -> Models.ClaimModel "De/serializes JSON from/to and returns"
                        }
                    }

                    WebServices_Proxy_Tests = container "WebServices.Proxy.Tests" {
                        description "Tests the ClaimProxy"
                        technology "MSTest Unit Test Project"

                        ClaimProxyTests = component "ClaimProxyTests" {
                            description "Tests the ClaimProxy"
                            technology "class"
                            -> WebServices_Proxy.ClaimProxy "Tests"
                            -> Models.ClaimModel "Uses"
                        }
                    }
                }

                group "AcmeInsurance.Claims.Services" {
                    Services_DeciderService = container "Services.DeciderService" {
                        description "Decides whether to approve or deny a claim"
                        technology "Windows Service"

                        Service = component "Service" {
                            description "Decides whether to approve or deny a claim"
                            technology "class"
                            -> Business "Manages claim status using"
                            -> Business.ClaimBl "Manages claim status using"
                            -> Business.CriteriaBl "Lists criteria using"
                            -> Models "Uses"
                            -> Models.ClaimModel "Uses"
                            -> Models.CriteriaModel "Uses"
                        }
                    }
                }
            }

            Manager = person "Acme Insurance Manager" {
                description "Manages criteria used to approve or deny a claim"
                -> AcmeInsurance_Claims.Web_CriteriaManager.Views "Manages criteria using" "HTTPS"
            }
        }
    }

    views {
        container AcmeInsurance_Claims {
            properties {
                "structurizr.softwareSystemBoundaries" "true"
                "structurizr.enterpriseBoundary" "true"
            }
            include element.parent==AcmeInsurance_Claims.Services_DeciderService
            include element.parent==AcmeInsurance_Claims.WebServices_Proxy_Tests
            include element.parent==AcmeInsurance_Claims.WebServices_Proxy
            include element.parent==AcmeInsurance_Claims.WebServices_Api
            include element.parent==AcmeInsurance_Claims.Web_CriteriaManager
            include element.parent==AcmeInsurance_Claims.Business
            include element.parent==AcmeInsurance_Claims.Models
            include element.parent==AcmeInsurance_Claims.Data
            include element.parent==AcmeInsurance_Claims.Database
            include element.parent==AcmeInsurance_Claims
            include element.type==person
            include element.type==softwareSystem
            include element.type==container
            include element.type==component
        }
    }
}
```

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

![AcmeInsurance.Claims.Database component diagram](https://kroki.io/structurizr/svg/eNq9WNtu4zYQffdXsHxOlS_IAo5TYANsAjdO08cFLY0TohKpklRcb5F_75C6SzQlJenqwUhIzXDmzDlDUkep_tI5i4H8uyL4_MITEIYfOChNXvCXqfiFxyxduelMJpBWr9onVzIHZTjozqB9qDaqiE2h-A8VPStZ5DvImWJGKkroJW1efls1f7rXCF3HGdwKXSgmYqADv73J75uU8UyTK6LlwRyZgt1JG8gGPqLytaEr-9www_ZMA7qIpTCMC1CE1qM-C_skoGPFc8OlIHSHKWH6cRkKE0n5J8E3DCjOqNeFgfhFyFQ-n9DD79_IDtQrLt2s7DXyDvpQqzKOanfRI9unoM_lY5-tkq9Ye-WQyHIpkAaE1qMhyxEit8kF2SBRLsg9y_D3Vt-KezBHpJr9Z6vgAEpBQoMuuwi58M-_3uHQ8HFA7Awzhe5n1plYnNwTSwv4adF74l4c8ZahRoUp61EX1U6sM1kIc0G-MluXdY6CfmXpRRe3288rlH1-_dIrCu2thLlOLYf2DVdpm8qEZQjkSqgDnGv5LoX6BgRn6R0XPCuyGt4H-Lvg2CaaeLuSGE82EmknHUz9Kn0iAxdA5vpdgrHGkBS28_VgK2dJOx3Cr4ddlT220DgGrYmRxLwAqVp8EmyMo446imIVomNNAHrHBHu2zbwescsSLuhqHmweyGwLHu8us3aWMCLhraKLRnT_26O1Qhff-B539BNdvWsrcT9rF0yorDcs7ZMCBxbJqC7DO5kwzD-2qU82leYoQMsMYb5JNNIE3bA01Z-kuBsjByqzI7OF9QA5hmTtHJsPSmbvV5bD8hw0Z-Kv9YWBSM0RqpO_1bbzs3MbCnZeEp2FVsHypi0ZHGQ1VqTQXDxPGNd0WtZUpqnl9zcjIksjzB0P5UJ_oKPd2TuA7ve0cmxZV8tKPwepFpyVZ_eymlQuMj_f3NRyGS1lW6D9zML72pbW9sEe4vXoMsz3tS8Mjccl9uXl6PORvz4D-3X6P8t7ot9X6upIe7GS7F7o6Wof9FgKdCMF3gSN3fYuba8O2pW6i_pkH7lwt1L1YeGHjgdNBK5iKvoT9ueqjFPfB6_3qY0vDP3NZsw6TeVRk8rMnR2QfNC2GCwGngxxmLlTNBCUQALiRFiphHkUW--2Tg93Txt6_mbxxOHY6ZaNEtrxRceiP6rIc6vBPsP2eJcAEG6TekXv1YcIxFTJNEUgFp6WQL_rKvXklh7luizNqoRNgpjxvswK07dpQYIzCy6lD-yHVJOHuU6x6A3XecpOunNisgvr8lPP5P4dQCikIQ_vo02nhBMg1qatib__tvNTHj_Qj3116Cw8aYcFqXe4qLOhjA8_E711fq_s1N8W_NLIWV67tGl8vdNPl3lNgpZ4zCsJ19hQEzMgeFt8EwmPDBjetvEciepEjAQnDcNJoI0HCba8WyOUvm_EkWfXiSrMz3CK0K-Pj9sdXflhKP-qgCh7bptcKztvMM3loA_H2a_poy_q_e_d17IQCVPcfm2h-JbnLtezdsTJFddQWZ68dv2ScxGnRYKsSyFDB1GOywtzdRVMsOcB_ik9BC1GF55gEOaUw9VVH41Rhd7-AzULd1A=)

User Story #212032: bit502 Final Project - Part 2: ACME Insurance Claim
Criteria Website
------------------------------------------------------------------------

> - Create an MVC website where managers at Acme Insurance will enter
>   the criteria used to approve or deny a claim.
> - This site should NOT use the Waystar template, as it's not a Waystar
>   site.
> - It should have a "New / Edit" view and a "Read Only" view of the
>   data.
> - It should allow viewing / editing the following criteria:
>   - Claim Amount (This is the amount the claim must be less than to be
>     approved)
>   - Claim Network Status (In / Out of Network)
>   - Preferred Provider (true / false)
>   - Pre-Approval Obtained (true / false)
> - It should save the data to a database (Acme Claims Database - Claim
>   Criteria Table).
> - You can enter MUTLIPLE criteria - each will be a row in the Claim
>   Criteria Table.
>   - Example: You could enter:
>     - 100.00, In Network, False, False
>     - 500.00, In Network, True, False
>     - 300.00, Out of Network, True, True
>   - The claim will be approved by the Decider Service if the Claim
>     meets any of the entered criteria (that's another part of the
>     assignment).

![AcmeInsurance.Claims.Web.CriteriaManager component diagram](https://kroki.io/structurizr/svg/eNrNWNtu4zYQffdXsHzOKl_gBRynwBrYBG6cpo8LWhonRCVSJam43iL_3iF1l2hdHKOoHoyE5IxmzpwzJHWU6k-dshDIPwuCzy88AmH4gYPS5A1_mQrfeMjihZtOZARxsdQ-qZIpKMNBNwbtQ7VRWWgyxX-q4FXJLN1ByhQzUlFCb2m1-GNR_emWEboKE9gInSkmQqAdv63JH-uY8USTJdHyYI5Mwe6kDSQdH0G-rOvKPvfMsD3TgC5CKQzjAhSh5ajPwj4R6FDx1HApCN1hSph-mIfCRJT_SXCFAcUZ9bowEL4JGcvXE3r47TvZgXrHV1dv9hp5B32oFRkHpbvgme1j0Ofysc9WyXesvXJIJKkUSANCy9Ehyx4im-iGrJEoN-SRJfi70RvxCOaIVLP_bBUcQCmI6KDLJkIu_PPLGxzqPg6InWEm0-3MGhOzk3thcQb_WfSeuGdHvGWoUWHyepRFtROrRGbC3JBvzNZllaKg31l808Rtc71C2efL11ZRaOtNmOvY69C-4iqtUxmxHAK5EGoH51K-c6G-B8FZ_MAFT7KkhPcJ_so4tokq3qYk-pOVROpJB1O7Sldk4AzIXL-LMNYQosx2vhZs-Sypp4fwa2FXZI8tNAxBa2IkMW9AihYfDTbGXkftRbEYoyOhD0yw17KTuxcSLoYNS-bUtuXIoHkfbw_Wtnf3t6VJW9IwlMN7TBPG4PHXZ2uFLr7zPR4FTnRx0R7kflYumCE-3LO4zSYcmKW_sgwXUqibf2hTH-1G1RmC5hnCdJOgJya6ZnGsryTVeyM78rQjkxX5BCmGZO0cmw9KJpdL0mF5Dpoz8Zf6wkCk5gjVyd-j6_nJuXUFOyOJwZZgKVzxwKFVwkQyzcXriHHJpHn9ZJxVfn8TIrIMQiLgQV7oTzSzB3tv0O12lo_Na2hJ7ucg1Yzz9eQ2VvLJReanmpuar6ArEm0S3ne2tLYFthAvR-dhvi99YWg8zLHPL1TXR_7uDOx38f9A2aiYWtqzlWS3QU9D-6THXKBrKfD2aOyOd2vb9KBdrrugTfaeC3eTVZ8W_tDJoIrAVUwFf8D-XJVx6kdneZvauKDrbzJjVnEsj5oUZu7YgOSDusVgMfA0icPMnbyBoAQiECfCciVMo9hqt3V6eHhZ0_O3kRcOx0a3rJRQj886Ef1eRJ5aDbYZtsf7B4Bwm9Q7ei8-XiCmSsYxAjHzoAT6ouvXi3t1L9d5aRYlrBLEjPd5Vpi-TQsinJlxkX1iP6UaPcc1ikXvuU5jdtKNw5J9sc4_D43u3wMIDWnIw_tg3SjhCIilaW3i77_1_JjHT_RjXx0agU04jRdlKfe5oLGt9I9AIx12esdssMCW_dbISV6b5Kl8Xeinyb8qQUs_5hWGa2-ojAkQfMy-igyPdHheN_MU6eqkjDQnFc_JQDMfpNn8no1Q-r4uB569JygwP8MpQr89P293dOGHIf-rACLvvHVytfimBtNB5uwn-d5n-fZH8zuZiYgpbj_ZUFzlude1rB2HUsU1FJYnr127-lyEcRYhAWNI0EGQ4uuFWS6n5jrozJxSWC5zHk1YWCHdWgt_52u9EVU3LPf_l6-9kn78C0JLk5o=)

User Story #212033: bit502 Final Project - Part 3: ACME Insurance - New
Claim API
------------------------------------------------------------------------

> - Create an API that can be called to send a claim into Acme
>   Insurance.
> - Required fields in the Request are:
>   - Patient Name
>   - Provider
>   - Claim Amount
>   - Pre-Approval Obtained (true / false)
> - It will write the claim to a database (Acme Claims Database - Claims
>   Table).
> - It will return the Claim ID.
> - Set up the API in IIS.
> - Create an API Proxy and Nuget package in your Local Nuget.
> - You should set up Unit Tests for your API Proxy.
> - **HINT / IMPORTANT NOTE:** We are trying to use Unity to do
>   Dependency Injection. In doing that, we are supposed to work through
>   the Interfaces. HOWEVER, your front-end API request that takes in
>   your Model CANNOT reference the Interface - it won't work. Instead,
>   just reference the model itself. In other words, use THIS:
>
>   ```csharp
>   public int Post(ClaimModel claimModel)
>   ```
>
>   …NOT this:
>
>   ```csharp
>   public int Post(IClaimModel claimModel)
>   ```

![AcmeInsurance.Claims.WebServices.Proxy component diagram](https://kroki.io/structurizr/svg/eNrNWNuS4jYQfecrFD1PzBdMqpiZrSypHUJiNnmcEnIzKGNLLkkelk3x72nJGHzDF5hK4gfKSOp29-nTx5J3Sr-ZlHEgf08IXj-ICKQVGwHakC3-Ms23grN44qcTFUF8XOquVKsUtBVgSoPuosbqjNtMi-86eNUqS0NImWZWaUrolJ4WHyanW7-M0BlPYC5NppnkQGt-K5MvjzETiSH3xKiN3TEN4d5YSGo-gnxZ3ZW7nphla2YAXXAlLRMSNKHFaJuFuyIwXIvUCiUJDTElTJ_noTAZ5bcEV1jQgtFWFxb4VqpYve7Rw29fSAj6HR99enKrUetgG2rHjIPCXbBi6xjMpXzctdTqHWuvPRJJqiTSgNBitMuygcg8uiOPSJQ7smAJ_s7NXC7A7pBq7s9Swwa0hoh2uiwj5MO_vLzEofrlgQgts5mpZlaaGJ3cHyzO4F-LviXu0REvGfaotHk9iqK6iVmiMmnvyGfm6jJLsaHfWXxXxm3-cYVy148_VYpCK0_CXPseh_YnrtJzKj2Wh8kI6H1LR-ibQ5S55q5UIJ8l5-mualQqcYwWVYJzMIZYRewWyFHFos7eb4hGI4pJH-KEPjPJXgux8g8kQtLJMMBawHL60pTOQbLZjUW3DpZxCBafVs4KXXwRa3xd7enkKp30PzMfTFdBn1hcpQMOjGrHogZXcqCeP3ep93bM6T1H8wxhuEnQ6Ab6yOLYfFCvPVlV6y83MrilfocUQ3J2ns0brZLre8pjeQmaC_H7B2EUygjEad8i1ufJwVlV-tSMCL5TBBx1T_X3KBXwkMwI-dpjXDCoGly3ivRzqUWSBgTjSIO1x_2lNDfo17PbzpqqguVj4zQsyf1slB6x7RusXJ5FPqwWdvnx8e2C-pNDfjO5BgH94Grq5K4CdTE6Dux14QtDEzwHvaNProf8oQ3vh_g_72JskXMbl7twWBcHdcm6yVneio9K4vHFutfZ1Glwp13eYUGJ1g17f47SN_d31zv_T1i7Q5fglzdwpSUvs1RUyVuaDHByMCs-SdQHImFXOTJugb95STb5tlhtCHwTxmINRvFnFi49yTE6MlvO6eWzxUBsAiyN1SqOQfcelHLrs0FL-5Rm-7xd3U-tuKSiFNeATdORq4VEBYUi1F9_PW3STfsnmBr3sojFd_T4S_jrwu9gplZ1ezx8zN6rzG-U12_7ywz30_8TjjdVfLrIfgaLp1z-hrWhPQfqc6pVbvbmeBMjx2_aa_ITNNrruBEn1DFn-nm1Woa9TkexsF-Gb-fcywqMNT3MC_yiwfzLXZ7OAXlhB3HrOXS25KsUlvg7tP0LuB3EqXMmLcTqzeDGLG7nWI50qUXyCK6h1NfOE-Zh8Bu9_V9-dyTYu4Bd-evzGfy2b8VB_Z1ercfF79mNb9rVL84PKpMR08LtJSiuajlwVqzBCWSqhYGj5b7VrgqAkDzOIiAQQ4IOArtP4f7-lO-AtZibUbKB4-Efw3kYYw==)

User Story #212032: bit502 Final Project - Part 2: ACME Insurance Claim
Criteria Website
------------------------------------------------------------------------

> - Create an MVC website where managers at Acme Insurance will enter
>   the criteria used to approve or deny a claim.
> - This site should NOT use the Waystar template, as it's not a Waystar
>   site.
> - It should have a "New / Edit" view and a "Read Only" view of the
>   data.
> - It should allow viewing / editing the following criteria:
>   - Claim Amount (This is the amount the claim must be less than to be
>     approved)
>   - Claim Network Status (In / Out of Network)
>   - Preferred Provider (true / false)
>   - Pre-Approval Obtained (true / false)
> - It should save the data to a database (Acme Claims Database - Claim
>   Criteria Table).
> - You can enter MUTLIPLE criteria - each will be a row in the Claim
>   Criteria Table.
>   - Example: You could enter:
>     - 100.00, In Network, False, False
>     - 500.00, In Network, True, False
>     - 300.00, Out of Network, True, True
>   - The claim will be approved by the Decider Service if the Claim
>     meets any of the entered criteria (that's another part of the
>     assignment).

![AcmeInsurance.Claims.Services.DeciderService component diagram](https://kroki.io/structurizr/svg/eNrFWd2O2ygUvs9TsFxP3SfISpPJShtpZjTbdNvLirFPJqg2eAEnyq7m3QvYOMY_xHayLRdRApzj8_Od74Bz5OK7zEkM6L8F0uM3mgBTdEdBSLTXn0TEexqTdGGXM55AWm01Ixc8B6EoyMakGVgqUcSqEPRfEb0JXuRbyIkgiguM8Edcb35f1F_tNoTv4ww2TBaCsBhwS6-3-O0hJTSTaIkk36kjEbA9SQVZS0dUbmurMmNNFHklErSKmDNFKAOBsJvtkzAjARkLmivKGcJb7ZJ2Py5NISwpvyK9Q4GgBPeqUBDvGU_520lr-OsRbUEc9KPrJ_cK9U72Ra3yOHLqos_kNQU55I8ZL4IfdO6FjUSWc6ZhgLCbDUl2IrJJ7tCDBsodeiaZ_tzIDXsGddRQMz9eBOxACEhwUGUzQtb84e0NDLWHDcRWEVVI37PGwmTnvpC0gJ9mfY_dky1-IbpGmSrz4ZJqFu4zXjB1h_4kJi_3uS7oA0nvmnHb3C5RZnz43UsK9p6kfb30OC1fYxWfXbkgGQpyVaitOLvynRrqNTBK0ifKaFZkLryf4J-Capqo7W2WRHexLpHzog2Tn6UbInBCyCzfJdrWGJLCMJ8XtnIVnZdD8fNiV3mvKTSOQUqkOFJ7QBXFJ0Fi7DBqx4rFJTgi_EQYeXNMbh-IKAsLOuScZd1MULwb755YG-7utqVRLSkcynCPaYYxev7js5HSKh7pqz4KnPBiVg-yH_fWmBAe1iT10aQnJtWfS8NMCLX9j43rF9moPkPg0kMYLxJ1igk_kDSVNyrVteKt8jQzoyvyE-TaJCNn0bwTPJtfkjaWQ6EZsN8-SFvBJdVxOvU0wvPiaK-8MpcTjA9SgYFunX8bJRceVEjK3i4IOwT5xo0ioQCWehhthDEGNDr3-uzOJqarIr_BjHXWpycteKj-9WkbRf8TEufpu1HuRmTzyVzzpN99yrlp_Scr9ey4mHAdGt11LANYs3qYwc5PpzrdO8pyuRphF4qkz-7m0hzTb1Ydo0CyMng0bdaDiZudBpRXp0ubRuMSMAF-ng-XVR9WVukv7x66vM881GT_cTQUtVvlVcpKGnng7ABCmWPUR9P7g3IlO0SNkuzI23cj4oq-shqolznpu3kf8RM4lbdtCrvt80qNV-TRY6lZqRyR3NC1wbwQo_Hw_dGtf1tDbC7N1W-fjNymyN80Gi6lmETHPeizgTAXCmIv34A0QyXATtPaxVfKEn6Uzng8_FKi6c75cn3Z_P_FhXn3oro_tM6hsnzxcwHDLSWRY-_bKDszCn6kstE6R2qqDkj47zG3vR5unCTn12JY9H10Nfb_Kr9VtXqgcGy-2D9Dse81fDRUkj5aB_8y6Pxt4L_UX_GCJURQQwlY7-q5d3rS2kwQuaASKslTr5wfCMritEgAQQqZVhDl-vFMLZdT_J2v0OFzvoYSMkF5dcphuaxJspP59x9hk7JD)