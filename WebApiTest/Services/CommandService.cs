using System.Diagnostics;
using System.Security;
using System.Text.RegularExpressions;

namespace WebApiTest.Services
{
    public class CommandService
    {
        private static readonly string[] AllowedCommands = { "ping" };
        private readonly ILogger<CommandService> _logger;

        public CommandService(ILogger<CommandService> logger)
        {
            _logger = logger;
        }


        public string ExecuteCommand(string command)
        {
            _logger.LogInformation($"Executing command: {command}");

            if (!IsAllowedCommand(command))
            {
                throw new SecurityException("Command not allowed.");
            }

            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();
                _logger.LogInformation($"Finished command: {command}");
                return string.IsNullOrEmpty(error) ? output : error;
            }
        }

        private bool IsAllowedCommand(string command)
        {
            var commandName = command.Split(' ')[0];
            return AllowedCommands.Contains(commandName) && !ContainsInvalidCharacters(command);
        }

        private bool ContainsInvalidCharacters(string command)
        {
            return Regex.IsMatch(command, @"[&;|<>`]");
        }
    }
}
