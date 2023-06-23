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

        group "Acme Insurance" {
            AcmeInsurance_Claims = softwareSystem "AcmeInsurance.Claims" {
                Database = container "Database" {
                    description "Stores claims and claim criteria"
                    technology "SQL Server Database"

                    group "Tables" {
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
                }
            }
        }
    }

    views {
        component AcmeInsurance_Claims.Database {
            properties {
                "structurizr.softwareSystemBoundaries" "true"
                "structurizr.enterpriseBoundary" "true"
            }

            include element.type==person element.type==softwareSystem element.type==container element.type==component
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
