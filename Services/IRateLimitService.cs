using Microsoft.Extensions.Caching.Memory;

namespace LH_PET.Services
{
    public interface IRateLimitService
    {
        /// <summary>
        /// Verifica se o usuário excedeu o limite de tentativas
        /// </summary>
        Task<bool> IsLimitExceededAsync(string identifier);

        /// <summary>
        /// Registra uma tentativa falhada
        /// </summary>
        Task RecordFailedAttemptAsync(string identifier);

        /// <summary>
        /// Limpa o contador de tentativas (após sucesso ou expiração)
        /// </summary>
        Task ClearAttemptsAsync(string identifier);

        /// <summary>
        /// Obtém o número de tentativas restantes
        /// </summary>
        Task<int> GetRemainingAttemptsAsync(string identifier);
    }

    /// <summary>
    /// Implementação em memória para Rate Limiting
    /// Nota: Em produção, usar Redis para distribuído
    /// </summary>
    public class RateLimitService : IRateLimitService
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;

        private static class CacheKeys
        {
            public static string LoginAttempts(string username) => $"login_attempts_{username}";
            public static string LoginLockedUntil(string username) => $"login_locked_{username}";
        }

        public RateLimitService(IMemoryCache cache, IConfiguration config)
        {
            _cache = cache;
            _config = config;
        }

        public async Task<bool> IsLimitExceededAsync(string identifier)
        {
            return await Task.FromResult(_cache.TryGetValue(CacheKeys.LoginLockedUntil(identifier), out DateTime lockedUntil) &&
                                        DateTime.UtcNow < lockedUntil);
        }

        public async Task RecordFailedAttemptAsync(string identifier)
        {
            await Task.Run(() =>
            {
                var maxAttempts = _config.GetValue<int>("Security:RateLimiting:MaxAttempts", 5);
                var windowMinutes = _config.GetValue<int>("Security:RateLimiting:WindowMinutes", 15);

                var key = CacheKeys.LoginAttempts(identifier);

                if (_cache.TryGetValue(key, out int attempts))
                {
                    attempts++;
                }
                else
                {
                    attempts = 1;
                }

                if (attempts >= maxAttempts)
                {
                    // Bloqueia por windowMinutes
                    _cache.Set(CacheKeys.LoginLockedUntil(identifier), 
                               DateTime.UtcNow.AddMinutes(windowMinutes),
                               TimeSpan.FromMinutes(windowMinutes));
                }

                // Armazena tentativas com expiração
                _cache.Set(key, attempts, TimeSpan.FromMinutes(windowMinutes));
            });
        }

        public async Task ClearAttemptsAsync(string identifier)
        {
            await Task.Run(() =>
            {
                _cache.Remove(CacheKeys.LoginAttempts(identifier));
                _cache.Remove(CacheKeys.LoginLockedUntil(identifier));
            });
        }

        public async Task<int> GetRemainingAttemptsAsync(string identifier)
        {
            var maxAttempts = _config.GetValue<int>("Security:RateLimiting:MaxAttempts", 5);

            if (_cache.TryGetValue(CacheKeys.LoginAttempts(identifier), out int attempts))
            {
                return await Task.FromResult(Math.Max(0, maxAttempts - attempts));
            }

            return await Task.FromResult(maxAttempts);
        }
    }
}
