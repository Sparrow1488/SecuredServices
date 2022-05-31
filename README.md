# SecuredServices

## Introduction

Use this library to protect from changes your application models by different users who should not. Perform simple setup so you don't have to worry about unauthorized changes.

The main entities about you want know are

1. **ISessionManager** - This interface provides information about a client that wants to modify a protected object.
2. **IPolicyProvider** - This interface provides your application policies to use it in different Entity Protector's.
3. **ProtectProcessor** - This class-protector secure your entity from changes.

There are abstraction helps you to setup **EntityProtector** class.

```C#
public class EntityProtector<TEntity> : IEntityProtector<TEntity>
```

## Usage

For example, you have Group model with different properties: Id, Title, Description and Members. 

```C#
public class Group
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<User> Members { get; set; }
}
```

The task says:

> *"Only group members can change group information" @Customer*

Ok. Add attributes on properties

```C#
public class Group
{
    [ChangeProtection(SystemRole.Editor)]
    public int Id { get; set; }
    [ChangeProtection(SystemRole.Editor)]
    public string Title { get; set; }
    [ChangeProtection(SystemRole.Editor)]
    public string Description { get; set; }
    public IEnumerable<User> Members { get; set; }
}
```

And user model (and roles) look like this:

```C#
public class User
{
    public int Id { get; set; }
    public string Role { get; set; } // SystemRole
}

public class SystemRole
{
    [Policy(rank: 1)]
    public const string User = nameof(User);
    [Policy(rank: 2)]
    public const string Moderator = nameof(Moderator);
    [Policy(rank: 3)]
    public const string Administrator = nameof(Administrator);
    [Policy(rank: 4)]
    public const string Creator = nameof(Creator);
}
```

*Firstly*, add **PolicyAttribute** (as shown above) on system roles, where we set rank number. Rank number is priority number of this role (than higher number, the more rights). 

*Second*, implement SessionManager that will provide current client info.

```C#
internal class ApplicationSessionManager : SessionManager
{
    public ApplicationSessionManager() : base() { }

    public override void UpdateSession()
    {
        // Here you can use the IHttpContextAccessor (asp.net), work with the database
        // or something else (using constructor).
        // For example:
        UserModel.Policies = new string[] { TestSystemRole.User };
        UserModel.Identificator = Guid.NewGuid().ToString();
        IsAuthorized = true;
    }
}
```

*Third*, if we use Policies (roles) we need set it in **PolicyProvider**:

```C#
var policyProvider = new PolicyProvider(typeof(SystemRole));
```

In argument type should be variables with **PolicyAttribute**.

*Fourthly*, ...

## Dependencies

\> Microsoft.Extensions.DependencyInjection.Abstractions → v6.0.0
