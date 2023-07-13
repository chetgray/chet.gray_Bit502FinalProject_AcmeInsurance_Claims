using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using AcmeInsurance.Claims.Business;

using log4net;

using Unity;

using ZirMed.Architecture.Service.Business;

namespace AcmeInsurance.Claims.Services.DeciderService
{
    internal class Program : ServiceBase
    {
        private readonly IClaimBl _claimBl;
        private readonly ICriteriaBl _criteriaBl;
        private readonly ILog _logger;
        private readonly TimeSpan _taskWaitTimeout;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _serviceTask;

        public Program()
        {
            _claimBl = UnityConfig.Container.Resolve<IClaimBl>();
            _criteriaBl = UnityConfig.Container.Resolve<ICriteriaBl>();
            _logger = LogManager.GetLogger(GetType());
            try
            {
                _taskWaitTimeout = TimeSpan.FromSeconds(
                    double.Parse(ConfigurationManager.AppSettings["TaskWaitTimeoutSeconds"])
                );
            }
            catch (Exception ex)
            {
                _logger.Error("Error reading app settings", ex);
            }
        }

        private static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
#if DEBUG
            Array.Resize(ref args, args.Length + 1);
            args.SetValue("/a:debug", args.Length - 1);
#endif
            Program serviceInstance = new Program();
            serviceInstance.Execute(args, serviceInstance.OnStart, serviceInstance.OnStop);
        }

        protected override void OnShutdown()
        {
            _logger.Info("Shutting down service");
            OnStop();
            base.OnShutdown();
            _logger.Info("Service shut down");
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("Starting service");
            _cancellationTokenSource = new CancellationTokenSource();
            _serviceTask = Task.Run(
                () =>
                {
                    while (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        _logger.Debug("Running service");

                        // TODO: Add service logic here

                        stopwatch.Stop();
                        if (stopwatch.Elapsed < _taskWaitTimeout)
                        {
                            TimeSpan timeout = _taskWaitTimeout - stopwatch.Elapsed;
                            _logger.Debug($"Sleeping for {timeout}");
                            _cancellationTokenSource.Token.WaitHandle.WaitOne(timeout);
                            _logger.Debug("Waking up");
                        }
                    }
                },
                _cancellationTokenSource.Token
            );
        }

        protected override void OnStop()
        {
            _logger.Info("Stopping service");
            if (_cancellationTokenSource?.IsCancellationRequested == false)
            {
                _cancellationTokenSource.Cancel();
            }
            try
            {
                _serviceTask.Wait();
            }
            catch (Exception ex)
            {
                _logger.Error("Error waiting for service task to complete", ex);
            }
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _serviceTask?.Dispose();
            _serviceTask = null;
            _logger.Info("Service stopped");
        }
    }
}
