using Microsoft.Extensions.DependencyInjection;
using SecuredServices.Core.Attributes;
using SecuredServices.Core.Messages;
using SecuredServices.Core.Protectors.Processors;
using SecuredServices.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SecuredServices.Core.Protectors
{
    /// <summary>
    ///     4. Проверяет, что хочет изменить пользователь
    ///     5. Узнает, можно ли было вносить текущие изменения
    /// </summary>
    public class EntityProtector<TEntity> : IEntityProtector<TEntity>
    {
        public EntityProtector(
            ISessionManager session,
            IPolicyProvider policies,
            IServiceProvider services)
        {
            Session = session;
            _policies = policies;
            _services = services;
        }

        private TEntity _currentEntityCheck;
        private TEntity _initialEntityCheck;
        private readonly IPolicyProvider _policies;
        private readonly IServiceProvider _services;

        public ISessionManager Session { get; }

        public IEnumerable<IProtectorMessage> Messages => throw new NotImplementedException();

        public bool IsProtected(TEntity toProtect, TEntity initial)
        {
            _currentEntityCheck = toProtect;
            _initialEntityCheck = initial;
            bool isProtected = true;
            var protectedProps = GetProtectedProperties(toProtect);
            foreach (var prop in protectedProps)
            {
                var initProperty = initial.GetType().GetProperty(prop.Name);
                var isChangeVerify = IsChangeVerified(prop, initProperty);

                isProtected = isChangeVerify;
                if (!isProtected)
                    break;
            }
            return isProtected;
        }

        private IEnumerable<PropertyInfo> GetProtectedProperties(TEntity entity)
        {
            return entity.GetType().GetProperties()
                    .Where(p => p.GetCustomAttributes<ProtectionAttribute>().Any());
        }

        private bool IsChangeVerified(
            PropertyInfo property, PropertyInfo initProperty)
        {
            var isVerified = true;
            var changeAttr = property.GetCustomAttribute<ChangeProtectionAttribute>();
            if(changeAttr is not null)
            {
                var propValue = property.GetValue(_currentEntityCheck);
                var initPropValue = initProperty.GetValue(_initialEntityCheck);
                isVerified = propValue == initPropValue;
                if (!isVerified)
                {
                    isVerified = IsManagerPolicyGreaterOrEqual(changeAttr.Policy);
                }
            }
            return isVerified;
        }

        private bool IsManagerPolicyGreaterOrEqual(string policy)
        {
            var isGreaterOrEqual = false;
            var managerPolicyRank = _policies.GetPolicyRank(Session.Role);
            var currentPolicyRank = _policies.GetPolicyRank(policy);
            if (managerPolicyRank >= currentPolicyRank)
                isGreaterOrEqual = true;
            return isGreaterOrEqual;
        }

        private IEnumerable<ProtectProcessor<TEntity>> SelectProcessors(PropertyInfo property)
        {
            var assemblyProcessors =
                Assembly.GetExecutingAssembly().GetTypes()
                    .Where(x => x.IsAssignableFrom(typeof(ProtectProcessor<TEntity>)) && !x.IsAbstract)
                        .ToList();
            var createdProcessors = new List<ProtectProcessor<TEntity>>();
            assemblyProcessors.ForEach(x => createdProcessors.Add((ProtectProcessor<TEntity>)ActivatorUtilities.CreateInstance(_services, x)));
            var propAttributes = property.GetCustomAttributes<ProtectionAttribute>().Select(x => x.GetType());
            return createdProcessors.Where(x => propAttributes.Contains(x.HandleAttributeType));
        }

        public bool IsProtected(TEntity toCheck)
        {
            throw new NotImplementedException();
        }
    }
}
