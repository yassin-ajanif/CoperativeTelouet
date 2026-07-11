# Project architecture

Desktop app for **Coperative Telouet** (Gestion Commerciale + cold-storage bay rental).
Built with **Avalonia (MVVM)** on **.NET**, **EF Core**, **SQLite** for the MVP and
**PostgreSQL** later (see `database-schema.md` → *Portability*).

## Layers (3 + shared kernel)

```
UI (Avalonia) ──► Business ──► DataAccess ──► Domain
                       │                          ▲
                       └──────────────────────────┘
      (everyone may reference Domain; nothing references UI)
```

- **Domain** — pure POCO entities + enums. References nothing. Keeps entities clean and shareable.
- **DataAccess** — the **only** project that knows the database: `AppDbContext`, Fluent API configurations, EF migrations, repositories. Single place where the provider (`UseSqlite` → `UseNpgsql`) is chosen.
- **Business** — services, DTOs, business rules. Maps entities ↔ DTOs so entities never leave this layer.
- **UI** — Avalonia views/viewmodels. Talks to **Business** only; never references DataAccess or EF Core. Receives **DTOs**, never entities.

## Solution layout

```
CoperativeTelouet.sln
│
├── src/
│   ├── CoperativeTelouet.Domain/            ← shared kernel (no dependencies)
│   │   ├── Common/
│   │   │   └── BaseEntity.cs                 (Id, CreatedAt, UpdatedAt, CreatedByUserId)
│   │   ├── Entities/
│   │   │   ├── Tiers.cs
│   │   │   ├── Produit.cs / Service.cs / Categorie.cs
│   │   │   ├── MouvementStock.cs
│   │   │   ├── Charge.cs / TypeCharge.cs
│   │   │   ├── AppSettings.cs
│   │   │   ├── Client/      (Devis/BonCommande/BonLivraison/Facture/Paiement/Avoir + Lignes)
│   │   │   ├── Fournisseur/ (Devis/BonCommande/BonReception/Facture/Paiement/Avoir + Lignes)
│   │   │   └── Stockage/
│   │   │       ├── ChambreFroide.cs
│   │   │       ├── VarietePomme.cs
│   │   │       ├── StockBacsSociete.cs
│   │   │       ├── BonEntreeStockage.cs
│   │   │       └── BonSortieStockage.cs
│   │   └── Enums/
│   │       ├── TypeTiers.cs (Client|Fournisseur|LesDeux)
│   │       ├── TypeMouvement.cs
│   │       ├── ModePaiement.cs
│   │       └── EtatBac.cs   (Vide|Plein)
│   │
│   ├── CoperativeTelouet.DataAccess/         ← EF Core + repositories (refs Domain)
│   │   ├── AppDbContext.cs
│   │   ├── Configurations/                   (Fluent API: CHECK, HasPrecision, HasConversion<string>)
│   │   ├── Repositories/
│   │   │   ├── IRepository.cs                (generic CRUD contract)
│   │   │   ├── Repository.cs                 (EF implementation)
│   │   │   └── (custom repos only when needed, e.g. IBonEntreeStockageRepository)
│   │   ├── Migrations/
│   │   └── DependencyInjection.cs            (AddDataAccess: UseSqlite(...) ← single swap point)
│   │
│   ├── CoperativeTelouet.Business/           ← services, DTOs, rules (refs Domain + DataAccess)
│   │   ├── DTOs/                             (read models + Create/Update command DTOs)
│   │   ├── Mapping/                          (AutoMapper profiles, e.g. StockageProfile.cs)
│   │   ├── Services/
│   │   │   ├── IGenericService.cs
│   │   │   ├── GenericService.cs             (concrete; uses IMapper)
│   │   │   └── (StockageService, FacturationService, ... — only entities with custom logic)
│   │   └── DependencyInjection.cs            (AddBusiness: AddAutoMapper + registrations)
│   │
│   └── CoperativeTelouet.UI/                 ← Avalonia (MVVM); refs Business only  [DEFERRED]
│       ├── App.axaml / App.axaml.cs          (composition root: AddDataAccess + AddBusiness)
│       ├── Views/ · ViewModels/ · Assets/
│       └── appsettings.json                  (connection string)
│
└── tests/                                    ← [DEFERRED]
    ├── CoperativeTelouet.Business.Tests/     (mock IRepository → test rules without a DB)
    └── CoperativeTelouet.DataAccess.Tests/   (SQLite/InMemory integration)
```

