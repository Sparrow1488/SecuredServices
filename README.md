# SecuredServices
## Entities

**IEntityProtector** - производит проверку объекта, используя роли доступа (Policy)

**IManagerSession** - пользователь, вносящий изменения в объект, который необходимо защитить. Имеет роль доступа

1. Знать, кто вносит изменения
2. Узнать, какие права нужны для внесения изменений
3. **Узнать, какие права есть у того, кто в носит изменения**
4. Проверить, что хочет изменить пользователь
5. Узнать, можно ли было вносить текущие изменения
6. Сломать колени / Одобрить





```C#
// current user can change group with id = 5
var currentContext = HttpContext;
var policies = new PolicyProvider(typeof(GroupRoles));
var groupService = new GroupService(id: 1); // can't use

var serviceProtector = new ServiceProtector<GroupsService>(currentContext, policies);
if(serviceProtector.CanAddChanges(groupService))
{
    var group = groupService.Current.Title = "Changed Title";
    groupService.SaveChanges(group);
}
else
{
    Log.Information("Current user can't be add changes in group (id = 1)");
}
```

