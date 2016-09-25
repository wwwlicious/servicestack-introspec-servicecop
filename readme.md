# Servicestack Robocop

### You have 15 seconds to comply... ;)

Robocop is a (micro)service validator for servicestack

## Quickstart

install the package 


## Notes

Functions:

1. intercepts new service registrations (from consul)
2. Checks registration against rules list
3. Fall back to default rules (If no specific rules for service like exceptional cases)
4. On failure either hold for review, notify service contact or if contact missing, notify robocop admin
5. Provides failure log
6. Provides success log
7. 

Types of rules

1. [Naming] all requests postfixed 'request' regex?
2. [Naming] all responses postfixed 'response'
3. [Naming] complexity, no single word requests *Service*Request is likely to be too simplistic and cause collisions
3. [Naming] request naming conflict detection (use of oversimplistic requests like 'status')
3. [List] no unbounded enumerable results, looks for skip/take or page/size or index/
4. [List] no unbounded enumerable properties
5. [Plugins] min viable plugin list 
6. [Snapshots] no breaking changes
7. [Versioning] semantic versioning ??
8. [Documentation] min viable documentation
9. [Security] min viable security
10. [Dependency] track and visualise service dependency
13. [Dependency] warn about external requests with no active service

11. [Custom] Arbitrary json key : value checks on service definitions


All customisable rules stored as config (which can use consul)

Hold functionality - ability to require human check/exception if service fails non-absolute rules

Non-absolute rules (scoring?)

health/perf checks (warnings when service reponse rates drop)


plugin checks

1. introspec (a must for contact details and service documentation/snapshot)
   2. Add to introspec, the scanning of any external requests and list them (this will help with dependency checks)

introspec will list the plugins registered which should include configurable list

ours is

identity service (security)
consul config
consul discovery (duh how would we know it registered otherwise)
ratelimit

Test endpoints - outputs validation report for pre-release/dev checking

1. url
2. introspec json