> **Status:** Domain, DataAccess, and Business are implemented. UI and unit tests come later.
> Wire-up from a future Avalonia app: `services.AddDataAccess(connectionString); services.AddBusiness();`

## Dependency rules

- **Domain** references nothing.
- **DataAccess** → Domain.
- **Business** → Domain + DataAccess.
- **UI** → Business (**not** DataAccess/EF).
- Provider choice (`UseSqlite`/`UseNpgsql`) lives only in `DataAccess/DependencyInjection.cs`, so the PostgreSQL migration stays a one-line swap.

## Generic repository (DataAccess)

Standard CRUD for every entity — no per-table repository unless custom queries are needed.

```csharp
// DataAccess/Repositories/IRepository.cs
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
```

```csharp
// DataAccess/Repositories/Repository.cs
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db;
    protected DbSet<T> Set => Db.Set<T>();

    public Repository(AppDbContext db) => Db = db;

    public async Task<T?> GetByIdAsync(int id) => await Set.FindAsync(id);
    public async Task<IReadOnlyList<T>> GetAllAsync() => await Set.AsNoTracking().ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        Set.Add(entity);
        await Db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        Set.Update(entity);
        await Db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var e = await Set.FindAsync(id);
        if (e is not null) { Set.Remove(e); await Db.SaveChangesAsync(); }
    }
}
```

## Generic business service (Business, DTO-aware)

Generic CRUD with **AutoMapper** doing the entity ↔ DTO mapping. The base is **concrete**
(no abstract members) — it takes an `IMapper` and works for any entity out of the box, so
**simple tables need no subclass at all**: register the closed generic and add a mapping profile.

`TEntity` is part of the interface so the service can be registered as an **open generic** in DI.

```csharp
// Business/Services/IGenericService.cs
public interface IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
{
    Task<TDto?> GetByIdAsync(int id);
    Task<IReadOnlyList<TDto>> GetAllAsync();
    Task<TDto> CreateAsync(TCreateDto dto);
    Task UpdateAsync(int id, TUpdateDto dto);
    Task DeleteAsync(int id);
}
```

```csharp
// Business/Services/GenericService.cs
public class GenericService<TEntity, TDto, TCreateDto, TUpdateDto>
    : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
{
    protected readonly IRepository<TEntity> Repo;
    protected readonly IMapper Mapper;

    public GenericService(IRepository<TEntity> repo, IMapper mapper)
    {
        Repo = repo;
        Mapper = mapper;
    }

    public async Task<TDto?> GetByIdAsync(int id)
    {
        var e = await Repo.GetByIdAsync(id);
        return e is null ? default : Mapper.Map<TDto>(e);
    }

    public async Task<IReadOnlyList<TDto>> GetAllAsync()
        => Mapper.Map<IReadOnlyList<TDto>>(await Repo.GetAllAsync());

    public async Task<TDto> CreateAsync(TCreateDto dto)
        => Mapper.Map<TDto>(await Repo.AddAsync(Mapper.Map<TEntity>(dto)));

    public async Task UpdateAsync(int id, TUpdateDto dto)
    {
        var e = await Repo.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        Mapper.Map(dto, e);           // apply changes onto the tracked entity
        await Repo.UpdateAsync(e);
    }

    public Task DeleteAsync(int id) => Repo.DeleteAsync(id);
}
```

## Mapping profiles (AutoMapper)

Mapping lives in `Business/Mapping` profiles — no hand-written `ToDto`/`ToEntity`.
Name-matching members map automatically; only the exceptions need `ForMember`.

