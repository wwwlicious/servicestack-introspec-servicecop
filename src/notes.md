# some notes

The project must do these main things

* Monitor service catalog for newly registered services
* Run ad-hoc validation, directly, or by URL 
* disable non-conforming services
* have configurable rules

The main components

* Service provider - interacts with the global catalog
* Validation, two main types
  * Instance validation - is a single service well-formed
  * Snapshot validation - is the service valid from a historical and global perspective
* Snapshot Provider - the ability to store and retrieve DTO contracts

General comments

* An IntroSpec document needs to be broken down into each DTO contract but the service id/name and version is contextually important so must be added to each
* Validation using FluentValidation is static so configuring it is awkward
* Some validation rules may require configuration (pre/post fixes, naming lengths etc)
* All rules require turning off and on
* Some rules are best-practice (pre/post), some are not (contract breaking changes). These should perhaps be handled differently
* A design-time analyzer could only validate an instance, not a snapshot
* Snapshot validation requires an introspec document which is only available at runtime

How to load rule config?

* ~Config file (appsettings). Could get rather large, trying to avoid custom sections!~
* ~Ruleset - suitable for analyzers but not much else~
* Bespoke - Simpler as DTO can just be serialized to a format (yaml, json, xml)
* Dynamic props for specific rules could be awkward

Things rules should have

* Id - easy way to find it
* Name - nice name
* Category - 
* ~Text - brief explanation of the rule violation~
* Url - where more info on the rule can be found
* Severity - configurable? ... sometimes