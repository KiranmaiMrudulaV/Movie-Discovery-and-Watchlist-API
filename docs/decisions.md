# Architecture Decisions

Lightweight ADR log — short entries on notable decisions and why, kept updated as the project evolves.

## ADR-001: MongoDB via local Docker as the datastore

**Context:** Need a database for movies, users, and watchlists. Movie data (genres, caption languages) fits naturally as documents with embedded arrays rather than normalized relational tables.

**Decision:** Use MongoDB, run locally via Docker rather than a native OS install.

**Consequences:** Reproducible local setup (no machine-specific install steps), easy to tear down/reset, and a straightforward path to a managed equivalent (MongoDB Atlas or AWS DocumentDB) in a real deployment later.

## ADR-002: `src/` and `tests/` project layout

**Context:** Needed a folder structure for the solution.

**Decision:** Split into `src/MovieWatchlistApi` and `tests/MovieWatchlistApi.Tests` as sibling folders under the repo root, rather than one flat project folder.

**Consequences:** Matches the layout convention used in most real .NET solutions, keeps production code and test code clearly separated as the project grows.

## ADR-003: Controller-based Web API instead of minimal APIs

**Context:** `dotnet new webapi` defaults to the minimal API style (routes defined inline in `Program.cs`).

**Decision:** Use the `-controllers` template flag instead, giving a `Controllers/` folder with attribute-routed classes.

**Consequences:** Scales better to multiple resources (movies, users, watchlists) with a clear controller-per-resource structure, and matches the controller → service → repository layering this project is built around.

## ADR-004: Scoped as a single-instance service with a documented scaling path

**Context:** A production movie/streaming platform would eventually need caching, database replication/sharding, a CDN for media, and possibly a microservices split. None of that is warranted for this project's actual data volume.

**Decision:** Build this as a single, well-structured service. Implement the scaling practices that are correct at any scale (pagination on list endpoints, database indexes on searched fields, fully async I/O), but do not stand up distributed infrastructure (Kubernetes, sharded clusters, CDN) that the project's real scale doesn't need.

**Consequences:** Avoids over-engineering a demo project with unused infrastructure. The reasoning for how this *would* scale is documented separately in `SCALING.md` rather than built.
