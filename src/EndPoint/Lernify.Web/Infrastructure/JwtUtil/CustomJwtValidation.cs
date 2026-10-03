namespace Learnify.Web.Infrastructure.JwtUtil;

//public class CustomJwtValidation
//{
//    private readonly IUserService _userFacade;
//    public CustomJwtValidation(IUserService userFacade)
//    {
//        _userFacade = userFacade;
//    }
//    public async Task Validate(TokenValidatedContext context)
//    {
//        var userId = context.Principal!.GetUserId();
//        var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//        var token = await _userFacade.GetUserTokenByJwtTokenQuery(jwtToken);
//        if (token == null)
//        {
//            context.Fail("شناسه نامعتبر است");
//            return;
//        }

//        var user = await _userFacade.GetUserById(userId);
//        if (user == null || user.IsActive == false)
//        {
//            context.Fail("حساب کاربری شما غیر فعال است ");
//            return;
//        }
//    }
//}
