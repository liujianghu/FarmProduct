using System;
using System.Collections.Generic;
using System.Linq;
using FarmProduct.Model;
using FarmProduct.Core.Extensioins;

namespace FarmProduct.Core
{
    public static class AuthorizationSvc
    {
        // Convert role strings into a Roles enum flags using the additive "|" (OR) operand.
        public static Role AggregateRoles(IEnumerable<string> roles)
        {
            return roles.Aggregate(Role.Guest, (current, role) => current | (Role)Enum.Parse(typeof(Role), role));
        }

        // Checks if a user's roles contains Administrator role.
        public static bool IsAdministrator(Role userRoles)
        {
            return userRoles.HasFlag(Role.Admin);
        }

        // Checks if user has ANY of the allowed role flags.
        public static bool IsUserInAnyRoles(Role userRoles, Role allowedRoles)
        {
            var flags = allowedRoles.GetFlags();
            return flags.Any(flag => userRoles.HasFlag(flag));
        }

        // Checks if user has ALL required role flags.
        public static bool IsUserInAllRoles(Role userRoles, Role requiredRoles)
        {
            return ((userRoles & requiredRoles) == requiredRoles);
        }

        // Validate authorization
        public static bool IsAuthorized(User user, Role role)
        {
            // convert comma delimited roles to enum flags, and check privileges.
            //var userRoles = AggregateRoles(user.UserRole);
            if (user == null || role == null)
            {
                throw new NullReferenceException("User is null or current rule is empty.");
            }
            return IsAdministrator(user.UserRole) || IsUserInAnyRoles(user.UserRole, role);
        }
    }
}
