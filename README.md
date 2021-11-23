#### Web Api 

##### 01. Setup & In-Memory Repository

- .NET Web Api
- .NET CLI
- Skapa vår första entity
  - Record vs Class
- Repository Pattern
- Dependency Injection
- Dtos

###### Skapa ett nytt Web Api-projekt

```
dotnet new webapi -n VinylCollection
```

Tryck på f5. Vid problem:

```
dotnet dev-certs https --trust
```

**task.json**

```
"group": {
	"kind": "build",
	"isDefault": true
}
```

Istället för f5:

```
dotnet watch run
```

#### 1. Skapa en Vinyl Entity

**Entities/Vinyl.cs**

```
public record Vinyl
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Artist { get; init; }
    }
```



###### Record vs Class

Record är väldigt lik en class men kan med fördel användas för **Immutable Objects**.

Record kommer även med With-expression support vilket vi kommer att använda senare i vår update route, där vi först hittar vår existerande entity och därefter använder **with** för att lägga till information vi har fått från användaren.

```c#
Vinyl updatedVinyl = existingVinyl with
    {
     Title = vinylDto.Title,
     Artist = vinylDto.Artist
};
```

Med record kan vi även använda oss av **value-based equality**.

###### init istället för set.

```
    public class Person
    {
        public int Age { get; init; }
    }
```

**C# Version 1**

```
public class Person
    {
        private int _age;

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }
    }
```

Anledningen till att man har det såhär är att man kan köra vissa conditionals i set.

```
if(value < 0)
{

}
_age = value;
...
```

**C# Version 2**

```
public class Person
    {
        private int _age;

        public int Age
        {
            get
            {
                return _age;
            }
            private set
            {   
                _age = value;
            }
        }
    }
```

**C# Version 3**

Auto-implemented property.

```
public int Age { get; set; }
```

```
public class Person
    {
        public int Age { get; private set; }

        public Person(int age)
        {
            Age = age;
        }
    }
```

**C# Version 6**

```
public int Age { get; }
```

Detta betyder att vi kan sätta age i constructorn, men ingen annanstans, vilket hjälper oss i vår strävan att hålla det **immutable**.

Problemet här är att vi inte kan använda oss av **member initializers**.

```
Person p = new Person { Age = 20};
```

Därför att vi inte har en setter.

**C# version 9**

```
public int Age { get; init;}
```

Vi kan nu göra:

```
Person p = new()
{
	Age = 20;
}
```

Men inte:

```
p.Age = 20;
```

#### 2. Repository

Vi ska nu skapa vårt repository, en klass som ansvarar för att lagra våra entities.

**Repositories/IMemVinylsRepository.cs**

```
public interface IMemVinylsRepository
    {
        Vinyl GetVinyl(Guid id);
        IEnumerable<Vinyl> GetVinyls();

        void CreateVinyl(Vinyl vinyl);

        void UpdateVinyl(Vinyl vinyl);

        void DeleteVinyl(Guid id);
    }
```

Det pattern vi använder oss av här kallas för **Repository Pattern**.

Vi kommer att ha en grupp klasser som fokuserar på att inkapsla logiken som krävs för att interagera med vår data.

*Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.* 

Detta ger följande fördelar:

- Centraliserar från var vi hämtar vår data
- Code maintainability
- Decouples vårt domain model layer från vårar infrastrukturslager(loose coupling) 
- DRY

**Client** skickar en request som går till vår **Controller**.

```
POST example.com/vinyls
JSON {"artist": "Mr Artist", "title": "Album Title"}
```

Controller har tillgång till ett **Repositorty**. Det ända controllern vet är att repositoryn har en metod som heter **CreateVinyl** och vilken typ som kan skickas med.

```
_repository.CreateVinyl(vinyl);
```

Repositoryn tar hand om resten, och nu när vi endast använder oss av **In-memory data** så har vi satt vår data till en List och kan därför köra:

```
 public void CreateVinyl(Vinyl vinyl)
        {
            _collection.Add(vinyl);
        }
```

#### 3. Controller

Controllern är alltså klassen som får requesten från client.

```
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VinylCollection.Dtos;
using VinylCollection.Entities;
using VinylCollection.Repositories;

namespace VinylCollection.Controllers
{
    [ApiController]
    [Route("vinyls")]
    public class VinylsController : ControllerBase
    {
        private readonly IMemVinylsRepository _repository;

        public VinylsController(IMemVinylsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IEnumerable<VinylDto> GetVinyls()
        {
            var vinyls = _repository.GetVinyls().Select(vinyl => vinyl.AsDto());
            return vinyls;
        }
}
```

I detta exempel ser vi några intressant saker:

```
[ApiController]
```

Här säger vi att **VinylsController** kommer att vara en **ApiController** och vi får tillgång till en mängd olika saker som är relaterade till att vara en Api Controller, bland annat

```
[Route("vinyls")]
```

Som säger att i denna klass lyssnar vi efter request till url /vinyls. Vi skulle även kunna skriva

```
[Route("api/[controller]")]
```

vilken då ansvarar för requests till /apy/vinyls då man strippar av "controller" i VinylsController.

Om vi ska sätta ord på vad vi gör här

```
private readonly IMemVinylsRepository _repository;

public VinylsController(IMemVinylsRepository repository)
{
 _repository = repository;
}
```

så kan vi göra det med två Design Principer som säger:

**Program to an interface, not an implementation**

och 

**Identify the aspects of your application that vary and separate them from what stays the same**.

Vår controller class kommer att förbli densamma, men repositoryt kan komma att förändras. Vi kanske skapar en persistent databas i framtiden, vem vet...

Genom att skapa en interface för det repository vi kommer att använda så försäkrar vi oss om att sålänge repository implementerar interfacet så kommer applikationen fortsatt att fungera.

**IMemRepository**

```
public interface IMemVinylsRepository
    {
        Vinyl GetVinyl(Guid id);
        IEnumerable<Vinyl> GetVinyls();
        void CreateVinyl(Vinyl vinyl);
        void UpdateVinyl(Vinyl vinyl);
        void DeleteVinyl(Guid id);
    }
```

Förbättringar här skulle kunna vara att använda oss av **Generics**, men låt oss hålla det simpelt för nu.

#### 4. Dependency Injection och Dtos

**Vad är dependecy injection?**

Här har vi en klass som vi använda sig av en annan klass.

```
VinylsController -> InMemVinylsRepository
```

```
public VinylsController() 
{
	repository = new InMemItemsRepositor();	
}
```



Länkar:

https://www.youtube.com/watch?v=Z8urV5AullQ&ab_channel=CodingTutorials

https://martinfowler.com/eaaCatalog/repository.html

https://www.youtube.com/watch?v=ZXdFisA_hOY&t=4827s&ab_channel=freeCodeCamp.org

https://www.youtube.com/watch?v=x6C20zhZHw8&ab_channel=CodingConcepts











