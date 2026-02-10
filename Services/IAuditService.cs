using Serilog;

namespace LH_PET.Services
{
    public interface IAuditService
    {
        /// <summary>
        /// Log de Login bem-sucedido
        /// </summary>
        void LogLoginSuccess(string username, string ipAddress);

        /// <summary>
        /// Log de Login falhado
        /// </summary>
        void LogLoginFailure(string username, string ipAddress, string reason);

        /// <summary>
        /// Log de Registro de novo usuário
        /// </summary>
        void LogUserRegistration(string username, string email);

        /// <summary>
        /// Log de tentativa bloqueada por rate limiting
        /// </summary>
        void LogRateLimitExceeded(string identifier, string ipAddress);

        /// <summary>
        /// Log de operação sensível
        /// </summary>
        void LogSensitiveOperation(string username, string operation, string resource);
    }

    public class AuditService : IAuditService
    {
        private readonly ILogger<AuditService> _logger;

        public AuditService(ILogger<AuditService> logger)
        {
            _logger = logger;
        }

        public void LogLoginSuccess(string username, string ipAddress)
        {
            _logger.LogInformation(
                "Successful login for user {Username} from IP {IpAddress}",
                username,
                ipAddress);
        }

        public void LogLoginFailure(string username, string ipAddress, string reason)
        {
            _logger.LogWarning(
                "Failed login attempt for user {Username} from IP {IpAddress} - Reason: {Reason}",
                username,
                ipAddress,
                reason);
        }

        public void LogUserRegistration(string username, string email)
        {
            _logger.LogInformation(
                "New user registered - Username: {Username}, Email: {Email}",
                username,
                email);
        }

        public void LogRateLimitExceeded(string identifier, string ipAddress)
        {
            _logger.LogWarning(
                "Rate limit exceeded for {Identifier} from IP {IpAddress}. Possible brute force attack.",
                identifier,
                ipAddress);
        }

        public void LogSensitiveOperation(string username, string operation, string resource)
        {
            _logger.LogInformation(
                "Sensitive operation performed - User: {Username}, Operation: {Operation}, Resource: {Resource}",
                username,
                operation,
                resource);
        }
    }
}
