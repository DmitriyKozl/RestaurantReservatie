namespace RestaurantReservatie.Rest.Middleware; 

public static class ResponseServiceMiddlewareExtension {
    public static IApplicationBuilder UseLogURLMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ResponseMiddleware>();
    }
}