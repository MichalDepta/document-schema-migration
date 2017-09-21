# document-schema-migration

This repository contains a small PoC of incremental Mongo DB schema updates.
The solution contains 3 projects:
- Models - contains definitions of data models representing musicians
- DataAccess - contains Mongo DB access infrastructure and mapping configuration
- Tests - contains unit tests which verify automatic incremental schema updates

The model of a musician is versioned and tests verify that simple schema changes can be coped with using mapping configuration.
