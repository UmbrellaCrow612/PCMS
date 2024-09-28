# Front end of the PCMS

Blazor front end 

## Folder structure

- Core
- Features
- Infra
- Shared

```python
graph TD
    A[YourBlazorApp]
    A --> B[Features]
    A --> C[Shared]
    A --> D[Core]
    A --> E[Infrastructure]

    B --> F[Feature1]
    B --> G[Feature2]
    B --> H[Feature3]

    F --> F1[Pages]
    F --> F2[Components]
    F --> F3[Services]
    F --> F4[Models]

    G --> G1[Pages]
    G --> G2[Components]
    G --> G3[Services]
    G --> G4[Models]

    C --> C1[Components]
    C --> C2[Layouts]

    D --> D1[Interfaces]
    D --> D2[Enums]
    D --> D3[Constants]

    E --> E1[API]
    E --> E2[Auth]
    E --> E3[Logging]
```