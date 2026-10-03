using Common.Query;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.GetByFilter;

public class GetUsersByFilterQuery(UserFilterParams filterParams) : QueryFilter<UserFilterResult, UserFilterParams>(filterParams);