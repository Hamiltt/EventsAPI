namespace EventsAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                await AttachUserToContext(context, token, authService);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token, IAuthService authService)
        {
            var user = await authService.GetUserFromTokenAsync(token);
            if (user != null)
            {
                context.Items["User"] = user;
            }
        }
    }
}