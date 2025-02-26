using Quartz;

namespace WebApiTest.Jobs
{
    public class TestTaskJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Scheduled task executed at: " + DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
