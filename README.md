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

                    CriteriaRepository = component "CriteriaRepository" {
                        description "Manages criteria"
                        technology "Repository"
                        -> Dal "Accesses the database using"
                        -> Database "Manages criteria data in"
                        -> Database.StoredProcedures "Manages criteria data using"
                        -> Dtos "Returns"
                    }
                }

                Models = container "Models" {
                    description "Provides models for criteria"
                    technology ".NET Class Library"

                    CriteriaModel = component "CriteriaModel" {
                        description "Represents criteria"
                        technology "class"
                    }
                }

                Business = container "Business" {
                    description "Provides business logic for claims"
                    technology ".NET Class Library"

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
                                technology "Controller"
                                -> Business.CriteriaBl "Manages criteria using"
                                -> Models.CriteriaModel "Converts ViewModels from/to"
                                -> ViewModels "Converts Models from/to"
                                -> Views "Displays criteria and allows criteria to be entered using"
                            }
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
        component AcmeInsurance_Claims.Web_CriteriaManager {
            properties {
                "structurizr.softwareSystemBoundaries" "true"
                "structurizr.enterpriseBoundary" "true"
            }
            include element.parent==AcmeInsurance_Claims.Web_CriteriaManager
            include element.parent==AcmeInsurance_Claims.Business
            include element.parent==AcmeInsurance_Claims.Models
            include element.parent==AcmeInsurance_Claims.Data
            include element.parent==AcmeInsurance_Claims.Database
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

![AcmeInsurance.Claims.Web.CriteriaManager component diagram](https://kroki.io/structurizr/svg/eNq9WNtu4zYQffdXsHxOlS9wAccusAY2gRun6eOClsYJsRKpklQMb5F_75C6S7QuTnb1YCQkZzRz5pwhqZNU33XKQiD_LQg-v_EIhOFHDkqTV_xlKnzlIYsXbjqREcTFUvukSqagDAfdGLQP1UZlockU_6GCFyWzdA8pU8xIRQm9pdXi90X1p1tG6CpMYCt0ppgIgXb8tia_rWPGE02WRMujOTEF-7M2kHR8BPmyriv7bJhhB6YBXYRSGMYFKELLUZ-FfSLQoeKp4VIQuseUMP0wD4WJKP-T4AoDijPqdWEgfBUyli9n9PDXV7IH9Yavrt7sNfIO-lArMg5Kd8ETO8SgL-Vjn52Sb1h75ZBIUimQBoSWo0OWPUS20Q1ZI1FuyANL8Hert-IBzAmpZv_ZKTiCUhDRQZdNhFz4l5c3ONR9HBB7w0ym25k1JmYn98ziDH5Z9J64Z0e8Y6hRYfJ6lEW1E6tEZsLckC_M1mWVoqDfWHzTxG37eYWyz-9_tIpCW2_CXMdeh_YVV2mdyojlEMiFUDs4l_KdC_UGBGfxPRc8yZIS3kf4N-PYJqp4m5LoT1YSqScdTO0qfSIDZ0Dm-l2EsYYQZbbztWDLZ0k9PYRfC7sie2yhYQhaEyOJeQVStPhosDH2OmovisUQHUsC0Hsm2Itt5uWIfS3hgi6mweaBzLbg_u4yaWcZRmR4q2iiETz8-WSt0MVXfsAd_UwXV20l7mflghkq64bFbVLgwCwZlWW4kgnd_EOb-mhTqY4CNM8QppsEPU3QNYtj_UmK2xjZUZkdmSysR0gxJGvn2HxUMrleWQ7LS9BciL_UFwYiNUeozv5WW89Pzq0r2GlJNF60GCxvXJPBQVZiRTLNxcuIcUmneU1lnFp-fxMisjTC3PFQLvQHOtq9vQPodk_Lx-Z1tST3c5Rqxll5ci8rSeUi8_PNTc2X0Vy2DbSfSXjf2dLaPthCvBydh_mh9IWh8TDHPr8cfT7ydxdgv4t_srxH-n2hroa0ZyvJ7oWervZBj7lA11LgTdDYbe_W9upBu1x3QZvsPRfuVqo-LPyh40EVgauYCv6Bw6Uq49S3zvI2tXFB199kxqziWJ40Kczc2QHJB3WLwWLgyRCHmTtFA0EJRCDOhOVKmEax1X7n9HD_vKaXbxbPHE6NblkpoR6fdSz6u4g8tRpsM-yAdwkA4TapN_RefIhATJWMYwRi5mkJ9FVXqWf36l6u89IsSlgliBkf8qwwfZsWRDgz41L6yH5INXqYaxSLbrhOY3bWjROTfbHOP_WM7t8DCA1pyMP7YN0o4QiIpWlt4u-_9fyYxw_0Y18dGi8etcOClDtc0NhQ-oefkd46vVc26m8LfmvkJK9N2lS-rvTTZF6VoCUe80rCNTbUxAQI3mffRIZHOgyv23iKRHUiRoKTiuFkoI0PEmx-t0Yofd-IA8-uExSYX-AUoV-ennZ7uvDDkP9VAJH33Dq5WnZTg-kgc_HDeu_jevvT953MRMQUtx9eKK7yXOta1o5DqeIaCsuz165dfS7COIuQgDEk6CBI8fXCLJdTc73eWdkTrveQS_N6e3tS81pfXO29xl3hogRxMHZzTmG5zFU4YWGbPBMMqmNaTwfv_wPR-exQ)