```csharp
// Business/Mapping/StockageProfile.cs
public class StockageProfile : Profile
{
    public StockageProfile()
    {
        // simple tables — convention maps everything by name
        CreateMap<VarietePomme, VarietePommeDto>();
        CreateMap<CreateVarietePommeDto, VarietePomme>();
        CreateMap<UpdateVarietePommeDto, VarietePomme>();

        // complex entity — read model is convention; server-computed fields are ignored on write
        CreateMap<BonEntreeStockage, BonEntreeStockageDto>();
        CreateMap<CreateBonEntreeStockageDto, BonEntreeStockage>()
            .ForMember(d => d.NumeroLot, o => o.Ignore())                 // generated by service
            .ForMember(d => d.PrixParBacParJourApplique, o => o.Ignore()) // snapshot by service
            .ForMember(d => d.BacsVidesAvant, o => o.Ignore())
            .ForMember(d => d.BacsVidesApres, o => o.Ignore())
            .ForMember(d => d.BacsPleinsAvant, o => o.Ignore())
            .ForMember(d => d.BacsPleinsApres, o => o.Ignore());
    }
}
```

## Simple entity plugs in (no subclass needed)

With AutoMapper, a plain lookup table needs **only** a profile mapping (above) plus a DI
registration of the closed generic — no service class to write:

```csharp
// in Business/DependencyInjection.cs
services.AddScoped<
    IGenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>,
    GenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>>();
```

The UI injects that `IGenericService<…>` and gets full CRUD.

## Special cases extend the generic base

Non-trivial flows (billing, snapshots, `StockBacsSociete` update in one transaction) subclass
the (now concrete) generic service and add domain methods. Mapping still comes from `IMapper`.

```csharp
public class StockageService
    : GenericService<BonEntreeStockage, BonEntreeStockageDto, CreateBonEntreeStockageDto, UpdateBonEntreeStockageDto>
{
    public StockageService(IRepository<BonEntreeStockage> repo, IMapper mapper)
        : base(repo, mapper) { }

    // custom, non-generic operation
    public async Task<BonEntreeStockageDto> EnregistrerDepotAsync(CreateBonEntreeStockageDto dto)
    {
        var entity = Mapper.Map<BonEntreeStockage>(dto);
        // compute snapshots, snapshot PrixParBacParJourApplique,
        // generate NumeroLot, update StockBacsSociete in the SAME transaction...
        throw new NotImplementedException();
    }
}
```

## DTO vs entity

- `Domain/Entities/.../BonEntreeStockage.cs` = **EF entity** (navigation props, FKs) — stays inside Domain/DataAccess/Business.
- `Business/DTOs/.../BonEntreeStockageDto.cs` = **flat read model** for the UI.
- `Business/DTOs/.../CreateBonEntreeStockageDto.cs` = **command** from the UI (no `Id`, no snapshots — the service computes those).
- Entities never cross into the UI; only DTOs do.

## Dependency injection

```csharp
// DataAccess: generic repository covers all entities
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Business: AutoMapper scans profiles in the Business assembly
services.AddAutoMapper(typeof(StockageProfile).Assembly);
// (optional but recommended) fail fast on bad mappings at startup:
// cfg.AssertConfigurationIsValid();  via AddAutoMapper config or a startup check

// simple entities: register the closed generic service (no subclass)
services.AddScoped<
    IGenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>,
    GenericService<VarietePomme, VarietePommeDto, CreateVarietePommeDto, UpdateVarietePommeDto>>();

// entities with custom logic: register the concrete service
services.AddScoped<StockageService>();
```

> Package: add **`AutoMapper`** (and, on older setups, `AutoMapper.Extensions.Microsoft.DependencyInjection`) to the Business project.

## Conventions summary

| Concern | Generic building block | Custom when… |
|---------|------------------------|--------------|
| Data access | `IRepository<T>` / `Repository<T>` | custom queries / eager loading needed |
| Business CRUD | `IGenericService<TEntity,TDto,TCreateDto,TUpdateDto>` / `GenericService<…>` (concrete, uses `IMapper`) | domain logic (billing, snapshots, transactions) → subclass |
| Mapping | AutoMapper profiles in `Business/Mapping` | odd fields → `ForMember` / `Ignore` |
| UI contract | DTOs (read + Create/Update) | — always DTOs, never entities |
| DB provider | `DataAccess/DependencyInjection.cs` | one-line `UseSqlite` → `UseNpgsql` swap |
